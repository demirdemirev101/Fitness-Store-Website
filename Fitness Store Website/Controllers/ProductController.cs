using Fitness_Store_Website.Data;
using Fitness_Store_Website.Models;
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
        public async Task<IActionResult> All(int? categoryId, string? sortByName, string? sortByPrice)
        {
            var productsQuery = data.Products.AsQueryable();

            if (categoryId!=null)
            {
                productsQuery = productsQuery
                                .Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(sortByName))
            {
                productsQuery = sortByName == "asc"
                    ? productsQuery.OrderBy(p => p.Name)
                    : productsQuery.OrderByDescending(p => p.Name);
            }

            if (!string.IsNullOrEmpty(sortByPrice))
            {
                productsQuery = sortByPrice == "asc"
                    ? productsQuery.OrderBy(p => p.Price)
                    : productsQuery.OrderByDescending(p => p.Price);
            }

            var allProducts = await productsQuery
                         .Select(x => new ProductsViewModel
                         {
                             Name = x.Name,
                             Price = x.Price,
                             URL = x.URL
                         })
                         .ToListAsync();

            return View(allProducts);
        }

    }
}
