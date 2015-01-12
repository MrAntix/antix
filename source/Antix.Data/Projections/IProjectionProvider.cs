using System;
using System.Linq.Expressions;

namespace Antix.Data.Projections
{
    public interface IProjectionProvider
    {
        Expression<Func<TData, TModel>> Get<TData, TModel>();
    }
}