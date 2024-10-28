using KenyattaUniversity.Models;
using System.Collections.Generic;

namespace KenyattaUniversity.ViewModels
{
    public class EnrollViewModel
    {
        public int SelectedCourseId { get; set; } // Selected course ID for enrollment

        public List<Course> Courses { get; set; } // List of available courses
    }
}