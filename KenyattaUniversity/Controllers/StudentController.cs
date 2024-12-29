using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KenyattaUniversity.Data;
using KenyattaUniversity.Models;
using System.Linq;
using System.Security.Claims;
using KenyattaUniversity.ViewModels;

namespace KenyattaUniversity.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly KUContext _context;

        public StudentController( KUContext context)
        {
            _context = context;
        }

        // GET: Student/Dashboard
        public IActionResult Dashboard()
        {
            // Get current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve student details based on StudentID
            var student = _context.Users.FirstOrDefault(s => s.SchoolID == userId);
            if (student == null)
            {
                return NotFound(); // Handle case where student is not found
            }

            // Fetch enrollments for the current student
            var enrollments = _context.Enrollments
                .Include(e => e.Course)  // Include related Course data
                .Where(e => e.SchoolID == userId) // Ensure SchoolID matches UserId from claims
                .ToList(); // Use ToList() synchronously

            // Create an anonymous object to pass both student and enrollments to the view
            var model = new StudentDashboardViewModel
            {
                User = student,
                Enrollments = enrollments
            };

            return View(model); // Pass the anonymous object to the view
        }
    }
}