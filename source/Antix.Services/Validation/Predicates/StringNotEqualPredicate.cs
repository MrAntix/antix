using System;
using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class StringNotEqualPredicate : ValidationPredicateBase<string>
    {
        readonly string _compareTo;
        readonly StringComparison _comparison;

        public StringNotEqualPredicate(
            string compareTo,
            StringComparison comparison)
        {
            _compareTo = compareTo;
            _comparison = comparison;
        }

        public override async Task<bool> IsAsync(string model)
        {
            return !string.Equals(model, _compareTo, _comparison);
        }

        public override string ToString()
        {
            return NameFormat("equal", _compareTo, _comparison);
        }
    }
}