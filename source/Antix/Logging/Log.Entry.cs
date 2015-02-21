using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antix.Logging
{
    public static partial class Log
    {
        public class EntryBuilder
        {
            readonly Log.Delegate _log;
            readonly List<Func<string>> _getMessages;

            public EntryBuilder(Delegate log)
            {
                _log = log;
                _getMessages = new List<Func<string>>();
            }

            public EntryBuilder Append(Action<Message> getMessage)
            {
                var resolve =
                    (Func<Action<Message>, string>)
                        (m =>
                        {
                            string message = null;
                            m((f, a) =>
                            {
                                message = string.Format(f, a);
                            });

                            return message;
                        });
                _getMessages.Add(() => resolve(getMessage));

                return this;
            }
        }
    }
}
