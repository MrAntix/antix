namespace Antix.Data.Keywords.Stemming
{
    internal class Among
    {
        public readonly int Size; /* search string */
        public readonly char[] s; /* search string */
        public readonly int substring_i; /* index to longest matching substring */
        public readonly int result; /* result of the lookup */

        public delegate bool boolDel();

        public readonly boolDel method; /* method to use if substring matches */

        public Among(string s, int substring_i, int result, boolDel linkMethod)
        {
            Size = s.Length;
            this.s = s.ToCharArray();
            this.substring_i = substring_i;
            this.result = result;
            method = linkMethod;
        }
    }
}