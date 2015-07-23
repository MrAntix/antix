using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class StringEmptyPredicate : ValidationPredicateBase<string>
    {
        public override async Task<bool> IsAsync(string model)
        {
            return model.Length == 0;
        }
    }
}