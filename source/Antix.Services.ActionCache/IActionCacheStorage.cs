using System;

namespace Antix.Services.ActionCache
{
    public interface IActionCacheStorage :
        IService
    {
        string Store(object data, TimeSpan expiresIn, string identifier);
        object TryRetrieve(string code);
        void Remove(string code);
    }
}