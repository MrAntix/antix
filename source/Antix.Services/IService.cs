namespace Antix.Services
{
    public interface IService
    {
    }

    public interface IServiceIn<in TIn> : IService
    {
        void Execute(TIn model);
    }

    public interface IServiceInOut<in TIn, out TOut> : IService
    {
        TOut Execute(TIn model);
    }

    public interface IServiceOut<out TOut> : IService
    {
        TOut Execute();
    }
}