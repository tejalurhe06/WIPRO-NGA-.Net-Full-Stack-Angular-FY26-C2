using Microsoft.AspNetCore.Identity;

namespace OnlineBookstoreMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
