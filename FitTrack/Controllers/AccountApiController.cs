using Microsoft.AspNetCore.Mvc;
using FitTrack.ViewModels;
using FitTrack.Data;
using FitTrack.Models;

namespace FitTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly FitTrackContext _context;

        public AccountApiController(FitTrackContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                return BadRequest("Email is already in use.");
            }  

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Shadow property to assign role
            _context.Entry(user).Property("Role").CurrentValue = "User";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful" });
        }
    }

}
