using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecords.Models
{
    public class Student
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 15)]
        [Required]
        [Display(Name = "Student Reg Number")]
        public string? StudentRegNumber { get; set; }
        public string? Name { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be either 'Male' or 'Female'.")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string? Email { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Date Of Admission")]
        [DataType(DataType.Date)]
        public DateTime DateOfAdmission { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("^(Enrolled|Graduated)$", ErrorMessage = "Status must be either 'Enrolled' or 'Graduated'.")]
        public string? Status { get; set; }

        [RegularExpression(@"^\+?[1-9][0-9]{7,14}$")]
        [Required]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}