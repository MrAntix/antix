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

            Assert.Equal(ExpectedStatusCode, serviceResponse.StatusCode);
        }

        [Fact]
        public void can_create_http_response_with_data()
        {
            var serviceResponse = ServiceResponse.Empty
                .AsHttp(ExpectedStatusCode)
                .WithData(ExpectedData);

            Assert.Equal(ExpectedStatusCode, serviceResponse.StatusCode);
            Assert.Equal(ExpectedData, serviceResponse.Data);
        }

        [Fact]
        public void can_create_http_response_with_data_map()
        {
            var serviceResponse = ServiceResponse.Empty
                .AsHttp(ExpectedStatusCode)
                .WithData("NOT ExpectedData")
                .Map(d => ExpectedData);

            Assert.Equal(ExpectedStatusCode, serviceResponse.StatusCode);
            Assert.Equal(ExpectedData, serviceResponse.Data);
        }

        [Fact]
        public void sets_not_found_when_null()
        {
            var serviceResponse = ServiceResponse.Empty
                .WithData(default(object))
                .AsHttp()
                .NotFoundWhenNull();

            Assert.Equal(HttpStatusCode.NotFound, serviceResponse.StatusCode);
        }

        [Fact]
        public void default_when_not_null()
        {
            var serviceResponse = ServiceResponse.Empty
                .WithData(ExpectedData)
                .AsHttp()
                .NotFoundWhenNull();

            Assert.Equal(default(HttpStatusCode), serviceResponse.StatusCode);
        }
    }
}