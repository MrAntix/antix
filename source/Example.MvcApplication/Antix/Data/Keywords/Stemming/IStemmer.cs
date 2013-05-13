namespace Antix.Data.Keywords.Stemming
{
    /// <summary>
    ///     http://stemmersnet.codeplex.com/SourceControl/changeset/view/5fdbab2d6294826d9c939981442b195f42d915cf#StemmersNet/IStemmer.cs
    /// </summary>
    public interface IStemmer
    {
        string Stem(string s);
    }
}