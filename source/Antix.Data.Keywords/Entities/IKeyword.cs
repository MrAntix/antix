namespace Antix.Data.Keywords.Entities
{
    public interface IKeyword
    {
        string Value { get; set; }
        int Frequency { get; set; }
    }
}