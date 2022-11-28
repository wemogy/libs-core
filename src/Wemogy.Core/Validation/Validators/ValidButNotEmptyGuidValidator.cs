using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Wemogy.Core.Validation.Validators
{
    public class ValidButNotEmptyGuidValidator : IValidator<string>
    {
        public ValidationResult Validate(IValidationContext context)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateAsync(IValidationContext context, CancellationToken cancellation = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IValidatorDescriptor CreateDescriptor()
        {
            throw new NotImplementedException();
        }

        public bool CanValidateInstancesOfType(Type type)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(string instance)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateAsync(string instance, CancellationToken cancellation = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
