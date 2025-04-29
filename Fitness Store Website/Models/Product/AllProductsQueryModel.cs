using Fitness_Store_Website.Models.Category;

namespace Fitness_Store_Website.Models.Product
{
    public class AllProductsQueryModel
    {
        public int? CategoryId { get; set; }
        public string SortOption { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new();
        public List<ProductsViewModel> Products { get; set; } = new();
    }
}