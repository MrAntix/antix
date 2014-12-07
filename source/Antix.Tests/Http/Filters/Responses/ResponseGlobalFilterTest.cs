using System;
using System.Net;
using Antix.Http.Services.Filters;
using Antix.Services.Models;
using Xunit;

namespace Antix.Tests.Http.Filters.Responses
{
    public class ResponseGlobalFilterTest
    {
        [Fact]
        public void when_response_is_empty_returns_null()
        {
            var serviceResponse = ServiceResponse.Empty;

            var processedStatus = default(HttpStatusCode?);
            var processedValue = new Object(); // not null

            ServiceResponseGlobalFilter
                .Process(
                    serviceResponse,
                    status => processedStatus = status,
                    value => processedValue = value);

            Assert.Equal(null, processedStatus);
            Assert.Equal(null, processedValue);
        }

        [Fact]
        public void when_response_has_errors_they_are_returned()
        {
            var errors = new[] {"Eek"};
            var serviceResponse = ServiceResponse
                .Empty
                .WithErrors(errors);

            var processedStatus = default(HttpStatusCode?);
            var processedValue = default(object);

            ServiceResponseGlobalFilter
                .Process(
                    serviceResponse,
                    status => processedStatus = status,
                    value => processedValue = value);

            Assert.Equal(HttpStatusCode.BadRequest, processedStatus);
            Assert.Equal(errors, processedValue);
        }

        [Fact]
        public void when_response_has_data_and_no_errors_data_is_returned()
        {
            var data = new {frog = true};
            var serviceResponse = ServiceResponse.Empty
                .WithData(data);

            var processedStatus = default(HttpStatusCode?);
            var processedValue = default(object);

            ServiceResponseGlobalFilter
                .Process(
                    serviceResponse,
                    status => processedStatus = status,
                    value => processedValue = value);

            Assert.Equal(null, processedStatus);
            Assert.Equal(data, processedValue);
        }

        [Fact]
        public void no_change_when_not_IServiceResponse()
        {
            var responseValue = new { frog = true };

            var processedStatus = default (HttpStatusCode?);
            var processedValue = default(object);

            ServiceResponseGlobalFilter
                .Process(
                    responseValue,
                    status => processedStatus = status,
                    value => processedValue = value);

            Assert.Equal(null, processedStatus);
            Assert.Equal(null, processedValue);
        }
    }
}