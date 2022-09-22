using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Wemogy.Core.Validation.Enums;

namespace Wemogy.Core.Validation
{
    public abstract class AbstractModelValidator<T> : AbstractModelValidator<T, DefaultCustomValidatorMode>
        where T : class
    {
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "We use generic overwrite here")]
    public abstract class AbstractModelValidator<T, TCustomValidatorMode> : AbstractValidator<T>
        where T : class
        where TCustomValidatorMode : struct
    {
        protected TCustomValidatorMode? CustomValidatorMode { get; private set; }

        public void Initialize(T? existingModel = null, TCustomValidatorMode? customValidatorMode = null)
        {
            CustomValidatorMode = customValidatorMode;
            if (existingModel == null)
            {
                CreateValidationRules();
            }
            else
            {
                UpdateValidationRules(existingModel);
            }
        }

        protected abstract void CreateValidationRules();

        protected abstract void UpdateValidationRules(T existing);
    }
}
