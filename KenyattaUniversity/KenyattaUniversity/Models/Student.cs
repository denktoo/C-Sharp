namespace KenyattaUniversity.Models
{
    public class Student
    {
        public int StudentID { get; set; } // Primary Key
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }

        // Navigation property
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}