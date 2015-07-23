using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation.Rules
{
    public interface IValidationRuleOr<TModel>
    {
        IValidationRulePredicated<TModel> Or(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates);
    }
}