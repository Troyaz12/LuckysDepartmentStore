using System.ComponentModel.DataAnnotations;

namespace LuckysDepartmentStore.Utilities.Validation
{
    public class FutureDateCheck : ValidationAttribute
    {
        private readonly string[] _propertyNames;

        public FutureDateCheck(params string[] properties)
        {
            _propertyNames = properties;        
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                foreach (var propertyName in _propertyNames)
                {
                    var property = validationContext.ObjectType.GetProperty(propertyName);

                    if (property == null)
                    {
                        return new ValidationResult($"Unknown property: {propertyName}");
                    }

                    var propertyValue = property.GetValue(validationContext.ObjectInstance, null);

                    if(propertyValue != null)
                    {
                        if (propertyValue is DateTime dateValue && DateTime.Compare(dateValue, DateTime.Now) > 0)
                        {
                            return ValidationResult.Success;
                        }
                    }
                }
                return new ValidationResult(ErrorMessage ?? "Date must be greater then current day.");
            }
            catch (Exception ex)
            {
                return new ValidationResult($"Validation error, contact support.");
            }
            
        }

    }
}
