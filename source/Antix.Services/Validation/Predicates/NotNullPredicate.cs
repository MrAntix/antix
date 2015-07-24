using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class NotNullPredicate : ValidationPredicateBase<object>
    {
        public new static readonly string Name =
            GetDefaultName(typeof(NotNullPredicate));

        public override async Task<bool> IsAsync(object model)
        {
            return !Equals(model, null);
        }
    }
}