using System;

namespace Example.MvcApplication.Sessions
{
    public class SessionServiceSettings
    {
        readonly Func<string, string> _hash;
        readonly int _expireMinutes;

        public SessionServiceSettings(
            Func<string, string> hash, int expireMinutes)
        {
            _hash = hash;
            _expireMinutes = expireMinutes;
        }

        public Func<string, string> Hash
        {
            get { return _hash; }
        }

        public int ExpireMinutes
        {
            get { return _expireMinutes; }
        }
    }
}