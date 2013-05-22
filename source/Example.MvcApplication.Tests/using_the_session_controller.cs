using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web.Http;
using Antix.Security.Sessions;
using Example.MvcApplication.Api.Handlers;
using Example.MvcApplication.Api.Models;
using Microsoft.Practices.Unity;
using Moq;
using Unity.WebApi;
using Xunit;

namespace Example.MvcApplication.Tests
{
    public class using_the_session_controller
    {
        [Fact]
        public void can_login()
        {
            const string expectedSessionIdentifier = "expectedSessionIdentifier";

            var mockSessionSevice = new Mock<ISessionService>();
            mockSessionSevice
                .Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Session {Identifier = expectedSessionIdentifier});

            var server = GetApiServer(mockSessionSevice.Object);

            using (var client = new HttpMessageInvoker(server))
            {
                using (var request = CreatePost("Session/Login", new LoginModel()))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    Assert.Equal(
                        expectedSessionIdentifier,
                        response.Headers.GetValues(AuthenticationMessageHandler.TokenHeader).Single());
                }
            }
        }

        [Fact]
        public void fail_login()
        {
            var mockSessionSevice = new Mock<ISessionService>();
            mockSessionSevice
                .Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(default(Session));

            var server = GetApiServer(mockSessionSevice.Object);

            using (var client = new HttpMessageInvoker(server))
            {
                using (var request = CreatePost("Session/Login", new LoginModel()))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
                }
            }
        }

        static HttpRequestMessage CreatePost<T>(string url, T content)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post, string.Concat("http://localhost/", url))
                {
                    Content = new ObjectContent<T>(content, new JsonMediaTypeFormatter())
                };

            return request;
        }

        static HttpServer GetApiServer(ISessionService sessionService)
        {
            var container = new UnityContainer();
            container.RegisterInstance(sessionService);

            var config = new HttpConfiguration
                {
                    DependencyResolver = new UnityDependencyResolver(container)
                };

            config.Routes.MapHttpRoute(name: "DefaultApi",
                                       routeTemplate: "{controller}/{action}",
                                       defaults: new {id = RouteParameter.Optional});

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.MessageHandlers.Add(new AuthenticationMessageHandler(sessionService));

            var server = new HttpServer(config);

            return server;
        }
    }
}