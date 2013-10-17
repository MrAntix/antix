using System.Threading.Tasks;
using Example.MvcApplication.Services.Models;

namespace Example.MvcApplication.Services
{
    public interface IBlogReadService
    {
        Task<BlogSearch> SearchAsync(BlogSearch criteria);
        BlogEntry TryGetByIdentifier(string identifier);
    }
}