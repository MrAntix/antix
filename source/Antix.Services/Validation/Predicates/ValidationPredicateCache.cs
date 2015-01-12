using System;
using System.Collections.Concurrent;

namespace Antix.Services.Validation.Predicates
{
    public abstract class ValidationPredicateCache
    {
        public static ValidationPredicateCache<T, TTuple> Create<T, TTuple>(
            Func<TTuple, T> create)
        {
            return new ValidationPredicateCache<T, TTuple>(create);
        }
    }

    public class ValidationPredicateCache<T, TTuple> :
        ValidationPredicateCache
    {
        readonly ConcurrentDictionary<TTuple, T> _items;

        readonly Func<TTuple, T> _create;

        public ValidationPredicateCache(Func<TTuple, T> create)
        {
            _create = create;
            _items = new ConcurrentDictionary<TTuple, T>();
        }

        public T GetOrCreate(TTuple key)
        {
            return _items.ContainsKey(key)
                ? _items[key]
                : Create(key);
        }

        T Create(TTuple key)
        {
            var item = _items.GetOrAdd(key, _create(key));

            return item;
        }
    }
}