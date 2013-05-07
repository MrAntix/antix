namespace Antix.Data.Keywords.Entities
{
    public interface IEntityKeyword
    {
        IKeyword Keyword { get; }
        int Frequency { get; }
    }
}