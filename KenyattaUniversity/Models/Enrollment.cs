namespace KenyattaUniversity.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; } // Primary Key
        public int CourseID { get; set; } // Foreign Key to Course
        public string SchoolID { get; set; } // Foreign Key to Student
        public string Grade { get; set; } // Nullable Grade

        // Navigation properties
        public virtual Course Course { get; set; } // The course associated with this enrollment
        public virtual User User { get; set; } // The student associated with this enrollment
    }
}