using System;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation
{
    public abstract class ValidatorBase<TModel> : IValidator<TModel>
        where TModel : class
    {
        readonly IObjectPredicates _is;
        readonly Func<IValidationRuleBuilder<TModel>> _getRulesBuilder;

        public ValidatorBase(
            IObjectPredicates @is,
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

        protected abstract void Validate(IValidationRuleBuilder<TModel> rules);
    }
}