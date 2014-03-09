using System.Threading.Tasks;

namespace Antix.Services
{
    public interface IService
    {
    }

    public interface IServiceIn<in TIn> : IService
    {
        Task ExecuteAsync(TIn model);
    }

    public interface IServiceInOut<in TIn, TOut> : IService
    {
        Task<TOut> ExecuteAsync(TIn model);
    }

    public interface IServiceOut<TOut> : IService
    {
        Task<TOut> ExecuteAsync();
    }
}