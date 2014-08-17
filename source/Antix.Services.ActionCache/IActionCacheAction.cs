using System.Threading.Tasks;

namespace Antix.Services.ActionCache
{
    public interface IActionCacheAction<in TIn, TOut> :
        IServiceInOut<TIn, TOut>, IActionCacheAction
    {
    }

    public interface IActionCacheAction
    {
        Task<object> ExecuteAsync(object model);
    }
}