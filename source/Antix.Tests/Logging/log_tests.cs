using System;
using Antix.Logging;
using Xunit;

namespace Antix.Tests.Logging
{
    public class log_tests
    {
        [Fact]
        public void can_write()
        {
            var actual = new Actual();

            GetDelegate(actual)
                .Error(m => m(EXPECTED_FORMAT, ExpectedArgs));

            Assert.Equal(EXPECTED_LEVEL, actual.Level);
            Assert.Equal(EXPECTED_FORMAT, actual.Format);
            Assert.Equal(ExpectedArgs, actual.Args);
        }

        [Fact]
        public void can_write_exception()
        {
            var actual = new Actual();

            GetDelegate(actual)
                .Error(m => m(ExpectedException, EXPECTED_FORMAT, ExpectedArgs));

            Assert.Equal(EXPECTED_LEVEL, actual.Level);
            Assert.Equal(EXPECTED_FORMAT, actual.Format);
            Assert.Equal(ExpectedArgs, actual.Args);
            Assert.Equal(ExpectedException, actual.Exception);
        }

        const Log.Level EXPECTED_LEVEL = Log.Level.Error;
        const string EXPECTED_FORMAT = "{0}";
        static readonly Exception ExpectedException = new Exception();
        static readonly object[] ExpectedArgs = {1};

        class Actual
        {
            public Log.Level Level { get; set; }
            public Exception Exception { get; set; }
            public string Format { get; set; }
            public object[] Args { get; set; }
        }

        static Log.Delegate GetDelegate(Actual actual)
        {
            return  (l, id, tags) => (ex, f, a) =>
            {
                actual.Level = l;
                actual.Exception = ex;
                actual.Format = f;
                actual.Args = a;
            };
        }
    }
}