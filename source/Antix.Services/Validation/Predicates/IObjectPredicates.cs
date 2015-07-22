namespace Antix.Services.Validation.Predicates
{
    public interface IObjectPredicates
    {
        IValidationPredicate<object> Equal(object value);
        IValidationPredicate<object> NotEqual(object value);
        IValidationPredicate<object> Null { get; }
        IValidationPredicate<object> NotNull { get; }
    }
}