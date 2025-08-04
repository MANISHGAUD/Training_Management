using System.ComponentModel.DataAnnotations;

namespace Training_Management_System.Helpers
{
    public class DateValidator
    {
        public static ValidationResult ValidateCurrentOrFutureDate(DateTime date, ValidationContext context)
        {
            if (date.Date < DateTime.Today)
            {
                return new ValidationResult("Date must be today or in the future.");
            }

            return ValidationResult.Success!;
        }
    }
}
