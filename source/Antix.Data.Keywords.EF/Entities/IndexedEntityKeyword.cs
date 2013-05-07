using Antix.Data.Keywords.Entities;

namespace Antix.Data.Keywords.EF.Entities
{
    public class IndexedEntityKeyword : IIndexedEntityKeyword
    {
        public int Id { get; set; }
        public Keyword Keyword { get; set; }
        public int Frequency { get; set; }

        IKeyword IIndexedEntityKeyword.Keyword
        {
            get { return Keyword; }
        }
    }
}