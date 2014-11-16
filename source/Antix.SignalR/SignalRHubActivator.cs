using System;
using Microsoft.AspNet.SignalR.Hubs;

namespace Antix.SignalR
{
    public class SignalRHubActivator : IHubActivator
    {
        readonly Func<Type, object> _resolve;

        public SignalRHubActivator(Func<Type, object> resolve)
        {
            _resolve = resolve;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub) _resolve(descriptor.HubType);
        }
    }
}