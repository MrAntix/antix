using System;

namespace Antix.Http.Filters.Logging
{
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Method,
        AllowMultiple = false, Inherited = true)]
    public class LogActionAttribute :
        Attribute, IFilterServiceAttribute
    {
        public Type ServiceType
        {
            get { return typeof (LogActionFilter); }
        }
    }
}