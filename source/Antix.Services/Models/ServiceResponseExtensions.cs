using System;
using System.Collections.Generic;
using System.Linq;

namespace Antix.Services.Models
{
    public static class ServiceResponseExtensions
    {
        public static ServiceResponse<TData> WithData<TData>(
            this IServiceResponse model, TData data)
        {
            return (ServiceResponse<TData>) model.WithData(data);
        }

        public static T WithErrors<T>(
            this T model, IEnumerable<string> errors)
            where T : IServiceResponse
        {
            return (T) model.WithErrors(errors);
        }

        public static T WithErrors<T>(
            this T model, params object[] errors)
            where T : IServiceResponse
        {
            var errorStrings = new List<string>();
            foreach (var error in errors)
            {
                if (error == null) continue;

                var errorString = error as string;
                if (errorString != null)
                    errorStrings.Add(errorString);

                else
                {
                    var enumerableStrings = error as IEnumerable<string>;
                    if (enumerableStrings != null)
                        errorStrings.AddRange(enumerableStrings);

                    else
                        errorStrings.Add(error.ToString());
                }
            }

            return (T) model.WithErrors(errorStrings.AsEnumerable());
        }

        public static ServiceResponse<TDataTo> Map<TData, TDataTo>(
            this IServiceResponse<TData> model,
            Func<TData, TDataTo> mapper)
        {
            return (ServiceResponse<TDataTo>) model.WithData(mapper(model.Data));
        }
    }
}