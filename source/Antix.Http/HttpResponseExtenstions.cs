using Antix.Services.Models;

namespace Antix.Http
{
    public static class HttpResponseExtenstions
    {
        public static CreatedServiceResponse<TData> Created<TData>(
            this IServiceResponse serviceResponse,
            string routeName,
            TData data)
        {
            return new CreatedServiceResponse<TData>(
                routeName,
                data,
                serviceResponse.Errors);
        }
    }
}