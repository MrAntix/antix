using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class NullPredicate : ValidationPredicateBase<object>
    {
        public override async Task<bool> IsAsync(object model)
        {
            return Equals(model, null);
        }
    }
}