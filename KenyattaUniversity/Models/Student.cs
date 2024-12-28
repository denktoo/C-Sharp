using System.Collections.Generic;

namespace KenyattaUniversity.Models
{
    public class Student
    {
        public string RegNo { get; set; } // Primary Key
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Navigation property
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>(); // Enrollments associated with this student
    }
}