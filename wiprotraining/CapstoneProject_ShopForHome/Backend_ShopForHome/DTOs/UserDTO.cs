using System;
using System.ComponentModel.DataAnnotations;
namespace ShopForHome.API.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required, EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Range(1, 2, ErrorMessage = "UserType must be 1 (User) or 2 (Admin).")]
        public int UserType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; }
    }


    public class CreateUserDTO
    {
        [Required, EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6,
            ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } // Will be hashed later

        [Required, StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Range(1, 2, ErrorMessage = "UserType must be 1 (User) or 2 (Admin).")]
        public int UserType { get; set; } = 1; // Default to User
    }

}