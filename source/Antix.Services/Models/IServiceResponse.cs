using System.Collections.Generic;

namespace Antix.Services.Models
{
    public interface IServiceResponse
    {
        string[] Errors { get; }

        IServiceResponse Copy(IEnumerable<string> errors);
        IServiceResponse<TData> Copy<TData>(TData data);
    }

    public interface IServiceResponse<TData> :
        IServiceResponse, IServiceResponseHasData
    {
        new TData Data { get; }
    }
}