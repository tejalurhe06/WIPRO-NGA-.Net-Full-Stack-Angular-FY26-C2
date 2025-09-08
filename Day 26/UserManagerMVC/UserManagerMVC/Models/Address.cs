using System.ComponentModel.DataAnnotations;

namespace UserManagerMVC.Models
{
    public class Address
    {
        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Zip code must be 5 digits")]
        public string ZipCode { get; set; }
    }
}
