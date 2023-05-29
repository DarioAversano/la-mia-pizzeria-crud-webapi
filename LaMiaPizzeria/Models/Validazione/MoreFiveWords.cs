using System.ComponentModel.DataAnnotations;

namespace LaMiaPizzeria.Models.Validazione
{
    public class MoreFiveWords : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;

            if (fieldValue == null || fieldValue.Trim().Split(" ").Length < 5)
            {
                return new ValidationResult("Il campo deve contenere almeno 5 parole");
            }

            return ValidationResult.Success;
        }
    }
}
