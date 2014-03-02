using System;
using System.Threading.Tasks;

namespace Antix.Services
{
    public class ServiceHandler : IServiceHandler
    {
        readonly Func<Type, IService> _resolve;
        readonly Action<IService> _release;

        public ServiceHandler(
            Func<Type, IService> resolve,
            Action<IService> release)
        {
            _resolve = resolve;
            _release = release;
        }

        public async Task HandleAsync<TIn>(TIn model)
        {
            var service = Resolve<IServiceIn<TIn>>();
            try
            {
                await service.ExecuteAsync(model);
            }
            finally
            {
                _release(service);
            }
        }

        public async Task<TOut> HandleAsync<TIn, TOut>(TIn model)
        {
            var service = Resolve<IServiceInOut<TIn, TOut>>();
            try
            {
                return await service.ExecuteAsync(model);
            }
            finally
            {
                _release(service);
            }
        }

        public async Task<TOut> HandleAsync<TOut>()
        {
            var service = Resolve<IServiceOut<TOut>>();
            try
            {
                return await service.ExecuteAsync();
            }
            finally
            {
                _release(service);
            }
        }

        T Resolve<T>()
        {
            return (T) _resolve(typeof (T));
        }
    }
}