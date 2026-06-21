namespace Fitness_Store_Website.Models.Cart
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string URL { get; set; } = null!;
    }
}