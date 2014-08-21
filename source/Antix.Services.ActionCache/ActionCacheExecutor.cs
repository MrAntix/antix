using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Antix.Services.ActionCache
{
    public class ActionCacheExecutor : IActionCacheExecutor
    {
        readonly IActionCacheStorage _storage;
        readonly IDictionary<Type, IActionCacheAction> _actions;

        static readonly Type GenericActionType = typeof (IActionCacheAction<,>);

        public ActionCacheExecutor(
            IActionCacheStorage storage,
            IEnumerable<IActionCacheAction> actions)
        {
            _storage = storage;
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
            var data = _storage.TryRetrieve(identifier);

            if (data == null)
                throw new ActionCacheDataNotFoundException(identifier);

            var dataType = data.GetType();

            var result = await _actions[dataType].ExecuteAsync(data);

            _storage.Remove(identifier);

            return result;
        }
    }
}