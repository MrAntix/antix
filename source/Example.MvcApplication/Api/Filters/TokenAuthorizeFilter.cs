using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Antix.Security.Sessions;

namespace Example.MvcApplication.Api.Filters
{
    public class TokenAuthorizeFilter : AuthorizationFilterAttribute
    {
        readonly ISessionService _sessionService;

        public TokenAuthorizeFilter(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext == null) return;

            if (!AuthorizeRequest(actionContext.ControllerContext.Request))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        RequestMessage = actionContext.ControllerContext.Request
                    };
            }
        }

        bool AuthorizeRequest(HttpRequestMessage request)
        {
            if (request.Headers.Contains(TokenAuthorizeAttribute.Header))
            {
                var token = request.GetAuthorizationToken();

                var session = _sessionService.Authenticate(token);
                if (session != null)
                {
                    var identity = new GenericIdentity(session.User.Name);
                    var principle = new GenericPrincipal(identity, new string[] {});
                    Thread.CurrentPrincipal = principle;
                    if (HttpContext.Current != null)
                        HttpContext.Current.User = principle;

                    return true;
                }
            }

            return false;
        }
    }

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