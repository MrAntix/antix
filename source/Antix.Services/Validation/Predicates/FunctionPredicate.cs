using System;
using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class FunctionPredicate<TModel> : ValidationPredicateBase<TModel>
    {
        readonly Func<TModel, bool> _function;

        public FunctionPredicate(
            string name,
            Func<TModel, bool> function) :
                base(name)
        {
            _function = function;
        }

        public override async Task<bool> IsAsync(TModel model)
        {
            return _function(model);
        }
    }
}