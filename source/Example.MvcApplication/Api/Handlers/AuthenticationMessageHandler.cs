using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Antix.Security.Sessions;

namespace Example.MvcApplication.Api.Handlers
{
    public class AuthenticationMessageHandler :
        DelegatingHandler
    {
        readonly ISessionService _sessionService;
        public const string TokenHeader = "Authentication_Token";

        public AuthenticationMessageHandler(
            ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            
            if (request.Headers.Contains(TokenHeader))
            {
                var token = request.Headers.GetValues(TokenHeader).Single();

                var session = _sessionService.Authenticate(token);

                var identity = new GenericIdentity(session.User.Name);
                var principle = new GenericPrincipal(identity, new string[] {});
                Thread.CurrentPrincipal = principle;
                if (HttpContext.Current != null)
                    HttpContext.Current.User = principle;
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}