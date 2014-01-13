using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Antix.Data.Static
{
    public class Phone
    {
        readonly string _countryCode;
        readonly string _nationalDirectDial;
        readonly string _number;
        readonly string _extension;

        public Phone(
            string countryCode, string nationalDirectDial,
            string number,
            string extension)
        {
            _number = Clean(number);
            if (_number == null) throw new ArgumentException("number");

            _countryCode = Clean(countryCode);
            _nationalDirectDial = Clean(nationalDirectDial);
            _extension = Clean(extension);
        }

        public Phone(
            string countryCode, string nationalDirectDial,
            string number)
            : this(countryCode, nationalDirectDial, number, null)
        {
        }

        public Phone(
            string nationalDirectDial,
            string number)
            : this(null, nationalDirectDial, number, null)
        {
        }

        public Phone(
            string number)
            : this(null, null, number, null)
        {
        }

        public string CountryCode
        {
            get { return _countryCode; }
        }

        public string NationalDirectDial
        {
            get { return _nationalDirectDial; }
        }

        public string Number
        {
            get { return _number; }
        }

        public string Extension
        {
            get { return _extension; }
        }

        public override string ToString()
        {
            return Format(this);
        }

        public static string Format(Phone phone)
        {
            var output = new StringBuilder();

            if (!string.IsNullOrEmpty(phone.CountryCode))
                output.AppendFormat("+{0} ", phone.CountryCode);

            if (!string.IsNullOrEmpty(phone.NationalDirectDial))
                output.AppendFormat("({0}) ", phone.NationalDirectDial);

            output.Append(phone.Number);

            if (!string.IsNullOrEmpty(phone.Extension))
                output.AppendFormat(" x{0}", phone.Extension);

            return output.ToString();
        }

        static readonly Regex ParseRegex
            = new Regex(
                @"(\+(?<countryCode>\d+){1,4}\s*)?(\((?<ndd>\d{1,3})\))?(?<number>[\d\s\-\.]+)(?<extension>\s*x\d+)?",
                RegexOptions.Compiled);

        public static bool TryParse(string value, out Phone phoneNumber)
        {
            var match = ParseRegex.Match(value);

            if (match.Success)
            {
                phoneNumber = new Phone
                    (
                    match.Groups["countryCode"].Value,
                    match.Groups["ndd"].Value,
                    match.Groups["number"].Value,
                    match.Groups["extension"].Value
                    );
                return true;
            }

            phoneNumber = null;
            return false;
        }

        public static Phone Parse(string value)
        {
            return value;
        }

        public static implicit operator Phone(string value)
        {
            Phone result;
            return TryParse(value, out result)
                       ? result
                       : null;
        }

        static string Clean(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                       ? null
                       : new string(value.Where(char.IsDigit).ToArray());
        }

        public static IEnumerable<PhoneCountryConfiguration> CountryConfigurations
        {
            get { return PhoneConfigurationSection.Default.Countries.Cast<PhoneCountryConfiguration>(); }
        }
    }
}