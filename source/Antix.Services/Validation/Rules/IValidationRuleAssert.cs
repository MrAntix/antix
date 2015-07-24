using System;
using System.Linq.Expressions;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation.Rules
{
    public interface IValidationRuleAssert<TModel> 
    {
        IValidationRulePredicated<TModel> Assert(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates);

        IValidationRulePredicated<TModel> Assert(
            string name,
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions);

        IValidationRulePredicated<TModel> Assert(
            IValidator<TModel> validator);

        IValidationRulePredicated<TModel> Assert(
            Action<IValidationRule<TModel>> action);
    }
}