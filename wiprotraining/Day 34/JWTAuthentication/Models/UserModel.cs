using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "EmailAddress is required")]
        public string? EmailAddress { get; set; }
        public string? Password { get; set; } // optional, if you want login with password
    }
}