using Microsoft.AspNetCore.Mvc;

namespace KenyattaUniversity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Return the home page view
        }

        public IActionResult Privacy()
        {
            return View(); // Return the privacy policy view
        }
    }
}