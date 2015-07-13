using System.Collections.Generic;
using System.Linq;

namespace Antix.Services.Models
{
    public class ServiceResponse :
        IServiceResponse
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

        IServiceResponse IServiceResponse.Copy(
            IEnumerable<string> errors)
        {
            return new ServiceResponse(errors);
        }

        IServiceResponse<TData> IServiceResponse.Copy<TData>(
            TData data)
        {
            return new ServiceResponse<TData>(data, Errors);
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

    public class ServiceResponse<TData> :
        ServiceResponse, IServiceResponse<TData>
    {
        readonly TData _data;

        internal ServiceResponse(
            TData data,
            IEnumerable<string> errors) :
                base(errors)
        {
            _data = data;
        }

        public TData Data
        {
            get { return _data; }
        }

        IServiceResponse IServiceResponse.Copy(
            IEnumerable<string> errors)
        {
            return new ServiceResponse<TData>(Data, errors);
        }

        IServiceResponse<TDataTo> IServiceResponse.Copy<TDataTo>(
            TDataTo data)
        {
            return new ServiceResponse<TDataTo>(data, Errors);
        }

        public new static readonly ServiceResponse<TData> Empty
            = new ServiceResponse<TData>(default(TData), null);

        object IServiceResponseHasData.Data
        {
            get { return Data; }
        }
    }
}