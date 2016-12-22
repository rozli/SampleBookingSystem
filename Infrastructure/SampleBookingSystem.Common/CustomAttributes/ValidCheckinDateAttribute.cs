using System;
using System.ComponentModel.DataAnnotations;

namespace SampleBookingSystem.Common.CustomAttributes
{
    public class ValidCheckinDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value >= DateTime.Now.Date)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
