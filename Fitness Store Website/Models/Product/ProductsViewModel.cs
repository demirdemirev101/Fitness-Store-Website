namespace Fitness_Store_Website.Models.Product
{
    public class ProductsViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public required string URL { get; set; }
    }
}
