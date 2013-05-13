using Antix.Data.Keywords.Entities;
using Antix.Data.Keywords.Processing;

namespace Antix.Data.Keywords
{
    public interface IKeywordsIndexer
    {
        IKeywordsBuilder<T> Entity<T>()
            where T : IIndexedEntity;

        string[] GetKeywords(object entity);
    }
}