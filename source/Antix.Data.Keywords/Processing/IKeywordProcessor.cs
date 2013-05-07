using System.Collections.Generic;

namespace Antix.Data.Keywords.Processing
{
    public interface IKeywordProcessor
    {
        IEnumerable<string> Process(object value);
    }
}