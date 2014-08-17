using System;
using System.Runtime.Serialization;

namespace Antix.Services.ActionCache
{
    [Serializable]
    public class ActionCacheDataNotFoundException : Exception
    {
        public ActionCacheDataNotFoundException(string identifier) :
            base(string.Format(ActionCacheResources.ActionCacheDataNotFound, identifier))
        {
        }

        protected ActionCacheDataNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}