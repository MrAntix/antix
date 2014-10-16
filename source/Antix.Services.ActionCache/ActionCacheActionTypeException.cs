using System;
using System.Runtime.Serialization;

namespace Antix.Services.ActionCache
{
    [Serializable]
    public class ActionCacheActionTypeException : Exception
    {
        public ActionCacheActionTypeException() :
            base(ActionCacheResources.ActionCacheActionType)
        {
        }

        protected ActionCacheActionTypeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}