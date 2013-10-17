namespace Example.MvcApplication.Services.Models
{
    public class SearchSettings
    {
        readonly int _pageSize;

        public SearchSettings(int pageSize)
        {
            _pageSize = pageSize;
        }

        public int PageSize { get { return _pageSize; } }

        public static readonly SearchSettings Default
            = new SearchSettings(20);
    }
}