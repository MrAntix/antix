using System;
using System.Linq.Expressions;

namespace Antix.Data.Projections
{
    public abstract class ProjectionBase<TData, TModel> :
        IProjection<TData, TModel>
    {
        Func<TData, TModel> _func;

        public abstract Expression<Func<TData, TModel>> GetExpression();

        public Func<TData, TModel> GetFunc()
        {
            return _func ?? (_func = GetExpression().Compile());
        }
    }
}