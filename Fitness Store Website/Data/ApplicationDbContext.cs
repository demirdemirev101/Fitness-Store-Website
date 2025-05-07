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
                },
                new Product
                {
                    Id = 3,
                    Name = "Creatine Monohydrate",
                    Description = "High-quality compound for strength and muscle growth.",
                    Price = 29.99m,
                    URL = "https://www.silabg.com/uf/product/148_pm_micronized-creatine-powder-optimum-nutrition-466x801.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = 4,
                    Name = "Knee Sleeves",
                    Description = "Supports the knee during squats and variations exercises.",
                    Price = 29.99m,
                    URL = "https://warmbody-coldmind.com/cdn/shop/files/Thegirlisputtingonkneesleeves.jpg?v=1704913120",
                    CategoryId = 3
                },
                new Product
                {
                    Id = 5,
                    Name = "Shorts",
                    Description = "Allows for easy movement and ventilation.",
                    Price = 19.50m,
                    URL = "https://m.media-amazon.com/images/I/61OGyjgFBEL._AC_UL480_FMwebp_QL65_.jpg",
                    CategoryId = 2
                },
                new Product
                {
                    Id = 6,
                    Name = "Sneekers",
                    Description = "The perfect footwear for running and jogging.",
                    Price = 129.99m,
                    URL = "https://m.media-amazon.com/images/I/81iJLmjmuLL._AC_UL480_FMwebp_QL65_.jpg",
                    CategoryId = 2
                },
                new Product
                {
                    Id = 7,
                    Name = "Resist band",
                    Description = "Accessable gym equipment for working out anywhere.",
                    Price = 14.00m,
                    URL = "https://m.media-amazon.com/images/I/81LLONLLdSL._AC_UL480_FMwebp_QL65_.jpg",
                    CategoryId = 3
                },
                new Product
                {
                    Id = 8,
                    Name = "Pull up bar.",
                    Description = "Pull-Up bar for versatile home workouts.",
                    Price = 19.19m,
                    URL = "https://m.media-amazon.com/images/I/71yILyDij-L._AC_UL480_FMwebp_QL65_.jpg",
                    CategoryId = 3
                }
                );
            base.OnModelCreating(builder);
        }
    }
}
