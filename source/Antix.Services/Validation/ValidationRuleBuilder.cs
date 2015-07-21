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
        protected readonly ValidationActionList<TModel> Actions =
            new ValidationActionList<TModel>();

        public virtual void Build(
            ValidationBuildState state,
            TModel model, string path)
        {
            var originalActions = Actions.ToArray();

            var i = 0;
            while (i < Actions.Count)
            {
                Actions[i](state, model, path);
                i++;
            }

            Actions.Clear();
            Actions.AddRange(originalActions);
        }

        public IValidationRuleBuilder<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression)
        {
            var builder = new ValidationRuleBuilder<TProperty>();
            Actions.Add(
                (state, model, path) => builder.Build(
                    state,
                    model, propertyExpression,
                    path));

            return builder;
        }

        public IValidationRuleBuilder<TModel> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression,
            Action<IValidationRuleBuilder<TProperty>> action)
        {
            Actions.Add(
                (state, model, path) =>
                {
                    var builder = new ValidationRuleBuilder<TProperty>();
                    action(builder);

                    builder.Build(state,
                        model, propertyExpression, path);
                });

            return this;
        }

        public IValidationRuleBuilder<TProperty> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression)
        {
            var builder = new ValidationRuleBuilder<TProperty>();
            Actions.Add(
                (state, model, path) => builder
                    .BuildEach(state,
                        model, propertyExpression, path));

            return builder;
        }

        public IValidationRuleBuilder<TModel> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression,
            Action<IValidationRuleBuilder<TProperty>> action)
        {
            Actions.Add(
                (state, model, path) =>
                {
                    var builder = new ValidationRuleBuilder<TProperty>();
                    action(builder);

                    builder.BuildEach(state,
                        model, propertyExpression, path);
                });

            return this;
        }

        public IValidationRuleBuilder<TModel> Validate(
            IValidator<TModel> validator)
        {
            Actions.Add(
                (state, model, path) =>
                    state.Errors.AddRange(validator.Validate(model, path))
                );

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
            string predicateName,
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions)
        {
            return GetAssertionBuilder(
                true,
                predicateName,
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

        public IValidationAssertionBuilder<TModel> Then(
            Action<IValidationRuleBuilder<TModel>> action)
        {
            var actions = new ValidationActionList<TModel>();
            Actions.Add(
                (state, model, path) =>
                {
                    if (state.Errors.Any()) return;

                    var builder = new ValidationRuleBuilder<TModel>();
                    action(builder);

                    builder.Build(state, model, path);
                });

            var assertionBuilder = new ValidationAssertionBuilder<TModel>(actions, false);

            Actions.Add(assertionBuilder.Build);

            return assertionBuilder;
        }

        IValidationAssertionBuilder<TModel> GetAssertionBuilder(
            bool assert,
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates
            )
        {
            var assertion = new ValidationActionList<TModel>();
            assertion.Add(predicate, predicates);

            var assertionBuilder = new ValidationAssertionBuilder<TModel>(assertion, assert);

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