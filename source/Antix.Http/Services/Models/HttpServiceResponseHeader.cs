namespace Antix.Http.Services.Models
{
    public class HttpServiceResponseHeader
    {
        readonly string _name;
        readonly string _value;

        public HttpServiceResponseHeader(
            string name,
            string value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Value
        {
            get { return _value; }
        }
    }
}