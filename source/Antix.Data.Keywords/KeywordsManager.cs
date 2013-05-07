using System;
using System.Collections.Generic;
using Antix.Data.Keywords.Processing;

namespace Antix.Data.Keywords
{
    public class KeywordsManager
    {
        internal readonly IDictionary<Type, IKeywordsBuilder> Builders;
        internal readonly IKeywordProcessor Processor;

        public KeywordsManager(IKeywordProcessor processor)
        {
            Processor = processor;
            Builders = new Dictionary<Type, IKeywordsBuilder>();
        }

        public KeywordsBuilder<T> IndexEntity<T>()
        {
            var builder = new KeywordsBuilder<T>(Processor);

            Builders.Add(typeof (T), builder);

            return builder;
        }

        public string[] GetKeywords(object entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            var type = entity.GetType();
            if (!Builders.ContainsKey(type))
                throw new EntityTypeNotIndexedException(type);

            return Builders[entity.GetType()].Build(entity);
        }
    }
}