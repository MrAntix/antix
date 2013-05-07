using System.Collections.Generic;

namespace Antix.Data.Keywords.Entities
{
    public interface IIndexedEntity
    {
        IEnumerable<IEntityKeyword> Keywords { get; }
    }
}