using System;

namespace Example.MvcApplication.Models
{
    public class DateTimeModel
    {
        public DateTime? Value { get; set; }

        public string Rfc
        {
            get { return string.Format("{0:s}", Value); }
        }

        public string Display
        {
            get { return string.Format("{0:r}", Value); }
        }
    }
}