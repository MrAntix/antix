using System;
using System.Linq;

namespace Antix.Services.Models
{
    public static class IServiceResponseExtensions
    {
        public static T WithErrors<T>(
            this T serviceResponse,
            params string[] errors)
            where T : IServiceResponse
        {
            return (T) serviceResponse.Create(errors);
        }

        public static T WithErrors<T>(
            this T serviceResponse,
            params Enum[] errors)
            where T : IServiceResponse
        {
            return (T) serviceResponse.Create(
                errors
                    .Select(e => e.ToString("G"))
                    .ToArray());
        }

        public static IServiceResponse<TData> WithData<TData>(
            this IServiceResponse serviceResponse,
            TData data)
        {
            return serviceResponse.Create(data, serviceResponse.Errors);
        }

        public static IServiceResponse<TModel> Map<TData, TModel>(
            this IServiceResponse<TData> serviceResponse,
            Func<TData, TModel> map)
        {
            return serviceResponse.WithData(
                map(serviceResponse.Data)
                );
        }
    }
}