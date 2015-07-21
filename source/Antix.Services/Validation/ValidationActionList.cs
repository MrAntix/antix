using System;
using System.Collections.Generic;
using System.Linq;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation
{
    public class ValidationActionList<TModel> :
        List<Action<ValidationBuildState, TModel, string>>
    {
        public void Add(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            Add(
                (state, model, path) =>
                {
                    var errors = new[] {predicate}.Concat(predicates)
                        .Where(p => !p.Is(model))
                        .Select(p => string.Format("{0}:{1}", path, p.Name));

                    state.Errors.AddRange(errors);
                });
        }
    }
}