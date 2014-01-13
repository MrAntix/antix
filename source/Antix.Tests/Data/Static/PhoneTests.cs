using System;
using System.Configuration;
using Antix.Data.Static;

using Xunit;

namespace Antix.Tests.Data.Static
{
    public class PhoneTests
    {
        //[Fact]
        //public void load_defaults()
        //{
        //    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    var section = (PhoneConfigurationSection)config.GetSection(PhoneConfigurationSection.CONFIG_PATH);

        //    if (section == null)
        //        section = new PhoneConfigurationSection();

        //    section.SectionInformation.ForceSave = true;
        //    config.Save(ConfigurationSaveMode.Full);
        //}

        [Fact]
        public void format()
        {
            Assert.Equal("5559999", new Phone("(555)9999").ToString());
            Assert.Equal("(0) 5559999", new Phone("0", "555-9999").ToString());
            Assert.Equal("+44 (0) 5559999", new Phone("44", "0", "5. 559999").ToString());
            Assert.Equal("+44 (0) 5559999 x1234", new Phone("44", "0", "555 9999", "1234").ToString());

            Assert.Throws<ArgumentException>(() => new Phone("44", "0", " ", "1234"));
        }

        [Fact]
        public void parse_number()
        {
            var phone = Phone.Parse("5559999");

            Assert.Null(phone.CountryCode);
            Assert.Null(phone.NationalDirectDial);
            Assert.Equal("5559999", phone.Number);
            Assert.Null(phone.Extension);
        }

        [Fact]
        public void parse_number_and_ndd()
        {
            var phone = Phone.Parse("(0)5559999");

            Assert.Null(phone.CountryCode);
            Assert.Equal("0", phone.NationalDirectDial);
            Assert.Equal("5559999", phone.Number);
            Assert.Null(phone.Extension);
        }

        [Fact]
        public void parse_number_ndd_and_country_code()
        {
            var phone = Phone.Parse("+44(0)5559999");

            Assert.Equal("44", phone.CountryCode);
            Assert.Equal("0", phone.NationalDirectDial);
            Assert.Equal("5559999", phone.Number);
            Assert.Null(phone.Extension);
        }

        [Fact]
        public void parse_number_ndd_country_code_and_extension()
        {
            var phone = Phone.Parse("+44(0)5559999x1234");

            Assert.Equal("44", phone.CountryCode);
            Assert.Equal("0", phone.NationalDirectDial);
            Assert.Equal("5559999", phone.Number);
            Assert.Equal("1234", phone.Extension);
        }

        [Fact]
        public void parse_formatted_number_ndd_country_code_and_extension()
        {
            var phone = Phone.Parse("+44 (0) 555 9999 x1234");

            Assert.Equal("44", phone.CountryCode);
            Assert.Equal("0", phone.NationalDirectDial);
            Assert.Equal("5559999", phone.Number);
            Assert.Equal("1234", phone.Extension);
        }
    }
}