using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using KenyattaUniversity.Data;
using KenyattaUniversity.Models;
using KenyattaUniversity.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KenyattaUniversity.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentControllers : Controller
    {
        private readonly KUContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentControllers(KUContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Student/Dashboard
        public IActionResult Dashboard()
        {
            var userId = _userManager.GetUserId(User); // Get current user's ID

            // Fetch the student's enrollments using the correct foreign key relationship
            var enrollments = _context.Enrollments
                .Where(e => e.StudentID == int.Parse(userId)) // Ensure this matches your data type (int or string)
                .Include(e => e.Course) // Include course details if needed
                .ToList();

            return View(enrollments); // Return enrollments to view
        }

        // GET: Student/Enroll
        [HttpGet]
        public IActionResult Enroll()
        {
            var courses = _context.Courses.ToList(); // Fetch all available courses
            var viewModel = new EnrollViewModel
            {
                Courses = courses
            };
            return View(viewModel);
        }

        // POST: Student/Enroll
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(EnrollViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                var enrollment = new Enrollment
                {
                    CourseID = model.SelectedCourseId,
                    StudentID = int.Parse(userId) // Ensure this matches your data type (int or string)
                };

                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();

                return RedirectToAction("Dashboard"); // Redirect to the dashboard after enrollment
            }

            var courses = _context.Courses.ToList();
            model.Courses = courses; // Re-populate courses in case of error

            return View(model); // Return the view with the model to show validation errors
        }
    }
}