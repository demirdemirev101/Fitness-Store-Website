using Fitness_Store_Website.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Store_Website.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : Controller
    {
        private readonly ApplicationDbContext data;
        public AdminOrderController(ApplicationDbContext _data)
        {
            data = _data;
        }

        public IActionResult Index()
        {
            var orders = data.Orders
                .Select(o => new Models.AdminOrder.OrderViewModel
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    CreatedOn = o.CreatedOn,
                    Total = o.Total,
                    PaymentMethod = o.PaymentMethod,
                    PaymentStatus = o.PaymentStatus
                })
                .ToList();

            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = data.Orders.Where(o => o.Id == id)
                .Select(o => new Models.AdminOrder.OrderViewModel
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    CreatedOn = o.CreatedOn,
                    Total = o.Total,
                    PaymentMethod = o.PaymentMethod,
                    PaymentStatus = o.PaymentStatus,
                    Items = o.Items.Select(i => new Models.AdminOrder.OrderItemViewModel
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        UnitPrice = i.UnitPrice,
                        Quantity = i.Quantity
                    }).ToList()
                }).FirstOrDefault();

            if (order == null) return NotFound();

            return View(order);
        }
    }
}
