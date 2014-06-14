using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Antix.Logging;
using Antix.Services;

namespace Antix.Http.Filters.Logging
{
    public class LogActionFilter :
        FilterServiceBase<LogActionAttribute>,
        IActionFilter, IService
    {
        readonly Log.Delegate _log;

        public LogActionFilter(Log.Delegate log)
        {
            _log = log;
        }
        
        public Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext, 
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            _log.Debug(m => m("Action {0}", actionContext.ActionDescriptor.ActionName));

            return continuation();
        }
    }
}