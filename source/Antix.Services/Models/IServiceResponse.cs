using System;
using System.Collections.Generic;

namespace Antix.Services.Models
{
    public interface IServiceResponse
    {
        string[] Errors { get; }
    }

    public interface IServiceResponse<out T> :
        IServiceResponse
        where T : IServiceResponse<T>
    {
        T WithErrors(IEnumerable<string> errors);
    }

    public interface IServiceResponse<out T, out TData> :
        IServiceResponse<T>, IServiceResponseHasData
        where T : IServiceResponse<T>
    {
        new TData Data { get; }
    }
}