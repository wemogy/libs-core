using FluentValidation;
using Mapster;

namespace Wemogy.Core.Mapster.Extensions
{
    public static class AdaptExtensions
    {
        /// <summary>
        /// Adapt a source object to new destination object then do the ValidateAndThrow by the given Validator
        /// </summary>
        /// <typeparam name="TDestination">>Type of destination object</typeparam>
        /// <typeparam name="TFluentValidator">>Type of validator</typeparam>
        /// <param name="source">The source object</param>
        /// <returns>The new instance of destination after be adapted with source object</returns>
        public static TDestination AdaptValidateAndThrow<TDestination, TFluentValidator>(this object source)
            where TFluentValidator : IValidator<TDestination>, new()
        {
            var validator = new TFluentValidator();
            var mapped = source.Adapt<TDestination>();
            validator.ValidateAndThrow(mapped);

            return mapped;
        }

        /// <summary>
        /// Adapt the source object base on a existing instance of object then do the ValidateAndThrow by the given Validator
        /// </summary>
        /// <typeparam name="TDestination">Type of destination object</typeparam>
        /// <typeparam name="TFluentValidator">Type of validator</typeparam>
        /// <param name="source">The source object</param>
        /// <param name="destination">The existing instance of destination object</param>
        /// <returns>The destination instance after be adapted with source object</returns>
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
