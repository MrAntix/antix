using System.Collections.Generic;
using System.Linq;

namespace Antix.Services.Models
{
    public class ServiceResponseWithData<T> :
        IServiceResponseWithData
    {
        readonly IReadOnlyCollection<string> _errors;
        readonly T _data;

        public ServiceResponseWithData(
            T data,
            IEnumerable<string> errors = null)
        {
            _data = data;
            _errors = errors == null ? new string[] {} : errors.ToArray();
        }


        public IEnumerable<string> Errors
        {
            get { return _errors; }
        }

        public T Data
        {
            get { return _data; }
        }

        object IServiceResponseWithData.Data
        {
            get { return _data; }
        }

        public ServiceResponseWithData<T> WithErrors(
            IEnumerable<string> errors)
        {
            return new ServiceResponseWithData<T>(_data, errors);
        }
    }
}