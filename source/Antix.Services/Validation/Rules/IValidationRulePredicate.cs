using System;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation.Rules
{
    public interface IValidationRulePredicate<TModel>
    {
        IValidationRulePredicated<TModel> When(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates);

        IValidationRulePredicated<TModel> When(
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions);

        IValidationRulePredicated<TModel> Assert(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates);

        IValidationRulePredicated<TModel> Assert(
            string name,
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions);
    }
}