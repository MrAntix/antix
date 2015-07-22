using System;

namespace Antix.Services.Validation.Predicates
{
    public interface IStringPredicates
    {
        IValidationPredicate<string> Equal(string value, StringComparison comparison);
        IValidationPredicate<string> NotEqual(string value, StringComparison comparison);
        IValidationPredicate<string> Empty { get; }
        IValidationPredicate<string> NullOrEmpty { get; }
        IValidationPredicate<string> NullOrWhiteSpace { get; }
        IValidationPredicate<string> NotEmpty { get; }
        IValidationPredicate<string> NotNullOrEmpty { get; }
        IValidationPredicate<string> NotNullOrWhiteSpace { get; }

        IValidationPredicate<string> Length(int min, int max);
        IValidationPredicate<string> MaxLength(int max);
        IValidationPredicate<string> MinLength(int min);

        IValidationPredicate<string> Email { get; }
    }
}