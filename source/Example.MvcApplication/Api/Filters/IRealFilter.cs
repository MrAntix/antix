namespace Example.MvcApplication.Api.Filters
{
    public interface IRealFilter<T>
        where T : IProxyFilterAttribute
    {
        IProxyFilterAttribute Proxy { set; }
    }
}