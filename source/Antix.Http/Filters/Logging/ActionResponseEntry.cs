using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Antix.Http.Filters.Logging
{
    public class ActionResponseEntry
    {
        readonly HttpStatusCode _statusCode;
        readonly string _content;
        readonly HttpResponseHeaders _headers;

        public ActionResponseEntry(HttpResponseMessage result)
        {
            _statusCode = result.StatusCode;
            _content = result.Content == null
                ? "[NULL]"
                : result.Content.ReadAsStringAsync().Result;
            _headers = result.Headers;
        }

        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }

        public string Content
        {
            get { return _content; }
        }

        public HttpResponseHeaders Headers
        {
            get { return _headers; }
        }
    }
}