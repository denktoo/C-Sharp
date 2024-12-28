using System.ComponentModel.DataAnnotations;

namespace KenyattaUniversity.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        public string EmpNo { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}