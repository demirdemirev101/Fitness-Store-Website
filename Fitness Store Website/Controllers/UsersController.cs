using Fitness_Store_Website.Data.IdentityModels;
using Fitness_Store_Website.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Store_Website.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(UserManager<IdentityUser> _userManager,
         RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            roleManager = _roleManager;
        }

        public async Task<IActionResult> BecomeAdmin()
        {
            return View(new BecomeAdminViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> BecomeAdmin(BecomeAdminViewModel model)
        {

            if(!ModelState.IsValid)
            {
                return View(model); 
            }
            
            var user= await userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            var admins = await userManager.GetUsersInRoleAsync("Admin");

            if (admins.Any(a=>a.PhoneNumber==model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Този телефонен номер вече е използван от друг админ.");

                return View(model);
            }

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            user.PhoneNumber = model.PhoneNumber;
            await userManager.UpdateAsync(user);

            await userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction("Index", "Home");
        }
    }
}
