namespace IdentityApp.Models
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}