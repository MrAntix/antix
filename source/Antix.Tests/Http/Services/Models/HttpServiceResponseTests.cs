using System.Net;
using Antix.Http.Services.Models;
using Antix.Services.Models;
using Xunit;

namespace Antix.Tests.Http.Services.Models
{
    public class HttpServiceResponseTests
    {
        const HttpStatusCode ExpectedStatusCode = HttpStatusCode.Ambiguous;
        const string ExpectedData = "Some Data";

        [Fact]
        public void can_create_http_response()
        {
            var serviceResponse = ServiceResponse.Empty
                .AsHttp(ExpectedStatusCode);

            Assert.IsType<HttpServiceResponse>(serviceResponse);
            Assert.Equal(ExpectedStatusCode, serviceResponse.StatusCode);
        }
        [Fact]
        public void can_create_http_response_with_data()
        {
            var serviceResponse = ServiceResponse.Empty
                .WithData(ExpectedData)
                .AsHttp(ExpectedStatusCode);

            Assert.IsType<HttpServiceResponse<string>>(serviceResponse);
            Assert.Equal(ExpectedStatusCode, serviceResponse.StatusCode);
            Assert.Equal(ExpectedData, serviceResponse.Data);
        }
    }
}