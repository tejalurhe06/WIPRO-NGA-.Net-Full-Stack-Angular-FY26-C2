using System.ComponentModel.DataAnnotations;
using System.Net;

namespace UserManagerMVC.Models
{
    public class User
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        // Nested Complex Type
        public Address Address { get; set; }
    }
}
