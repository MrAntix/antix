using System.Web.Http;
using Antix.Security.Sessions;
using Example.MvcApplication.Api.Handlers;

namespace Example.MvcApplication.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );

            config.MessageHandlers.Add(
                new AuthenticationMessageHandler(
                    (ISessionService) config.DependencyResolver.GetService(typeof (ISessionService))
                    ));
        }
    }
}