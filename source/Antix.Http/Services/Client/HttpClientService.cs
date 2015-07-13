using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace Antix.Http.Services.Client
{
    public class HttpClientService :
        IHttpClientService
    {
        readonly Func<HttpClient> _getClient;
        readonly MediaTypeFormatter _mediaTypeFormatter;

        public HttpClientService(
            Func<HttpClient> getClient,
            MediaTypeFormatter mediaTypeFormatter)
        {
            _getClient = getClient;
            _mediaTypeFormatter = mediaTypeFormatter;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(
            HttpClientServiceRequest model)
        {
            using (var client = _getClient())
            {
                var requestMessage = new HttpRequestMessage
                {
                    Method = model.Method,
                    RequestUri = new Uri(model.UriString)
                };
                foreach (var header in model.Headers)
                    requestMessage.Headers.Add(header.Key, header.Value);

                if (model.Content != null)
                {
                    requestMessage.Content =
                        new ObjectContent(
                            model.Content.GetType(), model.Content,
                            _mediaTypeFormatter);
                }

                return await client.SendAsync(requestMessage);
            }
        }
    }
}