using System.Linq;
using System.Threading.Tasks;

namespace Antix.Services.Validation.Rules
{
    public class ValidationRuleConditionalValidator<TModel> :
        ValidationRuleValidator<TModel>
    {
        readonly ValidationRulePredicateGroupList<TModel> _predicateGroups;
        readonly bool _onSuccess;
        readonly bool _assert;

        public ValidationRuleConditionalValidator(
            ValidationRulePredicateGroupList<TModel> predicateGroups,
            bool onSuccess, bool assert,
            IValidationRuleBuilder<TModel> builder) :
                base(builder)
        {
            _predicateGroups = predicateGroups;
            _onSuccess = onSuccess;
            _assert = assert;
        }

        public override async Task<string[]> ExecuteAsync(
            ValidateRequest<TModel> model)
        {
            var errors = await _predicateGroups.EvaluateAsync(model);
            if (
                (_onSuccess && !errors.Any())
                || (!_onSuccess && errors.Any()))
                return await base.ExecuteAsync(model);

            return _assert
                ? errors
                : new string[] {};
        }
    }
}