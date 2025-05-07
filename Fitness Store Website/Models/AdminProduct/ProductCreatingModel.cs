using Fitness_Store_Website.Data;
using System.ComponentModel.DataAnnotations;

namespace Fitness_Store_Website.Models.AdminProduct
{
    public class ProductCreatingModel
    {
        public int Id { get; set; }
        [StringLength(Constants.productNameLength)]
        public required string Name { get; set; }

        [StringLength(Constants.productDesciptionLength)]
        public required string Description { get; set; }

        public decimal Price { get; set; }
        public required string URL { get; set; }

        public required int Category { get; set; }
    }
}
