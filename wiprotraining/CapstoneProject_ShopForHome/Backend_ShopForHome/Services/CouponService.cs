using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopForHome.API.Data;
using ShopForHome.API.DTOs;
using ShopForHome.API.Models;
using ShopForHome.API.Interfaces;

namespace ShopForHome.API.Services
{
    public class CouponService : ICouponService
    {
        private readonly ShopForHomeDbContext _context;
        private readonly ILogger<CouponService> _logger;

        public CouponService(ShopForHomeDbContext context, ILogger<CouponService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<CouponDTO>> GetUserCouponsAsync(int userId)
        {
            var coupons = await _context.UserCoupons
                .Where(uc => uc.UserId == userId && !uc.IsUsed)
                .Include(uc => uc.Coupon)
                .Where(uc => uc.Coupon.IsActive && uc.Coupon.ExpiresAt > DateTime.UtcNow)
                .Select(uc => new CouponDTO
                {
                    CouponId = uc.Coupon.CouponId,
                    Code = uc.Coupon.Code,
                    Description = uc.Coupon.Description,
                    DiscountType = uc.Coupon.DiscountType,
                    DiscountValue = uc.Coupon.DiscountValue,
                    MinimumAmount = uc.Coupon.MinimumAmount,
                    ExpiresAt = uc.Coupon.ExpiresAt
                })
                .ToListAsync();

            return coupons;
        }

        public async Task<(decimal Discount, decimal FinalTotal, string Code)> ApplyCouponAsync(int userId, string code, decimal cartTotal)
        {
            var userCoupon = await _context.UserCoupons
                .Include(uc => uc.Coupon)
                .FirstOrDefaultAsync(uc => uc.UserId == userId
                                        && uc.Coupon.Code == code
                                        && !uc.IsUsed
                                        && uc.Coupon.IsActive
                                        && uc.Coupon.ExpiresAt > DateTime.UtcNow);

            if (userCoupon == null)
                throw new ApplicationException("Invalid or expired coupon");

            if (cartTotal < userCoupon.Coupon.MinimumAmount)
                throw new ApplicationException($"Minimum order amount of {userCoupon.Coupon.MinimumAmount} required");

            decimal discount = userCoupon.Coupon.DiscountType == 1
                ? cartTotal * (userCoupon.Coupon.DiscountValue / 100)
                : userCoupon.Coupon.DiscountValue;

            _logger.LogInformation("Coupon {Code} applied for User {UserId}", code, userId);

            return (discount, cartTotal - discount, userCoupon.Coupon.Code);
        }
    }
}
