using System.ComponentModel.DataAnnotations;

namespace KenyattaUniversity.ViewModels
{
    public class RegisterViewModel
    {
        // Property for role selection
        public string SelectedRole { get; set; }
        public string? StudentID { get; set; }

        [Required]
        public string Fname { get; set; }

        [Required]
        public string Lname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}