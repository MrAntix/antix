using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Antix.Services.Models;

namespace Antix.Http.Services.Filters
{
    public class ServiceResponseGlobalFilter :
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

            var objectContent = result.Content as ObjectContent;
            if (objectContent == null) return result;

            Process(
                objectContent.Value,
                status => result.StatusCode = status,
                value => result.Content = GetObjectContent(value, objectContent.Formatter)
                );

            return result;
        }

        private HttpContent GetObjectContent(
            object value, System.Net.Http.Formatting.MediaTypeFormatter mediaTypeFormatter)
        {
            return new ObjectContent(
                value == null ? typeof(object) : value.GetType(),
                value,
                mediaTypeFormatter);
        }

        public static void Process(
            object responseValue,
            Action<HttpStatusCode> setStatusCode,
            Action<object> setValue)
        {
            var serviceResponse
                = responseValue as IServiceResponse;
            if (serviceResponse == null) return;

            if (serviceResponse.Errors.Any())
            {
                setStatusCode(HttpStatusCode.BadRequest);
                setValue(serviceResponse.Errors);

                return;
            }

            var serviceResponseWithData
                = serviceResponse as IServiceResponseWithData;
            if (serviceResponseWithData != null)
            {
                setValue(serviceResponseWithData.Data);
            }
            else
            {
                setValue(null);
            }
        }
    }
}