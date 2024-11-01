using System.Collections.Generic;
using KenyattaUniversity.Models;

namespace KenyattaUniversity.ViewModels
{
    public class StudentDashboardViewModel
    {
        public Student Student { get; set; }
        public List<Enrollment> Enrollments { get; set; } // List of enrollments for this student
    }
}