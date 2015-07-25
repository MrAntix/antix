using System;
using System.Threading.Tasks;

namespace Antix.Services.Validation.Predicates
{
    public class FunctionPredicateAsync<TModel> : ValidationPredicateBase<TModel>
    {
        readonly Func<TModel, Task<bool>> _function;

        public FunctionPredicateAsync(
            string name,
            Func<TModel, Task<bool>> function) :
                base(name)
        {
            _function = function;
        }

        public override async Task<bool> IsAsync(TModel model)
        {
            return await _function(model);
        }
    }
}