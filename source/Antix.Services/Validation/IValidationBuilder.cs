namespace Antix.Services.Validation
{
    public interface IValidationBuilder<in TModel>
    {
        string[] Build(TModel model, string path);
    }
}