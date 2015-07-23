namespace Antix.Services.Validation.Rules
{
    public interface IValidationRulePredicated<TModel> :
        IValidationRule<TModel>, IValidationRuleOr<TModel>
    {
    }
}