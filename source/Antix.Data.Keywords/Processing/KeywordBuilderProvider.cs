namespace Antix.Data.Keywords.Processing
{
    public class KeywordBuilderProvider : IKeywordBuilderProvider
    {
        readonly IKeywordProcessor _processor;

        public KeywordBuilderProvider(
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