using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation
{
    public interface IValidationAssertionBuilder<TModel> :
        IValidationRuleBuilder<TModel>
    {
        IValidationAssertionBuilder<TModel> Or(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates);
    }
}