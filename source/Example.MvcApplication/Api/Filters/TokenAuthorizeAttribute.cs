using System.Web.Http.Filters;

namespace Example.MvcApplication.Api.Filters
{
    public class TokenAuthorizeAttribute :
        AuthorizationFilterAttribute
    {
        public const string Header = "Authentication_Token";
    }
}