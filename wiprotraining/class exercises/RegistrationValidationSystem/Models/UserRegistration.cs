using System.ComponentModel.DataAnnotations;

namespace RegistrationValidationSystem.Models
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
