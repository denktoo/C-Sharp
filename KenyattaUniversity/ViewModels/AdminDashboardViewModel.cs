using KenyattaUniversity.Models;
using System.Collections.Generic;

namespace KenyattaUniversity.ViewModels
{
    public class AdminDashboardViewModel
    {
        public IEnumerable<User> Users { get; set; } // List of students
        public IEnumerable<Course> Courses { get; set; } // List of courses
        public IEnumerable<Enrollment> Enrollments { get; set; } // List of enrollments

        public AdminDashboardViewModel()
        {
            Users = new List<User>();
            Courses = new List<Course>();
            Enrollments = new List<Enrollment>();
        }
    }
}