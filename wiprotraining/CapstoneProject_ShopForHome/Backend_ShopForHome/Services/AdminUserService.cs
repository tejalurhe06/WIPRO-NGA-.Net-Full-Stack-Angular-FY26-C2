using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Models;
using ShopForHome.API.Interfaces;

namespace ShopForHome.API.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<AdminUserService> _logger;

        public AdminUserService(ShopForHomeDbContext context, ILogger<AdminUserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            _logger.LogInformation("Fetching all users");

            return await _context.Users
                .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserType = u.UserType,
                    CreatedAt = u.CreatedAt,
                    IsActive = u.IsActive
                })
                .ToListAsync();
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Where(u => u.UserId == id)
                .Select(u => new UserDTO
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserType = u.UserType,
                    CreatedAt = u.CreatedAt,
                    IsActive = u.IsActive
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                _logger.LogWarning("User with id {UserId} not found", id);
                throw new KeyNotFoundException("User not found");
            }

            _logger.LogInformation("Fetched user {UserId}", id);
            return user;
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            var user = await _context.Users.FindAsync(userDto.UserId);
            if (user == null)
            {
                _logger.LogWarning("User with id {UserId} not found", userDto.UserId);
                throw new KeyNotFoundException("User not found");
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.UserType = userDto.UserType;
            user.IsActive = userDto.IsActive;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Updated user {UserId}", userDto.UserId);
        }

        public async Task DeactivateUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with id {UserId} not found", id);
                throw new KeyNotFoundException("User not found");
            }

            // Soft delete
            user.IsActive = false;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deactivated user {UserId}", id);
        }
    }
}
