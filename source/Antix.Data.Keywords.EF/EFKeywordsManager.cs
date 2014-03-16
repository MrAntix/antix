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
            var objectContext = ((IObjectContextAdapter) context).ObjectContext;
            var entities =
                objectContext.ObjectStateManager.GetObjectStateEntries(
                    EntityState.Added | EntityState.Modified)
                             .Where(es => es.Entity is IndexedEntity)
                             .Select(es =>
                                     new EFEntityState
                                         {
                                             Entity = (IndexedEntity) es.Entity,
                                             IsDeleted = es.State == EntityState.Deleted
                                         })
                             .ToArray();

            await UpdateKeywordsAsync(entities, context.Set<Keyword>());

            objectContext.DetectChanges();
        }

        public async Task UpdateKeywordsAsync(
            EFEntityState[] entityStates,
            IDbSet<Keyword> keywordsSet)
        {
            if (!entityStates.Any()) return;

            // get all the entities and their new keywords
            var entityKeywordValues = (from entityState in entityStates
                                       select new
                                           {
                                               entity = entityState.Entity,
                                               keywordValues = entityState.IsDeleted
                                                                   ? new string[] {}
                                                                   : GetKeywords(entityState.Entity)
                                           })
                .ToArray();

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
            var toRemove = entity.Keywords
                                 .ToDictionary(ek => ek, ek => ek.Frequency);

            foreach (var keywordValue in keywordValues)
            {
                var entityKeyword
                    = entity.Keywords.SingleOrDefault(ek => ek.Keyword.Value == keywordValue);

                if (entityKeyword != null)
                {
                    if (toRemove.ContainsKey(entityKeyword))
                    {
                        toRemove[entityKeyword]--;
                    }
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
                }

                entityKeyword.Frequency++;
                entityKeyword.Keyword.Frequency++;
                if (!entity.Keywords.Contains(entityKeyword))
                {
                    entity.Keywords.Add(entityKeyword);
                }
            }

            foreach (var kv in toRemove)
            {
                kv.Key.Frequency -= kv.Value;
                kv.Key.Keyword.Frequency -= kv.Value;

                if (kv.Key.Frequency == 0)
                {
                    entity.Keywords.Remove(kv.Key);
                }

                if (kv.Key.Keyword.Frequency == 0)
                {
                    keywordsSet.Remove(kv.Key.Keyword);
                }
            }
        }
    }
}