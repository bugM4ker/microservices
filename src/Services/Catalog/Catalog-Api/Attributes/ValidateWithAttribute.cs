using FluentValidation;

namespace Catalog_Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateWithAttribute : Attribute
    {
        public Type ValidatorType { get; }
        public ValidateWithAttribute(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new ArgumentException("ValidatorType must implement IValidator interface", nameof(validatorType));
            }
            ValidatorType = validatorType;
        }
    }
}
