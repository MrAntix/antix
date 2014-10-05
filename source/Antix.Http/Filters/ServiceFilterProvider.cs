using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Antix.Http.Filters
{
    public class ServiceFilterProvider : IFilterProvider
    {
        readonly FilterServiceResolver _resolver;

        public ServiceFilterProvider(Func<Type, object> resolve)
        {
            _resolver = new FilterServiceResolver(resolve);
        }

        public IEnumerable<FilterInfo> GetFilters(
            HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            // Taken from ActionDescriptorFilterProvider
            var controllerFilters
                = GetFilterInfos(actionDescriptor.ControllerDescriptor.GetFilters(), FilterScope.Controller);
            var actionFilters
                = GetFilterInfos(actionDescriptor.GetFilters(), FilterScope.Action);
            // Taken from ActionDescriptorFilterProvider

            var controllerProxiedFilters
                = GetFilterInfos(actionDescriptor.ControllerDescriptor.GetCustomAttributes<IFilterServiceAttribute>(),
                    FilterScope.Controller);
            var actionProxiedFilters
                = GetFilterInfos(actionDescriptor.GetCustomAttributes<IFilterServiceAttribute>(), FilterScope.Action);

            var allFilters = configuration.Filters
                .Concat(controllerFilters)
                .Concat(controllerProxiedFilters)
                .Concat(actionFilters)
                .Concat(actionProxiedFilters);

            return allFilters;
        }

        static IEnumerable<FilterInfo> GetFilterInfos(
            IEnumerable<IFilter> attributes, FilterScope scope)
        {
            return attributes.Select(i => new FilterInfo(i, scope));
        }

        IEnumerable<FilterInfo> GetFilterInfos(
            IEnumerable<IFilterServiceAttribute> allAttributes, FilterScope scope)
        {
            return _resolver
                .Resolve<IFilter>(allAttributes)
                .Select(f => new FilterInfo(f, scope));
        }
    }
}