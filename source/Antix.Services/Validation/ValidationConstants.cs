namespace Antix.Services.Validation
{
    internal static class ValidationConstants
    {
        public const string ATOM_PATTERN = @"[a-z0-9!#$%&'*+\-/=?^_`{|}~]+";
        public const string DOT_ATOM_PATTERN = ATOM_PATTERN + "(" + DOT_PATTERN + ATOM_PATTERN + ")*";

        public const string DOMAIN_LABEL_PATTERN = "[a-z]([a-z0-9-]{0,61}[a-z0-9])?";

        public const string DOMAIN_FULLY_QUALIFIED_PATTERN =
            DOMAIN_LABEL_PATTERN + "(" + DOT_PATTERN + DOMAIN_LABEL_PATTERN + ")*";

        public const int DOMAIN_MIN_LENGTH = 1;
        public const int DOMAIN_MAX_LENGTH = 255;

        public const string DOT_PATTERN = @"\.";
        public const string BYTE_PATTERN = @"(25[0-5]|2[0-4][0-9]|[01]?[0-9]?[0-9])";

        public const string IPV4_PATTERN = BYTE_PATTERN + "(" + DOT_PATTERN + BYTE_PATTERN + "){3}";

        public const string IPV6_BITS_PATTERN = "[0-9A-Fa-f]{1,4}";

        public const string IPV6_PATTERN
            = "((" +
              "(" + IPV6_BITS_PATTERN + ":){7}" + IPV6_BITS_PATTERN + "?" +
              "|::" +
              "|:(:" + IPV6_BITS_PATTERN + "){1,7}" +
              "|" + IPV6_BITS_PATTERN + ":(:" + IPV6_BITS_PATTERN + "){1,6}" +
              "|(" + IPV6_BITS_PATTERN + ":){2}(:" + IPV6_BITS_PATTERN + "){1,5}" +
              "|(" + IPV6_BITS_PATTERN + ":){3}(:" + IPV6_BITS_PATTERN + "){1,4}" +
              "|(" + IPV6_BITS_PATTERN + ":){4}(:" + IPV6_BITS_PATTERN + "){1,3}" +
              "|(" + IPV6_BITS_PATTERN + ":){5}(:" + IPV6_BITS_PATTERN + "){1,2}" +
              "|(" + IPV6_BITS_PATTERN + ":){6}:" + IPV6_BITS_PATTERN +
              "|(" + IPV6_BITS_PATTERN + ":){1,7}:" +
              ")|(" +
              "(" + IPV6_BITS_PATTERN + ":){5}" + IPV6_BITS_PATTERN + "" +
              "|:" +
              "|:(:" + IPV6_BITS_PATTERN + "){1,5}" +
              "|" + IPV6_BITS_PATTERN + ":(:" + IPV6_BITS_PATTERN + "){1,4}" +
              "|(" + IPV6_BITS_PATTERN + ":){2}(:" + IPV6_BITS_PATTERN + "){1,3}" +
              "|(" + IPV6_BITS_PATTERN + ":){3}(:" + IPV6_BITS_PATTERN + "){1,2}" +
              "|(" + IPV6_BITS_PATTERN + ":){4}:" + IPV6_BITS_PATTERN +
              "|(" + IPV6_BITS_PATTERN + ":){1,5}" +
              "):" + IPV4_PATTERN + ")";

        public const string QUOTED_STRING = @"""[^""\\]*(?:\\.[^""\\]*)*""";

        public const int EMAIL_MIN_LENGTH = EMAIL_LOCALPART_MIN_LENGTH + 1 + DOMAIN_MIN_LENGTH;
        public const int EMAIL_MAX_LENGTH = 256;

        public const int EMAIL_LOCALPART_MIN_LENGTH = 1;
        public const int EMAIL_LOCALPART_MAX_LENGTH = 64;

        public const string EMAIL_LOCAL_PART =
            "(?<localpart>" +
            DOT_ATOM_PATTERN +
            "|" + QUOTED_STRING +
            ")";

        public const string EMAIL_DOMAIN_PART =
            "(?<domain>" +
            DOMAIN_FULLY_QUALIFIED_PATTERN +
            @"|\[" + IPV4_PATTERN + @"\]" +
            @"|\[(ipv6:)?" + IPV6_PATTERN + @"\]" +
            ")";

        public const string EMAIL_PATTERN =
            EMAIL_LOCAL_PART + "@" + EMAIL_DOMAIN_PART;
    }
}