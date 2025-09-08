using SecureDatabaseApp.Models;

namespace SecureDatabaseApp.Services;

public interface IUserService
{
    Task<User?> RegisterUserAsync(UserRegistrationDto userRegistrationDto);
    Task<User?> AuthenticateUserAsync(UserLoginDto userLoginDto);
    Task<User?> GetUserByIdAsync(int id);
}