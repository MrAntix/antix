using Antix.Data.Keywords.EF.Entities;

namespace Antix.Data.Keywords.EF
{
    public class EFEntityState
    {
        public IndexedEntity Entity { get; set; }
        public bool IsDeleted { get; set; }
    }
}