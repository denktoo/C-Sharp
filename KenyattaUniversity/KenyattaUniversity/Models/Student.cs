using System.Collections.Generic;

namespace KenyattaUniversity.Models
{
    public class Student
    {
        public string StudentID { get; set; } // Primary Key
        public string Fname { get; set; } // First name of the student
        public string Lname { get; set; } // Last name of the student
        public string Email { get; set; } // Email address of the student

        // Navigation property
        public ICollection<Enrollment> Enrollments { get; set; } // Enrollments associated with this student
    }
}