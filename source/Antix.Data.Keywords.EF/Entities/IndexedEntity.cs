using System.Collections.Generic;
using System.Collections.ObjectModel;
using Antix.Data.Keywords.Entities;

namespace Antix.Data.Keywords.EF.Entities
{
    public abstract class IndexedEntity : IIndexedEntity
    {
        protected IndexedEntity()
        {
            Keywords = new Collection<IndexedEntityKeyword>();
        }

        public ICollection<IndexedEntityKeyword> Keywords { get; set; }

        IEnumerable<IIndexedEntityKeyword> IIndexedEntity.Keywords
        {
            get { return Keywords; }
        }
    }
}