using System.Collections.Generic;
using System.Linq;

namespace Antix.Services.Models
{
    public class ServiceResponse :
        IServiceResponse
    {
        readonly IReadOnlyCollection<string> _errors;

        public ServiceResponse(
            IEnumerable<string> errors)
        {
            _errors = errors == null ? new string[] {} : errors.ToArray();
        }

        public ServiceResponse() :
            this(null)
        {
        }

        IEnumerable<string> IServiceResponse.Errors
        {
            get { return _errors; }
        }

        IServiceResponse IServiceResponse.Create(
            IEnumerable<string> errors)
        {
            return new ServiceResponse(errors);
        }

        IServiceResponse<TData> IServiceResponse.Create<TData>(
            TData data,
            IEnumerable<string> errors)
        {
            return new ServiceResponse<TData>(data, errors);
        }

        public static readonly IServiceResponse Empty = new ServiceResponse();
    }

    public class ServiceResponse<TData> :
        ServiceResponse, IServiceResponse<TData>
    {
        readonly TData _data;

        public ServiceResponse(
            TData data,
            IEnumerable<string> errors) :
                base(errors)
        {
            _data = data;
        }

        public ServiceResponse(
            IEnumerable<string> errors) :
                this(default(TData), errors)
        {
        }

        public ServiceResponse(
            TData data) :
                this(data, null)
        {
        }

        TData IServiceResponse<TData>.Data
        {
            get { return _data; }
        }

        object IServiceResponseWithData.Data
        {
            get { return _data; }
        }

        IServiceResponse IServiceResponse.Create(
            IEnumerable<string> errors)
        {
            return new ServiceResponse<TData>(_data, errors);
        }

        public new static readonly ServiceResponse<TData> Empty
            = new ServiceResponse<TData>(null);
    }
}