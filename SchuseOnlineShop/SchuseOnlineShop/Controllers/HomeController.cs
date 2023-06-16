using Microsoft.AspNetCore.Mvc;
using SchuseOnlineShop.Models;
using System.Diagnostics;

namespace SchuseOnlineShop.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

    }
}