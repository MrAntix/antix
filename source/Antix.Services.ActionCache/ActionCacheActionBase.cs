using System.Threading.Tasks;

namespace Antix.Services.ActionCache
{
    public abstract class ActionCacheActionBase<TIn, TOut> :
        IActionCacheAction<TIn, TOut>
    {
        public abstract Task<TOut> ExecuteAsync(TIn model);

        async Task<object> IActionCacheAction
            .ExecuteAsync(object model)
        {
            return await ExecuteAsync((TIn) model);
        }
    }
}