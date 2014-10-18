using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Antix.Logging;

namespace Antix.Http.Dispatcher
{
    public class ServiceHttpControllerActivator : IHttpControllerActivator
    {
        readonly Func<Type, IHttpController> _create;
        readonly Action<IHttpController> _release;
        readonly Log.Delegate _log;

        public ServiceHttpControllerActivator(
            Func<Type, IHttpController> create,
            Action<IHttpController> release,
            Log.Delegate log)
        {
            _create = create;
            _release = release;
            _log = log;
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var controller = _create(controllerType);

            request.RegisterForDispose(
                new Disposable(() => _release(controller)));

            return controller;
        }

        class Disposable : IDisposable
        {
            readonly Action _release;

            public Disposable(Action release)
            {
                _release = release;
            }

            public void Dispose()
            {
                _release();
            }
        }
    }
}