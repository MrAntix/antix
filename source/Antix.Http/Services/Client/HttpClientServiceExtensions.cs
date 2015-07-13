using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Antix.Http.Services.Models;
using Antix.Services;
using Antix.Services.Models;

namespace Antix.Http.Services.Client
{
    public static class HttpClientServiceExtensions
    {
        public static async Task<HttpServiceResponse<T>> ExecuteAndReadAsync<T>(
            this IServiceInOut<HttpClientServiceRequest, HttpResponseMessage> client,
            HttpClientServiceRequest request
            )
        {
            var responseMessage = await client.ExecuteAsync(request);
            var serviceResponse = await ReadAsync(
                HttpServiceResponse<T>.Empty,
                responseMessage);

            serviceResponse = serviceResponse
                .WithData(
                    await responseMessage.Content.ReadAsAsync<T>()
                );

            return serviceResponse
                ;
        }

        public static async Task<HttpServiceResponse> ExecuteAndReadAsync(
            this IServiceInOut<HttpClientServiceRequest, HttpResponseMessage> client,
            HttpClientServiceRequest request
            )
        {
            var responseMessage = await client.ExecuteAsync(request);
            var serviceResponse = await ReadAsync(
                HttpServiceResponse.Empty,
                responseMessage);

            return serviceResponse;
        }

        static async Task<T> ReadAsync<T>(
            T serviceResponse,
            HttpResponseMessage responseMessage)
            where T : IHttpServiceResponse
        {
            serviceResponse = HttpServiceResponseExtensions.WithStatusCode(serviceResponse, responseMessage.StatusCode);

            if (responseMessage.IsSuccessStatusCode)
                return serviceResponse;

            var errors = await responseMessage.Content
                .ReadAsAsync<IEnumerable<string>>();

            return ServiceResponseExtensions.WithErrors(serviceResponse, errors);
        }
    }
}