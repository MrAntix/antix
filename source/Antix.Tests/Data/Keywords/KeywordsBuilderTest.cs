using System.Collections.Generic;
using Antix.Data.Keywords.Processing;
using Xunit;

namespace Antix.Tests.Data.Keywords
{
    public class KeywordsBuilderTest
    {
        class Entity
        {
            public string Text { get; set; }
            public ICollection<SubEntity> SubCollection { get; set; }

            public Entity Sub { get; set; }
        }

        class SubEntity
        {
            public string Text { get; set; }
        }

        IKeywordsBuilder<Entity> GetService()
        {
            return new KeywordsBuilder<Entity>(
                WordSplitKeywordProcessor.Create());
        }

        [Fact]
        public void GetsKeywordsFromTextProperties()
        {
            var entity = new Entity
            {
                Text = "aa"
            };

            var builder = GetService()
                .Index(e => e.Text);

            var result = builder.Build(entity);

            Assert.Equal(new[] {"aa"}, result);
        }

        [Fact]
        public void GetsKeywordsFromSubEntityTextProperties()
        {
            var entity = new Entity
            {
                Text = "aa",
                Sub = new Entity
                {
                    Text = "bb"
                }
            };

            var builder = GetService()
                .Index(e => e.Text)
                .For(e => e.Sub, b => b.Index(e => e.Text));

            var result = builder.Build(entity);

            Assert.Equal(new[] {"aa", "bb"}, result);
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

            var builder = GetService()
                .Index(e => e.Text)
                .ForEach(e => e.SubCollection, b => b.Index(e => e.Text));

            var result = builder.Build(entity);

            Assert.Equal(new[] {"aa", "bb", "cc", "dd", "ee", "ff"}, result);
        }
    }
}