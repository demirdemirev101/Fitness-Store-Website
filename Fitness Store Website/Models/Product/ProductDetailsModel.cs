using Fitness_Store_Website.Data;
using System.ComponentModel.DataAnnotations;

namespace Fitness_Store_Website.Models.Product
{
    public class ProductDetailsModel
    {
        public int Id { get; set; }
        [StringLength(Constants.productNameLength)]
        public required string Name { get; set; }

        [StringLength(Constants.productDesciptionLength)]
        public required string Description { get; set; }

        public decimal Price { get; set; }
        public required string URL { get; set; }

        public required string Category { get; set; }
    }
}
