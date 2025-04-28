using System.ComponentModel.DataAnnotations;

namespace Fitness_Store_Website.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(Constants.productNameLength)]
        public required string Name { get; set; }

        [StringLength(Constants.productDesciptionLength)]
        public required string Description { get; set; }

        public decimal Price { get; set; }
        public required string URL { get; set; }
    }
}
