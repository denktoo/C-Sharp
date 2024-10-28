using KenyattaUniversity.Models;
using System.Collections.Generic;

namespace KenyattaUniversity.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<ApplicationUser> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}