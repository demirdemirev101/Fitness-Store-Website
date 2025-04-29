using Fitness_Store_Website.Data;
using Fitness_Store_Website.Models;
using Fitness_Store_Website.Models.Category;
using Fitness_Store_Website.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Store_Website.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext data;
        public ProductController(ApplicationDbContext _data)
        {
            data = _data;
        }
        public async Task<IActionResult> All(int? categoryId, string? sortOption, int currentPage = 1)
        {
            var productsQuery = data.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery
                                .Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(sortOption))
            {
                if (sortOption == "nameAsc")
                {
                    productsQuery = productsQuery.OrderBy(p => p.Name);
                }
                else if (sortOption == "nameDesc")
                {
                    productsQuery = productsQuery.OrderByDescending(p => p.Name);
                }
                else if (sortOption == "priceAsc")
                {
                    productsQuery = productsQuery.OrderBy(p => p.Price);
                }
                else if (sortOption == "priceDesc")
                {
                    productsQuery = productsQuery.OrderByDescending(p => p.Price);
                }
            }

            int totalProducts = await productsQuery.CountAsync();


            var products = await productsQuery
                         .Skip((currentPage-1)*6)
                         .Take(6)
                         .Select(x => new ProductsViewModel()
                         {
                             Name = x.Name,
                             Price = x.Price,
                             URL = x.URL
                         })
                         .ToListAsync();


            var categories = await data.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();


            var model = new AllProductsQueryModel()
            {
                CategoryId = categoryId,
                SortOption = sortOption,
                Categories = categories,
                Products = products,
                CurrentPage=currentPage,
                ProductsPerPage=6,
                TotalProductsCount=totalProducts
            };

            return View(model);
        }

    }
}
