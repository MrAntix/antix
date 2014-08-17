using System;

namespace Antix.Services.ActionCache
{
    public interface IActionCacheStore :
        IService
    {
        string Add(object data, TimeSpan expiresIn);
        object TryGet(string identifier);
        void Remove(string identifier);
    }
}