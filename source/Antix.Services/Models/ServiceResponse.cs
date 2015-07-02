using System.Collections.Generic;
using System.Linq;

namespace Antix.Services.Models
{
    public class ServiceResponse<TData> :
        IServiceResponse<ServiceResponse<TData>, TData>
    {
        readonly string[] _errors;
        readonly TData _data;

        internal ServiceResponse(
            TData data,
            IEnumerable<string> errors)
        {
            _data = data;
            _errors = ServiceResponse.GetValueOrDefault(errors);
        }

        public string[] Errors
        {
            get { return _errors; }
        }

        public TData Data
        {
            get { return _data; }
        }

        public ServiceResponse<TData> WithErrors(IEnumerable<string> errors)
        {
            return new ServiceResponse<TData>(Data, errors);
        }

        public ServiceResponse<TData> WithData(TData data)
        {
            return new ServiceResponse<TData>(data, Errors);
        }

        public static readonly ServiceResponse<TData> Empty
            = new ServiceResponse<TData>(default(TData), null);

        object IServiceResponseHasData.Data
        {
            get { return Data; }
        }
    }

    public class ServiceResponse :
        IServiceResponse<ServiceResponse>
    {
        readonly string[] _errors;

        internal ServiceResponse(IEnumerable<string> errors)
        {
            _errors = GetValueOrDefault(errors);
        }

        public string[] Errors
        {
            get { return _errors; }
        }

        public ServiceResponse WithErrors(
            IEnumerable<string> errors)
        {
            return new ServiceResponse(errors);
        }

        public static readonly ServiceResponse Empty
            = new ServiceResponse(null);

        public static string[] GetValueOrDefault(
            IEnumerable<string> errors)
        {
            return errors == null
                ? new string[] {}
                : errors.ToArray();
        }
    }
}