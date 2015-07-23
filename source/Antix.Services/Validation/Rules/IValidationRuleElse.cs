namespace Antix.Services.Validation.Rules
{
    public interface IValidationRuleElse<TModel>
    {
        IValidationRule<TModel> Else();
    }
}