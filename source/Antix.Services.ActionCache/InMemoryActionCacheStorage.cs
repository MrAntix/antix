using System;
using System.Collections.Generic;
using System.Threading;

namespace Antix.Services.ActionCache
{
    public class InMemoryActionCacheStorage :
        IActionCacheStorage
    {
        readonly Dictionary<string, object> _data;
        readonly Dictionary<string, Timer> _timers;
        readonly Dictionary<string, string> _index;

        public InMemoryActionCacheStorage(
            Dictionary<string, object> data = null)
        {
            _data = data ?? new Dictionary<string, object>();
            _timers = new Dictionary<string, Timer>();
            _index = new Dictionary<string, string>();
        }

        public string Store(
            object data,
            TimeSpan expiresIn,
            string identifier = null)
        {
            var code = default(string);

            if (!string.IsNullOrWhiteSpace(identifier)
                && _index.ContainsKey(identifier))
            {
                code = _index[identifier];
                _data[code] = data;

                TryRemoveTimer(code);
            }
            else
            {
                code = Guid.NewGuid().ToString("N");
                _data.Add(code, data);

                if(!string.IsNullOrWhiteSpace(identifier))
                    _index.Add(identifier, code);
            }

            AddTimerIfExpires(code, data, expiresIn);
            return code;
        }

        public object TryRetrieve(string code)
        {
            return _data.ContainsKey(code) ? _data[code] : null;
        }

        public void Remove(string code)
        {
            _data.Remove(code);
            TryRemoveTimer(code);
        }

        void TryRemoveTimer(string code)
        {
            if (!_timers.ContainsKey(code)) return;

            _timers[code].Dispose();
            _timers.Remove(code);
        }

        void AddTimerIfExpires(string code, object data, TimeSpan expiresIn)
        {
            if (expiresIn.Ticks < 0) return;

            var timer =
                new Timer(
                    o => Remove((string)o), code,
                    expiresIn, TimeSpan.FromMilliseconds(-1));
            _timers.Add(code, timer);
        }
    }
}