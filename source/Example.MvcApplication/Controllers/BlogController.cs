using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Example.MvcApplication.Models;
using Example.MvcApplication.Services;

namespace Example.MvcApplication.Controllers
{
    public class BlogController : Controller
    {
        readonly IBlogReadService _readService;

        public BlogController(IBlogReadService readService)
        {
            _readService = readService;
        }

        public async Task<ActionResult> Index(
            string search, int index = 0, int pageSize = 20)
        {
            var blogEntries = await _readService.SearchAsync(
                search, false, index, pageSize,
                e => new BlogEntryInfoModel
                    {
                        Identifier = e.Identifier,
                        Title = e.Title,
                        PublishedOn = new DateTimeModel { Value = e.PublishedOn },
                        Summary = e.Summary
                    });

            return View(
                new BlogIndexModel
                    {
                        BlogEntries = blogEntries
                    });
        }

        public ActionResult Read(string id)
        {
            var model = _readService
                .TryGetByIdentifier(id, e => new BlogEntryModel
                    {
                        Title = e.Title,
                        Author = e.Author,
                        PublishedOn = new DateTimeModel { Value = e.PublishedOn },
                        Summary = e.Summary,
                        Content = e.Content,
                        Tags = e.Tags.Select(et => et.Title)
                    });

            return View(model);
        }
    }
}