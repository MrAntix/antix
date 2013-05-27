using System;

namespace Example.MvcApplication.Api.Filters
{
    public interface IProxyFilterAttribute
    {
        Type FilterType { get; }
    }
}