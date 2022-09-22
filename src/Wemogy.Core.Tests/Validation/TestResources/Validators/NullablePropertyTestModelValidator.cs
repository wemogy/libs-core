using FluentValidation;
using Wemogy.Core.Tests.Validation.TestResources.Models;
using Wemogy.Core.Validation.Extensions;

namespace Wemogy.Core.Tests.Validation.TestResources.Validators
{
    public class NullablePropertyTestModelValidator : AbstractValidator<NullablePropertyTestModel>
    {
        public NullablePropertyTestModelValidator()
        {
            RuleFor(x => x.NullableProperty)
                .SetNullableValidator(new UserValidator());
        }
    }
}
