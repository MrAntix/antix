using System.Threading.Tasks;

namespace Antix.Services.Validation.Rules
{
    public class ValidationRules<TModel> :
        IValidationRules<TModel>
    {
        readonly IValidationRuleBuilder<TModel> _builder;
        readonly IValidationRule<TModel> _first;

        public ValidationRules()
        {
            _builder = new ValidationRuleBuilder<TModel>();
            _first = new ValidationRule<TModel>(_builder);
        }

        public async Task<string[]> ExecuteAsync(
            ValidateRequest<TModel> request)
        {
            var validator = new ValidationRuleValidator<TModel>(_builder);

            return await validator.ExecuteAsync(request);
        }

        public IValidationRule<TModel> First
        {
            get { return _first; }
        }
    }
}