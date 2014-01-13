using System.Configuration;
using System.Xml.Serialization;

namespace Antix.Data.Static
{
    public class PhoneCountryConfiguration : ConfigurationElement
    {
        [XmlAttribute]
        [ConfigurationProperty("country", IsRequired = true)]
        public string CountryCode
        {
            get { return (string)this["country"]; }
            set { this["country"] = value; }
        }

        [XmlAttribute]
        [ConfigurationProperty("cd", IsRequired = true)]
        public string CountryDialing
        {
            get { return (string)this["cd"]; }
            set { this["cd"] = value; }
        }
        
        [XmlAttribute]
        [ConfigurationProperty("idd", IsRequired = true)]
        public string InternationalDirectDialing
        {
            get { return (string)this["idd"]; }
            set { this["idd"] = value; }
        }

        [XmlAttribute]
        [ConfigurationProperty("ndd", IsRequired = true)]
        public string NationalDirectDialing
        {
            get { return (string)this["ndd"]; }
            set { this["ndd"] = value; }
        }

        [XmlAttribute]
        [ConfigurationProperty("lengths", IsRequired = true)]
        public string Lengths
        {
            get { return (string)this["lengths"]; }
            set { this["lengths"] = value; }
        }
    }
}