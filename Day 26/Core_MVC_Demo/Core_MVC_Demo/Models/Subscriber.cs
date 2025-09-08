using System.ComponentModel.DataAnnotations;

namespace Core_MVC_Demo.Models
{
    public class Subscriber
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50,ErrorMessage ="Username must be less than 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100,MinimumLength =6,ErrorMessage ="Password must be at least 6 characters")]
        public string Email { get; set; }
    }
}
