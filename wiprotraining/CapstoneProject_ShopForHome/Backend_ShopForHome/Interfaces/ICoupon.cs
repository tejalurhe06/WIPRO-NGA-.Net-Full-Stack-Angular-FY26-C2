namespace ShopForHome.API.Interfaces
{
    using ShopForHome.API.DTOs;

    public interface ICouponService
    {
        Task<List<CouponDTO>> GetUserCouponsAsync(int userId);
        Task<(decimal Discount, decimal FinalTotal, string Code)> ApplyCouponAsync(int userId, string code, decimal cartTotal);
    }
}
