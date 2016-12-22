using System;
using System.ComponentModel.DataAnnotations;

namespace SampleBookingSystem.Common.CustomAttributes
{
    public class ValidCheckoutDateAttribute : ValidationAttribute
    {
        private const string CheckinPropertyName = "CheckinDate";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var checkinDate = (DateTime)validationContext.ObjectType.GetProperty(CheckinPropertyName).GetValue(validationContext.ObjectInstance, null);
            var checkoutDate = (DateTime)value;

            if (checkoutDate.Date > checkinDate.Date)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
