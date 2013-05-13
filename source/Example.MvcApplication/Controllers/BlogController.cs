using System.Linq;
using System.Web.Mvc;
using Antix.Data.Keywords.EF;
using Example.MvcApplication.App_Start.Data;
using Example.MvcApplication.Models;

namespace Example.MvcApplication.Controllers
{
    public class BlogController : Controller
    {
        readonly DataContext _dataContext;

        public BlogController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ActionResult Index(
            string search, int index = 0, int pageSize = 20)
        {
            var blogEntries = _dataContext.BlogEntries
                                          .Matches(search)
                                          .OrderBy(e => e.Title)
                                          .Skip(index*pageSize)
                                          .Take(pageSize);

            return View(
                new BlogIndexModel
                    {
                        BlogEntries = blogEntries
                    });
        }
    }
}