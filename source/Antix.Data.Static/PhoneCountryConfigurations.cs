using System.Configuration;

namespace Antix.Data.Static
{
    public class PhoneCountryConfigurations :
        ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PhoneCountryConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var setting = (PhoneCountryConfiguration) element;
            var key = string.Format("{0}{1}", setting.CountryCode, setting.CountryDialing);

            return string.IsNullOrEmpty(setting.NationalDirectDialing)
                       ? key
                       : string.Format("{0}-{1}", key, setting.NationalDirectDialing);
        }

        internal void Add(PhoneCountryConfiguration phoneCountryConfiguration)
        {
            BaseAdd(phoneCountryConfiguration);
        }
    }
}