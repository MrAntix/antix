using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public interface IValidationPredicate<in TModel> : IValidationPredicate
    {
        Task<bool> IsAsync(TModel model);
    }

    public interface IValidationPredicate
    {
        string Name { get; }
    }
}