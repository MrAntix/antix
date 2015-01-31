namespace Antix.Services.Validation.Predicates
{
    public interface IValidationPredicate<in TModel> : IValidationPredicate
    {
        bool Is(TModel model);
    }

    public interface IValidationPredicate
    {
        string Name { get; }
    }
}