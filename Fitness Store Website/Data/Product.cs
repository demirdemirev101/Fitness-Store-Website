using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int CategoryId {  get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
