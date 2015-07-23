using System.Threading.Tasks;

namespace Antix.Services.Validation
{
    public interface IValidationBuilder<in TModel>
    {
        Task Build(ValidationBuildState state, TModel model, string path);
    }
}