using Microsoft.AspNetCore.Identity;

namespace KenyattaUniversity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? StudentID { get; set; }
        public string? FullName { get; set; } // User's full name
        //public string? DisplayName { get; set; } // A display name for the user
    }
}