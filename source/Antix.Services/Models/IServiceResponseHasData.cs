namespace Antix.Services.Models
{
    public interface IServiceResponseHasData :
        IServiceResponse
    {
        object Data { get; }
    }
}