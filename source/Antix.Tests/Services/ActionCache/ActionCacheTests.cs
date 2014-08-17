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
            var store = new TestActionCacheStore();
            var identifier = store.Add(new TestActionData(), TimeSpan.FromDays(1));

            var sut = GetServiceUnderTest(store, new TestAction());

            var result = sut.ExecuteAsync(identifier).Result;

            Assert.Equal(true, result);
        }

        ActionCacheExecutor GetServiceUnderTest(
            TestActionCacheStore store,
            params IActionCacheAction[] actions)
        {
            return new ActionCacheExecutor(
                store,
                actions);
        }

        [Fact]
        public void execute_action_stored_data_removed()
        {
            var data = new Dictionary<string, object>();
            var store = new TestActionCacheStore(data);
            var identifier = store.Add(new TestActionData(), TimeSpan.FromDays(1));

            var sut = GetServiceUnderTest(store, new TestAction());

            var _ = sut.ExecuteAsync(identifier).Result;

            Assert.DoesNotContain(identifier, data.Keys);
        }

        [Fact]
        public void execute_action_identifier_does_not_exist()
        {
            var store = new TestActionCacheStore();
            const string identifier = "NON-EXISTING";

            var sut = GetServiceUnderTest(store, new TestAction());

            var ex = Assert.Throws<AggregateException>(
                () => sut.ExecuteAsync(identifier).Result
                ).InnerException;

            Assert.Contains(identifier, ex.Message);
        }

        [Fact]
        public void execute_action_identifier_expired()
        {
            var store = new TestActionCacheStore();
            var identifier = store.Add(new TestActionData(), TimeSpan.Zero);

            Thread.Sleep(1);

            var sut = GetServiceUnderTest(store, new TestAction());

            var ex = Assert.Throws<AggregateException>(
                () => sut.ExecuteAsync(identifier).Result
                ).InnerException;

            Assert.Contains(identifier, ex.Message);
        }

        [Fact]
        public void register_invalid_action()
        {
            var invalidTestAction = Mock.Of<IActionCacheAction>();

            Assert.Throws<ActionCacheActionTypeException>(
                () =>
                    GetServiceUnderTest(
                        new TestActionCacheStore(),
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

        public class TestActionCacheStore :
            IActionCacheStore
        {
            readonly Dictionary<string, object> _data;

            public TestActionCacheStore(
                Dictionary<string, object> data = null)
            {
                _data = data ?? new Dictionary<string, object>();
            }

            public string Add(object data, TimeSpan expiresIn)
            {
                var identifier = Guid.NewGuid().ToString("N");
                _data.Add(identifier, data);

                new Timer(
                    o => Remove((string) o), identifier,
                    expiresIn, TimeSpan.FromMilliseconds(-1));

                return identifier;
            }


            public object TryGet(string identifier)
            {
                return _data.ContainsKey(identifier) ? _data[identifier] : null;
            }

            public void Remove(string identifier)
            {
                _data.Remove(identifier);
            }
        }
    }
}