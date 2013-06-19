using System;
using System.Collections.Generic;
using System.Linq;
using Antix.Html;

namespace Antix.Tests.Html.Services
{
    public class QueueHtmlReader : IHtmlReader
    {
        readonly Queue<char> _data;

        public QueueHtmlReader(string data)
        {
            _data = new Queue<char>(data);
        }

        public bool Any()
        {
            return _data.Any();
        }

        public char Peek()
        {
            return _data.Any()
                       ? _data.Peek()
                       : (char) 0;
        }

        public string Peek(int count)
        {
            return ToString(_data.Take(count));
        }

        public char Consume()
        {
            return _data.Dequeue();
        }

        public string Consume(int count)
        {
            return ToString(
                Enumerable.Range(1, count)
                          .Select(i => _data.Dequeue())
                );
        }

        public void Consume(Func<char, bool> match)
        {
            while (_data.Any()
                   && match(_data.Peek()))
            {
                _data.Dequeue();
            }
        }

        public bool TryConsume(
            string target,
            bool seek, bool consumeTarget)
        {
            string consumed;
            if (_data.Any()
                && TryConsume(target, seek, out consumed))
            {
                if (consumeTarget)
                    Consume(target.Length);

                return true;
            }

            return false;
        }

        public bool TryConsume(
            string target,
            bool seek, bool consumeTarget,
            out string consumed)
        {
            if (_data.Any()
                && TryConsume(target, seek, out consumed))
            {
                if (consumeTarget)
                    Consume(target.Length);

                return true;
            }

            consumed = null;
            return false;
        }

        bool TryConsume(string target,
                        bool seek,
                        out string consumed)
        {
            if (target.Length < _data.Count)
            {
                if (IsTarget(0, target))
                {
                    consumed = string.Empty;
                    return true;
                }

                if (seek)
                    for (var i = 1; i <= _data.Count - target.Length; i++)
                    {
                        if (!IsTarget(i, target)) continue;

                        consumed = Consume(i);

                        return true;
                    }
            }

            consumed = null;
            return false;
        }

        bool IsTarget(
            int dataIndex,
            string target)
        {
            return
                ToString(_data
                             .Skip(dataIndex)
                             .Take(target.Length))
                == target;
        }

        static string ToString(IEnumerable<char> data)
        {
            return new string(data.ToArray());
        }

        public override string ToString()
        {
            return ToString(_data);
        }
    }
}