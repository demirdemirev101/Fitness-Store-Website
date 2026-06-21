using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Stripe;

namespace Fitness_Store_Website.Controllers
{
    [ApiController]
    [Route("webhook/stripe")]
    public class StripeWebhookController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly Fitness_Store_Website.Data.ApplicationDbContext data;

        public StripeWebhookController(IConfiguration configuration, Fitness_Store_Website.Data.ApplicationDbContext _data)
        {
            config = configuration;
            data = _data;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var webhookSecret = config.GetValue<string>("Stripe:WebhookSecret");

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], webhookSecret);

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    if (session != null && session.Metadata != null && session.Metadata.ContainsKey("orderId"))
                    {
                        if (int.TryParse(session.Metadata["orderId"], out var orderId))
                        {
                            var order = data.Orders.FirstOrDefault(o => o.Id == orderId);
                            if (order != null)
                            {
                                order.PaymentStatus = "Paid";
                                await data.SaveChangesAsync();
                            }
                        }
                    }
                }

                return Ok();
            }
            catch (Stripe.StripeException)
            {
                return BadRequest();
            }
        }
    }
}
