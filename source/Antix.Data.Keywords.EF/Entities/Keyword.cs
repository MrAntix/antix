using Antix.Data.Keywords.Entities;

namespace Antix.Data.Keywords.EF.Entities
{
    public class Keyword : IKeyword
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int Frequency { get; set; }
    }
}