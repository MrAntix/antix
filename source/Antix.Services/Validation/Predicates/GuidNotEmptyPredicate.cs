using System;
using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class GuidNotEmptyPredicate : ValidationPredicateBase<Guid>
    {
        public new static readonly string Name =
            GetDefaultName(typeof(GuidNotEmptyPredicate));

        public override async Task<bool> IsAsync(Guid model)
        {
            return !Equals(model, Guid.Empty);
        }
    }
}