using System.Collections.Generic;
using System.Linq;

namespace Antix.Html
{
    public class HtmlQueue
    {
        readonly Queue<char> _data;

        public HtmlQueue(string data)
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

        public void ConsumeWhiteSpace()
        {
            while (_data.Any()
                   && char.IsWhiteSpace(_data.Peek()))
            {
                _data.Dequeue();
            }
        }

        public bool TryConsume(
            string target,
            bool seek, bool consumeTarget)
        {
            if (_data.Any()
                && TryConsume(0, target, seek, null))
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
            var consumedList = new List<char>();

            if (_data.Any()
                && TryConsume(0, target, seek, consumedList))
            {
                if (consumeTarget)
                    Consume(target.Length);

                consumed = new string(consumedList.ToArray());
                return true;
            }

            consumed = null;
            return false;
        }

        bool TryConsume(
            int dataIndex,
            string target,
            bool seek,
            ICollection<char> consumed)
        {
            if (dataIndex == _data.Count) return false;

            if (TryConsumeTarget(dataIndex, target, 0))
                return true;

            if (seek
                && TryConsume(++dataIndex, target, true, consumed))
            {
                var c = _data.Dequeue();
                if (consumed != null) consumed.Add(c);

                return true;
            }

            return false;
        }

        bool TryConsumeTarget(
            int dataIndex,
            string target, int targetIndex)
        {
            if (targetIndex == target.Length) return true;

            return target[targetIndex] == _data.ElementAt(dataIndex)
                   && TryConsumeTarget(++dataIndex, target, ++targetIndex);
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