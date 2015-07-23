using System;

namespace Antix.Services.Validation.Rules
{
    public interface IValidationRuleWith<TModel>
    {
        IValidationRule<TModel> Assert(
            IValidator<TModel> validator);

        IValidationRule<TModel> Assert(
            Action<IValidationRule<TModel>> method);
    }
}