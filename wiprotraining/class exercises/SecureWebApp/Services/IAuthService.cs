using SecureWebApp.Models;

namespace SecureWebApp.Services
{
    public interface IAuthService
    {
        Task<TokenResponse> Authenticate(LoginModel login);
    }
}