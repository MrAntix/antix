using System;
using Antix.Logging;
using Antix.Testing;
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
        public void can_write_event()
        {
            var actual = new Actual();
            var message = string.Format(EXPECTED_FORMAT, ExpectedArgs);

            GetDelegate(actual)
                .Write(new Log.Event(
                    Guid.NewGuid(),
                    EXPECTED_LEVEL, ExpectedException,
                    message,
                    ExpectedTags));

            Assert.Equal(EXPECTED_LEVEL, actual.Level);
            Assert.Equal(ExpectedException, actual.Exception);
            Assert.Equal(message, actual.Format);
            Assert.Equal(ExpectedTags, actual.Tags);
        }

        [Fact]
        public void can_write_exception()
        {
            var actual = new Actual();

            GetDelegate(actual)
                .Error(m => m(EXPECTED_FORMAT, ExpectedArgs), ExpectedException);

            Assert.Equal(EXPECTED_LEVEL, actual.Level);
            Assert.Equal(EXPECTED_FORMAT, actual.Format);
            Assert.Equal(ExpectedArgs, actual.Args);
            Assert.Equal(ExpectedException, actual.Exception);
        }

        [Fact]
        public void can_build_log()
        {
            var actual = new Actual();

            var logAction = Log
                .Entry(m => m(EXPECTED_FORMAT, ExpectedArgs))
                .Append(m => m(EXPECTED_FORMAT, ExpectedArgs));

            GetDelegate(actual)
                .Error(logAction);

            Assert.Equal(EXPECTED_LEVEL, actual.Level);
            Assert.Equal(EXPECTED_BUILD_FORMAT, actual.Format);
            Assert.Equal(ExpectedBuildArgs, actual.Args);
        }

        [Fact]
        public void thrash_log()
        {
            Log.Delegate log = (l, id, ex, tags) => (f, a) => { var _ = string.Format(f, a); };

            var originalByteCount = GC.GetTotalMemory(true);

            var benchmark = Benchmark
                .Run(() => log.Debug(m => m("Testing")), 100, 10000, 1000000);

            foreach (var result in benchmark.Results)
            {
                Console.WriteLine(result);
            }

            var finalByteCount = GC.GetTotalMemory(true);
            Console.WriteLine(@"Memory {0} => {1}", originalByteCount, finalByteCount);
        }

        const Log.Level EXPECTED_LEVEL = Log.Level.Error;
        const string EXPECTED_FORMAT = "{0}";
        static readonly Exception ExpectedException = new Exception();
        static readonly object[] ExpectedArgs = { 1 };
        static readonly string[] ExpectedTags = { "TAG" };

        const string EXPECTED_BUILD_FORMAT = "{0}\n{1}";
        static readonly object[] ExpectedBuildArgs = {"1", "1"};

        class Actual
        {
            public Log.Level Level { get; set; }
            public Exception Exception { get; set; }
            public string Format { get; set; }
            public object[] Args { get; set; }
            public string[] Tags { get; set; }
        }

        static Log.Delegate GetDelegate(Actual actual)
        {
            return (l, id, ex, tags) => (f, a) =>
            {
                actual.Level = l;
                actual.Exception = ex;
                actual.Format = f;
                actual.Args = a;
                actual.Tags = tags;
            };
        }
    }
}