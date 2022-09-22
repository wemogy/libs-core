namespace Wemogy.Core.Validation
{
    public class WildcardModelValidator<T> : AbstractModelValidator<T>
        where T : class
    {
        protected override void CreateValidationRules()
        {
            // empty
        }

        protected override void UpdateValidationRules(T existing)
        {
            // empty
        }
    }
}
