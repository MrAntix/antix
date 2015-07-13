using System;
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
            where T : IServiceResponse<TData>
        {
            return new ObjectContent<TData>(
                response.Data,
                new JsonMediaTypeFormatter());
        }

        public static HttpServiceResponse AsHttp(
            this IServiceResponse model,
            HttpStatusCode? statusCode = null)
        {
            return new HttpServiceResponse(statusCode, null, model.Errors);
        }

        public static HttpServiceResponse<TData> AsHttp<TData>(
            this IServiceResponse<TData> model,
            HttpStatusCode? statusCode = null)
        {
            return new HttpServiceResponse<TData>(statusCode, model.Data, null, model.Errors);
        }

        public static T WithStatusCode<T>(
            this T model,
            HttpStatusCode statusCode)
            where T : IHttpServiceResponse
        {
            return (T) model.WithStatusCode(statusCode);
        }

        public static T WithHeaders<T>(
            this T model,
            IReadOnlyDictionary<string, string> headers)
            where T : IHttpServiceResponse
        {
            return (T) model.WithHeaders(headers);
        }

        public static HttpServiceResponse<TData> WithData<TData>(
            this IHttpServiceResponse model, TData data)
        {
            return (HttpServiceResponse<TData>) model.WithData(data);
        }

        public static HttpServiceResponse<TDataTo> Map<TData, TDataTo>(
            this IHttpServiceResponse<TData> model,
            Func<TData, TDataTo> mapper)
        {
            return (HttpServiceResponse<TDataTo>) model.WithData(mapper(model.Data));
        }

        public static HttpServiceResponse AsHttpCreated(
            this IServiceResponse model,
            string location)
        {
            return new HttpServiceResponse(
                HttpStatusCode.Created,
                new Dictionary<string, string>
                {
                    {"location", location}
                },
                model.Errors);
        }

        public static HttpServiceResponse<TData> AsHttpCreated<TData>(
            this IServiceResponse<TData> model,
            string location)
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