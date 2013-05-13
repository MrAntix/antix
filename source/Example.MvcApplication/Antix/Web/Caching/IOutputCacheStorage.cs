using System;
using System.Collections.Specialized;

namespace Antix.Web.Caching
{
    public interface IOutputCacheStorage
    {
        void Initialize(NameValueCollection config);

        bool Exists(string key);
        bool IsExpired(string key);

        object Read(string key);
        void Write(string key, object data, DateTime expiresOn);
        void Delete(string key);
    }
}