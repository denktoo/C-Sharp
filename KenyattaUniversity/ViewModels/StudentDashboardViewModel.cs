using KenyattaUniversity.Models;

namespace KenyattaUniversity.ViewModels
{
    public class StudentDashboardViewModel
    {
        public User User { get; set; } // Student details
        public List<Enrollment> Enrollments { get; set; } // List of enrollments
    }
}