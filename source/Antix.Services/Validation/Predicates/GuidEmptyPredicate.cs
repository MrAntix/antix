using System;
using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class GuidEmptyPredicate : ValidationPredicateBase<Guid>
    {
        public new static readonly string Name =
            GetDefaultName(typeof(GuidEmptyPredicate));

        public override async Task<bool> IsAsync(Guid model)
        {
            return Equals(model, Guid.Empty);
        }
    }
}