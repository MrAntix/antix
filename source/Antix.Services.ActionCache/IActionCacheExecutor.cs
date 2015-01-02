using System.Threading.Tasks;

namespace Antix.Services.ActionCache
{
    public interface IActionCacheExecutor :
        IServiceInOut<string, object>
    {
        Task<T> ExecuteAsync<T>(string identifier);
    }
}