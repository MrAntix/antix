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

        public bool TryConsumeIncluding(char target)
        {
            while (_data.Any())
            {
                if (target == _data.Dequeue()) return true;
            }

            return false;
        }

        public bool TryConsumeUpto(
            Func<char, bool> target, out string result)
        {
            var word = new List<char>();
            while (_data.Any())
            {
                if (target(_data.Peek())) break;

                word.Add(_data.Dequeue());
            }

            result = new string(word.ToArray());

            return result.Length > 0;
        }

        public bool TryConsumeIncluding(string target)
        {
            return TryConsumeIncluding(_data, target);
        }

        static string AsString(IEnumerable<char> data)
        {
            return new string(data.ToArray());
        }

        public override string ToString()
        {
            return AsString(_data);
        }

        #region TryConsumeTo

        public static bool TryConsumeIncluding(Queue<char> data, string target)
        {
            return data.Any()
                   && TryConsumeIncluding(data, 0, target);
        }

        static bool TryConsumeIncluding(
            Queue<char> data, int dataIndex,
            string target)
        {
            if (dataIndex == data.Count) return false;

            if (TryConsumeIncluding(data, dataIndex, target, 0))
                return true;

            if (TryConsumeIncluding(data, ++dataIndex, target))
            {
                data.Dequeue();
                return true;
            }

            return false;
        }

        static bool TryConsumeIncluding(
            Queue<char> data, int dataIndex,
            string target, int targetIndex)
        {
            if (targetIndex == target.Length) return true;

            if (target[targetIndex] == data.ElementAt(dataIndex)
                && TryConsumeIncluding(data, ++dataIndex, target, ++targetIndex))
            {
                data.Dequeue();
                return true;
            }

            return false;
        }

        public bool TryConsume(string value)
        {
            if (!_data.Any()) return false;

            var candidate = _data.Take(value.Length);
            if (value != AsString(candidate)) return false;

            for (var i = 0; i < value.Length && _data.Any(); i++)
                _data.Dequeue();

            return true;
        }

        #endregion
    }
}