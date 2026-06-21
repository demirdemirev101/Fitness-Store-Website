using System.Collections.Generic;

namespace Fitness_Store_Website.Models.AdminOrder
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;
        public List<OrderItemViewModel> Items { get; set; } = new List<OrderItemViewModel>();
    }
}
