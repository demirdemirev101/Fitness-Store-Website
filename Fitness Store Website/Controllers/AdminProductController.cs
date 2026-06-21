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
        public IActionResult Index()
        {
            var products = data.Products
                .Select(p => new Models.AdminProduct.ProductCreatingModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    URL = p.URL,
                    Category = p.CategoryId
                })
                .ToList();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await data.Products.FindAsync(id);
            if (product == null) return NotFound();

            var model = new Models.AdminProduct.ProductCreatingModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                URL = product.URL,
                Category = product.CategoryId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await data.Products.FindAsync(id);
            if (product == null) return NotFound();

            data.Products.Remove(product);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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

            // handle uploaded image
            if (productModel.ImageFile != null && productModel.ImageFile.Length > 0)
            {
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productModel.ImageFile.FileName);
                var filePath = Path.Combine(imagesPath, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await productModel.ImageFile.CopyToAsync(stream);
                }
                productModel.URL = "/images/" + fileName;
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
            // handle uploaded image
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(imagesPath, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                product.URL = "/images/" + fileName;
            }
            else
            {
                product.URL = model.URL;
            }
            product.CategoryId = model.Category;

            await data.SaveChangesAsync();

            return RedirectToAction("All", "Product", new { area = "" });
        }
    }
}
