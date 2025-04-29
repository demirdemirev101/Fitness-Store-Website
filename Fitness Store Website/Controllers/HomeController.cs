using System.Diagnostics;
using Fitness_Store_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Store_Website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
