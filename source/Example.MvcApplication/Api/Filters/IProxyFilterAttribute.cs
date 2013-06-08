using System;

namespace Example.MvcApplication.Api.Filters
{
    public interface IProxyFilterAttribute
    {
        Type FilterType { get; }
    }

    public interface IRealFilter<T>
        where T : IProxyFilterAttribute
    {
        IProxyFilterAttribute Proxy { set; }
    }
}