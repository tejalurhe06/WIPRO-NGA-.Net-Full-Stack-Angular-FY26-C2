using Microsoft.AspNetCore.Identity; // <-- ADD THIS NAMESPACE
using Microsoft.EntityFrameworkCore;
using SecureDatabaseApp.Data;
using SecureDatabaseApp.Models;

namespace SecureDatabaseApp.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly AuthService _authService;

    public UserService(ApplicationDbContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<User?> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
    {
        // Check if user already exists
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == userRegistrationDto.Username || u.Email == userRegistrationDto.Email);

        if (existingUser != null)
        {
            return null;
        }

        // Hash the password FIRST
        var passwordHash = _authService.HashPassword(null, userRegistrationDto.Password); // Pass null for user

        // Now create the user object with all properties set
        var newUser = new User
        {
            Username = userRegistrationDto.Username,
            Email = userRegistrationDto.Email,
            FullName = userRegistrationDto.FullName,
            PasswordHash = passwordHash // Set it here in the initializer
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task<User?> AuthenticateUserAsync(UserLoginDto userLoginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == userLoginDto.Username);

        if (user == null)
        {
            return null;
        }

        // Verify the provided password against the hash
        // PasswordVerificationResult is from Microsoft.AspNetCore.Identity
        var result = _authService.VerifyPassword(user, user.PasswordHash, userLoginDto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return user;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}