namespace Antix.Services.Validation
{
    public interface IValidator<TModel> :
        IServiceInOut<ValidateRequest<TModel>, string[]>
    {
    }
}