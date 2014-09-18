using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Antix.Logging;
using Antix.Services;

using Newtonsoft.Json;

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

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            _log.Debug(m => m("Action {0}.{1} [{2}=>{3}] ({4}) ",
                actionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                actionContext.ActionDescriptor.ActionName,
                actionContext.Request.GetClientIpAddress(),
                actionContext.Request.Method,
                JsonConvert.SerializeObject(actionContext.ActionArguments)
                ));

            try
            {
                var result = await continuation();
                _log.Debug(m => m("{0} => {1}",
                    result.StatusCode,
                    result.Content == null
                        ? "[NULL]"
                        : result.Content.ReadAsStringAsync().Result)
                    );

                return result;
            }
            catch (Exception ex)
            {
                _log.Error(m => m(ex, "Action Error"));
                throw;
            }
        }
    }
}