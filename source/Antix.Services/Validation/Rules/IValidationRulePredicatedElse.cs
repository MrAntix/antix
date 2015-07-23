namespace Antix.Services.Validation.Rules
{
    public interface IValidationRulePredicatedElse<TModel> :
        IValidationRule<TModel>, IValidationRuleElse<TModel>
    {
    }
}