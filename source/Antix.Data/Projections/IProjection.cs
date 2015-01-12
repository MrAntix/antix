using System;
using System.Linq.Expressions;

namespace Antix.Data.Projections
{
    public interface IProjection
    {
    }

    public interface IProjection<TData, TModel> :
        IProjection
    {
        Expression<Func<TData, TModel>> GetExpression();
        Func<TData, TModel> GetFunc();
    }
}