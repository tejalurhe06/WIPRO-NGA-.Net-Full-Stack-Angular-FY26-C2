using System.ComponentModel.DataAnnotations;

namespace OnlineBookstoreMVC.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name="Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
