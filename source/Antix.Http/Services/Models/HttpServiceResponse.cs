using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using Antix.Services.Models;

namespace Antix.Http.Services.Models
{
    public class HttpServiceResponse :
        IHttpServiceResponse<HttpServiceResponse>
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

        public HttpServiceResponse WithErrors(IEnumerable<string> errors)
        {
            return new HttpServiceResponse(_statusCode, _headers, errors);
        }

        public HttpServiceResponse WithStatusCode(HttpStatusCode statusCode)
        {
            return new HttpServiceResponse(statusCode, _headers, _errors);
        }

        public HttpServiceResponse WithHeaders(IReadOnlyDictionary<string, string> headers)
        {
            return new HttpServiceResponse(_statusCode, headers, _errors);
        }

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
        IHttpServiceResponse<HttpServiceResponse<TData>, TData>
    {
        readonly HttpStatusCode? _statusCode;
        readonly TData _data;
        readonly string[] _errors;
        readonly IReadOnlyDictionary<string, string> _headers;

        internal HttpServiceResponse(
            HttpStatusCode? statusCode,
            TData data,
            IEnumerable<KeyValuePair<string, string>> headers,
            IEnumerable<string> errors)
        {
            _statusCode = statusCode;
            _data = data;
            _headers = HttpServiceResponse.GetValueOrDefault(headers);
            _errors = ServiceResponse.GetValueOrDefault(errors);
        }


        object IServiceResponseHasData.Data
        {
            get { return Data; }
        }

        public HttpStatusCode? StatusCode
        {
            get { return _statusCode; }
        }

        public IReadOnlyDictionary<string, string> Headers
        {
            get { return _headers; }
        }

        public TData Data
        {
            get { return _data; }
        }

        public string[] Errors
        {
            get { return _errors; }
        }

        public HttpServiceResponse<TData> WithErrors(IEnumerable<string> errors)
        {
            return new HttpServiceResponse<TData>(StatusCode, Data, Headers, errors);
        }

        public HttpServiceResponse<TData> WithStatusCode(HttpStatusCode statusCode)
        {
            return new HttpServiceResponse<TData>(statusCode, Data, Headers, Errors);
        }

        public HttpServiceResponse<TData> WithHeaders(IReadOnlyDictionary<string, string> headers)
        {
            return new HttpServiceResponse<TData>(StatusCode, Data, headers, Errors);
        }

        public HttpServiceResponse<TData> WithData(TData data)
        {
            return new HttpServiceResponse<TData>(StatusCode, data, Headers, Errors);
        }
    }
}