using Fitness_Store_Website.Models.Category;

namespace Fitness_Store_Website.Models.Product
{
    public class AllProductsQueryModel
    {
        public int? CategoryId { get; set; }
        public string SortOption { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int ProductsPerPage { get; set; } = 6;
        public int TotalProductsCount { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new();
        public List<ProductsViewModel> Products { get; set; } = new();
    }
}