using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Antix.Data.Projections
{
    public class ProjectionProvider :
        IProjectionProvider
    {
        readonly Func<IEnumerable<IProjection>> _getItems;
        IDictionary<Tuple<Type, Type>, IProjection> _items;

        public ProjectionProvider(
            Func<IEnumerable<IProjection>> getItems)
        {
            _getItems = getItems;
        }

        static Tuple<Type, Type> GetKey(IProjection projection)
        {
            var types = projection.GetType().GetInterfaces()
                .Single(i => i.Name == typeof (IProjection<,>).Name)
                .GetGenericArguments();

            return Tuple.Create(types[0], types[1]);
        }

        public Expression<Func<TData, TModel>> Get<TData, TModel>()
        {
            if (_items == null)
            {
                _items = _getItems()
                    .ToDictionary(GetKey);
            }

            var projection = (IProjection<TData, TModel>)
                _items[Tuple.Create(typeof (TData), typeof (TModel))];

            return projection.GetExpression();
        }
    }
}