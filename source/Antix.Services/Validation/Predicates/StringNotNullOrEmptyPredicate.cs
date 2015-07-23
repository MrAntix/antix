using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class StringNotNullOrEmptyPredicate : ValidationPredicateBase<string>
    {
        public override async Task<bool> IsAsync(string model)
        {
            return !string.IsNullOrEmpty(model);
        }
    }
}