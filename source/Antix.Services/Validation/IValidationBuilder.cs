namespace Antix.Services.Validation
{
    public interface IValidationBuilder<in TModel>
    {
        void Build(ValidationBuildState state, TModel model, string path);
    }
}