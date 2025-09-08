using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;

        public AuthService(ShopForHomeDbContext context, ILogger<AuthService> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<UserDTO> RegisterAsync(CreateUserDTO createUserDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == createUserDto.Email))
            {
                _logger.LogWarning("Registration failed: Email {Email} already exists", createUserDto.Email);
                throw new ApplicationException("Email already exists");
            }

            var user = new User
            {
                Email = createUserDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                UserType = createUserDto.UserType,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Create a cart for the new user
            _context.Carts.Add(new Cart { UserId = user.UserId, CreatedAt = DateTime.UtcNow });
            await _context.SaveChangesAsync();

            _logger.LogInformation("New user registered: {UserId}", user.UserId);

            return new UserDTO
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.IsActive);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                _logger.LogWarning("Login failed for email: {Email}", loginRequest.Email);
                throw new ApplicationException("Invalid credentials");
            }

            var token = GenerateJwtToken(user);

            _logger.LogInformation("User logged in: {UserId}", user.UserId);

            return new LoginResponse
            {
                Token = token,
                User = new UserDTO
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserType = user.UserType,
                    CreatedAt = user.CreatedAt,
                    IsActive = user.IsActive
                }
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.UserType == 2 ? "Admin" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
