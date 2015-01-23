using System.Collections.Generic;
using Antix.Services.Models;

namespace Antix.Http
{
    public class CreatedServiceResponse<TData> :
        ServiceResponse<TData>, ICreatedServiceResponse
    {
        readonly string _routeName;

        public CreatedServiceResponse(
            string routeName,
            TData data,
            IEnumerable<string> errors)
            : base(data, errors)
        {
            _routeName = routeName;
        }

        public string RouteName
        {
            get { return _routeName; }
        }
    }
}