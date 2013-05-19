using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Example.MvcApplication.App_Start.Data;

namespace Example.MvcApplication.Services
{
    public interface IBlogReadService
    {
        Task<IEnumerable<T>> SearchAsync<T>(
            string text, bool includeUnpublished, int index, int pageSize, 
            Expression<Func<BlogEntry, T>> projection);

        T TryGetByIdentifier<T>(string identifier, 
            Expression<Func<BlogEntry, T>> projection);
    }
}