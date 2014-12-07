using System.Collections.Generic;

namespace Antix.Services.Models
{
    public interface IServiceResponse
    {
        IEnumerable<string> Errors { get; }
    }

    public interface IServiceResponseWithData :
        IServiceResponse
    {
        object Data { get; }
    }
    
    public interface IServiceResponse<out T> :
        IServiceResponseWithData
    {
        new T Data { get; }
    }
}