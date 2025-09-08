using System.ComponentModel.DataAnnotations;
namespace ShopForHome.API.DTOs
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6,
            ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }


    public class LoginResponse
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public UserDTO User { get; set; }
    }

}