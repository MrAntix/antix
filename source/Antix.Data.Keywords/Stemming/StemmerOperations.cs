using System.Text;

namespace Antix.Data.Keywords.Stemming
{
    public abstract class StemmerOperations
    {
        //    // current string
        protected StringBuilder Current;
        protected int Cursor;
        protected int Limit;
        protected int LimitBackward;
        protected int bra;
        protected int ket;


        protected StemmerOperations()
        {
            Current = new StringBuilder();
            setCurrent("");
        }

        //    /**
        //     * Set the current string.
        //     */
        protected void setCurrent(string value)
        {
            //           current.replace(0, current.length(), value);
            //current=current.Replace(current.ToString(), value);
            //current = StringBufferReplace(0, current.Length, current, value);
            //current = StringBufferReplace(0, value.Length, current, value);
            Current.Remove(0, Current.Length);
            Current.Append(value);
            Cursor = 0;
            Limit = Current.Length;
            LimitBackward = 0;
            bra = Cursor;
            ket = Limit;
        }

        //    /**
        //     * Get the current string.
        //     */
        protected string getCurrent()
        {
            var result = Current.ToString();
            // Make a new StringBuffer.  If we reuse the old one, and a user of
            // the library keeps a reference to the buffer returned (for example,
            // by converting it to a String in a way which doesn't force a copy),
            // the buffer size will not decrease, and we will risk wasting a large
            // amount of memory.
            // Thanks to Wolfram Esser for spotting this problem.
            //current = new  StringBuilder();
            return result;
        }

        protected void copy_from(StemmerOperations other)
        {
            Current = other.Current;
            Cursor = other.Cursor;
            Limit = other.Limit;
            LimitBackward = other.LimitBackward;
            bra = other.bra;
            ket = other.ket;
        }

        protected bool in_grouping(char[] s, int min, int max)
        {
            if (Cursor >= Limit) return false;
            //           char ch = current.charAt(cursor);
            int ch = Current[Cursor];
            if (ch > max || ch < min) return false;
            //           ch -= min;
            ch -= min;
            if ((s[ch >> 3] & (0X1 << (ch & 0X7))) == 0) return false;
            Cursor++;
            return true;
        }

        protected bool in_grouping_b(char[] s, int min, int max)
        {
            if (Cursor <= LimitBackward) return false;
            //           char ch = current.charAt(cursor - 1);
            int ch = Current[Cursor - 1];
            if (ch > max || ch < min) return false;
            ch -= min;
            if ((s[ch >> 3] & (0X1 << (ch & 0X7))) == 0) return false;
            Cursor--;
            return true;
        }

        protected bool out_grouping(char[] s, int min, int max)
        {
            if (Cursor >= Limit) return false;
            //           char ch = current.charAt(cursor);
            int ch = Current[Cursor];
            if (ch > max || ch < min)
            {
                Cursor++;
                return true;
            }
            ch -= min;
            if ((s[ch >> 3] & (0X1 << (ch & 0X7))) == 0)
            {
                Cursor++;
                return true;
            }
            return false;
        }

        protected bool out_grouping_b(char[] s, int min, int max)
        {
            if (Cursor <= LimitBackward) return false;
            //           char ch = current.charAt(cursor - 1);
            int ch = Current[Cursor - 1];
            if (ch > max || ch < min)
            {
                Cursor--;
                return true;
            }
            ch -= min;
            if ((s[ch >> 3] & (0X1 << (ch & 0X7))) == 0)
            {
                Cursor--;
                return true;
            }
            return false;
        }

        protected bool in_range(int min, int max)
        {
            if (Cursor >= Limit) return false;
            //           char ch = current.charAt(cursor);
            int ch = Current[Cursor];
            if (ch > max || ch < min) return false;
            Cursor++;
            return true;
        }

        protected bool in_range_b(int min, int max)
        {
            if (Cursor <= LimitBackward) return false;
            //           char ch = current.charAt(cursor - 1);
            int ch = Current[Cursor - 1];
            if (ch > max || ch < min) return false;
            Cursor--;
            return true;
        }

        protected bool out_range(int min, int max)
        {
            if (Cursor >= Limit) return false;
            //           char ch = current.charAt(cursor);
            int ch = Current[Cursor];
            if (!(ch > max || ch < min)) return false;
            Cursor++;
            return true;
        }

        protected bool out_range_b(int min, int max)
        {
            if (Cursor <= LimitBackward) return false;
            //           char ch = current.charAt(cursor - 1);
            int ch = Current[Cursor - 1];
            if (!(ch > max || ch < min)) return false;
            Cursor--;
            return true;
        }

        protected bool eq_s(int s_size, string s)
        {
            if (Limit - Cursor < s_size) return false;
            int i;
            for (i = 0; i != s_size; i++)
            {
                if (Current[Cursor + i] != s[i]) return false;
                //               if (current[cursor + i] != s[i]) return false;
            }
            Cursor += s_size;
            return true;
        }

        protected bool eq_s_b(int s_size, string s)
        {
            if (Cursor - LimitBackward < s_size) return false;
            int i;
            for (i = 0; i != s_size; i++)
            {
                //               if (current.charAt(cursor - s_size + i) != s.charAt(i)) return false;
                if (Current[Cursor - s_size + i] != s[i]) return false;
            }
            Cursor -= s_size;
            return true;
        }

        protected bool eq_v(StringBuilder s)
        {
            return eq_s(s.Length, s.ToString());
        }

        protected bool eq_v_b(StringBuilder s)
        {
            return eq_s_b(s.Length, s.ToString());
        }


