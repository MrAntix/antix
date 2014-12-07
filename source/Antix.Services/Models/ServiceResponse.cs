using System.Collections.Generic;
using System.Linq;

namespace Antix.Services.Models
{
    public class ServiceResponse:IServiceResponse
    {
        readonly IReadOnlyCollection<string> _errors;

        public ServiceResponse(
            IEnumerable<string> errors = null)
        {
            _errors = errors == null ? new string[] {} : errors.ToArray();
        }

        public IEnumerable<string> Errors
        {
            get { return _errors; }
        }

        public static readonly ServiceResponse Empty = new ServiceResponse();

        public ServiceResponse WithErrors(
            IEnumerable<string> errors)
        {
            return new ServiceResponse(errors);
        }

        public ServiceResponseWithData<T> WithData<T>(
            T data)
        {
            return new ServiceResponseWithData<T>(data, _errors);
        }
    }
}