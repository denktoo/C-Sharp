namespace KenyattaUniversity.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; } // Primary Key
        public int CourseID { get; set; } // Foreign Key
        public int StudentID { get; set; } // Foreign Key
        public string? Grade { get; set; } // Nullable Grade

        // Navigation properties
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}