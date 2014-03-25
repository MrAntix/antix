using System;
using System.Collections.Generic;
using Antix.Data.Keywords.Stemming;

namespace Example.MvcApplication.Stemming
{
    public class LocalEnglishStemmer : IStemmer
    {
        readonly Ruletable ttable;

        public LocalEnglishStemmer(string azrules)
        {
            ttable = new Ruletable(azrules);
        }

        public static bool isvalidstr(string s)
        {
            if (s.Length == 0)
                return false;
            var length = s.Length;
            if (length >= 2 && (char.IsDigit(s[length - 1]) && char.IsDigit(s[length - 2])))
                length -= 2;
            for (var index = 0; index < length; ++index)
            {
                if (!char.IsLower(s[index]) && s[index] != 39)
                    return false;
            }
            return true;
        }

        public string Stem(string s)
        {
            if (s.Length < 3 || !isvalidstr(s))
                return s;
            var isintact = true;
            var word = s;
            var str = s;
            var length = s.Length;
            var num = 0;
            Rule.EAction eaction;
            while ((eaction = ttable.WalkRules(ref word, isintact)) != Rule.EAction.actStop)
            {
                isintact = false;
                if (word.Length >= 3)
                {
                    ++num;
                    if (num <= 2*length)
                        str = word;
                    else
                        break;
                }
                else
                    break;
            }
            if (word.Length < 3)
                word = str;
            return (eaction == Rule.EAction.actStop ? word : str).TrimEnd(new char[1]
                {
                    '\''
                });
        }

        internal class Rule
        {
            const string blank = "?";
            public const int MINSTEMSIZE = 3;
            public const int MAXWDSZ = 25;
            public const int MAXKEYSZ = 12;
            public const int MAXSUFFSZ = 8;
            public string keystr;
            public string repstr;
            public bool intact;
            public bool cont;
            public int rulenum;
            public bool protect;
            public int deltotal;

            public Rule(string s, int line)
            {
                var str = "";
                var startIndex1 = 0;
                var length1 = s.IndexOf(',');
                var s1 = s.Substring(startIndex1, length1);
                var startIndex2 = length1 + 1;
                var num1 = s.IndexOf(',', startIndex2);
                var s2 = s.Substring(startIndex2, num1 - startIndex2);
                var startIndex3 = num1 + 1;
                var num2 = s.IndexOf('\t', startIndex3);
                var s3 = s.Substring(startIndex3, num2 - startIndex3);
                if (s1.Length == 0 || !isvalidstr(s1) || s1.Length >= 12)
                    str = str + string.Format("Invalid key string:line {0}\n", line);
                if (s2.Length == 0 || s2.Length >= 8 || !isvalidstr(s2) && s2 != "?")
                    str = str + string.Format("Invalid replace string:line {0}\n", line);
                if (s3.Length == 0 || flagerror(s3))
                    str = str + string.Format("Invalid flag or missing TAB after flag:line {0}\n", line);
                if (str.Length > 0)
                    throw new Exception("Rule:\n" + str);
                if (s2 == "?")
                    s2 = "";
                keystr = s1;
                repstr = s2;
                cont = s3 == "continue" || s3 == "contint";
                protect = s3 == "protect" || s3 == "protint";
                intact = s3 == "intact" || s3 == "contint" || s3 == "protint";
                deltotal = s1.Length;
                rulenum = line;
                var length2 = s2.Length;
                if (length2 <= 1 || !char.IsDigit(s2[length2 - 1]) || !char.IsDigit(s2[length2 - 2]) || cont)
                    return;
                Console.Error.WriteLine("** WARNING ** State marker may require CONTINUE:line {0}\n", line);
            }

            bool flagerror(string s)
            {
                return s != "continue" && s != "intact" && (s != "stop" && s != "protect") && s != "protint" &&
                       s != "contint";
            }

            public override string ToString()
            {
                var num = 0;
                if (intact)
                    ++num;
                if (cont)
                    num += 2;
                if (protect)
                    num += 4;
                var str = "???";
                switch (num)
                {
                    case 0:
                        str = "stop";
                        break;
                    case 1:
                        str = "intact";
                        break;
                    case 2:
                        str = "cont.";
                        break;
                    case 3:
                        str = "contint";
                        break;
                    case 4:
                        str = "prot.";
                        break;
                    case 6:
                        str = "protint";
                        break;
                }
                return string.Format("({0})->({1}) {2}", keystr, repstr, str);
            }

            public EAction apply(ref string word, bool isintact)
            {
                if (!isintact && intact)
                    return EAction.actNotapply;
                var num = word.Length - deltotal;
                var length = repstr.Length;
                if (num < 0 || num + length < 3 || !(word.Substring(num) == keystr))
                    return EAction.actNotapply;
                if (protect)
                    return EAction.actStop;
                if (length >= 2 && (char.IsDigit(repstr[length - 1]) && char.IsDigit(repstr[length - 2])))
                    length -= 2;
                if (num + length < 3)
                    return EAction.actNotapply;
                word = word.Substring(0, num);
                if (repstr != "")
                {
                    // ISSUE: explicit reference operation
                    // ISSUE: variable of a reference type
                    var local = @word;
                    // ISSUE: explicit reference operation
                    var str = local + repstr;
                    // ISSUE: explicit reference operation
                    local = str;
                }
                return cont ? EAction.actContinue : EAction.actStop;
            }

            public enum EAction
            {
                actNotapply,
                actContinue,
                actStop,
                actNotintact,
            }
        }

        internal class Ruletable
        {
            readonly List<Rule>[] buckets;

            public Ruletable(string rules)
            {
                var line = 0;
                var str1 = "";
                buckets = new List<Rule>[26];
                for (var index = 0; index < buckets.Length; ++index)
                    buckets[index] = new List<Rule>();
                var str2 = rules;
                var chArray1 = new char[1]
                    {
                        '\n'
                    };
                foreach (var str3 in str2.Split(chArray1))
                {
                    var chArray2 = new char[1]
                        {
                            '\r'
                        };
                    var s = str3.TrimEnd(chArray2);
                    if (s.Length != 0 && s[0] != 59 && s[0] != 13 && s[0] != 10)
                    {
                        ++line;
                        var rule = new Rule(s, line);
                        if (rule.keystr == "?")
                            str1 = str1 + string.Format("*** ERROR in line {0}: {1}\n", line, s);
                        else
                            AddRule(rule);
                    }
                }
                if (str1.Length > 0)
                    throw new Exception("Ruletable:\n" + str1);
            }

            void AddRule(Rule rule)
            {
                buckets[tblindex(rule.keystr)].Add(rule);
            }

            int tblindex(string s)
            {
                var ch = s[s.Length - 1];
                if (ch >= 97 && ch <= 122)
                    return ch - 97;
                else
                    return 25;
            }

            public Rule.EAction WalkRules(ref string word, bool isintact)
            {
                foreach (var rule in buckets[tblindex(word)])
                {
                    var word1 = word;
                    var eaction = rule.apply(ref word1, isintact);
                    if (eaction != Rule.EAction.actNotapply)
                    {
                        word = word1;
                        return eaction;
                    }
                }
                return Rule.EAction.actStop;
            }
        }
    }
}