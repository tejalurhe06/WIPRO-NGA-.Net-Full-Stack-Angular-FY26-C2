using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Models;
using ShopForHome.API.Interfaces;

namespace ShopForHome.API.Services
{
    public class AdminCouponService : IAdminCouponService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<AdminCouponService> _logger;

        public AdminCouponService(ShopForHomeDbContext context, ILogger<AdminCouponService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<CouponDTO>> GetAllCouponsAsync()
        {
            _logger.LogInformation("Fetching all coupons");
            return await _context.Coupons
                .Include(c => c.CreatedBy)
                .Select(c => new CouponDTO
                {
                    CouponId = c.CouponId,
                    Code = c.Code,
                    Description = c.Description,
                    DiscountType = c.DiscountType,
                    DiscountValue = c.DiscountValue,
                    MinimumAmount = c.MinimumAmount,
                    CreatedAt = c.CreatedAt,
                    ExpiresAt = c.ExpiresAt,
                    IsActive = c.IsActive,
                    CreatedBy = c.CreatedBy.Email
                })
                .ToListAsync();
        }

        public async Task<CouponDTO> CreateCouponAsync(CreateCouponDTO dto, int adminId)
        {
            if (await _context.Coupons.AnyAsync(c => c.Code == dto.Code))
            {
                _logger.LogWarning("Coupon code {Code} already exists", dto.Code);
                throw new ApplicationException("Coupon code already exists");
            }

            var coupon = new Coupon
            {
                Code = dto.Code,
                Description = dto.Description,
                DiscountType = dto.DiscountType,
                DiscountValue = dto.DiscountValue,
                MinimumAmount = dto.MinimumAmount,
                CreatedById = adminId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = dto.ExpiresAt,
                IsActive = true
            };

            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Coupon {Code} created by admin {AdminId}", dto.Code, adminId);

            return new CouponDTO
            {
                CouponId = coupon.CouponId,
                Code = coupon.Code,
                Description = coupon.Description,
                DiscountType = coupon.DiscountType,
                DiscountValue = coupon.DiscountValue,
                MinimumAmount = coupon.MinimumAmount,
                CreatedAt = coupon.CreatedAt,
                ExpiresAt = coupon.ExpiresAt,
                IsActive = coupon.IsActive,
                CreatedBy = _context.Users.FirstOrDefault(u => u.UserId == adminId)?.Email
            };
        }

        public async Task AssignCouponToUserAsync(AssignCouponDTO dto)
        {
            var coupon = await _context.Coupons
                .FirstOrDefaultAsync(c => c.Code == dto.CouponCode && c.IsActive);

            if (coupon == null)
            {
                _logger.LogWarning("Coupon {Code} not found or inactive", dto.CouponCode);
                throw new KeyNotFoundException("Coupon not found");
            }

            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", dto.UserId);
                throw new KeyNotFoundException("User not found");
            }

            var existingAssignment = await _context.UserCoupons
                .FirstOrDefaultAsync(uc => uc.UserId == dto.UserId && uc.CouponId == coupon.CouponId);

            if (existingAssignment != null)
            {
                _logger.LogWarning("Coupon {Code} already assigned to user {UserId}", dto.CouponCode, dto.UserId);
                throw new ApplicationException("Coupon already assigned to user");
            }

            _context.UserCoupons.Add(new UserCoupon
            {
                UserId = dto.UserId,
                CouponId = coupon.CouponId,
                IsUsed = false
            });

            await _context.SaveChangesAsync();

            _logger.LogInformation("Coupon {Code} assigned to user {UserId}", dto.CouponCode, dto.UserId);
        }

        public async Task DeactivateCouponAsync(int couponId)
        {
            var coupon = await _context.Coupons.FindAsync(couponId);
            if (coupon == null)
            {
                _logger.LogWarning("Coupon with ID {CouponId} not found", couponId);
                throw new KeyNotFoundException("Coupon not found");
            }

            coupon.IsActive = false;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Coupon {CouponId} deactivated", couponId);
        }
    }
}