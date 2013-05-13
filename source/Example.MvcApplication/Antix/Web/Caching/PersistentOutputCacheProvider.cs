using System;
using System.Collections.Specialized;
using System.Web.Caching;

namespace Antix.Web.Caching
{
    public class PersistentOutputCacheProvider : OutputCacheProvider
    {
        readonly IOutputCacheStorage _storage;

        public PersistentOutputCacheProvider()
        {
            _storage = new FileSystemOutputCacheStorage();
        }

        public PersistentOutputCacheProvider(IOutputCacheStorage storage)
        {
            _storage = storage;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            _storage.Initialize(config);

            base.Initialize(name, config);
        }

        public override object Add(string key, object entry, DateTime utcExpiry)
        {
            var data = Get(key);
            if (data != null) return data;

            Set(key, entry, utcExpiry);

            return entry;
        }

        public override object Get(string key)
        {
            if (!_storage.Exists(key)) return null;

            if (_storage.IsExpired(key))
            {
                _storage.Delete(key);
                return null;
            }

            return _storage.Read(key);
        }

        public override void Remove(string key)
        {
            if (!_storage.Exists(key)) return;

            _storage.Delete(key);
        }

        public override void Set(string key, object entry, DateTime utcExpiry)
        {
            _storage.Write(key, entry, utcExpiry);
        }
    }
}