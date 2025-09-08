using System;
using System.Collections.Generic;

namespace ShopForHome.API.Models;

public partial class Coupon
{
    public int CouponId { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public int DiscountType { get; set; }

    public decimal DiscountValue { get; set; }

    public decimal? MinimumAmount { get; set; }

    public int CreatedById { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public bool IsActive { get; set; }

    public virtual User CreatedBy { get; set; } = null!;

    public virtual ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();
}
