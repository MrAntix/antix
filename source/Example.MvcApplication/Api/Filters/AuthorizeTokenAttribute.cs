using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Example.MvcApplication.Api.Handlers;

namespace Example.MvcApplication.Api.Filters
{
    public class AuthorizeTokenAttribute : 
        AuthorizationFilterAttribute
    {
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

        static bool AuthorizeRequest(HttpRequestMessage request)
        {
            if (request.Headers.Contains(AuthenticationMessageHandler.TokenHeader))
            {
                var token = request.Headers
                                   .GetValues(AuthenticationMessageHandler.TokenHeader)
                                   .Single();

                return true;
            }
            return false;
        }
    }
}