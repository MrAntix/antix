using System.Threading.Tasks;

namespace Antix.Services
{
    public interface IServiceInOut<in TIn, TOut> : IService
    {
        Task<TOut> ExecuteAsync(TIn model);
    }
}