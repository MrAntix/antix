using System.Net;
using System.Net.Http;
using System.Web.Http;
using Antix.Security.Sessions;
using Example.MvcApplication.Api.Filters;
using Example.MvcApplication.Api.Handlers;
using Example.MvcApplication.Api.Models;

namespace Example.MvcApplication.Api.Controllers
{
    public class SessionController : ApiController
    {
        readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public HttpResponseMessage Login(
            [FromBody] LoginModel model)
        {
            var session = _sessionService.Login(model.Email, model.Password);
            if (session != null)
            {
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Headers
                    .Add(AuthenticationMessageHandler.TokenHeader, session.Identifier);

                return response;
            }

            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [AuthorizeToken]
        public void PostLogout()
        {
        }
    }
}