using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Models
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }
    }
}