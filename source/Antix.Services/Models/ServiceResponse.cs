using System.Collections.Generic;
using System.Linq;

namespace Antix.Services.Models
{
    public class ServiceResponse : IServiceResponse
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

        public ServiceResponse WithErrors(
            IEnumerable<string> errors)
        {
            return new ServiceResponse(errors);
        }

        public IServiceResponse<T> WithData<T>(
            T data)
        {
            return new ServiceResponse<T>(data, _errors);
        }

        public static readonly ServiceResponse Empty = new ServiceResponse();

        public static IServiceResponse<T> Data<T>(
            T data)
        {
            return new ServiceResponse<T>(data);
        }

        public static IServiceResponse<T> Data<T>()
        {
            return Data(default(T));
        }
    }

    public class ServiceResponse<T> :
        IServiceResponse<T>
    {
        readonly IReadOnlyCollection<string> _errors;
        readonly T _data;

        public ServiceResponse(
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

        public ServiceResponse<T> WithErrors(
            IEnumerable<string> errors)
        {
            return new ServiceResponse<T>(_data, errors);
        }
    }
}