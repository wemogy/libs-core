using System;
using FluentValidation;

namespace Wemogy.Core.Validation.Extensions
{
    public static class RuleBuilderInitialExtensions
    {
        /// <summary>
        /// Use this extension method to add a validator for a nullable property.
        /// </summary>
        public static IRuleBuilderOptions<T, TProperty?> SetNullableValidator<T, TProperty>(
            this IRuleBuilderInitial<T, TProperty?> ruleFor, AbstractValidator<TProperty> validator)
        {
            return ruleFor.SetValidator(validator!).When(x => x != null);
        }

        public static IRuleBuilderOptions<T, string> MustBeLowercaseWithoutSpecialCharacters<T>(
            this IRuleBuilderInitial<T, string> ruleFor)
        {
            return ruleFor.Matches("^[a-z0-9]+$");
        }

        public static IRuleBuilderOptions<T, string> MustBeValidButNotEmptyGuid<T>(
            this IRuleBuilderInitial<T, string> ruleFor)
        {
            return ruleFor.Must(
                value =>
                {
                    if (Guid.TryParse(
                            value,
                            out var guid))
                    {
                        return guid != Guid.Empty;
                    }

                    return false;
                });
        }

        public static IRuleBuilderOptions<T, string> MustBeAValidSlug<T>(
            this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .Matches("^(?!-)[a-z]+(?:-[a-z]+)*(?!-)$")
                .WithMessage("The slug cannot contain special characters.");
        }
    }
}
