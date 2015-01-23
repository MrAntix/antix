using System.Threading.Tasks;

namespace Antix.Services
{
    public interface IServiceIn<in TIn> : IService
    {
        Task ExecuteAsync(TIn model);
    }
}