using KenyattaUniversity.Models;
using System.Collections.Generic;

namespace KenyattaUniversity.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalStudents { get; set; }
        public List<Students> Users { get; set; } // List of students
        public IEnumerable<Course> Courses { get; set; } // List of courses
        public IEnumerable<Enrollment> Enrollments { get; set; } // List of enrollments

        public AdminDashboardViewModel()
        {
            Courses = new List<Course>();
            Enrollments = new List<Enrollment>();
        }

        public class Students
        {
            public string SchoolID { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }
    }
}