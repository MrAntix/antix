using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
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

        public void UpdateKeywords(DbContext context)
        {
            var objectContext = ((IObjectContextAdapter) context).ObjectContext;

            var entities =
                objectContext.ObjectStateManager.GetObjectStateEntries(
                    EntityState.Added | EntityState.Modified)
                             .Where(es => es.Entity is IndexedEntity)
                             .ToArray();

            if (!entities.Any()) return;

            var keywordsSet = context.Set<Keyword>();
            foreach (var entityState in entities)
            {
                UpdateEntityKeywords((IndexedEntity) entityState.Entity, keywordsSet);
            }

            objectContext.DetectChanges();
            objectContext.SaveChanges();

            foreach (var keyword in keywordsSet.Local)
            {
                keyword.Frequency = context
                    .Set<IndexedEntityKeyword>()
                    .Where(ek => ek.Keyword.Id == keyword.Id)
                    .Sum(ek => ek.Frequency);
            }

            objectContext.DetectChanges();
        }

        void UpdateEntityKeywords(
            IndexedEntity entity, IDbSet<Keyword> keywordsSet)
        {
            var existing = entity.Keywords
                                 .Select(ek =>
                                     {
                                         ek.Frequency = 0;
                                         return ek;
                                     }).ToArray();

            foreach (var keywordValue in GetKeywords(entity))
            {
                var entityKeyword
                    = entity.Keywords.SingleOrDefault(k => k.Keyword.Value == keywordValue)
                      ?? existing.SingleOrDefault(k => k.Keyword.Value == keywordValue);

                if (entityKeyword == null)
                {
                    entityKeyword = new IndexedEntityKeyword();
                    var keyword =
                        keywordsSet.Local.SingleOrDefault(k => k.Value == keywordValue)
                        ?? keywordsSet.SingleOrDefault(k => k.Value == keywordValue);

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
                if (!entity.Keywords.Contains(entityKeyword))
                {
                    entity.Keywords.Add(entityKeyword);
                }
            }

            foreach (var entityKeyword in 
                entity.Keywords.Where(ek => ek.Frequency == 0))
                entity.Keywords.Remove(entityKeyword);
        }
    }
}