using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TCKimlikValidationAttribute.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TCKimlikValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; 
            }

            string tckn = value.ToString();

            if (tckn.Length != 11 || !tckn.All(char.IsDigit))
            {
                return new ValidationResult("Geçersiz T.C. Kimlik Numarası");
            }

            int[] digits = tckn.Select(c => int.Parse(c.ToString())).ToArray();

            if (digits[0] == 0)
            {
                return new ValidationResult("Geçersiz T.C. Kimlik Numarası");
            }

            int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            int evenSum = digits[1] + digits[3] + digits[5] + digits[7];

            int tenthDigit = (oddSum * 7 - evenSum) % 10;
            int eleventhDigit = (oddSum + evenSum + digits[9]) % 10;

            if (digits[9] != tenthDigit || digits[10] != eleventhDigit)
            {
                return new ValidationResult("Geçersiz T.C. Kimlik Numarası");
            }

            return ValidationResult.Success;
        }
    }
}
