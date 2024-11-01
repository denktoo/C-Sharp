using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KenyattaUniversity.Data;
using KenyattaUniversity.Models;
using System.Linq;
using System.Security.Claims;

namespace KenyattaUniversity.Controllers
{
    //[Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly KUContext _context;

        public StudentController(UserManager<ApplicationUser> userManager, KUContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Student/Dashboard
        public IActionResult Dashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get current user's ID

            // Log the userId for debugging purposes
            Console.WriteLine($"Current User ID: {userId}");

            // Retrieve student details based on StudentID
            var student = _context.Students.FirstOrDefault(s => s.StudentID == userId);
            if (student == null)
            {
                return NotFound(); // Handle case where student is not found
            }

            // Fetch enrollments for the current student
            var enrollments = _context.Enrollments
                .Include(e => e.Course)  // Include related Course data
                .Where(e => e.StudentID == userId) // Ensure StudentID matches UserId from claims
                .ToList(); // Use ToList() synchronously

            // Create an anonymous object to pass both student and enrollments to the view
            var model = new
            {
                Student = student,
                Enrollments = enrollments
            };

            return View(model); // Pass the anonymous object to the view
        }
    }
}