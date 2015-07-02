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
using Antix.Http.Services.Models;
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
                Process(objectContent.Value);

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
            object responseValue)
        {
            var serviceResponse
                = responseValue as IServiceResponse;
            if (serviceResponse == null) return null;

            var processResponse = 
                ProcessErrors(serviceResponse)
                ?? ProcessHttp(serviceResponse as IHttpServiceResponse)
                ?? ProcessContent(serviceResponse as IServiceResponseHasData);

            return processResponse;
        }

        public static ProcessResponse ProcessErrors(
            IServiceResponse serviceResponse)
        {
            if (!serviceResponse.Errors.Any()) return null;

            return new ProcessResponse
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = serviceResponse.Errors
            };
        }

        public static ProcessResponse ProcessHttp(
            IHttpServiceResponse httpResponse)
        {
            if (httpResponse == null) return null;

            var processResponse =
                new ProcessResponse
                {
                    StatusCode = httpResponse.StatusCode,
                    Headers = httpResponse.Headers
                };

            var withData = httpResponse as IServiceResponseHasData;
            if (withData != null)
                processResponse.Content = withData.Data;

            return processResponse;
        }

        public static ProcessResponse ProcessContent(
            IServiceResponseHasData serviceResponse)
        {
            if (serviceResponse == null) return null;

            return new ProcessResponse
            {
                Content = serviceResponse.Data
            };
        }

        public class ProcessResponse
        {
            public HttpStatusCode? StatusCode { get; set; }
            public object Content { get; set; }
            public IReadOnlyDictionary<string, string> Headers { get; set; }
        }
    }
}