using System.Collections.Generic;

namespace KenyattaUniversity.Models
{
    public class Course
    {
        public int CourseID { get; set; } // Primary Key
        public string Title { get; set; }
        public int Credits { get; set; }

        // Navigation property
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();// Enrollments related to this course
    }
}