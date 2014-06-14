using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antix.Http.Filters.Logging
{
    public class LogActionAttribute :
     Attribute, IFilterServiceAttribute
    {
        public Type ServiceType
        {
            get { return typeof(LogActionFilter); }
        }
    }
}
