using System.Collections.Generic;
using System.Web.Http.Filters;

namespace Antix.Http.Filters
{
    public interface IFilterService<in T> :
        IFilter
        where T : IFilterServiceAttribute
    {
        IEnumerable<T> Attributes { set; }
    }
}