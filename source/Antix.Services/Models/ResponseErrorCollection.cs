using System;
using System.Collections;
using System.Collections.Generic;

namespace Antix.Services.Models
{
    public class ResponseErrorCollection : IEnumerable<Enum>
    {
        readonly HashSet<Enum> _errors;

        public ResponseErrorCollection()
        {
            _errors = new HashSet<Enum>();
        }

        public void Add(Enum value)
        {
            _errors.Add(value);
        }

        public IEnumerator<Enum> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}