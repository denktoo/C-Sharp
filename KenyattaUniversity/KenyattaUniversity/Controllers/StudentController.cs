using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KenyattaUniversity.Data;
using KenyattaUniversity.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using KenyattaUniversity.ViewModels;

namespace KenyattaUniversity.Controllers
{
    //[Authorize(Roles = "Student")] // Ensure only students can access this controller
    //[Authorize(Policy = "StudentOnly")]
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
        public async Task<IActionResult> Dashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get current user's ID

            // Retrieve student details based on StudentID
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentID == userId);
            if (student == null)
            {
                return NotFound(); // Handle case where student is not found
            }

            // Fetch enrollments for the current student
            var enrollments = await _context.Enrollments
                .Include(e => e.Course)  // Include related Course data
                .Where(e => e.StudentID == userId) // Ensure StudentID matches UserId from claims
                .ToListAsync();

            // Create the view model with student info and enrollments
            var viewModel = new StudentDashboardViewModel
            {
                Student = student,
                Enrollments = enrollments // List of enrollments for this student
            };

            return View(viewModel); // Pass model to the view
        }
    }
}