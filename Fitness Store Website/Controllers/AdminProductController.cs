using Fitness_Store_Website.Data;
using Fitness_Store_Website.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fitness_Store_Website.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminProductController : Controller
    {
        private readonly ApplicationDbContext data;
        public AdminProductController( ApplicationDbContext _data)
        {
            data = _data;
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = data.Categories
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductCreatingModel productModel)
        {
            if(!ModelState.IsValid)
            {
                return View(productModel);
            }

            var product=new Product()
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                URL = productModel.URL,
                CategoryId=productModel.Category
            };

            data.Products.Add(product);
            await data.SaveChangesAsync();

            return RedirectToAction("All", "Product", new { area = "" });
        }
    }
}
