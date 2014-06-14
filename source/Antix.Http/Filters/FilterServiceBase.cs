using System.Collections.Generic;

namespace Antix.Http.Filters
{
    public abstract class FilterServiceBase<T> :
        IFilterService<T>
        where T : IFilterServiceAttribute
    {
        public IEnumerable<T> Attributes { set; private get; }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}