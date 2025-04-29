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
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasData(
                new Category() { Id = 1, Name = "Suppliments" },
                new Category() { Id = 2, Name = "Clothing" },
                new Category() { Id = 3, Name = "Accessories" }
                );

            builder.Entity<Product>()
                .HasData(
                new Product
                {
                    Id = 1,
                    Name = "Whey Protein",
                    Description = "High-quality whey protein for muscle growth.",
                    Price = 79.99m,
                    URL = "https://www.silabg.com/uf/product/176_pm_2270.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Fitness T-Shirt",
                    Description = "Breathable sports t-shirt.",
                    Price = 29.99m,
                    URL = "https://resources.fitshop.com/bilder/cardiostrong/textilien/tsm/cst-shirt-herren_1600.jpg",
                    CategoryId = 2
                }
                );
            base.OnModelCreating(builder);
        }
    }
}
