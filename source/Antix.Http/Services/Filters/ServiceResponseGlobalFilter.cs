using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Routing;
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

            var processResponse =
                Process(objectContent.Value, actionContext.RequestContext.Url);

            if (processResponse == null) return result;

            if (processResponse.StatusCode.HasValue)
                result.StatusCode = processResponse.StatusCode.Value;

            result.Content = GetObjectContent(
                processResponse.Content,
                objectContent.Formatter);

            if (processResponse.Headers != null)
                foreach (var header in processResponse.Headers)
                {
                    result.Headers.Add(header.Key, header.Value);
                }

            return result;
        }

        static HttpContent GetObjectContent(
            object value, MediaTypeFormatter mediaTypeFormatter)
        {
            return new ObjectContent(
                value == null ? typeof (object) : value.GetType(),
                value,
                mediaTypeFormatter);
        }

        public static ProcessResponse Process(
            object responseValue,
            UrlHelper url)
        {
            var serviceResponse
                = responseValue as IServiceResponse;
            if (serviceResponse == null) return null;

            return
                ProcessErrors(serviceResponse)
                ?? ProcessCreated(serviceResponse as ICreatedServiceResponse, url)
                ?? ProcessContent(serviceResponse as IServiceResponseWithData);
        }

        public static ProcessResponse ProcessErrors(
            IServiceResponse serviceResponse)
        {
            if (serviceResponse.Errors.Any())
            {
                return new ProcessResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = serviceResponse.Errors
                };
            }

            return null;
        }

        public static ProcessResponse ProcessCreated(
            ICreatedServiceResponse serviceResponse,
            UrlHelper url)
        {
            if (serviceResponse != null)
            {
                return new ProcessResponse
                {
                    StatusCode = HttpStatusCode.Created,
                    Content = serviceResponse.Data,
                    Headers = new Dictionary<string, string>
                    {
                        {"Location", url.Route(serviceResponse.RouteName, serviceResponse.Data)}
                    }
                };
            }

            return null;
        }

        public static ProcessResponse ProcessContent(
            IServiceResponseWithData serviceResponse)
        {
            if (serviceResponse != null)
            {
                return new ProcessResponse
                {
                    Content = serviceResponse.Data
                };
            }

            return null;
        }

        public class ProcessResponse
        {
            public HttpStatusCode? StatusCode { get; set; }
            public object Content { get; set; }
            public IDictionary<string, string> Headers { get; set; }
        }
    }
}