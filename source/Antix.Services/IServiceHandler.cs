using System.Threading.Tasks;

namespace Antix.Services
{
    public interface IServiceHandler
    {
        Task HandleAsync<TIn>(TIn model);
        Task<TOut> HandleAsync<TIn, TOut>(TIn model);
        Task<TOut> HandleAsync<TOut>();
    }
}