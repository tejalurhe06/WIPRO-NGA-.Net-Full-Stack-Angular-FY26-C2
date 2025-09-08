using System.ComponentModel.DataAnnotations;

namespace Core_MVC_Demo.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; } = string.Empty;

    }
}
