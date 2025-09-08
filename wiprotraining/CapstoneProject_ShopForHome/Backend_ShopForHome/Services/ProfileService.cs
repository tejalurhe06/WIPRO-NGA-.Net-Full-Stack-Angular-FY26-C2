using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Models;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<ProfileService> _logger;

        public ProfileService(ShopForHomeDbContext context, ILogger<ProfileService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ProfileDTO> GetProfileAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", userId);
                throw new KeyNotFoundException("User not found.");
            }

            return new ProfileDTO
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                Addresses = user.Addresses.Select(a => new AddressDTO
                {
                    AddressId = a.AddressId,
                    FullName = a.FullName,
                    PhoneNumber = a.PhoneNumber,
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    City = a.City,
                    State = a.State,
                    PostalCode = a.PostalCode,
                    Country = a.Country,
                    IsDefault = a.IsDefault
                }).ToList()
            };
        }

        public async Task UpdateProfileAsync(int userId, UpdateProfileDTO dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found for update", userId);
                throw new KeyNotFoundException("User not found.");
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Profile updated for UserId={UserId}", userId);
        }

        public async Task<AddressDTO> AddAddressAsync(int userId, AddressDTO dto)
        {
            if (dto.IsDefault)
            {
                var currentDefaults = await _context.Addresses
                    .Where(a => a.UserId == userId && a.IsDefault)
                    .ToListAsync();

                foreach (var addr in currentDefaults)
                    addr.IsDefault = false;
            }

            var address = new Address
            {
                UserId = userId,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                PostalCode = dto.PostalCode,
                Country = dto.Country,
                IsDefault = dto.IsDefault
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            dto.AddressId = address.AddressId;
            _logger.LogInformation("Address {AddressId} added for UserId={UserId}", address.AddressId, userId);
            return dto;
        }

        public async Task DeleteAddressAsync(int userId, int addressId)
        {
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.AddressId == addressId && a.UserId == userId);

            if (address == null)
            {
                _logger.LogWarning("Address {AddressId} not found for UserId={UserId}", addressId, userId);
                throw new KeyNotFoundException("Address not found.");
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Address {AddressId} deleted for UserId={UserId}", addressId, userId);
        }
    }
}
