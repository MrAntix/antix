using System.Collections.Generic;
using System.Net;
using Antix.Services.Models;

namespace Antix.Http.Services.Models
{
    public interface IHttpServiceResponse :
        IServiceResponse
    {
        HttpStatusCode? StatusCode { get; }
        IReadOnlyDictionary<string, string> Headers { get; }
    }

    public interface IHttpServiceResponse<out T> :
        IHttpServiceResponse
        where T : IHttpServiceResponse<T>
    {
        T WithStatusCode(HttpStatusCode statusCode);
        T WithHeaders(IReadOnlyDictionary<string, string> headers);
    }

    public interface IHttpServiceResponse<out T, TData> :
        IHttpServiceResponse<T>, IServiceResponseHasData
        where T : IHttpServiceResponse<T>
    {
        new TData Data { get; }

        T WithData(TData data);
    }
}