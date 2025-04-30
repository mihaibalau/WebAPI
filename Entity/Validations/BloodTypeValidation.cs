using System.ComponentModel.DataAnnotations;
using System.Linq;

public class BloodTypeValidationAttribute : ValidationAttribute
{
    private static readonly string[] AllowedTypes =
        { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string str && AllowedTypes.Contains(str))
            return ValidationResult.Success;

        return new ValidationResult($"Invalid blood type. Allowed values: {string.Join(", ", AllowedTypes)}");
    }
}
