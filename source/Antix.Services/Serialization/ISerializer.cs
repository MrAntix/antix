
namespace Antix.Services.Serialization
{
    public interface ISerializer :
        IService
    {
        string Serialize(object data);
    }
}