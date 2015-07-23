using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class NotNullPredicate : ValidationPredicateBase<object>
    {
        public override async Task<bool> IsAsync(object model)
        {
            return !Equals(model, null);
        }
    }
}