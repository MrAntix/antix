using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Antix.Services.ActionCache;
using Moq;
using Xunit;

namespace Antix.Tests.Services.ActionCache
{
    public class ActionCacheTests
    {
        [Fact]
        public void execute_action()
        {
            var storage = new InMemoryActionCacheStorage();
            var code = storage.Store(new TestActionData(), TimeSpan.FromDays(1));

            var sut = GetServiceUnderTest(storage, new TestAction());

            var result = sut.ExecuteAsync(code).Result;

            Assert.Equal(true, result);
        }

        ActionCacheExecutor GetServiceUnderTest(
            IActionCacheStorage storage,
            params IActionCacheAction[] actions)
        {
            return new ActionCacheExecutor(
                storage,
                actions);
        }

        [Fact]
        public void execute_action_stored_data_removed()
        {
            var data = new Dictionary<string, object>();
            var storage = new InMemoryActionCacheStorage(data);
            var code = storage.Store(new TestActionData(), TimeSpan.FromDays(1));

            var sut = GetServiceUnderTest(storage, new TestAction());

            var _ = sut.ExecuteAsync(code).Result;

            Assert.DoesNotContain(code, data.Keys);
        }

        [Fact]
        public void replace_existing_action_data()
        {
            var storage = new InMemoryActionCacheStorage();
            var idenifier = Guid.NewGuid().ToString("N");

            var code = storage.Store(new Object(), TimeSpan.FromDays(1), idenifier);
            storage.Store(new TestActionData(), TimeSpan.FromDays(1), idenifier);

            var retrieved = storage.TryRetrieve(code) as TestActionData;

            Assert.NotNull(retrieved);
        }

        [Fact]
        public void execute_action_code_does_not_exist()
        {
            var storage = new InMemoryActionCacheStorage();
            const string code = "NON-EXISTING";

            var sut = GetServiceUnderTest(storage, new TestAction());

            var ex = Assert.Throws<AggregateException>(
                () => sut.ExecuteAsync(code).Result
                ).InnerException;

            Assert.Contains(code, ex.Message);
        }

        [Fact]
        public void execute_action_code_expired()
        {
            var storage = new InMemoryActionCacheStorage();
            var code = storage.Store(new TestActionData(), TimeSpan.Zero);

            Thread.Sleep(500);

            var sut = GetServiceUnderTest(storage, new TestAction());

            var ex = Assert.Throws<AggregateException>(
                () => sut.ExecuteAsync(code).Result
                ).InnerException;

            Assert.Contains(code, ex.Message);
        }

        [Fact]
        public void register_invalid_action()
        {
            var invalidTestAction = Mock.Of<IActionCacheAction>();

            Assert.Throws<ActionCacheActionTypeException>(
                () =>
                    GetServiceUnderTest(
                        new InMemoryActionCacheStorage(),
                        invalidTestAction)
                );
        }

        public class TestActionData
        {
        }

        public class TestAction : ActionCacheActionBase<TestActionData, bool>
        {
            public override async Task<bool> ExecuteAsync(TestActionData model)
            {
                return true;
            }
        }
    }
}