using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KenyattaUniversity.Data; // Ensure you have the correct using directive for your context
using KenyattaUniversity.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace KenyattaUniversity.Controllers
{
    //[Authorize(Roles = "Student")] // Ensure only students can access this controller
    //[Authorize]
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
            var enrollments = _context.Enrollments
                .Include(e => e.Course)  // Include related Course data
                .Include(e => e.Student)  // Include related Student data (optional if already linked)
                .Where(e => e.StudentID == userId) // Fetch enrollments for the current student
                .ToList();

            return View(enrollments); // Pass enrollments to the view for rendering data
        }
    }
}