using System;
using System.Collections.Generic;
using System.Linq;

namespace Antix.Services.Models
{
    public class Response
    {
        readonly IReadOnlyCollection<Enum> _errors;

        protected Response(
            IEnumerable<Enum> errors = null)
        {
            _errors = errors == null ? new Enum[] {} : errors.ToArray();
        }

        public IEnumerable<Enum> Errors
        {
            get { return _errors; }
        }

        public static Response Empty()
        {
            return new Response();
        }

        public static Response Empty(IEnumerable<Enum> errors)
        {
            return new Response(errors);
        }

        public static Response<T> Data<T>(T data)
        {
            return new Response<T>(data);
        }

        public static Response<T> Data<T>(T data, IEnumerable<Enum> errors)
        {
            return new Response<T>(data, errors);
        }
    }

    public class Response<T> : Response
    {
        readonly T _data;

        internal Response(
            T data,
            IEnumerable<Enum> errors = null) :
                base(errors)
        {
            _data = data;
        }

        public T Data
        {
            get { return _data; }
        }
    }
}