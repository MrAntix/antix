namespace Antix.Services.Validation.Predicates
{
    public interface IObjectPredicates
    {
        IValidationPredicate<object> Null { get; }
        IValidationPredicate<object> NotNull { get; }
    }
}