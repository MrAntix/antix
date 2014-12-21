using System.Linq;
using Antix.Services.Models;
using Xunit;

namespace Antix.Tests.Services.Models
{
    public class ServiceResponseTest
    {
        [Fact]
        public void can_create_with_errors_array()
        {
            var result = ServiceResponse.Empty
                .WithErrors(new[] {"An Error", "An Other Error"});

            Assert.Equal(2, result.Errors.Count());
        }

        [Fact]
        public void can_create_with_errors_params()
        {
            var result = ServiceResponse.Empty
                .WithErrors("An Error", "An Other Error");

            Assert.Equal(2, result.Errors.Count());
        }

        [Fact]
        public void can_create_with_data()
        {
            var data = new object();
            var result = ServiceResponse.Empty
                .WithData(data);

            Assert.Equal(data, result.Data);
        }

        [Fact]
        public void can_create_with_data_and_errors()
        {
            var data = new object();
            var errors = new[] { "An Error", "An Other Error" };
            var result = ServiceResponse.Empty
                .WithData(data)
                .WithErrors(errors);

            Assert.Equal(data, result.Data);
            Assert.Equal(errors, result.Errors);
        }
    }
}