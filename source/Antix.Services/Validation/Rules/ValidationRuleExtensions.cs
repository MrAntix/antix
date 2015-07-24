using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation.Rules
{
    public static class ValidationRuleExtensions
    {
        public static IEnumerable<IValidationPredicate<TModel>> And<TModel>(
            this IValidationPredicate<TModel> predicate,
            IEnumerable<IValidationPredicate<TModel>> predicates)
        {
            return new[] {predicate}.Concat(predicates);
        }

        public static async Task<string[]> EvaluateAsync<TModel>(
            this IEnumerable<IValidationPredicate<TModel>> predicates,
            ValidateRequest<TModel> request)
        {
            var errors = new List<string>();
            foreach (var predicate in predicates)
            {
                var result = await predicate.IsAsync(request.Model);
                if (!result)
                {
                    errors.Add(
                        request.FormatError(predicate.Name)
                        );
                }
            }
            return errors.ToArray();
        }

        public static string FormatError<TModel>(
            this ValidateRequest<TModel> request,
            string name)
        {
            return string.Format("{0}:{1}", request.Path, name);
        }
    }

    //public class RulePredicatedElse<TModel> :
    //    Rule<TModel>, IRulePredicatedElse<TModel>
    //{
    //    public IRule<TModel> Else()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}