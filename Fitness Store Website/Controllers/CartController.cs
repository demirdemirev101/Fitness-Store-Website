using Fitness_Store_Website.Data;
using Fitness_Store_Website.Infrastructure;
using Fitness_Store_Website.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Store_Website.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext data;
        private const string SessionKey = "Cart";
        private readonly IConfiguration config;

        public CartController(ApplicationDbContext _data, IConfiguration configuration)
        {
            data = _data;
            config = configuration;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<CartViewModel>(SessionKey) ?? new CartViewModel();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            var product = await data.Products.FindAsync(productId);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.GetObject<CartViewModel>(SessionKey) ?? new CartViewModel();

            var existing = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                    URL = product.URL
                });
            }

            HttpContext.Session.SetObject(SessionKey, cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = HttpContext.Session.GetObject<CartViewModel>(SessionKey) ?? new CartViewModel();
            cart.Items.RemoveAll(i => i.ProductId == productId);
            HttpContext.Session.SetObject(SessionKey, cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = HttpContext.Session.GetObject<CartViewModel>(SessionKey) ?? new CartViewModel();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity = Math.Max(1, quantity);
            }
            HttpContext.Session.SetObject(SessionKey, cart);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObject<CartViewModel>(SessionKey) ?? new CartViewModel();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(string paymentMethod)
        {
            var cart = HttpContext.Session.GetObject<CartViewModel>(SessionKey) ?? new CartViewModel();
            if (!cart.Items.Any())
            {
                TempData["Message"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            if (paymentMethod == "Cash")
            {
                // Save order as Pay on Delivery
                var cashOrder = new Data.Order
                {
                    UserId = User?.Identity?.Name ?? "guest",
                    CreatedOn = DateTime.UtcNow,
                    Total = cart.Total,
                    PaymentMethod = "Cash",
                    PaymentStatus = "Pending"
                };

                foreach (var i in cart.Items)
                {
                    cashOrder.Items.Add(new Data.OrderItem
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Name,
                        Quantity = i.Quantity,
                        UnitPrice = i.Price
                    });
                }

                data.Orders.Add(cashOrder);
                data.SaveChanges();

                HttpContext.Session.Remove(SessionKey);
                TempData["Message"] = "Order placed. Pay on delivery (Cash).";
                return RedirectToAction("Index");
            }

            // Card -> use Stripe Checkout
            var secretKey = config.GetValue<string>("Stripe:SecretKey");
            Stripe.StripeConfiguration.ApiKey = secretKey;

            // Create order record with pending status and attach its id to Stripe session metadata
            var order = new Data.Order
            {
                UserId = User?.Identity?.Name ?? "guest",
                CreatedOn = DateTime.UtcNow,
                Total = cart.Total,
                PaymentMethod = "Card",
                PaymentStatus = "Pending"
            };

            foreach (var i in cart.Items)
            {
                order.Items.Add(new Data.OrderItem
                {
                    ProductId = i.ProductId,
                    ProductName = i.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.Price
                });
            }

            data.Orders.Add(order);
            await data.SaveChangesAsync();

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Cart", null, Request.Scheme),
                CancelUrl = Url.Action("Index", "Cart", null, Request.Scheme),
                Metadata = new Dictionary<string, string>
                {
                    { "orderId", order.Id.ToString() }
                },
                LineItems = cart.Items.Select(i => new Stripe.Checkout.SessionLineItemOptions
                {
                    Quantity = i.Quantity,
                    PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = i.Price * 100, // cents
                        Currency = "eur",
                        ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                        {
                            Name = i.Name
                        }
                    }
                }).ToList()
            };

            var service = new Stripe.Checkout.SessionService();
            var session = await service.CreateAsync(options);

            return Redirect(session.Url);
        }

        public IActionResult Success()
        {
            // Clear cart after successful payment
            HttpContext.Session.Remove(SessionKey);
            return View();
        }
    }
}
