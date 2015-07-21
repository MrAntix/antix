using System.Linq;
using Antix.Services.Validation.Predicates;

namespace Antix.Services.Validation
{
    public class ValidationAssertionBuilder<TModel> :
        ValidationRuleBuilder<TModel>, IValidationAssertionBuilder<TModel>
    {
        readonly ValidationActionList<TModel> _predicateActions;
        readonly bool _assert;

        public ValidationAssertionBuilder(
            ValidationActionList<TModel> predicateActions,
            bool assert)
        {
            _predicateActions = predicateActions;
            _assert = assert;
        }

        public bool Assert
        {
            get { return _assert; }
        }

        public IValidationAssertionBuilder<TModel> Or(
            IValidationPredicate<TModel> predicate,
            params IValidationPredicate<TModel>[] predicates)
        {
            _predicateActions.Add(predicate, predicates);

            return this;
        }

        public override void Build(
            ValidationBuildState state, TModel model, string path)
        {
            foreach (var predicateAction in _predicateActions)
            {
                var localState = new ValidationBuildState();
                predicateAction(localState, model, path);

                if (!localState.Errors.Any())
                {
                    if (Actions.Any())
                        base.Build(state, model, path);

                    return;
                }

                if (Assert)
                    state.Errors.AddRange(localState.Errors);
            }
        }
    }
}