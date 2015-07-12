using System;
using System.Linq;

namespace Antix.Services.Models
{
    public static class ServiceResponseExtensions
    {
        public static ServiceResponse<TData> WithData<T, TData>(
            this IServiceResponse<T> model, TData data)
            where T : IServiceResponse<T>
        {
            return new ServiceResponse<TData>(data, model.Errors);
        }

        public static T WithErrors<T>(
            this T model, params object[] errors)
            where T : IServiceResponse<T>
        {
            return model.WithErrors(errors.Select(e => e.ToString()));
        }

        public static ServiceResponse<TDataTo> Map<T, TData, TDataTo>(
            this IServiceResponse<T, TData> model,
            Func<TData, TDataTo> mapper)
            where T : IServiceResponse<T, TData>
        {
            return new ServiceResponse<TDataTo>(mapper(model.Data), model.Errors);
        }
    }
}