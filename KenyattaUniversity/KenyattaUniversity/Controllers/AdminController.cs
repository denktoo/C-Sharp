using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KenyattaUniversity.Data;
using KenyattaUniversity.ViewModels;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KenyattaUniversity.Models;
using Microsoft.AspNetCore.Identity;

namespace KenyattaUniversity.Controllers
{
    //[Authorize(Roles = "Admin")] // This restricts access to users with the Admin role
    //[Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly KUContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            KUContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Dashboard()
        {
            try
            {
                var viewModel = new AdminDashboardViewModel
                {
                    Students = _context.Students.ToList(), // Ensure only students are retrieved
                    Courses = _context.Courses.ToList(),
                    Enrollments = _context.Enrollments.ToList() // You can include this if needed
                };

                return View(viewModel);
            }
            catch (UnauthorizedAccessException)
            {
                // Log unauthorized access attempt
                Console.WriteLine("Unauthorized access attempt to Admin Dashboard.");
                // Return a view or redirect to an error page
                return View("AccessDenied"); // You can create an AccessDenied view to inform the user
            }
            catch (Exception ex)
            {
                // Log other exceptions
                Console.WriteLine($"Error retrieving dashboard data: {ex.Message}");
                return View("Error");
            }
        }

        // GET: Admin/CreateCourse
        public IActionResult CreateCourse()
        {
            return View(); // Return the CreateCourse view
        }

        // POST: Admin/CreateCourse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard"); // Redirect to the dashboard after creation
            }
            return View(course); // Return to the view with current course model if validation fails
        }

        // GET: Admin/EditCourse/5
        public IActionResult EditCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course); // Return the EditCourse view with the course model
        }

        // POST: Admin/EditCourse/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard"); // Redirect back to the dashboard after editing
            }
            return View(course); // Return to the view with current course model if validation fails
        }

        // GET: Admin/UpdateCourse/5
        public IActionResult UpdateCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound(); // Return 404 if course not found
            }
            return View(course); // Return the UpdateCourse view with the course model
        }

        // POST: Admin/UpdateCourse/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard"); // Redirect back to the dashboard after update
            }
            return View(course); // Return to the view with current course model if validation fails
        }

        // POST: Admin/DeleteCourse/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Dashboard"); // Redirect back to the dashboard after deletion
        }

        // GET: Admin/CreateStudent
        public IActionResult CreateStudent()
        {
            return View(new RegisterViewModel());
        }

        // POST: Admin/CreateStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the StudentID already exists in the Students table
                if (await _context.Students.AnyAsync(s => s.StudentID == model.StudentID))
                {
                    ModelState.AddModelError("StudentID", "This Student ID already exists.");
                    return View(model); // Return to view with error message
                }

                // Create a new ApplicationUser object with StudentID as password
                var applicationUser = new ApplicationUser
                {
                    UserName = model.Email, // Use email as username or any other unique identifier
                    Email = model.Email,
                    StudentID = model.StudentID,
                    FullName = $"{model.Fname} {model.Lname}"
                };

                // Set the default password as the StudentID
                var result = await _userManager.CreateAsync(applicationUser, model.StudentID);

                if (result.Succeeded)
                {
                    // Create a new Student object and add it to the database context
                    var student = new Student
                    {
                        StudentID = model.StudentID,
                        Fname = model.Fname,
                        Lname = model.Lname,
                        Email = model.Email // Optional field
                    };

                    _context.Students.Add(student);
                    await _context.SaveChangesAsync(); // Save changes asynchronously

                    // Assign role to user (make sure "Student" role exists)
                    if (!await _roleManager.RoleExistsAsync("Student"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Student"));
                    }

                    await _userManager.AddToRoleAsync(applicationUser, "Student");

                    return RedirectToAction("Dashboard"); // Redirect after successful registration
                }

                // If there are errors during user creation, add them to ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model); // Return to view with validation errors if not valid
        }

        // GET: Admin/EditStudent/{id}
        public async Task<IActionResult> EditStudent(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(); // Return 404 if student not found
            }
            return View(student); // Return the EditStudent view with the student model
        }

        // POST: Admin/EditStudent/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard"); // Redirect back to the dashboard after editing
            }
            return View(student); // Return to the view with current student model if validation fails
        }

        // GET: Admin/EditEnrollment/{id}
        public async Task<IActionResult> EditEnrollment(int id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Course) // Include related Course data
                .Include(e => e.Student) // Include related Student data
                .FirstOrDefaultAsync(e => e.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound(); // Return 404 if enrollment not found
            }
            return View(enrollment); // Return the EditEnrollment view with the enrollment model
        }

        // POST: Admin/EditEnrollment/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEnrollment(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Update(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard"); // Redirect back to the dashboard after editing
            }

            return View(enrollment); // Return to the view with current enrollment model if validation fails
        }
    }
}