using System;
using ShopForHome.API.Validation;
using System.ComponentModel.DataAnnotations;

namespace ShopForHome.API.DTOs
{
    public class CouponDTO
{
    public int CouponId { get; set; }

    [Required, StringLength(20)]
    public string Code { get; set; }

    [StringLength(200)]
    public string Description { get; set; }

    [Range(1, 2, ErrorMessage = "DiscountType must be 1 (Percentage) or 2 (Fixed Amount).")]
    public int DiscountType { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Discount must be greater than 0.")]
    public decimal DiscountValue { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? MinimumAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public bool IsActive { get; set; }

    [EmailAddress]
    public string CreatedBy { get; set; } // Admin email
}

        

public class CreateCouponDTO
{
    [Required, StringLength(20, MinimumLength = 3)]
    [RegularExpression(@"^[A-Z0-9\-]+$", 
        ErrorMessage = "Coupon code must contain only uppercase letters, numbers, and dashes.")]
    public string Code { get; set; }

    [StringLength(200)]
    public string Description { get; set; }

    [Required, Range(1, 2, ErrorMessage = "DiscountType must be 1 (Percentage) or 2 (Fixed Amount).")]
    public int DiscountType { get; set; }

    [Required, Range(0.01, 100000,
        ErrorMessage = "Discount value must be greater than 0.")]
    public decimal DiscountValue { get; set; }

    [Range(0, 100000,
        ErrorMessage = "Minimum amount must be 0 or higher.")]
    public decimal? MinimumAmount { get; set; }

    [FutureDate(ErrorMessage = "Expiry date must be in the future.")]
    public DateTime? ExpiresAt { get; set; }
}


    public class AssignCouponDTO
{
    [Required, StringLength(20)]
    [RegularExpression(@"^[A-Z0-9\-]+$",
        ErrorMessage = "Coupon code must contain only uppercase letters, numbers, and dashes.")]
    public string CouponCode { get; set; }

    [Required, Range(1, int.MaxValue, ErrorMessage = "UserId must be valid.")]
    public int UserId { get; set; }
}

}