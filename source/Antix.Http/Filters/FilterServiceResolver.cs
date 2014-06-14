using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Antix.Http.Filters
{
    public class FilterServiceResolver
    {
        readonly Func<Type, object> _resolve;
        readonly MethodInfo _setAttributesMethod;

        public FilterServiceResolver(Func<Type, object> resolve)
        {
            _resolve = resolve;
            _setAttributesMethod = GetType()
                .GetMethod("SetAttributes", BindingFlags.Static | BindingFlags.NonPublic);
        }

        public IEnumerable<T> Resolve<T>(
            IEnumerable<IFilterServiceAttribute> allAttributes)
        {
            foreach (var group in allAttributes.GroupBy(p => p.ServiceType))
            {
                var filter = (T) _resolve(group.Key);
                var attributes = group.ToArray();

                _setAttributesMethod
                    .MakeGenericMethod(attributes.First().GetType())
                    .Invoke(null, new object[] {filter, attributes});

                yield return filter;
            }
        }

        static void SetAttributes<T>(
            object service,
            IEnumerable<IFilterServiceAttribute> attributes)
            where T : IFilterServiceAttribute
        {
            var real = service as IFilterService<T>;
            if (real != null)
                real.Attributes = attributes.Cast<T>().ToArray();
        }
    }
}