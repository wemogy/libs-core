using FluentValidation;
using Mapster;

namespace Wemogy.Core.Mapster.Extensions
{
    public static class AdaptExtensions
    {
        public static TDestination AdaptValidateAndThrow<TDestination, TFluentValidator>(this object source)
            where TFluentValidator : IValidator<TDestination>, new()
        {
            var validator = new TFluentValidator();
            var mapped = source.Adapt<TDestination>();
            validator.ValidateAndThrow(mapped);

            return mapped;
        }

        public static TDestination AdaptValidateAndThrow<TDestination, TFluentValidator>(this object source, TDestination destination)
            where TFluentValidator : IValidator<TDestination>, new()
        {
            var validator = new TFluentValidator();
            var mapped = source.Adapt(destination);
            validator.ValidateAndThrow(mapped);

            return mapped;
        }
    }
}
