using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Antix.Data.Keywords.EF;
using Antix.Data.Keywords.EF.Entities;
using Antix.Data.Keywords.Processing;
using Moq;
using Xunit;

namespace Antix.Tests.Data.Keywords.EF
{
    public class EFKeywordsManagerTests
    {
        static EFKeywordsManager GetService()
        {
            var mockBuilder = new Mock<IKeywordsBuilder<TestEntity>>();
            mockBuilder
                .As<IKeywordsBuilder>()
                .Setup(o => o.Build(It.IsAny<TestEntity>()))
                .Returns((TestEntity e) => e.Name.Split(' '));

            var mockBuilderProvider = new Mock<IKeywordsBuilderProvider>();
            mockBuilderProvider.Setup(o => o.Create<TestEntity>())
                .Returns(mockBuilder.Object);

            var manager = new EFKeywordsManager(mockBuilderProvider.Object);

            manager.Entity<TestEntity>();

            return manager;
        }

        [Fact]
        public async Task entity_added()
        {
            var keywords = new List<Keyword>();
            var keywordsSet = keywords.AsDbSet();

            var service = GetService();

            await service.UpdateKeywordsAsync(
                new[]
                {
                    new EFEntityState
                    {
                        Entity = new TestEntity
                        {
                            Name = "one two two"
                        }
                    }
                },
                keywordsSet);

            Assert.Equal(2, keywords.Count);
            Assert.Equal("one", keywords.ElementAt(0).Value);
            Assert.Equal(1, keywords.ElementAt(0).Frequency);
            Assert.Equal("two", keywords.ElementAt(1).Value);
            Assert.Equal(2, keywords.ElementAt(1).Frequency);
        }

        [Fact]
        public async Task entity_updated()
        {
            var keyword = new Keyword
            {
                Value = "one",
                Frequency = 2
            };
            var keywords = new List<Keyword> {keyword};
            var keywordsSet = keywords.AsDbSet();

            var service = GetService();

            await service.UpdateKeywordsAsync(
                new[]
                {
                    new EFEntityState
                    {
                        Entity = new TestEntity
                        {
                            Name = "two two",
                            Keywords = new List<IndexedEntityKeyword>
                            {
                                new IndexedEntityKeyword
                                {
                                    Keyword = keyword,
                                    Frequency = 1
                                }
                            }
                        }
                    }
                },
                keywordsSet);

            Assert.Equal(1, keyword.Frequency);
        }

        [Fact]
        public async Task entity_deleted()
        {
            var keyword = new Keyword
            {
                Value = "one",
                Frequency = 1
            };
            var keywords = new List<Keyword> {keyword};
            var keywordsSet = keywords.AsDbSet();

            var service = GetService();

            await service.UpdateKeywordsAsync(
                new[]
                {
                    new EFEntityState
                    {
                        IsDeleted = true,
                        Entity = new TestEntity
                        {
                            Name = "one",
                            Keywords = new List<IndexedEntityKeyword>
                            {
                                new IndexedEntityKeyword
                                {
                                    Keyword = keyword,
                                    Frequency = 1
                                }
                            }
                        }
                    }
                },
                keywordsSet);

            Assert.False(keywords.Contains(keyword));
        }

        // ReSharper disable MemberCanBePrivate.Global
        // ef needs this to be public
        public class TestEntity : IndexedEntity
        {
            public string Name { get; set; }
        }

        // ReSharper restore MemberCanBePrivate.Global
    }
}