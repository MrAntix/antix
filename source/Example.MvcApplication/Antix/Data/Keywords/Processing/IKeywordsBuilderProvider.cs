namespace Antix.Data.Keywords.Processing
{
    public interface IKeywordsBuilderProvider
    {
        IKeywordsBuilder<T> Create<T>();
    }
}