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

        public static ServiceResponse<TTo> Map<T, TFrom, TTo>(
            this T model, Func<TFrom, TTo> mapper)
            where T : IServiceResponse<T, TFrom>
        {
            return model.WithData(mapper(model.Data));
        }
    }
}