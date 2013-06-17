using System;
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
            return AsString(_data.Take(count));
        }

        public char Consume()
        {
            return _data.Dequeue();
        }

        public string Consume(int count)
        {
            return AsString(
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
            bool upto)
        {
            if (_data.Any()
                && TryConsume(0, target, upto, null))
            {
                Consume(target.Length);

                return true;
            }

            return false;
        }

        public bool TryConsume(
            string target,
            bool upto,
            out string consumed)
        {
            var consumedList = new List<char>();

            if (_data.Any()
                && TryConsume(0, target, upto, consumedList))
            {
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
            bool upto,
            ICollection<char> consumed)
        {
            if (dataIndex == _data.Count) return false;

            if (TryConsumeTarget(dataIndex, target, 0))
                return true;

            if (upto
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

        public bool TryConsume(
            Func<char, bool> test,
            bool upto,
            out string consumed,
            out char target)
        {
            var consumedList = new List<char>();

            if (_data.Any()
                && TryConsume(0, test, upto, consumedList, out target))
            {
                Consume(); // target
                consumed = new string(consumedList.ToArray());
                return true;
            }

            consumed = null;
            target = default(char);
            return false;
        }

        bool TryConsume(
            int dataIndex,
            Func<char, bool> test,
            bool upto,
            ICollection<char> consumed,
            out char target)
        {
            if (dataIndex == _data.Count)
            {
                target = default(char);
                return false;
            }

            if (TryConsumeTarget(dataIndex, test, out target))
                return true;

            if (upto
                && TryConsume(dataIndex, test, true, consumed, out target))
            {
                var c = _data.Dequeue();
                if (consumed != null) consumed.Add(c);

                return true;
            }

            return false;
        }

        bool TryConsumeTarget(
            int dataIndex,
            Func<char, bool> test,
            out char target)
        {
            var c = _data.ElementAt(dataIndex);
            if (test(c))
            {
                target = c;

                return true;
            }

            target = default(char);
            return false;
        }

        static string AsString(IEnumerable<char> data)
        {
            return new string(data.ToArray());
        }

        public override string ToString()
        {
            return AsString(_data);
        }
    }
}