using System.Net.Http;
using System.Net.Http.Formatting;
using Antix.Services.Models;

namespace Antix.Http.Services
{
    public static class ServiceModelExtensions
    {
        public static ObjectContent<T> AsJsonContent<T>(
            IServiceResponse<T> response)
        {
            return new ObjectContent<T>(
                response.Data,
                new JsonMediaTypeFormatter());
        }
    }
}