namespace Antix.Services
{
    public static class ServiceSyncExtensions
    {
        public static void Execute(
            this IServiceNInOut service)
        {
            service.ExecuteAsync().Wait();
        }

        public static void Execute<TIn>(
            this IServiceIn<TIn> service, TIn model)
        {
            service.ExecuteAsync(model).Wait();
        }

        public static TOut Execute<TIn, TOut>(
            this IServiceInOut<TIn, TOut> service, TIn model)
        {
            return service.ExecuteAsync(model).Result;
        }

        public static TOut Execute<TOut>(
            this IServiceOut<TOut> service)
        {
            return service.ExecuteAsync().Result;
        }
    }
}