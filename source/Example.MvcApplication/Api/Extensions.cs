using System.Linq;
using System.Net.Http;

namespace Example.MvcApplication.Api.Filters
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