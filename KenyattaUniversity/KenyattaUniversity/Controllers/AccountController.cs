using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using KenyattaUniversity.Models;
using KenyattaUniversity.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KenyattaUniversity.Data;
using Microsoft.EntityFrameworkCore;

namespace KenyattaUniversity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly KUContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IConfiguration configuration,
                                 IServiceProvider serviceProvider,
                                 KUContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _context = context;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to sign in the user
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Find the user by email
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        // Generate JWT token
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                        // Retrieve all roles for the user
                        var roles = await _userManager.GetRolesAsync(user);
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id), // User ID
                    new Claim(JwtRegisteredClaimNames.Iss, "KenyattaUniversity"),
                    new Claim(JwtRegisteredClaimNames.Aud, "KenyattaUniversityUsers"),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("FullName", user.FullName)
                };

                        // Add StudentID claim if it exists
                        var studentIdClaim = await _userManager.GetClaimsAsync(user);
                        var studentId = studentIdClaim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        if (!string.IsNullOrEmpty(studentId))
                        {
                            claims.Add(new Claim("StudentID", studentId)); // Add StudentID claim if needed
                        }

                        // Add each role as a claim
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(claims),
                            Expires = DateTime.UtcNow.AddHours(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var tokenString = tokenHandler.WriteToken(token);

                        // Redirect based on roles
                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Dashboard", "Admin");
                            //return RedirectToAction("Index", "Home");
                        }
                        else if (roles.Contains("Student"))
                        {
                            return RedirectToAction("Dashboard", "Student");
                        }

                        return Ok(new { Token = tokenString });
                    }
                }
                else
                {
                    // Log the failed login attempt for debugging
                    Console.WriteLine($"Failed login attempt for email: {model.Email}");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = $"{model.Fname} {model.Lname}" // Store full name if needed
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Assign selected role
                    if (!string.IsNullOrEmpty(model.SelectedRole))
                    {
                        await _userManager.AddToRoleAsync(user, model.SelectedRole);
                    }

                    // If the user is a student, create an entry in the students table
                    if (model.SelectedRole == "Student")
                    {
                        var student = new Student
                        {
                            StudentID = model.StudentID,
                            Fname = model.Fname,
                            Lname = model.Lname,
                            Email = model.Email
                        };

                        _context.Students.Add(student); // Add to context
                        await _context.SaveChangesAsync(); // Save changes to students table

                        // Add claim for StudentID
                        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, model.StudentID));
                    }

                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}