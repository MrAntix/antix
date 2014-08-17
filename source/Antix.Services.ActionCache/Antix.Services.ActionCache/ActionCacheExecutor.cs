using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Antix.Services.ActionCache
{
    public class ActionCacheExecutor : IActionCacheExecutor
    {
        readonly IActionCacheStore _store;
        readonly IDictionary<Type, IActionCacheAction> _actions;

        static readonly Type GenericActionType = typeof (IActionCacheAction<,>);

        public ActionCacheExecutor(
            IActionCacheStore store,
            IEnumerable<IActionCacheAction> actions)
        {
            _store = store;
            _actions = new Dictionary<Type, IActionCacheAction>();
            foreach (var action in actions)
            {
                var actionInType =
                    (from i in action.GetType().GetInterfaces()
                        where
                            i.IsGenericType
                            && i.GetGenericTypeDefinition() == GenericActionType
                        select i.GenericTypeArguments.First()).SingleOrDefault();

                if (actionInType == null)
                    throw new ActionCacheActionTypeException();

                _actions.Add(
                    actionInType, action);
            }
        }

        public async Task<object> ExecuteAsync(string identifier)
        {
            var data = _store.TryGet(identifier);

            if (data == null)
                throw new ActionCacheDataNotFoundException(identifier);

            var dataType = data.GetType();

            var result = await _actions[dataType].ExecuteAsync(data);

            _store.Remove(identifier);

            return result;
        }
    }
}