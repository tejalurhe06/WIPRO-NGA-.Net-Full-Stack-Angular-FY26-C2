using Microsoft.AspNetCore.Identity;
using SecureDatabaseApp.Models;

namespace SecureDatabaseApp.Services;

public class AuthService
{
    private readonly PasswordHasher<User> _passwordHasher = new();

    public string HashPassword(User user, string password)
    {
        // If user is null (like during registration), it will still work
        return _passwordHasher.HashPassword(user, password);
    }

    public PasswordVerificationResult VerifyPassword(User user, string hashedPassword, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
    }
}