using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation.Rules
{
    public class ValidationRulePredicateGroupList<TModel> :
        IValidator<TModel>
    {
        readonly List<IValidationPredicate<TModel>[]> _predicateGroups
            = new List<IValidationPredicate<TModel>[]>();

        public void Add(
            IValidationPredicate<TModel>[] predicates)
        {
            _predicateGroups.Add(predicates);
        }

        public async Task<string[]> ExecuteAsync(ValidateRequest<TModel> request)
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
}