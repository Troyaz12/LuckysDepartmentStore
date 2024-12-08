using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Utilities.Validation
{
    public class OneChoiceRequired : ValidationAttribute
    {
       
        private readonly string[] _propertyNames;

        public OneChoiceRequired(params string[] properties)
        {
            _propertyNames = properties;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            foreach (var propertyName in _propertyNames)
            {
                var property = validationContext.ObjectType.GetProperty(propertyName);
                if (property == null)
                    return new ValidationResult($"Unknown property: {propertyName}");

                var propertyValue = property.GetValue(validationContext.ObjectInstance, null);
                if (propertyValue != null && (decimal) propertyValue != 0)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage ?? "A discount must have either a dollar amount or percent.");
        }
    }
}
