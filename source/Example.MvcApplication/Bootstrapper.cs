using System.Web.Mvc;
using Antix.Data.Keywords;
using Antix.Data.Keywords.EF;
using Antix.Data.Keywords.Processing;
using Example.MvcApplication.App_Start;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace Example.MvcApplication
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

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
            container.RegisterInstance<IKeywordProcessor>(WordSplitKeywordProcessor.Create());
            container.RegisterType<IKeywordsBuilderProvider, KeywordsBuilderProvider>();
            container.RegisterType<IKeywordsIndexer, EFKeywordsManager>(new ContainerControlledLifetimeManager());
        }
    }
}