using System;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation
{
    public abstract class ValidatorBase<TModel, TPredicates> :
        IValidator<TModel>
        where TModel : class
        where TPredicates : IObjectPredicates
    {
        readonly TPredicates _is;
        readonly Func<IValidationRuleBuilder<TModel>> _getRulesBuilder;

        public ValidatorBase(
            TPredicates @is,
            Func<IValidationRuleBuilder<TModel>> getRulesBuilder)
        {
            _is = @is;
            _getRulesBuilder = getRulesBuilder;
        }

        public string[] Validate(
            TModel model,
            string path)
        {
            var rules = _getRulesBuilder();
            rules.When(_is.NotNull)
                .Then(Validate);

            return rules.Build(model, path);
        }

        protected TPredicates Is
        {
            get { return _is; }
        }

        protected abstract void Validate(IValidationRuleBuilder<TModel> rules);
    }

    public abstract class ValidatorBase<TModel> :
        ValidatorBase<TModel, IStandardValidationPredicates>
        where TModel : class

    {
        public ValidatorBase(
            IStandardValidationPredicates @is,
            Func<IValidationRuleBuilder<TModel>> getRulesBuilder) :
                base(@is, getRulesBuilder)
        {
        }
    }
}