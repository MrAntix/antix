using System.Collections.Generic;

namespace Antix.Services.Models
{
    public interface IServiceResponse
    {
        IEnumerable<string> Errors { get; }

        IServiceResponse Create(
            IEnumerable<string> errors);

        IServiceResponse<TData> Create<TData>(
            TData data,
            IEnumerable<string> errors);
    }

    public interface IServiceResponse<out TData> :
        IServiceResponseWithData
    {
        new TData Data { get; }
    }
}