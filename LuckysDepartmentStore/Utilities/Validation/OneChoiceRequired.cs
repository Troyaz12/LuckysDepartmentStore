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
            try
            {
                foreach (var propertyName in _propertyNames)
                {
                    var property = validationContext.ObjectType.GetProperty(propertyName);
                    if (property == null)
                        return new ValidationResult($"Unknown property: {propertyName}");

                    var propertyValue = property.GetValue(validationContext.ObjectInstance, null);

                    if (propertyValue != null)
                    {
                        if(propertyValue is int intValue &&  intValue != 0)
                        {
                            return ValidationResult.Success;
                        }
                        if (propertyValue is decimal decimalValue && decimalValue != 0)
                        {
                            return ValidationResult.Success;
                        }
                        if (propertyValue is string stringValue && !string.IsNullOrWhiteSpace(stringValue))
                        {
                            return ValidationResult.Success;
                        }
                    }
                }
                return new ValidationResult(ErrorMessage ?? "At least one field is required.");
            }
            catch (Exception ex)
            { 
                return new ValidationResult($"Validation error, contact support.");
            }
        }
    }
}
