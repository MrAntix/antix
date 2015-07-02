﻿using System.Net;
using Antix.Http.Services.Filters;
using Antix.Http.Services.Models;
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

            var processResponse =
                ServiceResponseGlobalFilter
                    .Process(serviceResponse);

            Assert.Equal(null, processResponse);
        }

        [Fact]
        public void when_response_has_errors_they_are_returned()
        {
            var errors = new[] {"Eek"};
            var serviceResponse = ServiceResponse
                .Empty
                .WithErrors(errors);

            var processResponse =
                ServiceResponseGlobalFilter
                    .Process(serviceResponse);

            Assert.Equal(HttpStatusCode.BadRequest, processResponse.StatusCode);
            Assert.Equal(errors, processResponse.Content);
        }

        [Fact]
        public void when_response_has_data_and_no_errors_data_is_returned()
        {
            var data = new {frog = true};
            var serviceResponse = ServiceResponse.Empty
                .WithData(data);

            var processResponse =
                ServiceResponseGlobalFilter
                    .Process(serviceResponse);

            Assert.Equal(null, processResponse.StatusCode);
            Assert.Equal(data, processResponse.Content);
        }

        [Fact]
        public void no_change_when_not_IServiceResponse()
        {
            var responseValue = new { frog = true };

            var processResponse =
                ServiceResponseGlobalFilter
                    .Process(responseValue);

            Assert.Equal(null, processResponse);
        }

        [Fact]
        public void using_created_http_response()
        {
            var data = new { frog = true };
            var url = "/somewhere";

            var serviceResponse = ServiceResponse.Empty
                .AsHttpCreated(url)
                .WithData(data);

            var processResponse =
                ServiceResponseGlobalFilter
                    .Process(serviceResponse);

            Assert.Equal(HttpStatusCode.Created, processResponse.StatusCode);
            Assert.Equal(data, processResponse.Content);
            Assert.Equal(url, processResponse.Headers["location"]);
        }

        [Fact]
        public void using_created_http_response_set_data_first()
        {
            var data = new { frog = true };
            var url = "/somewhere";

            var serviceResponse = ServiceResponse.Empty
                .WithData(data)
                .AsHttpCreated(url);

            var processResponse =
                ServiceResponseGlobalFilter
                    .Process(serviceResponse);

            Assert.Equal(HttpStatusCode.Created, processResponse.StatusCode);
            Assert.Equal(data, processResponse.Content);
            Assert.Equal(url, processResponse.Headers["location"]);
        }
    }
}