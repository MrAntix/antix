using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Filters;
using Antix.Security.Sessions;
using Example.MvcApplication.Api.Filters;
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
                .Returns(new Session {Identifier = expectedSessionIdentifier})
                .Verifiable();

            var server = GetApiServer(mockSessionSevice.Object);

            using (var client = new HttpMessageInvoker(server))
            {
                using (var request = CreatePost("Session/Login", new LoginModel()))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    Assert.Equal(
                        expectedSessionIdentifier,
                        response.Headers.GetValues(TokenAuthorizeAttribute.Header).Single());
                }
            }

            mockSessionSevice.Verify();
        }

        [Fact]
        public void fail_login()
        {
            var mockSessionSevice = new Mock<ISessionService>();
            mockSessionSevice
                .Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(default(Session))
                .Verifiable();

            var server = GetApiServer(mockSessionSevice.Object);

            using (var client = new HttpMessageInvoker(server))
            {
                using (var request = CreatePost("Session/Login", new LoginModel()))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
                }
            }

            mockSessionSevice.Verify();
        }

        [Fact]
        public void fail_logout_when_not_authorized()
        {
            var mockSessionSevice = new Mock<ISessionService>();

            var server = GetApiServer(mockSessionSevice.Object);

            using (var client = new HttpMessageInvoker(server))
            {
                using (var request = CreatePost("Session/Logout", new LoginModel()))
                using (var response = client.SendAsync(request, CancellationToken.None).Result)
                {
                    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
                }
            }

            mockSessionSevice
                .Verify(m => m.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public void fail_logout_when_not_authorized_with_token_mismatch()
        {
            var mockSessionSevice = new Mock<ISessionService>();

            var server = GetApiServer(mockSessionSevice.Object);
            mockSessionSevice
                .Setup(m => m.Authenticate(It.IsAny<string>()))
                .Returns(default(Session));

            using (var client = new HttpMessageInvoker(server))
            {
                using (var request = CreatePost("Session/Logout", new LoginModel()))
                {
                    request.Headers.Add(TokenAuthorizeAttribute.Header, "MISMATCH");
                    using (var response = client.SendAsync(request, CancellationToken.None).Result)
                    {
                        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
                    }
                }
            }

            mockSessionSevice
                .Verify(m => m.Authenticate(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void can_logout_when_authorized()
        {
            var mockSessionSevice = new Mock<ISessionService>();
            mockSessionSevice
                .Setup(m => m.Authenticate(It.IsAny<string>()))
                .Returns(new Session{User = new SessionUser{Name = "USER"}});

            mockSessionSevice
                .Setup(m => m.Logout(It.IsAny<string>()));

            var server = GetApiServer(mockSessionSevice.Object);

            using (var client = new HttpMessageInvoker(server))
            {
                using (var request = CreatePost("Session/Logout", new LoginModel()))
                {
                    request.Headers.Add(TokenAuthorizeAttribute.Header, "MATCH");
                    using (var response = client.SendAsync(request, CancellationToken.None).Result)
                    {
                        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    }
                }
            }

            mockSessionSevice
                .Verify(m => m.Authenticate(It.IsAny<string>()), Times.Once());

            mockSessionSevice
                .Verify(m => m.Logout(It.IsAny<string>()), Times.Once());
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

            config.Services.Replace(
                typeof (IFilterProvider), new InjectingFilterProvider(t => container.Resolve(t)));

            var server = new HttpServer(config);

            return server;
        }
    }
}