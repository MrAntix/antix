using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation.Rules
{
    public class ValidationRulePredicateGroupsValidator<TModel> :
        IValidator<TModel>
    {
        readonly List<Func<ValidateRequest<TModel>, Task<string[]>>> _validateFunctions
            = new List<Func<ValidateRequest<TModel>, Task<string[]>>>();

        public ValidationRulePredicateGroupsValidator(
            IValidator<TModel> validator)
        {
            if (validator != null)
                _validateFunctions.Add(validator.ExecuteAsync);
        }

        public ValidationRulePredicateGroupsValidator() : this(null)
        {
        }

        public void Add(
            IEnumerable<IValidationPredicate<TModel>> predicates)
        {
            _validateFunctions.Add(predicates.EvaluateAsync);
        }

        public async Task<string[]> ExecuteAsync(
            ValidateRequest<TModel> request)
        {
            var allErrors = new List<string>();

            foreach (var validate in _validateFunctions)
            {
                var errors = await validate(request);

                if (!errors.Any()) return new string[] {};

                allErrors.AddRange(errors);
            }

            return allErrors.ToArray();
        }
    }
}