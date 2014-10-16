using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Antix.Http.Filters.Response
{
    namespace Antix.Http.Filters.Response
    {
        public class ResponseGlobalFilter :
            IActionFilter
        {
            public bool AllowMultiple
            {
                get { return false; }
            }

            public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
                HttpActionContext actionContext,
                CancellationToken cancellationToken,
                Func<Task<HttpResponseMessage>> continuation)
            {
                var result = await continuation();
                if (!typeof (Services.Models.Response)
                    .IsAssignableFrom(
                        actionContext.ActionDescriptor.ReturnType)) return result;

                var objectContent = result.Content as ObjectContent;
                if (objectContent == null) return result;

                var response = objectContent.Value as Services.Models.Response;

                if (response != null && response.Errors.Any())
                    result.StatusCode = HttpStatusCode.BadRequest;

                return result;
            }
        }
    }
}