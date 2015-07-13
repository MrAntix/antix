using System;
using System.Linq;

namespace Antix.Services.Models
{
    public static class ServiceResponseExtensions
    {
        public static ServiceResponse<TData> WithData<TData>(
            this IServiceResponse model, TData data)
        {
            return (ServiceResponse<TData>) model.Copy(data);
        }

        public static T WithErrors<T>(
            this T model, params object[] errors)
            where T : IServiceResponse
        {
            return (T) model.Copy(errors.Select(e => e.ToString()));
        }

        public static ServiceResponse<TDataTo> Map<TData, TDataTo>(
            this IServiceResponse<TData> model,
            Func<TData, TDataTo> mapper)
        {
            return (ServiceResponse<TDataTo>) model.Copy(mapper(model.Data));
        }
    }
}