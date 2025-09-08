using ShopForHome.API.DTOs;
using ShopForHome.API.Models;

namespace ShopForHome.API.Interfaces
{
    public interface IAdminCouponService
    {
        Task<List<CouponDTO>> GetAllCouponsAsync();
        Task<CouponDTO> CreateCouponAsync(CreateCouponDTO dto, int adminId);
        Task AssignCouponToUserAsync(AssignCouponDTO dto);
        Task DeactivateCouponAsync(int couponId);
    }
}