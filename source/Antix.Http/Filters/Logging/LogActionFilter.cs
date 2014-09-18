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
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private readonly Log.Delegate _log;

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
            var entry = new ActionLogEntry(actionContext);


            _log.Debug(m => m("Action {0}",
                JsonConvert.SerializeObject(entry, _jsonSerializerSettings)
                ));

            try
            {
                HttpResponseMessage result = await continuation();
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