using System;
using System.Threading.Tasks;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation.Rules
{
    public interface IValidationRuleWhen<TModel>
    {
        IValidationRulePredicated<TModel> When(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates);

        IValidationRulePredicated<TModel> When(
            Func<TModel, Task<bool>> function,
            params Func<TModel, Task<bool>>[] functions);

        IValidationRulePredicated<TModel> When(
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions);

        IValidationRulePredicated<TModel> When(
            IValidator<TModel> validator);

        IValidationRulePredicated<TModel> When(
            Action<IValidationRule<TModel>> action);
    }
}