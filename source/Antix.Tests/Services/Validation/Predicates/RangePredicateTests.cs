using System;
using Antix.Services.Validation.Predicates;
using Xunit;

namespace Antix.Tests.Services.Validation.Predicates
{
    public class RangePredicateTests
    {
        [Fact]
        public void date_in_range()
        {
            var service = new DateTimeOffsetRangePredicate(
                DateTimeOffset.UtcNow.AddYears(-100), DateTimeOffset.UtcNow);

            Assert.True(service.Is(DateTimeOffset.UtcNow.AddYears(-1)));
        }
    }
}