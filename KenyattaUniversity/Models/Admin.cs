using System.ComponentModel.DataAnnotations;

namespace KenyattaUniversity.Models
{
    public class Admin
    {
        public string EmpNo { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
