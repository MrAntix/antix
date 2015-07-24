using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation.Rules
{
    public class ValidationRule<TModel> :
        IValidationRule<TModel>
    {
        protected readonly IValidationRuleBuilder<TModel> Builder;

        public ValidationRule(IValidationRuleBuilder<TModel> builder)
        {
            Builder = builder;
        }

        public IValidationRule<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return Builder.For(propertyExpression);
        }

        public IValidationRule<TProperty> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression)
        {
            return Builder.ForEach(propertyExpression);
        }

        public IValidationRulePredicated<TModel> When(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            return Builder.When(
                predicate.And(predicates).ToArray());
        }

        public IValidationRulePredicated<TModel> When(
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions)
        {
            return Builder.When(
                new FunctionPredicate<TModel>(string.Empty, function)
                    .And(functions.Select(f => new FunctionPredicate<TModel>(string.Empty, f)))
                );
        }

        public IValidationRulePredicated<TModel> When(
            IValidator<TModel> validator)
        {
            return Builder.When(validator);
        }

        public IValidationRulePredicated<TModel> When(
            Action<IValidationRule<TModel>> action)
        {
            return Builder.When(action);
        }

        public IValidationRulePredicated<TModel> Assert(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            return Builder.Assert(
                predicate.And(predicates).ToArray());
        }

        public IValidationRulePredicated<TModel> Assert(
            string name,
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions)
        {
            return Builder.Assert(
                new FunctionPredicate<TModel>(name, function)
                    .And(functions.Select(f => new FunctionPredicate<TModel>(name, f)))
                );
        }

        public IValidationRulePredicated<TModel> Assert(
            IValidator<TModel> validator)
        {
            return Builder.Assert(validator);
        }

        public IValidationRulePredicated<TModel> Assert(
            Action<IValidationRule<TModel>> action)
        {
            return Builder.Assert(action);
        }
    }
}