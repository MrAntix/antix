using System.Linq;
using System.Net.Http;
using Example.MvcApplication.Api.Filters;

namespace Example.MvcApplication.Api
{
    public static class Extensions
    {
        public static string GetAuthorizationToken(this HttpRequestMessage request)
        {
            return request.Headers
                          .GetValues(TokenAuthorizeAttribute.Header)
                          .Single();
        }
    }
}