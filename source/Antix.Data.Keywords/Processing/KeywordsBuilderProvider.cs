namespace Antix.Data.Keywords.Processing
{
    public class KeywordsBuilderProvider : IKeywordsBuilderProvider
    {
        readonly IKeywordProcessor _processor;

        public KeywordsBuilderProvider(
            IKeywordProcessor processor)
        {
            _processor = processor;
        }

        public IKeywordsBuilder<T> Create<T>()
        {
            return new KeywordsBuilder<T>(_processor);
        }
    }
}