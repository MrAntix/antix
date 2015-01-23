using System.Threading.Tasks;

namespace Antix.Services
{
    public interface IServiceOut<TOut> : IService
    {
        Task<TOut> ExecuteAsync();
    }
}