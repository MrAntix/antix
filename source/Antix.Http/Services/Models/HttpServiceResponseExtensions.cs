using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using Antix.Services.Models;

namespace Antix.Http.Services.Models
{
    public static class HttpServiceResponseExtensions
    {
        public static ObjectContent<TData> AsJsonContent<T, TData>(
            T response)
            where T : IServiceResponse<T, TData>
        {
            return new ObjectContent<TData>(
                response.Data,
                new JsonMediaTypeFormatter());
        }

        public static HttpServiceResponse<TData> WithData<T, TData>(
            this IHttpServiceResponse<T> model, TData data)
            where T : IHttpServiceResponse<T>
        {
            return new HttpServiceResponse<TData>(
                model.StatusCode, data, model.Headers, model.Errors);
        }

        public static HttpServiceResponse AsHttp<T>(
            this IServiceResponse<T> model,
            HttpStatusCode statusCode,
            IReadOnlyDictionary<string, string> headers = null)
            where T : IServiceResponse<T>
        {
            return new HttpServiceResponse(statusCode, headers, model.Errors);
        }

        public static HttpServiceResponse<TData> AsHttp<T, TData>(
            this IServiceResponse<T, TData> model,
            HttpStatusCode statusCode,
            IReadOnlyDictionary<string, string> headers = null)
            where T : IServiceResponse<T, TData>
        {
            return new HttpServiceResponse<TData>(
                statusCode, 
                model.Data, 
                headers, 
                model.Errors);
        }

        public static HttpServiceResponse AsHttpCreated<T>(
            this IServiceResponse<T> model,
            string location)
            where T : IServiceResponse<T>
        {
            return new HttpServiceResponse(
                HttpStatusCode.Created,
                new Dictionary<string, string>
                {
                    {"location", location}
                },
                model.Errors);
        }

        public static HttpServiceResponse<TData> AsHttpCreated<T, TData>(
            this IServiceResponse<T, TData> model,
            string location)
            where T : IServiceResponse<T, TData>
        {
            return new HttpServiceResponse<TData>(
                HttpStatusCode.Created,
                model.Data,
                new Dictionary<string, string>
                {
                    {"location", location}
                },
                model.Errors
                );
        }
    }
}