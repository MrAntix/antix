namespace Antix.Services.Validation
{
    public interface IValidator<in TModel> :
        IService
    {
        string[] Validate(TModel model, string path);
    }
}