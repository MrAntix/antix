using Antix.Data.Keywords.Processing;

namespace Antix.Data.Keywords
{
    public interface IKeywordsIndexer
    {
        IKeywordsBuilder<T> Entity<T>();
        string[] GetKeywords(object entity);
    }
}