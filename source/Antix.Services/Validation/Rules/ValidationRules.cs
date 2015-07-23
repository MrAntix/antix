using System.Threading.Tasks;

namespace Antix.Services.Validation.Rules
{
    public class ValidationRules<TModel> :
        IValidationRules<TModel>
    {
        readonly IValidationRuleBuilder<TModel> _builder;

        public ValidationRules()
        {
            _builder = new ValidationRuleBuilder<TModel>();
            First = new ValidationRule<TModel>(_builder);
        }

        public async Task<string[]> ExecuteAsync(
            ValidateRequest<TModel> request)
        {
            var validator = new ValidationRuleValidator<TModel>(_builder);

            return await validator.ExecuteAsync(request);
        }

        public IValidationRule<TModel> First { get; }
    }
}