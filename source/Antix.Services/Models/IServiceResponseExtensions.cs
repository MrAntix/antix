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

        public static IServiceResponse<TData> WithData<TData>(
            this IServiceResponse serviceResponse,
            TData data)
        {
            return serviceResponse.Create(data, serviceResponse.Errors);
        }
    }
}