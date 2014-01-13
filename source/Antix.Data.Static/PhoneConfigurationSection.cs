using System.Configuration;

namespace Antix.Data.Static
{
    public class PhoneConfigurationSection : ConfigurationSection
    {
        public const string CONFIG_PATH = "staticData/phoneSettings";

        [ConfigurationProperty("countries")]
        public PhoneCountryConfigurations Countries
        {
            get { return (PhoneCountryConfigurations)this["countries"]; }
        }

        public static readonly PhoneConfigurationSection Default
            = (PhoneConfigurationSection) ConfigurationManager.GetSection(CONFIG_PATH)
              ?? new PhoneConfigurationSection();
    }
}