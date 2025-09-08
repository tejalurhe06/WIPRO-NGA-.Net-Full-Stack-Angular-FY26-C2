using System;
using System.ComponentModel.DataAnnotations;

namespace ShopForHome.API.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute()
        {
            ErrorMessage = "The date must be in the future.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true; // Allow null → handled by [Required] if needed

            if (value is DateTime dateValue)
            {
                return dateValue > DateTime.UtcNow;
            }

            return false;
        }
    }
}
