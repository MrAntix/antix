using System.Collections.Generic;

namespace Antix.Services.Models
{
    public interface IServiceResponse
    {
        string[] Errors { get; }

        IServiceResponse WithErrors(IEnumerable<string> errors);
        IServiceResponse<TData> WithData<TData>(TData data);
    }

    public interface IServiceResponse<out TData> :
        IServiceResponseHasData
    {
        new TData Data { get; }
    }
}