        internal int find_among(Among[] v, int v_size)
        {
            var i = 0;
            var j = v_size;

            var c = Cursor;
            var l = Limit;

            var common_i = 0;
            var common_j = 0;

            var first_key_inspected = false;
            while (true)
            {
                var k = i + ((j - i) >> 1);
                var diff = 0;
                var common = common_i < common_j ? common_i : common_j; // smaller
                var w = v[k];
                int i2;

                for (i2 = common; i2 < w.Size; i2++)
                {
                    if (c + common == l)
                    {
                        diff = -1;
                        break;
                    }
                    diff = Current[c + common] - w.s[i2];
                    if (diff != 0) break;
                    common++;
                }
                if (diff < 0)
                {
                    j = k;
                    common_j = common;
                }
                else
                {
                    i = k;
                    common_i = common;
                }
                if (j - i <= 1)
                {
                    if (i > 0) break; // v->s has been inspected
                    if (j == i) break; // only one item in v
                    // - but now we need to go round once more to get
                    // v->s inspected. This looks messy, but is actually
                    // the optimal approach.
                    if (first_key_inspected) break;
                    first_key_inspected = true;
                }
            }
            while (true)
            {
                var w = v[i];
                if (common_i >= w.Size)
                {
                    Cursor = c + w.Size;
                    if (w.method == null) return w.result;
                    //bool res;
                    //try
                    //{
                    //    Object resobj = w.method.invoke(w.methodobject,new Object[0]);
                    //    res = resobj.toString().equals("true");
                    //}
                    //catch (InvocationTargetException e)
                    //{
                    //    res = false;
                    //    // FIXME - debug message
                    //}
                    //catch (IllegalAccessException e)
                    //{
                    //    res = false;
                    //// FIXME - debug message
                    //}
                    //cursor = c + w.s_size;
                    //if (res) return w.result;
                }
                i = w.substring_i;
                if (i < 0) return 0;
            }
        }

        //    // find_among_b is for backwards processing. Same comments apply

        internal int find_among_b(Among[] v, int v_size)
        {
            var i = 0;
            var j = v_size;
            var c = Cursor;
            var lb = LimitBackward;
            var common_i = 0;
            var common_j = 0;
            var first_key_inspected = false;
            while (true)
            {
                var k = i + ((j - i) >> 1);
                var diff = 0;
                var common = common_i < common_j ? common_i : common_j;
                var w = v[k];
                int i2;
                for (i2 = w.Size - 1 - common; i2 >= 0; i2--)
                {
                    if (c - common == lb)
                    {
                        diff = -1;
                        break;
                    }
                    //                   diff = current.charAt(c - 1 - common) - w.s[i2];
                    diff = Current[c - 1 - common] - w.s[i2];
                    if (diff != 0) break;
                    common++;
                }
                if (diff < 0)
                {
                    j = k;
                    common_j = common;
                }
                else
                {
                    i = k;
                    common_i = common;
                }
                if (j - i <= 1)
                {
                    if (i > 0) break;
                    if (j == i) break;
                    if (first_key_inspected) break;
                    first_key_inspected = true;
                }
            }
            while (true)
            {
                var w = v[i];
                if (common_i >= w.Size)
                {
                    Cursor = c - w.Size;
                    if (w.method == null) return w.result;
                }
                i = w.substring_i;
                if (i < 0) return 0;
            }
        }

        //    /* to replace chars between c_bra and c_ket in current by the
        //     * chars in s.
        //     */
        protected int replace_s(int c_bra, int c_ket, string s)
        {
            var adjustment = s.Length - (c_ket - c_bra);
            //           current.replace(c_bra, c_ket, s);
            Current = StringBufferReplace(c_bra, c_ket, Current, s);
            Limit += adjustment;
            if (Cursor >= c_ket) Cursor += adjustment;
            else if (Cursor > c_bra) Cursor = c_bra;
            return adjustment;
        }

        StringBuilder StringBufferReplace(int start, int end, StringBuilder s, string s1)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < start; i++)
            {
                sb.Insert(sb.Length, s[i]);
            }
            //           for (int i = 1; i < end - start + 1; i++)
            //           {
            sb.Insert(sb.Length, s1);
            //           }
            for (var i = end; i < s.Length; i++)
            {
                sb.Insert(sb.Length, s[i]);
            }

            return sb;
            //string temp = s.ToString();
            //temp = temp.Substring(start - 1, end - start + 1);
            //s = s.Replace(temp, s1, start - 1, end - start + 1);
            //return s;
        }

        protected void slice_check()
        {
            if (bra < 0 ||
                bra > ket ||
                ket > Limit ||
                Limit > Current.Length) // this line could be removed
            {
                //System.err.println("faulty slice operation");
                // FIXME: report error somehow.
                /*
                    fprintf(stderr, "faulty slice operation:\n");
                    debug(z, -1, 0);
                    exit(1);
                    */
            }
        }

        protected void slice_from(string s)
        {
            slice_check();
            replace_s(bra, ket, s);
        }

        protected void slice_from(StringBuilder s)
        {
            slice_from(s.ToString());
        }

        protected void slice_del()
        {
            slice_from("");
        }

        protected void insert(int c_bra, int c_ket, string s)
        {
            var adjustment = replace_s(c_bra, c_ket, s);
            if (c_bra <= bra) bra += adjustment;
            if (c_bra <= ket) ket += adjustment;
        }

        protected void insert(int c_bra, int c_ket, StringBuilder s)
        {
            insert(c_bra, c_ket, s.ToString());
        }

        //    /* Copy the slice into the supplied StringBuffer */
        protected StringBuilder slice_to(StringBuilder s)
        {
            slice_check();
            var len = ket - bra;

            return StringBufferReplace(0, s.Length, s, Current.ToString().Substring(bra, len));
        }

        protected StringBuilder assign_to(StringBuilder s)
        {
            return StringBufferReplace(0, s.Length, s, Current.ToString().Substring(0, Limit));
        }
    }
}