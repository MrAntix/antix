namespace Antix.Data.Keywords.Processing
{
    public interface IKeywordBuilderProvider
    {
        IKeywordsBuilder<T> Create<T>();
    }
}