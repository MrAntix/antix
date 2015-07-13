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

        IHttpServiceResponse WithStatusCode(HttpStatusCode statusCode);
        IHttpServiceResponse WithHeaders(IReadOnlyDictionary<string, string> headers);
    }

    public interface IHttpServiceResponse<out TData> :
        IHttpServiceResponse, IServiceResponse<TData>
    {
    }
}