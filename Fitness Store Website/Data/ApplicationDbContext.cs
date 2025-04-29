using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Store_Website.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasData(
                new Product
                {
                    Id = 1,
                    Name = "Whey Protein",
                    Description = "High-quality whey protein for muscle growth.",
                    Price = 79.99m,
                    URL = "https://www.silabg.com/uf/product/176_pm_2270.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Fitness T-Shirt",
                    Description = "Breathable sports t-shirt.",
                    Price = 29.99m,
                    URL = "https://resources.fitshop.com/bilder/cardiostrong/textilien/tsm/cst-shirt-herren_1600.jpg"
                }
                );
            base.OnModelCreating(builder);
        }
    }
}
