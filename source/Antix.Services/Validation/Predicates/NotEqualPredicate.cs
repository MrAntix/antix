using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class NotEqualPredicate : ValidationPredicateBase<object>
    {
        readonly object _compareTo;

        public NotEqualPredicate(object compareTo)
        {
            _compareTo = compareTo;
        }

        public override async Task<bool> IsAsync(object model)
        {
            return !Equals(model, _compareTo);
        }

        public override string ToString()
        {
            return NameFormat("not-equal", _compareTo);
        }
    }
}