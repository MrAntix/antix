using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class EqualPredicate : ValidationPredicateBase<object>
    {
        readonly object _compareTo;

        public EqualPredicate(object compareTo)
        {
            _compareTo = compareTo;
        }

        public override async Task<bool> IsAsync(object model)
        {
            return Equals(model, _compareTo);
        }

        public override string ToString()
        {
            return NameFormat("equal", _compareTo);
        }
    }
}