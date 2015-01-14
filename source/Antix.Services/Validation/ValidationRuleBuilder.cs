using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation
{
    public class ValidationRuleBuilder<TModel> :
        IValidationRuleBuilder<TModel>
    {
        protected readonly List<Func<TModel, string, IEnumerable<string>>> Actions
            = new List<Func<TModel, string, IEnumerable<string>>>();

        public virtual string[] Build(TModel model, string path)
        {
            return Actions.SelectMany(a => a(model, path)).ToArray();
        }

        public IValidationRuleBuilder<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression)
        {
            var builder = new ValidationRuleBuilder<TProperty>();
            Actions.Add(
                (model, path) => builder.Build(
                    model,
                    propertyExpression,
                    path));

            return builder;
        }

        public IValidationRuleBuilder<TModel> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression,
            Action<IValidationRuleBuilder<TProperty>> action)
        {
            Actions.Add(
                (model, path) =>
                {
                    var builder = new ValidationRuleBuilder<TProperty>();
                    action(builder);

                    return builder.Build(model, propertyExpression, path);
                });

            return this;
        }

        public IValidationRuleBuilder<TProperty> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression)
        {
            var builder = new ValidationRuleBuilder<TProperty>();
            Actions.Add(
                (model, path) => builder.BuildEach(
                    model,
                    propertyExpression,
                    path));

            return builder;
        }

        public IValidationRuleBuilder<TModel> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression,
            Action<IValidationRuleBuilder<TProperty>> action)
        {
            Actions.Add(
                (model, path) =>
                {
                    var builder = new ValidationRuleBuilder<TProperty>();
                    action(builder);

                    return builder.BuildEach(model, propertyExpression, path);
                });

            return this;
        }

        public IValidationRuleBuilder<TModel> Validate(
            IValidator<TModel> validator)
        {
            Actions.Add(
                validator.Validate);

            return this;
        }

        public IValidationAssertionBuilder<TModel> Assert(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            return GetAssertionBuilder(true, predicate, predicates);
        }

        public IValidationAssertionBuilder<TModel> When(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            return GetAssertionBuilder(false, predicate, predicates);
        }

        public IValidationAssertionBuilder<TModel> Assert(
            string ruleName,
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions)
        {
            return GetAssertionBuilder(
                true,
                ruleName,
                function, functions);
        }

        public IValidationAssertionBuilder<TModel> When(
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions)
        {
            return GetAssertionBuilder(
                false,
                string.Empty,
                function, functions);
        }

        public IValidationRuleBuilder<TModel> Then(
            Action<IValidationRuleBuilder<TModel>> action)
        {
            Actions.Add(
                (model, path) =>
                {
                    var builder = new ValidationRuleBuilder<TModel>();
                    action(builder);

                    return builder.Build(model, path);
                });

            return this;
        }

        IValidationAssertionBuilder<TModel> GetAssertionBuilder(
            bool assert,
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates
            )
        {
            var assertion = new ValidationAssertionList<TModel>(assert);
            assertion.Add(predicate, predicates);

            var assertionBuilder = new ValidationAssertionBuilder<TModel>(assertion);

            Actions.Add(assertionBuilder.Build);

            return assertionBuilder;
        }

        IValidationAssertionBuilder<TModel> GetAssertionBuilder(
            bool assert,
            string ruleName,
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions)
        {
            return GetAssertionBuilder(
                assert,
                new FunctionPredicate<TModel>(ruleName, function),
                functions.Select(f =>
                    (IValidationPredicate<TModel>)
                        new FunctionPredicate<TModel>(ruleName, f))
                    .ToArray()
                );
        }
    }
}