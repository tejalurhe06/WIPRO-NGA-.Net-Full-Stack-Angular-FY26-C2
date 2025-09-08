using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopForHome.API.DTOs
{
    public class ProfileDTO
    {
        public int UserId { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<AddressDTO> Addresses { get; set; } = new List<AddressDTO>();
    }


    public class AddressDTO
    {
        public int AddressId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, Phone]
        [RegularExpression(@"^\+?[0-9]{10,15}$",
            ErrorMessage = "Phone number must be 10–15 digits (optionally starting with +).")]
        public string PhoneNumber { get; set; }

        [Required, StringLength(200)]
        public string AddressLine1 { get; set; }

        [StringLength(200)]
        public string AddressLine2 { get; set; }

        [Required, StringLength(100)]
        public string City { get; set; }

        [Required, StringLength(100)]
        public string State { get; set; }

        [Required, StringLength(20)]
        [RegularExpression(@"^[0-9A-Za-z\- ]+$", ErrorMessage = "Invalid postal code format.")]
        public string PostalCode { get; set; }

        [Required, StringLength(100)]
        public string Country { get; set; }

        public bool IsDefault { get; set; }
    }


    public class UpdateProfileDTO
    {
        [Required, StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
    }

}