using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KenyattaUniversity.Data; // Ensure you have the correct using directive for your context
using KenyattaUniversity.Models;
using System.Linq;
using System.Security.Claims;

namespace KenyattaUniversity.Controllers
{
    [Authorize(Roles = "Student")] // Ensure only students can access this controller
    public class StudentController : Controller
    {
        private readonly KUContext _context;

        public StudentController(KUContext context)
        {
            _context = context;
        }

        // GET: Student/Dashboard
        public IActionResult Dashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get current user's ID
            var enrollments = _context.Enrollments.Where(e => e.StudentID == userId).ToList(); // Fetch enrollments for the current student

            return View(enrollments); // Pass enrollments to the view for rendering data
        }
    }
}