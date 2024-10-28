using System.Collections.Generic;

namespace KenyattaUniversity.Models
{
    public class Course
    {
        public int CourseID { get; set; } // Primary Key
        public string Title { get; set; } // Course title
        public int Credits { get; set; } // Number of credits for the course

        // Navigation property
        public ICollection<Enrollment> Enrollments { get; set; } // Enrollments related to this course
    }
}