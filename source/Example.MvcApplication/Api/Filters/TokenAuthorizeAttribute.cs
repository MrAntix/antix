using System;
using System.Web.Http.Filters;

namespace Example.MvcApplication.Api.Filters
{
    public class TokenAuthorizeAttribute :
        AuthorizationFilterAttribute, IProxyFilterAttribute
    {
        public const string Header = "Authentication_Token";

        public Type FilterType { get { return typeof (TokenAuthorizeFilter); } }
    }
}