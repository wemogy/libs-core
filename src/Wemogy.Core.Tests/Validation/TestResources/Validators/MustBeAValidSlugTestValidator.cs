using FluentValidation;
using Wemogy.Core.Tests.Validation.TestResources.Models;
using Wemogy.Core.Validation.Extensions;

namespace Wemogy.Core.Tests.Validation.TestResources.Validators;

public class MustBeAValidSlugTestValidator : AbstractValidator<User>
{
    public MustBeAValidSlugTestValidator()
    {
        RuleFor(x => x.Firstname).MustBeAValidSlug();
        RuleFor(x => x.MiddleName)
            .MustBeAValidSlug()
            .Unless(x => x.MiddleName == null);
    }
}
