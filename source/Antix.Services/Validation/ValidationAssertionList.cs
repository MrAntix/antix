using System;
using System.Collections.Generic;
using System.Linq;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation
{
    public class ValidationAssertionList<TModel> 
    {
        readonly List<Func<TModel, string, IEnumerable<string>>> _actions
            = new List<Func<TModel, string, IEnumerable<string>>>();

        readonly bool _assert;

        public ValidationAssertionList(bool assert)
        {
            _assert = assert;
        }

        public bool Assert
        {
            get { return _assert; }
        }

        public void Add(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            Actions.Add(
                (model, path) =>
                {
                    var errors = new[] {predicate}.Concat(predicates)
                        .Where(p => !p.Is(model))
                        .Select(p => string.Format("{0}:{1}", path, p));

                    return errors;
                });
        }

        public List<Func<TModel, string, IEnumerable<string>>> Actions
        {
            get { return _actions; }
        }
    }
}