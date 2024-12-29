using System.ComponentModel.DataAnnotations;

namespace KenyattaUniversity.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string SchoolID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}