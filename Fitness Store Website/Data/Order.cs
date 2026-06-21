using System.ComponentModel.DataAnnotations;

namespace Fitness_Store_Website.Data
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
