Example Config using Castle.Windsor
===================================


        static void RegisterSignarR(
            IWindsorContainer container,
            ConnectionConfiguration hubConfiguration)
        {

            hubConfiguration.Resolver
                .Register(
                    typeof (IHubActivator),
                    () => new SignalRHubActivator(container.Resolve));
            hubConfiguration.Resolver
                .Register(
                    typeof (JsonSerializer),
                    () => new JsonSerializer
                    {
                        ContractResolver = new SignalRContractResolver()
                    });

            container.Register(
                Classes
                    .FromAssemblyContaining<[YOUR HUB]>()
                    .BasedOn<IHub>()
                    .WithServiceSelf()
                    .LifestyleTransient()
                );

        }