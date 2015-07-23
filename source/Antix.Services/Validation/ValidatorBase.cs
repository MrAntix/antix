using System;
using System.Threading.Tasks;
using Antix.Services.Validation.Predicates;
using Antix.Services.Validation.Rules;

namespace Antix.Services.Validation
{
    public abstract class ValidatorBase<TModel, TPredicates> :
        IValidator<TModel>
        where TModel : class
        where TPredicates : IObjectPredicates
    {
        readonly Func<IValidationRules<TModel>> _getRules;

        public ValidatorBase(
            TPredicates @is,
            Func<IValidationRules<TModel>> getRules)
        {
            Is = @is;
            _getRules = getRules;
        }

        public async Task<string[]> ExecuteAsync(
            ValidateRequest<TModel> request)
        {
            var rules = _getRules();

            Validate(rules.First);

            return await rules.ExecuteAsync(request);
        }

        protected TPredicates Is { get; }

        protected abstract void Validate(IValidationRule<TModel> validationRules);
    }

    public abstract class ValidatorBase<TModel> :
        ValidatorBase<TModel, IStandardValidationPredicates>
        where TModel : class

    {
        public ValidatorBase(
            IStandardValidationPredicates @is,
            Func<IValidationRules<TModel>> getRules) :
                base(@is, getRules)
        {
        }
    }
}