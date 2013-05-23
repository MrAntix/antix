using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Example.MvcApplication.Api.Filters
{
    public class InjectingFilterProvider : IFilterProvider
    {
        readonly Func<Type, object> _resolve;

        public InjectingFilterProvider(Func<Type, object> resolve)
        {
            _resolve = resolve;
        }

        public IEnumerable<FilterInfo> GetFilters(
            HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (actionDescriptor == null) throw new ArgumentNullException("actionDescriptor");

            var controllerFilters =
                actionDescriptor.ControllerDescriptor.GetFilters()
                                .Select(instance => new FilterInfo(instance, FilterScope.Controller));
            var actionFilters =
                actionDescriptor.GetFilters().Select(instance => new FilterInfo(instance, FilterScope.Action));

            return controllerFilters
                .Concat(actionFilters)
                .Select(fi =>
                    {
                        var ta = fi.Instance as TokenAuthorizeAttribute;
                        if (ta != null)
                        {
                            return new FilterInfo(
                                (IFilter) _resolve(typeof (TokenAuthorizeFilter)),
                                fi.Scope
                                );
                        }

                        return fi;
                    });
        }
    }
}