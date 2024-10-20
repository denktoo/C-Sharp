using System.Diagnostics;
using JkuatUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using JkuatUniversity.ViewModels;
using JkuatUniversity.Data;
using Microsoft.Extensions.Logging; // Ensure this is included for ILogger

namespace JkuatUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly JkuatContext db; // Change to readonly
        private readonly ILogger<HomeController> _logger;

        // Constructor to inject dependencies
        public HomeController(JkuatContext context, ILogger<HomeController> logger)
        {
            db = context; // Initialize the database context
            _logger = logger; // Initialize the logger
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<EnrollmentDateGroup> data = from student in db.Students
                                                   group student by student.EnrollmentDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       StudentCount = dateGroup.Count()
                                                   };
            return View(data.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}