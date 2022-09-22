using FluentValidation;
using Wemogy.Core.Tests.Validation.TestResources.Models;
using Wemogy.Core.Validation.Extensions;

namespace Wemogy.Core.Tests.Validation.TestResources.Validators
{
    public class MustBeLowercaseWithoutSpecialCharactersTestValidator : AbstractValidator<User>
    {
        public MustBeLowercaseWithoutSpecialCharactersTestValidator()
        {
            RuleFor(x => x.Firstname).MustBeLowercaseWithoutSpecialCharacters();
        }
    }
}
