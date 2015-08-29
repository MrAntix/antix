using System;

namespace Antix.Services.Validation.Predicates
{
    public interface IGuidPredicates
    {
        IValidationPredicate<Guid> GuidEmpty { get; }

        IValidationPredicate<Guid> GuidNotEmpty { get; }
    }
}