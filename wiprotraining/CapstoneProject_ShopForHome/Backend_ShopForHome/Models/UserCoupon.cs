using System;
using System.Collections.Generic;

namespace ShopForHome.API.Models;

public partial class UserCoupon
{
    public int UserCouponId { get; set; }

    public int UserId { get; set; }

    public int CouponId { get; set; }

    public bool IsUsed { get; set; }

    public virtual Coupon Coupon { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
