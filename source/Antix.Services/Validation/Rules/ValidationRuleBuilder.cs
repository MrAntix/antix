using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation.Rules
{
    public class ValidationRuleBuilder<TModel> :
        IValidationRuleBuilder<TModel>
    {
        readonly List<IValidator<TModel>> _validators
            = new List<IValidator<TModel>>();

        public IValidator<TModel>[] Build()
        {
            return _validators.ToArray();
        }

        public IValidationRule<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression)
        {
            var builder = new ValidationRuleBuilder<TProperty>();
            var rule = new ValidationRule<TProperty>(builder);

            _validators.Add(
                new ValidationRuleForValidator<TModel, TProperty>(propertyExpression, builder)
                );

            return rule;
        }

        public IValidationRule<TProperty> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression)
        {
            var builder = new ValidationRuleBuilder<TProperty>();
            var rule = new ValidationRule<TProperty>(builder);

            _validators.Add(
                new ValidationRuleForEachValidator<TModel, TProperty>(propertyExpression, builder)
                );

            return rule;
        }

        public IValidationRulePredicated<TModel> When(
            IEnumerable<IValidationPredicate<TModel>> predicates)
        {
            var predicateGroups
                = new ValidationRulePredicateGroupList<TModel>();
            predicateGroups.Add(predicates.ToArray());

            var builder = new ValidationRuleBuilder<TModel>();
            var rule = new ValidationRulePredicated<TModel>(builder, predicateGroups);

            _validators.Add(
                new ValidationRuleConditionalValidator<TModel>(
                    predicateGroups,
                    true, false,
                    builder)
                );

            return rule;
        }

        public IValidationRulePredicated<TModel> Assert(
            IEnumerable<IValidationPredicate<TModel>> predicates)
        {
            var predicateGroups
                = new ValidationRulePredicateGroupList<TModel>();
            predicateGroups.Add(predicates.ToArray());

            var builder = new ValidationRuleBuilder<TModel>();
            var rule = new ValidationRulePredicated<TModel>(builder, predicateGroups);

            Assert(
                new ValidationRuleConditionalValidator<TModel>(
                    predicateGroups,
                    true, true,
                    builder)
                );

            return rule;
        }

        public void Assert(
            IValidator<TModel> validator)
        {
            _validators.Add(validator);
        }
    }
}