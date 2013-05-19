namespace Example.MvcApplication.Models
{
    public class BlogEntryInfoModel
    {
        public string Title { get; set; }
        public string Summary { get; set; }

        public DateTimeModel PublishedOn { get; set; }
    }
}