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
                = new ValidationRulePredicateGroupsValidator<TModel>();
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

        public IValidationRulePredicated<TModel> When(
            IValidator<TModel> validator)
        {
            var predicateGroups
                = new ValidationRulePredicateGroupsValidator<TModel>(validator);

            var builder = new ValidationRuleBuilder<TModel>();
            var rule = new ValidationRulePredicated<TModel>(
                builder, predicateGroups);

            _validators.Add(
                new ValidationRuleConditionalValidator<TModel>(
                    predicateGroups,
                    true, false,
                    builder)
                );

            return rule;
        }

        public IValidationRulePredicated<TModel> When(
            Action<IValidationRule<TModel>> action)
        {
            return When(CreateValidator(action));
        }

        public IValidationRulePredicated<TModel> Assert(
            IEnumerable<IValidationPredicate<TModel>> predicates)
        {
            var predicateGroups
                = new ValidationRulePredicateGroupsValidator<TModel>();
            predicateGroups.Add(predicates.ToArray());

            var builder = new ValidationRuleBuilder<TModel>();
            var rule = new ValidationRulePredicated<TModel>(builder, predicateGroups);

            _validators.Add(
                new ValidationRuleConditionalValidator<TModel>(
                    predicateGroups,
                    true, true,
                    builder)
                );

            return rule;
        }

        public IValidationRulePredicated<TModel> Assert(
            IValidator<TModel> validator)
        {
            var predicateGroups =
                new ValidationRulePredicateGroupsValidator<TModel>(validator);

            var builder = new ValidationRuleBuilder<TModel>();
            var rule = new ValidationRulePredicated<TModel>(
                builder, predicateGroups);

            _validators.Add(
                new ValidationRuleConditionalValidator<TModel>(
                    predicateGroups,
                    true, true,
                    builder)
                );

            return rule;
        }

        public IValidationRulePredicated<TModel> Assert(
            Action<IValidationRule<TModel>> action)
        {
            return Assert(CreateValidator(action));
        }

        static IValidator<TModel> CreateValidator(
            Action<IValidationRule<TModel>> action)
        {
            var predicateGroups =
                new ValidationRulePredicateGroupsValidator<TModel>();
            var builder = new ValidationRuleBuilder<TModel>();
            var rule = new ValidationRulePredicated<TModel>(builder, predicateGroups);

            action(rule);

            return new ValidationRuleValidator<TModel>(builder);
        }
    }
}