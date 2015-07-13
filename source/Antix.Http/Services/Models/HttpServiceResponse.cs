using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Antix.Services.Models;

namespace Antix.Http.Services.Models
{
    public class HttpServiceResponse :
        IHttpServiceResponse
    {
        readonly string[] _errors;
        readonly HttpStatusCode? _statusCode;
        readonly IReadOnlyDictionary<string, string> _headers;

        internal HttpServiceResponse(
            HttpStatusCode? statusCode,
            IEnumerable<KeyValuePair<string, string>> headers,
            IEnumerable<string> errors)
        {
            _statusCode = statusCode;
            _headers = GetValueOrDefault(headers);
            _errors = ServiceResponse.GetValueOrDefault(errors);
        }

        public string[] Errors
        {
            get { return _errors; }
        }

        public HttpStatusCode? StatusCode
        {
            get { return _statusCode; }
        }

        public IReadOnlyDictionary<string, string> Headers
        {
            get { return _headers; }
        }

        IServiceResponse IServiceResponse.WithErrors(IEnumerable<string> errors)
        {
            return new HttpServiceResponse(_statusCode, _headers, errors);
        }

        IServiceResponse<TData> IServiceResponse.WithData<TData>(TData data)
        {
            return new HttpServiceResponse<TData>(_statusCode, data, _headers, _errors);
        }

        IHttpServiceResponse IHttpServiceResponse.WithStatusCode(HttpStatusCode statusCode)
        {
            return new HttpServiceResponse(statusCode, _headers, _errors);
        }

        IHttpServiceResponse IHttpServiceResponse.WithHeaders(IReadOnlyDictionary<string, string> headers)
        {
            return new HttpServiceResponse(_statusCode, headers, _errors);
        }

        public static readonly HttpServiceResponse Empty
            = new HttpServiceResponse(null, null, null);

        public static IReadOnlyDictionary<string, string> GetValueOrDefault(
            IEnumerable<KeyValuePair<string, string>> value)
        {
            var readonlyDictionary = value as ReadOnlyDictionary<string, string>;
            if (readonlyDictionary != null) return readonlyDictionary;

            if (value == null) value = new Dictionary<string, string>();

            var dictionary = value as Dictionary<string, string>
                             ?? value.ToDictionary(i => i.Key, i => i.Value);

            return new ReadOnlyDictionary<string, string>(dictionary);
        }
    }

    public class HttpServiceResponse<TData> :
        HttpServiceResponse,
        IHttpServiceResponse<TData>
    {
        readonly TData _data;

        internal HttpServiceResponse(
            HttpStatusCode? statusCode,
            TData data,
            IEnumerable<KeyValuePair<string, string>> headers,
            IEnumerable<string> errors) :
                base(statusCode, headers, errors)
        {
            _data = data;
        }

        object IServiceResponseHasData.Data
        {
            get { return Data; }
        }

        public TData Data
        {
            get { return _data; }
        }

        public new static readonly HttpServiceResponse<TData> Empty
            = new HttpServiceResponse<TData>(null, default(TData), null, null);

        IHttpServiceResponse IHttpServiceResponse.WithStatusCode(HttpStatusCode statusCode)
        {
            return new HttpServiceResponse<TData>(statusCode, _data, Headers, Errors);
        }

        IHttpServiceResponse IHttpServiceResponse.WithHeaders(IReadOnlyDictionary<string, string> headers)
        {
            return new HttpServiceResponse<TData>(StatusCode, _data, headers, Errors);
        }

        IServiceResponse IServiceResponse.WithErrors(IEnumerable<string> errors)
        {
            return new HttpServiceResponse<TData>(StatusCode, _data, Headers, errors);
        }

        IServiceResponse<TDataTo> IServiceResponse.WithData<TDataTo>(TDataTo data)
        {
            return new HttpServiceResponse<TDataTo>(StatusCode, data, Headers, Errors);
        }
    }
}