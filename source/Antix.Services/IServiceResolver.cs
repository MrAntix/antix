namespace Antix.Services
{
    public interface IServiceResolver<T>
    {
        T Resolve();
        void Release(T service);
    }
}