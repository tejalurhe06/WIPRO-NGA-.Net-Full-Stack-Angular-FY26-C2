using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OnlineBookstoreMVC.Validations
{
    public class ISBNValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null) return new ValidationResult("ISBN is required.");

            string isbn = value.ToString();

            // Simple regex for ISBN-10 or ISBN-13
            string pattern = @"^(?:\d{9}[\dXx]|\d{13})$";
            if(Regex.IsMatch(isbn, pattern))
                return ValidationResult.Success;
            else
                return new ValidationResult("Invalid ISBN format. Must be ISBN-10 or ISBN-13.");
        }
    }
}
