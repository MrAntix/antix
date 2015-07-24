namespace Antix.Services.Validation.Rules
{
    public interface IValidationRule<TModel> :
        IValidationRuleFor<TModel>, 
        IValidationRuleWhen<TModel>,
        IValidationRuleAssert<TModel>
    {
        
    }
}