using System;
using System.Text.RegularExpressions;

namespace Antix.Services.Validation.Predicates
{
    public class EmailPredicate :
        ValidationPredicateBase<string>
    {
        static readonly Regex Regex;

        static EmailPredicate()
        {
            Regex =
                new Regex(
                    string.Concat(
                        "^", ValidationConstants.EMAIL_PATTERN, "$"),
                    RegexOptions.IgnoreCase);
        }

        public override bool Is(string model)
        {
            var valueString = Convert.ToString(model);
            if (string.IsNullOrWhiteSpace(valueString)) return false;

            var match = Regex.Match(valueString);

            var validPart =
                (Func<Match, string, int, int, bool>)
                    ((m, n, min, max) =>
                        m.Groups[n].Success
                        && m.Groups[n].Length >= min
                        && m.Groups[n].Length <= max);

            return match.Success
                   && match.Length >= ValidationConstants.EMAIL_MIN_LENGTH
                   && match.Length <= ValidationConstants.EMAIL_MAX_LENGTH
                   &&
                   validPart(match, "localpart", ValidationConstants.EMAIL_LOCALPART_MIN_LENGTH,
                       ValidationConstants.EMAIL_LOCALPART_MAX_LENGTH)
                   &&
                   validPart(match, "domain", ValidationConstants.DOMAIN_MIN_LENGTH,
                       ValidationConstants.DOMAIN_MAX_LENGTH);
        }
    }
}