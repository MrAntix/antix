using System.Collections.Generic;
using System.Web.Http;

namespace Example.MvcApplication.Controllers.Api
{
    public class BlogController : ApiController
    {
        // GET api/blog
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
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