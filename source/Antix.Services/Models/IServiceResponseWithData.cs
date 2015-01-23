namespace Antix.Services.Models
{
    public interface IServiceResponseWithData :
        IServiceResponse
    {
        object Data { get; }
    }
}