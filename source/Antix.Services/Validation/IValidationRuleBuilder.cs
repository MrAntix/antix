using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation
{
    public interface IValidationRuleBuilder<TModel> :
        IValidationBuilder<TModel>
    {
        IValidationRuleBuilder<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression);

        IValidationRuleBuilder<TModel> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression,
            Action<IValidationRuleBuilder<TProperty>> action);

        IValidationAssertionBuilder<TModel> When(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates);

        IValidationAssertionBuilder<TModel> When(
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions);

        IValidationAssertionBuilder<TModel> Assert(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates);

        IValidationAssertionBuilder<TModel> Assert(
            string ruleName,
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions);

        IValidationRuleBuilder<TModel> Then(
            Action<IValidationRuleBuilder<TModel>> action);

        IValidationRuleBuilder<TProperty> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression);

        IValidationRuleBuilder<TModel> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression,
            Action<IValidationRuleBuilder<TProperty>> action);

        IValidationRuleBuilder<TModel> Validate(
            IValidator<TModel> validator);
    }
}