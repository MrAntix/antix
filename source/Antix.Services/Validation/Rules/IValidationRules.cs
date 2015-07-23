namespace Antix.Services.Validation.Rules
{
    public interface IValidationRules<TModel> :
        IValidator<TModel>
    {
        IValidationRule<TModel> First { get; }
    }
}