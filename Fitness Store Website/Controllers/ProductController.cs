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
        public async Task<IActionResult> All()
        {
            var allProducts = await data.Products
                .Select(x => new ProductsViewModel
                {
                    Name=x.Name,
                    Price=x.Price,
                    URL=x.URL
                })
                .ToListAsync();

            return View(allProducts);
        }
        
    }
}
