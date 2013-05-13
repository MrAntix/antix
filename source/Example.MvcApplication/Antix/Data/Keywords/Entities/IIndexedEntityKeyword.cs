namespace Antix.Data.Keywords.Entities
{
    public interface IIndexedEntityKeyword
    {
        IKeyword Keyword { get; }
        int Frequency { get; }
    }
}