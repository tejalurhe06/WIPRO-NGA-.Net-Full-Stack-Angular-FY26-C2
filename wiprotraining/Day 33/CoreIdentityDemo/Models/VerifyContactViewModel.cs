using MimeKit.Encodings;

namespace IdentityApp.Models
{
    public class VerifyContactViewModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Status { get; set; }

        public string VerificationCode { get; set; }
    }
}