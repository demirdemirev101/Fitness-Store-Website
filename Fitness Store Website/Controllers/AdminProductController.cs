using Fitness_Store_Website.Data;
using Fitness_Store_Website.Models.AdminProduct;
using Fitness_Store_Website.Models.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fitness_Store_Website.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        private readonly ApplicationDbContext data;
        public AdminProductController(ApplicationDbContext _data)
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
            if (!ModelState.IsValid)
            {
                return View(productModel);
            }

            var product = new Product()
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                URL = productModel.URL,
                CategoryId = productModel.Category
            };

            data.Products.Add(product);
            await data.SaveChangesAsync();

            return RedirectToAction("All", "Product", new { area = "" });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = data.Categories
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();

            var product = await data.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = new EditProductModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                URL = product.URL,
                Category = product.CategoryId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = data.Products.Find(model.Id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.URL = model.URL;
            product.CategoryId = model.Category;

            await data.SaveChangesAsync();

            return RedirectToAction("All", "Product", new { area = "" });
        }
    }
}
