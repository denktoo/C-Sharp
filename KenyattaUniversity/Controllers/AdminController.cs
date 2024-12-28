using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KenyattaUniversity.Data;
using KenyattaUniversity.ViewModels;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KenyattaUniversity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace KenyattaUniversity.Controllers
{
    //[Authorize(Roles = "Admin")]  // Ensure that only users with the 'Admin' role can access this controller
    public class AdminController : Controller
    {
        private readonly KUContext _context;
        private readonly IConfiguration _configuration;

        public AdminController(KUContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Admin Dashboard
        public IActionResult Dashboard()
        {
            var viewModel = new AdminDashboardViewModel
            {
                Students = _context.Students.ToList(), // Ensure only students are retrieved
                Courses = _context.Courses.ToList(),
                Enrollments = _context.Enrollments.ToList()
            };

            return View(viewModel);
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

        // GET: Admin/EditCourse
        public IActionResult EditCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course); // Return the EditCourse view with the course model
        }

        // POST: Admin/UpdateCourse
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

        // GET: Admin/DeleteCourse
        public IActionResult DeleteCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course); // Return the EditCourse view with the course model
        }

        // POST: Admin/DeleteCourse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard"); // Redirect back to the dashboard after deletion
            }
            return View(course);
        }

        // GET: Admin/CreateStudent
        public IActionResult CreateStudent()
        {
            return View(); // Return the CreateStudent view
        }

        // POST: Admin/CreateStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if the registration number is already in use
            if (_context.Students.Any(u => u.RegNo == model.RegNo))
            {
                ModelState.AddModelError("RegNo", "The provided registration number is already in use.");
                return View(model);
            }

            // Check if the email is already in use
            if (_context.Students.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "The provided email is already in use.");
                return View(model);
            }

            // Map the view model to the Student entity
            var student = new Student
            {
                RegNo = model.RegNo,
                Username = model.Username,
                Email = model.Email,
                Password = model.RegNo // Set the default password to RegNo
            };

            // Add the student to the database
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // Assign the default role to the student ("Student")
            _context.Entry(student).Property("Role").CurrentValue = "Student";
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard");
        }

        // GET: Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Authenticate user
            var admin = _context.Admins.FirstOrDefault(u => u.EmpNo == model.EmpNo && u.Password == model.Password);
            if (admin == null)
            {
                ModelState.AddModelError("", "Invalid Employee No. or password.");
                return View(model);
            }

            // Create claims for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, admin.EmpNo.ToString()),
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Claims-based sign-in (cookie authentication)
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Redirect to Admin Dashboard
            return RedirectToAction("Dashboard", "Admin");
        }

        // POST: Admin/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}