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
        readonly IValidationRuleBuilder<TModel> _builder;

        public ValidationRule(IValidationRuleBuilder<TModel> builder)
        {
            _builder = builder;
        }

        public IValidationRule<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return _builder.For(propertyExpression);
        }

        public IValidationRule<TProperty> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression)
        {
            return _builder.ForEach(propertyExpression);
        }

        public IValidationRulePredicated<TModel> When(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            return _builder.When(
                predicate.And(predicates).ToArray());
        }

        public IValidationRulePredicated<TModel> When(
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions)
        {
            return _builder.When(
                new FunctionPredicate<TModel>(string.Empty, function)
                    .And(functions.Select(f => new FunctionPredicate<TModel>(string.Empty, f)))
                );
        }

        public IValidationRulePredicated<TModel> Assert(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            return _builder.Assert(
                predicate.And(predicates).ToArray());
        }

        public IValidationRulePredicated<TModel> Assert(
            string name,
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions)
        {
            return _builder.Assert(
                new FunctionPredicate<TModel>(name, function)
                    .And(functions.Select(f => new FunctionPredicate<TModel>(name, f)))
                );
        }

        public IValidationRule<TModel> Assert(
            IValidator<TModel> validator)
        {
            _builder.Assert(validator);

            return this;
        }

        public IValidationRule<TModel> Assert(
            Action<IValidationRule<TModel>> method)
        {
            method(this);

            return this;
        }
    }
}