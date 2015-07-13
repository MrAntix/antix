using System.Collections.Generic;
using System.Net.Http;

namespace Antix.Http.Services.Client
{
    public class HttpClientServiceRequest
    {
        readonly IDictionary<string, string> _headers;

        public HttpClientServiceRequest()
        {
            _headers = new Dictionary<string, string>();
        }

        public HttpMethod Method { get; set; }
        public string UriString { get; set; }
        public object Content { get; set; }

        public IDictionary<string, string> Headers
        {
            get { return _headers; }
        }
    }
}