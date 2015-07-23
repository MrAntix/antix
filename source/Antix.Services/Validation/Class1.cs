using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation
{
    public static class RuleExtensions
    {
        public static IEnumerable<IValidationPredicate<TModel>> And<TModel>(
            this IValidationPredicate<TModel> predicate,
            IEnumerable<IValidationPredicate<TModel>> predicates)
        {
            return new[] {predicate}.Concat(predicates);
        }

        public static async Task<string[]> EvaluateAsync<TModel>(
            this IEnumerable<IValidationPredicate<TModel>> predicates,
            ValidateRequest<TModel> request)
        {
            var errors = new List<string>();
            foreach (var predicate in predicates)
            {
                var result = await predicate.IsAsync(request.Model);
                if (!result)
                {
                    errors.Add(
                        string.Format("{0}:{1}", request.Path, predicate.Name));
                }
            }
            return errors.ToArray();
        }
    }

    public interface IRuleBuilder<TModel>
    {
        IValidator<TModel>[] Build();

        IRule<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression);

        IRule<TProperty> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression);

        IRulePredicated<TModel> When(
            IEnumerable<IValidationPredicate<TModel>> predicates);

        IRulePredicated<TModel> Assert(
            IEnumerable<IValidationPredicate<TModel>> predicates);

        void Assert(IValidator<TModel> validator);
    }

    public interface IRule<TModel> :
        IRuleFor<TModel>, IRuleWith<TModel>, IRulePredicate<TModel>
    {
    }

    public interface IRulePredicated<TModel> :
        IRule<TModel>, IRuleOr<TModel>
    {
    }

    public interface IRulePredicatedElse<TModel> :
        IRule<TModel>, IRuleElse<TModel>
    {
    }

    public interface IRuleElse<TModel>
    {
        IRule<TModel> Else();
    }

    public interface IRuleFor<TModel>
    {
        IRule<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression);

        IRule<TProperty> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression);
    }

    public interface IRuleWith<TModel>
    {
        IRule<TModel> Assert(
            IValidator<TModel> validator);

        IRule<TModel> Assert(
            Action<IRule<TModel>> method);
    }

    public interface IRulePredicate<TModel>
    {
        IRulePredicated<TModel> When(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates);

        IRulePredicated<TModel> When(
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions);

        IRulePredicated<TModel> Assert(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates);

        IRulePredicated<TModel> Assert(
            string name,
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions);
    }

    public interface IRuleOr<TModel>
    {
        IRulePredicated<TModel> Or(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates);
    }

    //public class RulePredicatedElse<TModel> :
    //    Rule<TModel>, IRulePredicatedElse<TModel>
    //{
    //    public IRule<TModel> Else()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class RuleBuilder<TModel> :
        IRuleBuilder<TModel>
    {
        readonly List<IValidator<TModel>> _validators
            = new List<IValidator<TModel>>();

        public IValidator<TModel>[] Build()
        {
            return _validators.ToArray();
        }

        public IRule<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression)
        {
            var builder = new RuleBuilder<TProperty>();
            var rule = new Rule<TProperty>(builder);

            _validators.Add(
                new RuleForValidator<TModel, TProperty>(propertyExpression, builder)
                );

            return rule;
        }

        public IRule<TProperty> ForEach<TProperty>(Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression)
        {
            var builder = new RuleBuilder<TProperty>();
            var rule = new Rule<TProperty>(builder);

            _validators.Add(
                new RuleForEachValidator<TModel, TProperty>(propertyExpression, builder)
                );

            return rule;
        }

        public IRulePredicated<TModel> When(
            IEnumerable<IValidationPredicate<TModel>> predicates)
        {
            var predicateGroups
                = new ValidationPredicateGroups<TModel>();
            predicateGroups.Add(predicates.ToArray());

            var builder = new RuleBuilder<TModel>();
            var rule = new RulePredicated<TModel>(builder, predicateGroups);

            _validators.Add(
                new RuleConditionalValidator<TModel>(
                    predicateGroups,
                    RuleCondition.Then, false,
                    builder)
                );

            return rule;
        }

        public IRulePredicated<TModel> Assert(
            IEnumerable<IValidationPredicate<TModel>> predicates)
        {
            var predicateGroups
                = new ValidationPredicateGroups<TModel>();
            predicateGroups.Add(predicates.ToArray());

            var builder = new RuleBuilder<TModel>();
            var rule = new RulePredicated<TModel>(builder, predicateGroups);

            Assert(
                new RuleConditionalValidator<TModel>(
                    predicateGroups,
                    RuleCondition.Then, true,
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

    public class ValidationPredicateGroups<TModel>
    {
        readonly List<IValidationPredicate<TModel>[]> _predicateGroups
            = new List<IValidationPredicate<TModel>[]>();

        public void Add(
            IValidationPredicate<TModel>[] predicates)
        {
            _predicateGroups.Add(predicates);
        }

        public async Task<string[]> EvaluateAsync(ValidateRequest<TModel> request)
        {
            var allErrors = new List<string>();
            foreach (var predicates in _predicateGroups)
            {
                var errors = await predicates.EvaluateAsync(request);

                if (!errors.Any()) return new string[] {};

                allErrors.AddRange(errors);
            }

            return allErrors.ToArray();
        }
    }

    public class Rule<TModel> :
        IRule<TModel>
    {
        readonly IRuleBuilder<TModel> _builder;

        public Rule(IRuleBuilder<TModel> builder)
        {
            _builder = builder;
        }

        public IRule<TProperty> For<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return _builder.For(propertyExpression);
        }

        public IRule<TProperty> ForEach<TProperty>(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression)
        {
            return _builder.ForEach(propertyExpression);
        }

        public IRulePredicated<TModel> When(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            return _builder.When(
                predicate.And(predicates).ToArray());
        }

        public IRulePredicated<TModel> When(
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions)
        {
            return _builder.When(
                new FunctionPredicate<TModel>(string.Empty, function)
                    .And(functions.Select(f => new FunctionPredicate<TModel>(string.Empty, f)))
                );
        }

        public IRulePredicated<TModel> Assert(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            return _builder.Assert(
                predicate.And(predicates).ToArray());
        }

        public IRulePredicated<TModel> Assert(
            string name,
            Func<TModel, bool> function,
            params Func<TModel, bool>[] functions)
        {
            return _builder.Assert(
                new FunctionPredicate<TModel>(name, function)
                    .And(functions.Select(f => new FunctionPredicate<TModel>(name, f)))
                );
        }

        public IRule<TModel> Assert(
            IValidator<TModel> validator)
        {
            _builder.Assert(validator);

            return this;
        }

        public IRule<TModel> Assert(
            Action<IRule<TModel>> method)
        {
            method(this);

            return this;
        }
    }

    public class RulePredicated<TModel> :
        Rule<TModel>, IRulePredicated<TModel>
    {
        readonly ValidationPredicateGroups<TModel> _predicateGroups;

        public RulePredicated(
            IRuleBuilder<TModel> builder,
            ValidationPredicateGroups<TModel> predicateGroups) :
                base(builder)
        {
            _predicateGroups = predicateGroups;
        }

        public IRulePredicated<TModel> Or(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            _predicateGroups
                .Add(predicate.And(predicates).ToArray());

            return this;
        }
    }

    public class RuleValidator<TModel> :
        IValidator<TModel>
    {
        readonly IRuleBuilder<TModel> _builder;

        public RuleValidator(
            IRuleBuilder<TModel> builder)
        {
            _builder = builder;
        }

        public virtual async Task<string[]> ExecuteAsync(
            ValidateRequest<TModel> model)
        {
            var errors = new List<string>();

            foreach (var validator in _builder.Build())
            {
                errors.AddRange(
                    await validator.ExecuteAsync(model));
            }

            return errors.ToArray();
        }
    }

    public enum RuleCondition
    {
        Then,
        Else
    }

    public class RuleConditionalValidator<TModel> :
        RuleValidator<TModel>
    {
        readonly ValidationPredicateGroups<TModel> _predicateGroups;
        readonly RuleCondition _type;
        readonly bool _assert;

        public RuleConditionalValidator(
            ValidationPredicateGroups<TModel> predicateGroups,
            RuleCondition type, bool assert,
            IRuleBuilder<TModel> builder) :
                base(builder)
        {
            _predicateGroups = predicateGroups;
            _type = type;
            _assert = assert;
        }

        public override async Task<string[]> ExecuteAsync(
            ValidateRequest<TModel> model)
        {
            var errors = await _predicateGroups.EvaluateAsync(model);
            if (
                (_type == RuleCondition.Then && !errors.Any())
                || (_type == RuleCondition.Else && errors.Any()))
                return await base.ExecuteAsync(model);

            return _assert
                ? errors
                : new string[] {};
        }
    }

    public class RuleForValidator<TModel, TProperty> :
        IValidator<TModel>
    {
        readonly Expression<Func<TModel, TProperty>> _propertyExpression;
        readonly IRuleBuilder<TProperty> _builder;

        public RuleForValidator(
            Expression<Func<TModel, TProperty>> propertyExpression,
            IRuleBuilder<TProperty> builder)
        {
            _propertyExpression = propertyExpression;
            _builder = builder;
        }

        public async Task<string[]> ExecuteAsync(
            ValidateRequest<TModel> request)
        {
            var forRequest = new ValidateRequest<TProperty>(
                _propertyExpression.Compile()(request.Model),
                ConcatPath(request.Path, _propertyExpression)
                );

            var validators = _builder.Build();
            var errors = new List<string>();
            foreach (var validator in validators)
            {
                errors.AddRange(
                    await validator.ExecuteAsync(forRequest));
            }

            return errors.ToArray();
        }

        static string ConcatPath(string path, Expression propertyExpression)
        {
            return string.Format("{0}{1}{2}",
                path,
                string.IsNullOrEmpty(path) ? string.Empty : ".",
                ExpressionPathVisitor.GetPath(propertyExpression));
        }
    }

    public class RuleForEachValidator<TModel, TProperty> :
        IValidator<TModel>
    {
        readonly Expression<Func<TModel, IEnumerable<TProperty>>> _propertyExpression;
        readonly IRuleBuilder<TProperty> _builder;

        public RuleForEachValidator(
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyExpression,
            IRuleBuilder<TProperty> builder)
        {
            _propertyExpression = propertyExpression;
            _builder = builder;
        }

        public async Task<string[]> ExecuteAsync(
            ValidateRequest<TModel> request)
        {
            var models = _propertyExpression.Compile()(request.Model);
            var path = ConcatPath(request.Path, _propertyExpression);

            var validators = _builder.Build();
            var errors = new List<string>();

            var index = 0;
            foreach (var model in models)
            {
                var forRequest = new ValidateRequest<TProperty>(
                    model,
                    string.Format("{0}[{1}]", path, index));
                index++;

                foreach (var validator in validators)
                {
                    errors.AddRange(
                        await validator.ExecuteAsync(forRequest));
                }
            }

            return errors.ToArray();
        }

        static string ConcatPath(string path, Expression propertyExpression)
        {
            return string.Format("{0}{1}{2}",
                path,
                string.IsNullOrEmpty(path) ? string.Empty : ".",
                ExpressionPathVisitor.GetPath(propertyExpression));
        }
    }

    public interface IRules<TModel> :
        IValidator<TModel>
    {
        IRule<TModel> First { get; }
    }

    public class Rules<TModel> :
        IRules<TModel>
    {
        readonly IRuleBuilder<TModel> _builder;

        public Rules()
        {
            _builder = new RuleBuilder<TModel>();
            First = new Rule<TModel>(_builder);
        }

        public async Task<string[]> ExecuteAsync(
            ValidateRequest<TModel> request)
        {
            var validator = new RuleValidator<TModel>(_builder);

            return await validator.ExecuteAsync(request);
        }

        public IRule<TModel> First { get; }
    }
}