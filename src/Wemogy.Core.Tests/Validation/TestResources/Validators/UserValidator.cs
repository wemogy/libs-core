using System;
using FluentValidation;
using Wemogy.Core.Tests.Validation.TestResources.Enums;
using Wemogy.Core.Tests.Validation.TestResources.Models;
using Wemogy.Core.Validation;

namespace Wemogy.Core.Tests.Validation.TestResources.Validators
{
    public class UserValidator : AbstractModelValidator<User, CustomUserUpdateType>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).Must(x => x != Guid.Empty).WithMessage("Must be not GUID empty");
        }

        protected override void CreateValidationRules()
        {
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("No must not be empty");
        }

        protected override void UpdateValidationRules(User existing)
        {
            if (CustomValidatorMode != CustomUserUpdateType.DefaultSetter)
            {
                RuleFor(x => x.IsDefault).Equal(existing.IsDefault).WithMessage("Can not change is default");
            }

            RuleFor(x => x.Firstname).Equal(existing.Firstname).WithMessage("Name can not be changed");
        }
    }
}
