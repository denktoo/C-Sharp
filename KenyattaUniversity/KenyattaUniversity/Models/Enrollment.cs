namespace KenyattaUniversity.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; } // Primary Key
        public int CourseID { get; set; } // Foreign Key to Course
        public string StudentID { get; set; } // Foreign Key to Student (ApplicationUser)
        public string Grade { get; set; } // Nullable Grade

        // Navigation properties
        public Course Course { get; set; } // The course associated with this enrollment
        public ApplicationUser Student { get; set; } // The student associated with this enrollment
    }
}