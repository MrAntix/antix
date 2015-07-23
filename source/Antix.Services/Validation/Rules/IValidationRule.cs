namespace Antix.Services.Validation.Rules
{
    public interface IValidationRule<TModel> :
        IValidationRuleFor<TModel>, IValidationRuleWith<TModel>, IValidationRulePredicate<TModel>
    {
    }
}