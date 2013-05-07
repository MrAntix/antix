using System;
using System.Collections.Generic;
using System.Linq;

namespace Antix.Data.Keywords.Processing
{
    public class KeywordsBuilder<T> : IKeywordsBuilder<T>
    {
        readonly IKeywordProcessor _processor;
        readonly HashSet<Func<T, IEnumerable<string>>> _items;

        public KeywordsBuilder(IKeywordProcessor processor)
        {
            _processor = processor;
            _items = new HashSet<Func<T, IEnumerable<string>>>();
        }

        public IKeywordsBuilder<T> Index(Func<T, string> func)
        {
            _items.Add(e => _processor.Process(func(e)));

            return this;
        }

        public IKeywordsBuilder<T> For<TProp>(
            Func<T, TProp> func, Action<KeywordsBuilder<TProp>> action)
        {
            _items.Add(e =>
                {
                    var v = func(e);
                    var b = new KeywordsBuilder<TProp>(_processor);
                    action(b);

                    return b.Build(v);
                });

            return this;
        }

        public IKeywordsBuilder<T> ForEach<TProp>(
            Func<T, IEnumerable<TProp>> func, Action<KeywordsBuilder<TProp>> action)
        {
            _items.Add(e =>
                {
                    var vs = func(e);
                    var b = new KeywordsBuilder<TProp>(_processor);
                    action(b);

                    return (vs).SelectMany(b.Build);
                });

            return this;
        }

        public string[] Build(T entity)
        {
            var result = new List<string>();

            foreach (var func in _items)
            {
                var keywords = func(entity);
                if (keywords != null)
                    result.AddRange(keywords);
            }

            return result.ToArray();
        }

        string[] IKeywordsBuilder.Build(object entity)
        {
            return Build((T) entity);
        }
    }
}