using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KenyattaUniversity.Data;
using KenyattaUniversity.Models;
using KenyattaUniversity.ViewModels;
using System.Linq;

namespace KenyattaUniversity.Controllers
{
    [Authorize(Roles = "Admin")] // Ensure only admins can access this controller
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
            var enrollments = _context.Enrollments.ToList(); // Fetch all enrollments

            var viewModel = new AdminDashboardViewModel
            {
                Students = students,
                Courses = courses,
                Enrollments = enrollments
            };

            return View(viewModel);
        }
    }
}
