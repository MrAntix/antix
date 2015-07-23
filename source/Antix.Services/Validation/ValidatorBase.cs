using System;
using System.Threading.Tasks;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation
{
    public abstract class ValidatorBase<TModel, TPredicates> :
        IValidator<TModel>
        where TModel : class
        where TPredicates : IObjectPredicates
    {
        readonly Func<IRules<TModel>> _getRules;

        public ValidatorBase(
            TPredicates @is,
            Func<IRules<TModel>> getRules)
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

        protected abstract void Validate(IRule<TModel> rules);
    }

    public abstract class ValidatorBase<TModel> :
        ValidatorBase<TModel, IStandardValidationPredicates>
        where TModel : class

    {
        public ValidatorBase(
            IStandardValidationPredicates @is,
            Func<IRules<TModel>> getRules) :
                base(@is, getRules)
        {
        }
    }
}