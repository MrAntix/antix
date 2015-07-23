using System.Collections.Generic;
using System.Threading.Tasks;

namespace Antix.Services.Validation.Rules
{
    public class ValidationRuleValidator<TModel> :
        IValidator<TModel>
    {
        readonly IValidationRuleBuilder<TModel> _builder;

        public ValidationRuleValidator(
            IValidationRuleBuilder<TModel> builder)
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
}