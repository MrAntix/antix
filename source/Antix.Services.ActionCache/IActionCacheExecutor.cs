using System;

namespace Antix.Services.ActionCache
{
    public interface IActionCacheExecutor :
        IServiceInOut<string, object>
    {
    }
}