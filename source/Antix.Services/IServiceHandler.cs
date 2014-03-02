namespace Antix.Services
{
    public interface IServiceHandler
    {
        void Handle<TIn>(TIn model);
        TOut Handle<TIn, TOut>(TIn model);
        TOut Handle<TOut>();
    }
}