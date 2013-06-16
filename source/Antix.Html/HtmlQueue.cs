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

        public void ConsumeWhiteSpace()
        {
            while (_data.Any()
                   && char.IsWhiteSpace(_data.Peek()))
            {
                _data.Dequeue();
            }
        }

        public bool TryConsume(char target)
        {
            while (_data.Any())
            {
                if (target == _data.Dequeue()) return true;
            }

            return false;
        }

        public bool TryConsume(string target)
        {
            if (!_data.Any()) return false;

            var candidate = _data.Take(target.Length);
            if (target != AsString(candidate)) return false;

            for (var i = 0; i < target.Length && _data.Any(); i++)
                _data.Dequeue();

            return true;
        }

        public bool TryConsume(
            string target,
            bool upto, bool including,
            out string consumed)
        {
            return TryConsume(
                _data, target, upto, including, out consumed);
        }

        public bool TryConsume(
            Func<char, bool> test, 
            bool upto, bool including,
            out string consumed)
        {
            return TryConsume(
                _data, test, upto, including, out consumed);
        }

        static string AsString(IEnumerable<char> data)
        {
            return new string(data.ToArray());
        }

        public override string ToString()
        {
            return AsString(_data);
        }

        public static bool TryConsume(
            Queue<char> data, string target,
            bool upto, bool including)
        {
            return data.Any()
                   && TryConsume(data, 0, target, upto, including, null);
        }

        public static bool TryConsume(
            Queue<char> data, string target,
            bool upto, bool including,
            out string consumed)
        {
            var consumedList = new List<char>();

            if (data.Any()
                && TryConsume(data, 0, target, upto, including, consumedList))
            {
                consumed = new string(consumedList.ToArray());
                return true;
            }

            consumed = null;
            return false;
        }

        static bool TryConsume(
            Queue<char> data, int dataIndex,
            string target,
            bool upto, bool including,
            ICollection<char> consumed)
        {
            if (dataIndex == data.Count) return false;

            if (TryConsumeTarget(data, dataIndex, target, 0, including, consumed))
                return true;

            if (upto
                && TryConsume(data, ++dataIndex, target, true, including, consumed))
            {
                var c = data.Dequeue();
                if (consumed != null) consumed.Add(c);

                return true;
            }

            return false;
        }

        static bool TryConsumeTarget(
            Queue<char> data, int dataIndex,
            string target, int targetIndex,
            bool including,
            ICollection<char> consumed)
        {
            if (targetIndex == target.Length) return true;

            if (target[targetIndex] == data.ElementAt(dataIndex)
                && TryConsumeTarget(data, ++dataIndex, target, ++targetIndex, including, consumed))
            {
                if (including)
                {
                    var c = data.Dequeue();
                    if (consumed != null) consumed.Add(c);
                }

                return true;
            }

            return false;
        }

        public static bool TryConsume(
            Queue<char> data,
            Func<char, bool> test,
            bool upto, bool including,
            out string consumed)
        {
            var consumedList = new List<char>();

            if (data.Any()
                && TryConsume(data, 0, test, upto, including, consumedList))
            {
                consumed = new string(consumedList.ToArray());
                return true;
            }

            consumed = null;
            return false;
        }

        static bool TryConsume(
            Queue<char> data, int dataIndex,
            Func<char, bool> test,
            bool upto, bool including,
            ICollection<char> consumed)
        {
            if (dataIndex == data.Count) return false;

            if (TryConsumeTarget(data, dataIndex, test, including, consumed))
                return true;

            if (upto
                && TryConsume(data, ++dataIndex, test, true, including, consumed))
            {
                var c = data.Dequeue();
                if (consumed != null) consumed.Add(c);

                return true;
            }

            return false;
        }

        static bool TryConsumeTarget(
            Queue<char> data, int dataIndex,
            Func<char, bool> test,
            bool including,
            ICollection<char> consumed)
        {
            if (test(data.ElementAt(dataIndex)))
            {
                if (including)
                {
                    var c = data.Dequeue();
                    if (consumed != null) consumed.Add(c);
                }

                return true;
            }

            return false;
        }
    }
}