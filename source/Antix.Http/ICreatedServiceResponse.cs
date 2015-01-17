using Antix.Services.Models;

namespace Antix.Http
{
    public interface ICreatedServiceResponse :
        IServiceResponseWithData
    {
        string RouteName { get; }
    }
}