using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation.Rules
{
    public interface IValidationRuleBuilder<TModel>
    {
        IValidator<TModel>[] Build();

        IValidationRule<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression);

        IValidationRule<TProperty> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression);

        IValidationRulePredicated<TModel> When(
            IEnumerable<IValidationPredicate<TModel>> predicates);

        IValidationRulePredicated<TModel> Assert(
            IEnumerable<IValidationPredicate<TModel>> predicates);

        void Assert(IValidator<TModel> validator);
    }
}