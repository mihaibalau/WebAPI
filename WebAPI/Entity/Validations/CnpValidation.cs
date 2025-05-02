using System.ComponentModel.DataAnnotations;

public class CnpValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var cnp = value as string;
        if (!string.IsNullOrEmpty(cnp) && cnp.Length == 13)
            return ValidationResult.Success;

        return new ValidationResult("CNP must be exactly 13 characters long.");
    }
}
