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
        readonly JsonSerializerSettings _jsonSerializerSettings;
        readonly Log.Delegate _log;
        const string LOG_TAG = "HTTP Action Filter";

        public LogActionFilter(Log.Delegate log)
        {
            _log = log;
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            var requestEntry = new ActionRequestEntry(actionContext);

            var logEntry = Log.Entry(
                m => m("Action Request: {0}",
                    JsonConvert.SerializeObject(requestEntry, _jsonSerializerSettings)
                    ));

            try
            {
                var result = await continuation();
                var responseEntry = new ActionResponseEntry(result);

                _log.Debug(
                    logEntry.Append(m => m("Action Response: {0}",
                        JsonConvert.SerializeObject(responseEntry, _jsonSerializerSettings)
                        )),
                    LOG_TAG);

                return result;
            }
            catch (Exception ex)
            {
                _log.Error(
                    logEntry.Append(m => m("Action Error")),
                    ex, LOG_TAG);

                throw;
            }
        }
    }
}