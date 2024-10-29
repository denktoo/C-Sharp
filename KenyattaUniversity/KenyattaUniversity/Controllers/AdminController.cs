using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KenyattaUniversity.Data; // Ensure you have the correct using directive for your context
using KenyattaUniversity.Models;
using KenyattaUniversity.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KenyattaUniversity.Controllers
{
    //[Authorize(Roles = "Admin")] // Ensure only admins can access this controller
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly KUContext _context;

        public AdminController(KUContext context)
        {
            _context = context;
        }

        // GET: Admin/Dashboard
        public IActionResult Dashboard()
        {
            var students = _context.Users.ToList(); // Fetch all students
            var courses = _context.Courses.ToList(); // Fetch all courses
            var enrollments = _context.Enrollments
                .Include(e => e.Student) // Include related Student data
                .Include(e => e.Course)  // Include related Course data
                .ToList();

            var viewModel = new AdminDashboardViewModel
            {
                Students = students,
                Courses = courses,
                Enrollments = enrollments
            };

            return View(viewModel); // Pass view model to view for rendering data
        }
    }
}