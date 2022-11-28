using FluentValidation;
using Wemogy.Core.Tests.Validation.TestResources.Models;
using Wemogy.Core.Validation.Extensions;

namespace Wemogy.Core.Tests.Validation.TestResources.Validators;

public class MustBeValidButNotEmptyGuidTestValidator : AbstractValidator<User>
{
    public MustBeValidButNotEmptyGuidTestValidator()
    {
        RuleFor(x => x.Firstname).MustBeValidButNotEmptyGuid();
    }
}
