using System.Threading.Tasks;

namespace Antix.Services
{
    public interface IServiceNInOut : IService
    {
        Task ExecuteAsync();
    }
}