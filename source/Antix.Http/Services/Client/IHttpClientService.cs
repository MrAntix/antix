using System.Net.Http;
using Antix.Services;

namespace Antix.Http.Services.Client
{
    public interface IHttpClientService :
        IServiceInOut<HttpClientServiceRequest, HttpResponseMessage>
    {
    }
}