using System;

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

        public void Handle<TIn>(TIn model)
        {
            var service = Resolve<IServiceIn<TIn>>();
            try
            {
                service.Execute(model);
            }
            finally
            {
                _release(service);
            }
        }

        public TOut Handle<TIn, TOut>(TIn model)
        {
            var service = Resolve<IServiceInOut<TIn, TOut>>();
            try
            {
                return service.Execute(model);
            }
            finally
            {
                _release(service);
            }
        }

        public TOut Handle<TOut>()
        {
            var service = Resolve<IServiceOut<TOut>>();
            try
            {
                return service.Execute();
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