using Microsoft.AspNetCore.Identity;

namespace JKUATUniversity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } // User's full name
        public DateTime DateOfBirth { get; set; } // User's date of birth
        public string DisplayName { get; set; } // A display name for the user
        public DateTime LastLoginDateTime { get; set; } // Timestamp for last login
        public string ProfilePictureUrl { get; set; } // URL for the user's profile picture
    }
}