using System;

namespace Example.MvcApplication.Models
{
    public struct DateTimeModel
    {
        readonly DateTime? _value;

        public DateTimeModel(DateTime? value) : this()
        {
            _value = value;
        }

        public string Rfc
        {
            get { return string.Format("{0:s}", _value); }
        }

        public string Display
        {
            get { return string.Format("{0:r}", _value); }
        }

        public static explicit operator DateTimeModel(DateTime? value)
        {
            return new DateTimeModel(value);
        }
    }
}