using System.Threading.Tasks;
using System.Web.Http;
using Example.MvcApplication.Services;
using Example.MvcApplication.Services.Models;

namespace Example.MvcApplication.Api.Controllers
{
    public class BlogController : ApiController
    {
        readonly IBlogReadService _readService;

        public BlogController(IBlogReadService readService)
        {
            _readService = readService;
        }

        public async Task<BlogSearch> Get(
            [FromBody] BlogSearch model)
        {
            var results = await _readService.SearchAsync(model);


            return model;
        }

        // GET api/blog/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/blog
        public void Post([FromBody] string value)
        {
        }

        // PUT api/blog/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/blog/5
        public void Delete(int id)
        {
        }
    }
}