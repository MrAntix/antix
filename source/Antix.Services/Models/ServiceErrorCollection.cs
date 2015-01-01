using System;
using System.Collections;
using System.Collections.Generic;

namespace Antix.Services.Models
{
    public class ServiceErrorCollection : IEnumerable<string>
    {
        readonly HashSet<string> _errors;

        public ServiceErrorCollection()
        {
            _errors = new HashSet<string>();
        }

        public void Add(string value)
        {
            _errors.Add(value);
        }

        public void Add(IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                _errors.Add(value);
            }
        }

        public void Add(Enum value)
        {
            Add(value.ToString("G"));
        }

        public void Add(IEnumerable<Enum> values)
        {
            foreach (var value in values) Add(value);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}