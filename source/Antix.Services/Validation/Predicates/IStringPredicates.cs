namespace Antix.Services.Validation.Predicates
{
    public interface IStringPredicates
    {
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