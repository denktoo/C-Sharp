using FitTrack.Data;
using FitTrack.Models;
using FitTrack.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FitTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly FitTrackContext _context;

        public AdminApiController(FitTrackContext context)
        {
            _context = context;
        }

        [HttpGet("Dashboard")]
        public IActionResult GetDashboardData()
        {
            var dashboardData = new AdminDashboardViewModel
            {
                TotalUsers = _context.Users.Count(u => EF.Property<string>(u, "Role") != "Admin"), // Filtered count to only Non Admins Users
                TotalWorkouts = _context.Workouts.Count(),
                GoalsAchieved = _context.Goals.Count(g => g.IsAchieved),
                PendingGoals = _context.Goals.Count(g => !g.IsAchieved),
                Users = _context.Users.Select(u => new UserOverview
                {
                    Id = u.Id,
                    Name = u.Username,
                    Role = EF.Property<string>(u, "Role"), // Access the shadow property
                    Email = u.Email,
                    WorkoutsCount = _context.Workouts.Count(w => w.UserId == u.Id),
                    GoalsAchievedCount = _context.Goals.Count(g => g.UserId == u.Id && g.IsAchieved)
                }).ToList(),
                RecentWorkouts = _context.Workouts
                    .OrderByDescending(w => w.WorkoutDate)
                    .Take(5)
                    .Select(w => new WorkoutOverview
                    {
                        UserName = _context.Users.FirstOrDefault(u => u.Id == w.UserId).Username,
                        Date = w.WorkoutDate,
                        Duration = w.Duration
                    }).ToList(),
                RecentGoals = _context.Goals
                    .OrderByDescending(g => g.TargetDate)
                    .Take(5)
                    .Select(g => new GoalOverview
                    {
                        UserName = _context.Users.FirstOrDefault(u => u.Id == g.UserId).Username,
                        Description = g.Description,
                        TargetDate = g.TargetDate
                    }).ToList()
            };
            return Ok(dashboardData);
        }

        [HttpGet("User/{id}")]
        public IActionResult GetUserDetails(int id)
        {
            var user = _context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserOverview
                {
                    Id = u.Id,
                    Name = u.Username,
                    Email = u.Email,
                    Role = EF.Property<string>(u, "Role"), // Access the shadow property
                    WorkoutsCount = _context.Workouts.Count(w => w.UserId == u.Id),
                    GoalsAchievedCount = _context.Goals.Count(g => g.UserId == u.Id && g.IsAchieved)
                })
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            // Prepare a detailed view for the user
            var userDetails = new AdminDashboardViewModel
            {
                User = user,
                RecentWorkouts = _context.Workouts
                    .Where(w => w.UserId == id)
                    .Select(w => new WorkoutOverview
                    {
                        UserName = user.Name,
                        Date = w.WorkoutDate,
                        Duration = w.Duration
                    })
                    .ToList(),
                RecentGoals = _context.Goals
                    .Where(g => g.UserId == id)
                    .Select(g => new GoalOverview
                    {
                        UserName = user.Name,
                        Description = g.Description,
                        TargetDate = g.TargetDate
                    })
                    .ToList()
            };

            return Ok(userDetails);
        }

        [HttpDelete("User/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}