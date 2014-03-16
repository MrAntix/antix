using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

using Antix.Data.Keywords.EF.Entities;
using Antix.Data.Keywords.Processing;

namespace Antix.Data.Keywords.EF
{
    public class EFKeywordsManager : KeywordsIndexer
    {
        public EFKeywordsManager(
            IKeywordsBuilderProvider builderProvider)
            : base(builderProvider)
        {
        }

        public async Task UpdateKeywordsAsync(DbContext context)
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;

            var entities =
                objectContext.ObjectStateManager.GetObjectStateEntries(
                    EntityState.Added | EntityState.Modified)
                             .Select(es => es.Entity)
                             .OfType<IndexedEntity>()
                             .ToArray();

            if (!entities.Any()) return;

            // get all the entities and their new keywords
            var entityKeywordValues = (from entity in entities
                                       select new
                                       {
                                           entity,
                                           keywordValues = GetKeywords(entity)
                                       })
                .ToArray();

            var keywordsSet = context.Set<Keyword>();
            var existingKeywords = await GetExistingKeywordsAsync(
                keywordsSet,
                entityKeywordValues.SelectMany(e => e.keywordValues)
                                             );

            foreach (var entityNewKeyword in entityKeywordValues)
            {
                UpdateEntityKeyword
                    (entityNewKeyword.entity,
                     entityNewKeyword.keywordValues,
                     existingKeywords,
                     keywordsSet);
            }

            objectContext.DetectChanges();
        }

        static async Task<Keyword[]> GetExistingKeywordsAsync(
            IDbSet<Keyword> keywordsSet,
            IEnumerable<string> keywordValues)
        {
            var localKeywords = keywordsSet.Local.ToArray();

            var keywordValuesToLoad = keywordValues
                .Except(localKeywords.Select(k => k.Value));

            var loadedKeywords = await keywordsSet
                .Where(k => keywordValuesToLoad.Contains(k.Value))
                .ToArrayAsync();

            return localKeywords
                .Concat(loadedKeywords)
                .ToArray();
        }

        static void UpdateEntityKeyword(
            IndexedEntity entity, IEnumerable<string> keywordValues,
            Keyword[] existingKeywords,
            IDbSet<Keyword> keywordsSet)
        {
            var toRemove = entity.Keywords.ToList();

            foreach (var keywordValue in keywordValues)
            {
                var entityKeyword
                    = entity.Keywords.SingleOrDefault(ek => ek.Keyword.Value == keywordValue);

                if (entityKeyword != null)
                {
                    toRemove.Remove(entityKeyword);
                }
                else
                {
                    entityKeyword = new IndexedEntityKeyword();
                    var keyword = existingKeywords.SingleOrDefault(k => k.Value == keywordValue);

                    if (keyword == null)
                    {
                        keyword = new Keyword
                        {
                            Value = keywordValue
                        };
                        keywordsSet.Add(keyword);
                    }
                    entityKeyword.Keyword = keyword;
                    keyword.Frequency++;
                }

                entityKeyword.Frequency++;
                if (!entity.Keywords.Contains(entityKeyword))
                {
                    entity.Keywords.Add(entityKeyword);
                }
            }

            foreach (var entityKeyword in toRemove)
            {
                entity.Keywords.Remove(entityKeyword);
                entityKeyword.Keyword.Frequency -= entityKeyword.Frequency;

                if (entityKeyword.Keyword.Frequency == 0)
                {
                    keywordsSet.Remove(entityKeyword.Keyword);
                }
            }
        }
    }
}