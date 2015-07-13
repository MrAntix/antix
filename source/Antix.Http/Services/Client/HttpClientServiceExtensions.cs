using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Antix.Http.Services.Models;
using Antix.Services.Models;

namespace Antix.Http.Services.Client
{
    public static class HttpClientServiceExtensions
    {
        public static async Task<HttpServiceResponse<T>> ReadAsync<T>(
            this HttpResponseMessage responseMessage)
        {
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

        public static async Task<HttpServiceResponse> ReadAsync(
            this HttpResponseMessage responseMessage)
        {
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
            serviceResponse = serviceResponse
                .WithStatusCode(responseMessage.StatusCode);

            if (responseMessage.IsSuccessStatusCode)
                return serviceResponse;

            var errors = await responseMessage.Content
                .ReadAsAsync<IEnumerable<string>>();

            return serviceResponse
                .WithErrors(errors);
        }
    }
}