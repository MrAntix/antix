using System.Web.Http;
using System.Web.Mvc;
using Antix.Data.Keywords;
using Antix.Data.Keywords.EF;
using Antix.Data.Keywords.Processing;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace Example.MvcApplication
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise(
            HttpConfiguration webApiConfiguration)
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            webApiConfiguration.DependencyResolver 
                = new Unity.WebApi.UnityDependencyResolver(container);

            return container;
        }

        static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            KeywordsConfig
                .RegisterKeywordIndexing(container.Resolve<IKeywordsIndexer>());

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(), 
                WithMappings.FromMatchingInterface,
                WithName.Default
                );

            container.RegisterInstance<IKeywordProcessor>(WordSplitKeywordProcessor.Create());
            container.RegisterType<IKeywordsBuilderProvider, KeywordsBuilderProvider>();
            container.RegisterType<IKeywordsIndexer, EFKeywordsManager>(new ContainerControlledLifetimeManager());
        }
    }
}