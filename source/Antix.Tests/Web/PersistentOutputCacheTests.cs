using System;
using Antix.Web.Caching;
using Moq;
using Xunit;

namespace Antix.Tests.Web
{
    public class PersistentOutputCacheTests
    {
        [Fact]
        public void ExistingItemIsReturnedOnAdd()
        {
            const string key = "SomeKey";
            const string data = "SomeData";

            var storageMock = new Mock<IOutputCacheStorage>();
            storageMock.Setup(m => m.Exists(key)).Returns(true).Verifiable();
            storageMock.Setup(m => m.IsExpired(key)).Returns(false).Verifiable();
            storageMock.Setup(m => m.Read(key)).Returns(data).Verifiable();

            var sut = new PersistentOutputCacheProvider(storageMock.Object);

            var result = sut.Add(key, new object(), DateTime.UtcNow.AddDays(1));

            Assert.Equal(data, result);
            storageMock.VerifyAll();
        }

        [Fact]
        public void NonExistingItemReturnNullOnGet()
        {
            const string key = "SomeKey";

            var storageMock = new Mock<IOutputCacheStorage>();
            storageMock.Setup(m => m.Exists(key)).Returns(false).Verifiable();

            var sut = new PersistentOutputCacheProvider(storageMock.Object);

            var result = sut.Get(key);

            Assert.Equal(null, result);
            storageMock.VerifyAll();
        }

        [Fact]
        public void ExpiredItemIsDeletedOnGet()
        {
            const string key = "SomeKey";

            var storageMock = new Mock<IOutputCacheStorage>();
            storageMock.Setup(m => m.Exists(key)).Returns(true).Verifiable();
            storageMock.Setup(m => m.IsExpired(key)).Returns(true).Verifiable();
            storageMock.Setup(m => m.Delete(key)).Verifiable();

            var sut = new PersistentOutputCacheProvider(storageMock.Object);

            var result = sut.Get(key);

            Assert.Equal(null, result);
            storageMock.VerifyAll();
        }
    }
}