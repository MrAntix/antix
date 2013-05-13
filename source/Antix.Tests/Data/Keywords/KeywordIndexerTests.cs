using System.Collections.Generic;
using Antix.Data.Keywords;
using Antix.Data.Keywords.Entities;
using Antix.Data.Keywords.Processing;
using Xunit;

namespace Antix.Tests.Data.Keywords
{
    public class KeywordIndexerTests
    {
        static IKeywordsIndexer GetService()
        {
            return new KeywordsIndexer(
                new KeywordsBuilderProvider(
                    WordSplitKeywordProcessor.Create()
                    )
                );
        }

        [Fact]
        public void GetsAllKeywords()
        {
            var entity = new Entity
            {
                Text = "aa bb",
                SubCollection = new[]
                        {
                            new SubEntity {Text = "cc dd ee"},
                            new SubEntity {Text = "ff"}
                        }
            };

            var manager = GetService();

            manager
                .Entity<Entity>()
                .Index(e => e.Text)
                .ForEach(e => e.SubCollection, b => b.Index(e => e.Text));

            var result = manager.GetKeywords(entity);

            Assert.Equal(new[] { "aa", "bb", "cc", "dd", "ee", "ff" }, result);
        }

        [Fact]
        public void GetsAllKeywordsWhenSubCollectionIsNull()
        {
            var entity = new Entity
            {
                Text = "aa bb",
                SubCollection = null
            };

            var manager = GetService();

            manager
                .Entity<Entity>()
                .Index(e => e.Text)
                .ForEach(e => e.SubCollection, b => b.Index(e => e.Text));

            var result = manager.GetKeywords(entity);

            Assert.Equal(new[] { "aa", "bb" }, result);
        }

        class Entity:IIndexedEntity
        {
            public string Text { get; set; }
            public ICollection<SubEntity> SubCollection { get; set; }

            public IEnumerable<IIndexedEntityKeyword> Keywords { get; set; }
        }

        class IndexedEntityKeyword : IIndexedEntityKeyword
        {
            public IKeyword Keyword { get; set; }
            public int Frequency { get; set; }
        }

        class SubEntity
        {
            public string Text { get; set; }
        }
    }
}