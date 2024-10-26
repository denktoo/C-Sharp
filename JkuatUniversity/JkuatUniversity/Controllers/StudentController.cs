using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JKUATUniversity.Data;
using JKUATUniversity.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace JKUATUniversity.Controllers
{
    [Authorize] // Ensure only authenticated users can access this controller
    public class StudentController : Controller
    {
        private readonly JkuatContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(JkuatContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Dashboard action to show enrolled courses.
        public IActionResult Dashboard()
        {
            var userId = _userManager.GetUserId(User); // Get current user's ID
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(); // Return 401 if not authenticated
            }

            // Convert userId to int if necessary; ensure that StudentID is of type int.
            var studentCourses = _context.Enrollments
                .Where(e => e.StudentID == Convert.ToInt32(userId)) // Ensure correct type comparison
                .Include(e => e.Course) // Include course details if needed
                .ToList(); // Fetch courses for the logged-in student

            return View(studentCourses); // Return courses to view
        }
    }
}