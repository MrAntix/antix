using System.Collections.Generic;
using System.Linq;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation
{
    public class ValidationAssertionBuilder<TModel> :
        ValidationRuleBuilder<TModel>, IValidationAssertionBuilder<TModel>
    {
        readonly ValidationAssertionList<TModel> _assertionList;

        public ValidationAssertionBuilder(
            ValidationAssertionList<TModel> assertionList)
        {
            _assertionList = assertionList;
        }

        public IValidationAssertionBuilder<TModel> Or(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            _assertionList.Add(predicate, predicates);

            return this;
        }

        public override string[] Build(TModel model, string path)
        {
            var errors = new List<string>();
            foreach (var actionErrors in _assertionList
                .Actions
                .Select(action => action(model, path).ToArray()))
            {
                if (!actionErrors.Any())
                {
                    return !Actions.Any()
                        ? new string[] {}
                        : base.Build(model, path);
                }

                if (_assertionList.Assert) errors.AddRange(actionErrors);
            }

            return errors.ToArray();
        }
    }
}