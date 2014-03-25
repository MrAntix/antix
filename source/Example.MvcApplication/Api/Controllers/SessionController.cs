using System.Net;
using System.Net.Http;
using System.Web.Http;
using Example.MvcApplication.Api.Filters;
using Example.MvcApplication.Api.Models;
using Example.MvcApplication.Sessions;

namespace Example.MvcApplication.Api.Controllers
{
    public class SessionController : ApiController
    {
        readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public HttpResponseMessage Login(
            [FromBody] LoginModel model)
        {
            var session = _sessionService.Login(model.Email, model.Password);
            if (session != null)
            {
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Headers
                        .Add(TokenAuthorizeAttribute.Header, session.Identifier);

                return response;
            }

            return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [HttpPost]
        [TokenAuthorize]
        public HttpResponseMessage Logout()
        {
            var token = Request.GetAuthorizationToken();
            _sessionService.Logout(token);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}