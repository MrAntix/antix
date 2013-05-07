using System;
using System.Collections.Generic;
using Antix.Data.Keywords.Processing;

namespace Antix.Data.Keywords
{
    public class KeywordsIndexer : IKeywordsIndexer
    {
        readonly IKeywordsBuilderProvider _builderProvider;
        internal readonly IDictionary<Type, IKeywordsBuilder> Builders;

        public KeywordsIndexer(
            IKeywordsBuilderProvider builderProvider)
        {
            _builderProvider = builderProvider;
            Builders = new Dictionary<Type, IKeywordsBuilder>();
        }

        public IKeywordsBuilder<T> Entity<T>()
        {
            var builder = _builderProvider.Create<T>();

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