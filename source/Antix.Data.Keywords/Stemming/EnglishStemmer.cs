namespace Antix.Data.Keywords.Stemming
{
    public class EnglishStemmer : StemmerOperations, IStemmer
    {
        static readonly Among[] A0 =
            {
                new Among("arsen", -1, -1, null),
                new Among("commun", -1, -1, null),
                new Among("gener", -1, -1, null)
            };


        static readonly Among[] A1 =
            {
                new Among("'", -1, 1, null),
                new Among("'s'", 0, 1, null),
                new Among("'s", -1, 1, null)
            };


        static readonly Among[] A2 =
            {
                new Among("ied", -1, 2, null),
                new Among("s", -1, 3, null),
                new Among("ies", 1, 2, null),
                new Among("sses", 1, 1, null),
                new Among("ss", 1, -1, null),
                new Among("us", 1, -1, null)
            };


        static readonly Among[] A3 =
            {
                new Among("", -1, 3, null),
                new Among("bb", 0, 2, null),
                new Among("dd", 0, 2, null),
                new Among("ff", 0, 2, null),
                new Among("gg", 0, 2, null),
                new Among("bl", 0, 1, null),
                new Among("mm", 0, 2, null),
                new Among("nn", 0, 2, null),
                new Among("pp", 0, 2, null),
                new Among("rr", 0, 2, null),
                new Among("at", 0, 1, null),
                new Among("tt", 0, 2, null),
                new Among("iz", 0, 1, null)
            };


        static readonly Among[] A4 =
            {
                new Among("ed", -1, 2, null),
                new Among("eed", 0, 1, null),
                new Among("ing", -1, 2, null),
                new Among("edly", -1, 2, null),
                new Among("eedly", 3, 1, null),
                new Among("ingly", -1, 2, null)
            };


        static readonly Among[] A5 =
            {
                new Among("anci", -1, 3, null),
                new Among("enci", -1, 2, null),
                new Among("ogi", -1, 13, null),
                new Among("li", -1, 16, null),
                new Among("bli", 3, 12, null),
                new Among("abli", 4, 4, null),
                new Among("alli", 3, 8, null),
                new Among("fulli", 3, 14, null),
                new Among("lessli", 3, 15, null),
                new Among("ousli", 3, 10, null),
                new Among("entli", 3, 5, null),
                new Among("aliti", -1, 8, null),
                new Among("biliti", -1, 12, null),
                new Among("iviti", -1, 11, null),
                new Among("tional", -1, 1, null),
                new Among("ational", 14, 7, null),
                new Among("alism", -1, 8, null),
                new Among("ation", -1, 7, null),
                new Among("ization", 17, 6, null),
                new Among("izer", -1, 6, null),
                new Among("ator", -1, 7, null),
                new Among("iveness", -1, 11, null),
                new Among("fulness", -1, 9, null),
                new Among("ousness", -1, 10, null)
            };


        static readonly Among[] A6 =
            {
                new Among("icate", -1, 4, null),
                new Among("ative", -1, 6, null),
                new Among("alize", -1, 3, null),
                new Among("iciti", -1, 4, null),
                new Among("ical", -1, 4, null),
                new Among("tional", -1, 1, null),
                new Among("ational", 5, 2, null),
                new Among("ful", -1, 5, null),
                new Among("ness", -1, 5, null)
            };


        static readonly Among[] A7 =
            {
                new Among("ic", -1, 1, null),
                new Among("ance", -1, 1, null),
                new Among("ence", -1, 1, null),
                new Among("able", -1, 1, null),
                new Among("ible", -1, 1, null),
                new Among("ate", -1, 1, null),
                new Among("ive", -1, 1, null),
                new Among("ize", -1, 1, null),
                new Among("iti", -1, 1, null),
                new Among("al", -1, 1, null),
                new Among("ism", -1, 1, null),
                new Among("ion", -1, 2, null),
                new Among("er", -1, 1, null),
                new Among("ous", -1, 1, null),
                new Among("ant", -1, 1, null),
                new Among("ent", -1, 1, null),
                new Among("ment", 15, 1, null),
                new Among("ement", 16, 1, null)
            };


        static readonly Among[] A8 =
            {
                new Among("e", -1, 1, null),
                new Among("l", -1, 2, null)
            };


        static readonly Among[] A9 =
            {
                new Among("succeed", -1, -1, null),
                new Among("proceed", -1, -1, null),
                new Among("exceed", -1, -1, null),
                new Among("canning", -1, -1, null),
                new Among("inning", -1, -1, null),
                new Among("earring", -1, -1, null),
                new Among("herring", -1, -1, null),
                new Among("outing", -1, -1, null)
            };


        static readonly Among[] A10 =
            {
                new Among("andes", -1, -1, null),
                new Among("atlas", -1, -1, null),
                new Among("bias", -1, -1, null),
                new Among("cosmos", -1, -1, null),
                new Among("dying", -1, 3, null),
                new Among("early", -1, 9, null),
                new Among("gently", -1, 7, null),
                new Among("howe", -1, -1, null),
                new Among("idly", -1, 6, null),
                new Among("lying", -1, 4, null),
                new Among("news", -1, -1, null),
                new Among("only", -1, 10, null),
                new Among("singly", -1, 11, null),
                new Among("skies", -1, 2, null),
                new Among("skis", -1, 1, null),
                new Among("sky", -1, -1, null),
                new Among("tying", -1, 5, null),
                new Among("ugly", -1, 8, null)
            };


        static readonly char[] g_v = {(char) 17, (char) 65, (char) 16, (char) 1};

        static readonly char[] g_v_WXY = {(char) 1, (char) 17, (char) 65, (char) 208, (char) 1};

        static readonly char[] g_valid_LI = {(char) 55, (char) 141, (char) 2};

        bool B_Y_found;
        int I_p2;
        int I_p1;


        void copy_from(EnglishStemmer other)
        {
            B_Y_found = other.B_Y_found;
            I_p2 = other.I_p2;
            I_p1 = other.I_p1;
            copy_from(other);
        }


        bool r_prelude()
        {
            var returnn = false;
            var subroot = false;
            int v_1;
            int v_2;
            int v_3;
            int v_4;
            int v_5;
            // (, line 25
            // unset Y_found, line 26
            B_Y_found = false;
            // do, line 27
            v_1 = Cursor;
            //        lab0:
            do
            {
                // (, line 27
                // [, line 27
                bra = Cursor;
                // literal, line 27
                if (!(eq_s(1, "'")))
                {
                    break;
                }
                // ], line 27
                ket = Cursor;
                // delete, line 27
                slice_del();
            } while (false);
            Cursor = v_1;
            // do, line 28
            v_2 = Cursor;
            do
            {
                // (, line 28
                // [, line 28
                bra = Cursor;
                // literal, line 28
                if (!(eq_s(1, "y")))
                {
                    break;
                }
                // ], line 28
                ket = Cursor;
                // <-, line 28
                slice_from("Y");
                // set Y_found, line 28
                B_Y_found = true;
            } while (false);
            Cursor = v_2;
            // do, line 29
            v_3 = Cursor;
            do
            {
                // repeat, line 29
                replab3:
                while (true)
                {
                    v_4 = Cursor;
                    do
                    {
                        // (, line 29
                        // goto, line 29
                        while (true)
                        {
                            v_5 = Cursor;
                            do
                            {
                                // (, line 29
                                if (!(in_grouping(g_v, 97, 121)))
                                {
                                    break;
                                }
                                // [, line 29
                                bra = Cursor;
                                // literal, line 29
                                if (!(eq_s(1, "y")))
                                {
                                    break;
                                }
                                // ], line 29
                                ket = Cursor;
                                Cursor = v_5;
                                subroot = true;
                                if (subroot) break;
                            } while (false);
                            if (subroot)
                            {
                                subroot = false;
                                break;
                            }
                            Cursor = v_5;
                            if (Cursor >= Limit)
                            {
                                subroot = true;
                                break;
                            }
                            Cursor++;
                        }
                        returnn = true;
                        if (subroot)
                        {
                            subroot = false;
                            break;
                        }
                        // <-, line 29
                        slice_from("Y");
                        // set Y_found, line 29
                        B_Y_found = true;
                        if (returnn)
                        {
                            goto replab3;
                        }
                    } while (false);
                    Cursor = v_4;
                    break;
                }
            } while (false);
            Cursor = v_3;
            return true;
        }


        bool r_mark_regions()
        {
            var subroot = false;
            int v_1;
            int v_2;
            // (, line 32
            I_p1 = Limit;
            I_p2 = Limit;
            // do, line 35
            v_1 = Cursor;
            do
            {
                // (, line 35
                // or, line 41
                do
                {
                    v_2 = Cursor;
                    do
                    {
                        // among, line 36
                        if (find_among(A0, 3) == 0)
                        {
                            break;
                        }
                        subroot = true;
                        if (subroot) break;
                    } while (false);
                    if (subroot)
                    {
                        subroot = false;
                        break;
                    }
                    Cursor = v_2;
                    // (, line 41
                    // gopast, line 41
                    while (true)
                    {
                        do
                        {
                            if (!(in_grouping(g_v, 97, 121)))
                            {
                                break;
                            }
                            subroot = true;
                            if (subroot) break;
                        } while (false);
                        if (subroot)
                        {
                            subroot = false;
                            break;
                        }
                        if (Cursor >= Limit)
                        {
                            goto breaklab0;
                        }
                        Cursor++;
                    }
                    // gopast, line 41
                    while (true)
                    {
                        do
                        {
                            if (!(out_grouping(g_v, 97, 121)))
                            {
                                break;
                            }
                            //                                    break golab5;
                            subroot = true;
                            if (subroot) break;
                        } while (false);
                        if (subroot)
                        {
                            subroot = false;
                            break;
                        }
                        if (Cursor >= Limit)
                        {
                            goto breaklab0;
                        }
                        Cursor++;
                    }
                } while (false);
                // setmark p1, line 42
                I_p1 = Cursor;
                // gopast, line 43
                while (true)
                {
                    do
                    {
                        if (!(in_grouping(g_v, 97, 121)))
                        {
                            break;
                        }
                        subroot = true;
                        if (subroot) break;
                    } while (false);
                    if (subroot)
                    {
                        subroot = false;
                        break;
                    }
                    if (Cursor >= Limit)
                    {
                        goto breaklab0;
                    }
                    Cursor++;
                }
                // gopast, line 43
                while (true)
                {
                    do
                    {
                        if (!(out_grouping(g_v, 97, 121)))
                        {
                            break;
                        }
                        subroot = true;
                        if (subroot) break;
                    } while (false);
                    if (subroot)
                    {
                        subroot = false;
                        break;
                    }
                    if (Cursor >= Limit)
                    {
                        goto breaklab0;
                    }
                    Cursor++;
                }
                // setmark p2, line 43
                I_p2 = Cursor;
            } while (false);
            breaklab0:
            Cursor = v_1;
            return true;
        }


        bool r_shortv()
        {
            var subroot = false;
            int v_1;
            // (, line 49
            // or, line 51
            //        lab0: 
            do
            {
                v_1 = Limit - Cursor;
                do
                {
                    // (, line 50
                    if (!(out_grouping_b(g_v_WXY, 89, 121)))
                    {
                        break;
                    }
                    if (!(in_grouping_b(g_v, 97, 121)))
                    {
                        break;
                    }
                    if (!(out_grouping_b(g_v, 97, 121)))
                    {
                        break;
                    }
                    subroot = true;
                    if (subroot) break;
                } while (false);
                if (subroot)
                {
                    subroot = false;
                    break;
                }
                Cursor = Limit - v_1;
                // (, line 52
                if (!(out_grouping_b(g_v, 97, 121)))
                {
                    return false;
                }
                if (!(in_grouping_b(g_v, 97, 121)))
                {
                    return false;
                }
                // atlimit, line 52
                if (Cursor > LimitBackward)
                {
                    return false;
                }
            } while (false);
            return true;
        }

        bool r_R1()
        {
            if (!(I_p1 <= Cursor))
            {
                return false;
            }
            return true;
        }

        bool r_R2()
        {
            if (!(I_p2 <= Cursor))
            {
                return false;
            }
            return true;
        }


        bool r_Step_1a()
        {
            var subroot = false;
            int among_var;
            int v_1;
            int v_2;
            // (, line 58
            // try, line 59
            v_1 = Limit - Cursor;
            do
            {
                // (, line 59
                // [, line 60
                ket = Cursor;
                // substring, line 60
                among_var = find_among_b(A1, 3);
                if (among_var == 0)
                {
                    Cursor = Limit - v_1;
                    break;
                }
                // ], line 60
                bra = Cursor;
                switch (among_var)
                {
                    case 0:
                        Cursor = Limit - v_1;
                        subroot = true;
                        break;
                    case 1:
                        // (, line 62
                        // delete, line 62
                        slice_del();
                        break;
                }
                if (subroot)
                {
                    subroot = false;
                    break;
                }
            } while (false);
            // [, line 65
            ket = Cursor;
            // substring, line 65
            among_var = find_among_b(A2, 6);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 65
            bra = Cursor;
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 66
                    // <-, line 66
                    slice_from("ss");
                    break;
                case 2:
                    // (, line 68
                    // or, line 68
                    //    lab1: 
                    do
                    {
                        v_2 = Limit - Cursor;
                        do
                        {
                            // (, line 68
                            // hop, line 68
                            {
                                var c = Cursor - 2;
                                if (LimitBackward > c || c > Limit)
                                {
                                    break;
                                }
                                Cursor = c;
                            }
                            // <-, line 68
                            slice_from("i");
                            subroot = true;
                            if (subroot) break;
                        } while (false);
                        if (subroot)
                        {
                            subroot = false;
                            break;
                        }
                        Cursor = Limit - v_2;
                        // <-, line 68
                        slice_from("ie");
                    } while (false);
                    break;
                case 3:
                    // (, line 69
                    // next, line 69
                    if (Cursor <= LimitBackward)
                    {
                        return false;
                    }
                    Cursor--;
                    // gopast, line 69
                    while (true)
                    {
                        do
                        {
                            if (!(in_grouping_b(g_v, 97, 121)))
                            {
                                break;
                            }
                            subroot = true;
                            if (subroot) break;
                        } while (false);
                        if (subroot)
                        {
                            subroot = false;
                            break;
                        }
                        if (Cursor <= LimitBackward)
                        {
                            return false;
                        }
                        Cursor--;
                    }
                    // delete, line 69
                    slice_del();
                    break;
            }
            return true;
        }


        bool r_Step_1b()
        {
            var subroot = false;
            int among_var;
            int v_1;
            int v_3;
            int v_4;
            // (, line 74
            // [, line 75
            ket = Cursor;
            // substring, line 75
            among_var = find_among_b(A4, 6);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 75
            bra = Cursor;
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 77
                    // call R1, line 77
                    if (!r_R1())
                    {
                        return false;
                    }
                    // <-, line 77
                    slice_from("ee");
                    break;
                case 2:
                    // (, line 79
                    // test, line 80
                    v_1 = Limit - Cursor;
                    // gopast, line 80
                    while (true)
                    {
                        do
                        {
                            if (!(in_grouping_b(g_v, 97, 121)))
                            {
                                break;
                            }
                            subroot = true;
                            if (subroot) break;
                        } while (false);
                        if (subroot)
                        {
                            subroot = false;
                            break;
                        }
                        if (Cursor <= LimitBackward)
                        {
                            return false;
                        }
                        Cursor--;
                    }
                    Cursor = Limit - v_1;
                    // delete, line 80
                    slice_del();
                    // test, line 81
                    v_3 = Limit - Cursor;
                    // substring, line 81
                    among_var = find_among_b(A3, 13);
                    if (among_var == 0)
                    {
                        return false;
                    }
                    Cursor = Limit - v_3;
                    switch (among_var)
                    {
                        case 0:
                            return false;
                        case 1:
                            // (, line 83
                            // <+, line 83
                            {
                                var c = Cursor;
                                insert(Cursor, Cursor, "e");
                                Cursor = c;
                            }
                            break;
                        case 2:
                            // (, line 86
                            // [, line 86
                            ket = Cursor;
                            // next, line 86
                            if (Cursor <= LimitBackward)
                            {
                                return false;
                            }
                            Cursor--;
                            // ], line 86
                            bra = Cursor;
                            // delete, line 86
                            slice_del();
                            break;
                        case 3:
                            // (, line 87
                            // atmark, line 87
                            if (Cursor != I_p1)
                            {
                                return false;
                            }
                            // test, line 87
                            v_4 = Limit - Cursor;
                            // call shortv, line 87
                            if (!r_shortv())
                            {
                                return false;
                            }
                            Cursor = Limit - v_4;
                            // <+, line 87
                            {
                                var c = Cursor;
                                insert(Cursor, Cursor, "e");
                                Cursor = c;
                            }
                            break;
                    }
                    break;
            }
            return true;
        }


        bool r_Step_1c()
        {
            var returnn = false;
            var subroot = false;
            int v_1;
            int v_2;
            // (, line 93
            // [, line 94
            ket = Cursor;
            // or, line 94
            //        lab0: 
            do
            {
                v_1 = Limit - Cursor;
                do
                {
                    // literal, line 94
                    if (!(eq_s_b(1, "y")))
                    {
                        break;
                    }
                    subroot = true;
                    if (subroot) break;
                } while (false);
                if (subroot)
                {
                    subroot = false;
                    break;
                }
                Cursor = Limit - v_1;
                // literal, line 94
                if (!(eq_s_b(1, "Y")))
                {
                    return false;
                }
            } while (false);
            // ], line 94
            bra = Cursor;
            if (!(out_grouping_b(g_v, 97, 121)))
            {
                return false;
            }
            // not, line 95
            {
                v_2 = Limit - Cursor;
                do
                {
                    returnn = true;
                    // atlimit, line 95
                    if (Cursor > LimitBackward)
                    {
                        break;
                    }
                    if (returnn)
                    {
                        return false;
                    }
                } while (false);
                Cursor = Limit - v_2;
            }
            // <-, line 96
            slice_from("i");
            return true;
        }


        bool r_Step_2()
        {
            int among_var;
            // (, line 99
            // [, line 100
            ket = Cursor;
            // substring, line 100
            among_var = find_among_b(A5, 24);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 100
            bra = Cursor;
            // call R1, line 100
            if (!r_R1())
            {
                return false;
            }
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 101
                    // <-, line 101
                    slice_from("tion");
                    break;
                case 2:
                    // (, line 102
                    // <-, line 102
                    slice_from("ence");
                    break;
                case 3:
                    // (, line 103
                    // <-, line 103
                    slice_from("ance");
                    break;
                case 4:
                    // (, line 104
                    // <-, line 104
                    slice_from("able");
                    break;
                case 5:
                    // (, line 105
                    // <-, line 105
                    slice_from("ent");
                    break;
                case 6:
                    // (, line 107
                    // <-, line 107
                    slice_from("ize");
                    break;
                case 7:
                    // (, line 109
                    // <-, line 109
                    slice_from("ate");
                    break;
                case 8:
                    // (, line 111
                    // <-, line 111
                    slice_from("al");
                    break;
                case 9:
                    // (, line 112
                    // <-, line 112
                    slice_from("ful");
                    break;
                case 10:
                    // (, line 114
                    // <-, line 114
                    slice_from("ous");
                    break;
                case 11:
                    // (, line 116
                    // <-, line 116
                    slice_from("ive");
                    break;
                case 12:
                    // (, line 118
                    // <-, line 118
                    slice_from("ble");
                    break;
                case 13:
                    // (, line 119
                    // literal, line 119
                    if (!(eq_s_b(1, "l")))
                    {
                        return false;
                    }
                    // <-, line 119
                    slice_from("og");
                    break;
                case 14:
                    // (, line 120
                    // <-, line 120
                    slice_from("ful");
                    break;
                case 15:
                    // (, line 121
                    // <-, line 121
                    slice_from("less");
                    break;
                case 16:
                    // (, line 122
                    if (!(in_grouping_b(g_valid_LI, 99, 116)))
                    {
                        return false;
                    }
                    // delete, line 122
                    slice_del();
                    break;
            }
            return true;
        }


        bool r_Step_3()
        {
            int among_var;
            // (, line 126
            // [, line 127
            ket = Cursor;
            // substring, line 127
            among_var = find_among_b(A6, 9);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 127
            bra = Cursor;
            // call R1, line 127
            if (!r_R1())
            {
                return false;
            }
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 128
                    // <-, line 128
                    slice_from("tion");
                    break;
                case 2:
                    // (, line 129
                    // <-, line 129
                    slice_from("ate");
                    break;
                case 3:
                    // (, line 130
                    // <-, line 130
                    slice_from("al");
                    break;
                case 4:
                    // (, line 132
                    // <-, line 132
                    slice_from("ic");
                    break;
                case 5:
                    // (, line 134
                    // delete, line 134
                    slice_del();
                    break;
                case 6:
                    // (, line 136
                    // call R2, line 136
                    if (!r_R2())
                    {
                        return false;
                    }
                    // delete, line 136
                    slice_del();
                    break;
            }
            return true;
        }


        bool r_Step_4()
        {
            var subroot = false;
            int among_var;
            int v_1;
            // (, line 140
            // [, line 141
            ket = Cursor;
            // substring, line 141
            among_var = find_among_b(A7, 18);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 141
            bra = Cursor;
            // call R2, line 141
            if (!r_R2())
            {
                return false;
            }
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 144
                    // delete, line 144
                    slice_del();
                    break;
                case 2:
                    // (, line 145
                    // or, line 145
                    do
                    {
                        v_1 = Limit - Cursor;
                        do
                        {
                            // literal, line 145
                            if (!(eq_s_b(1, "s")))
                            {
                                break;
                            }
                            subroot = true;
                            if (subroot) break;
                        } while (false);
                        if (subroot)
                        {
                            subroot = false;
                            break;
                        }
                        Cursor = Limit - v_1;
                        // literal, line 145
                        if (!(eq_s_b(1, "t")))
                        {
                            return false;
                        }
                    } while (false);
                    // delete, line 145
                    slice_del();
                    break;
            }
            return true;
        }


        bool r_Step_5()
        {
            var returnn = false;
            var subroot = false;
            int among_var;
            int v_1;
            int v_2;
            // (, line 149
            // [, line 150
            ket = Cursor;
            // substring, line 150
            among_var = find_among_b(A8, 2);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 150
            bra = Cursor;
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 151
                    // or, line 151
                    do
                    {
                        v_1 = Limit - Cursor;
                        do
                        {
                            // call R2, line 151
                            if (!r_R2())
                            {
                                break;
                            }
                            subroot = true;
                            if (subroot) break;
                        } while (false);
                        if (subroot)
                        {
                            subroot = false;
                            break;
                        }
                        Cursor = Limit - v_1;
                        // (, line 151
                        // call R1, line 151
                        if (!r_R1())
                        {
                            return false;
                        }
                        // not, line 151
                        {
                            v_2 = Limit - Cursor;
                            do
                            {
                                returnn = true;
                                // call shortv, line 151
                                if (!r_shortv())
                                {
                                    break;
                                }
                                if (returnn)
                                {
                                    return false;
                                }
                            } while (false);
                            Cursor = Limit - v_2;
                        }
                    } while (false);
                    // delete, line 151
                    slice_del();
                    break;
                case 2:
                    // (, line 152
                    // call R2, line 152
                    if (!r_R2())
                    {
                        return false;
                    }
                    // literal, line 152
                    if (!(eq_s_b(1, "l")))
                    {
                        return false;
                    }
                    // delete, line 152
                    slice_del();
                    break;
            }
            return true;
        }


        bool r_exception2()
        {
            // (, line 156
            // [, line 158
            ket = Cursor;
            // substring, line 158
            if (find_among_b(A9, 8) == 0)
            {
                return false;
            }
            // ], line 158
            bra = Cursor;
            // atlimit, line 158
            if (Cursor > LimitBackward)
            {
                return false;
            }
            return true;
        }


        bool r_exception1()
        {
            int among_var;
            // (, line 168
            // [, line 170
            bra = Cursor;
            // substring, line 170
            among_var = find_among(A10, 18);
            if (among_var == 0)
            {
                return false;
            }
            // ], line 170
            ket = Cursor;
            // atlimit, line 170
            if (Cursor < Limit)
            {
                return false;
            }
            switch (among_var)
            {
                case 0:
                    return false;
                case 1:
                    // (, line 174
                    // <-, line 174
                    slice_from("ski");
                    break;
                case 2:
                    // (, line 175
                    // <-, line 175
                    slice_from("sky");
                    break;
                case 3:
                    // (, line 176
                    // <-, line 176
                    slice_from("die");
                    break;
                case 4:
                    // (, line 177
                    // <-, line 177
                    slice_from("lie");
                    break;
                case 5:
                    // (, line 178
                    // <-, line 178
                    slice_from("tie");
                    break;
                case 6:
                    // (, line 182
                    // <-, line 182
                    slice_from("idl");
                    break;
                case 7:
                    // (, line 183
                    // <-, line 183
                    slice_from("gentl");
                    break;
                case 8:
                    // (, line 184
                    // <-, line 184
                    slice_from("ugli");
                    break;
                case 9:
                    // (, line 185
                    // <-, line 185
                    slice_from("earli");
                    break;
                case 10:
                    // (, line 186
                    // <-, line 186
                    slice_from("onli");
                    break;
                case 11:
                    // (, line 187
                    // <-, line 187
                    slice_from("singl");
                    break;
            }
            return true;
        }


        bool r_postlude()
        {
            var returnn = false;
            var subroot = false;
            int v_1;
            int v_2;
            // (, line 203
            // Boolean test Y_found, line 203
            if (!(B_Y_found))
            {
                return false;
            }
            // repeat, line 203
            replab0:
            while (true)
            {
                v_1 = Cursor;
                do
                {
                    // (, line 203
                    // goto, line 203
                    while (true)
                    {
                        v_2 = Cursor;
                        do
                        {
                            // (, line 203
                            // [, line 203
                            bra = Cursor;
                            // literal, line 203
                            if (!(eq_s(1, "Y")))
                            {
                                break;
                            }
                            // ], line 203
                            ket = Cursor;
                            Cursor = v_2;
                            subroot = true;
                            if (subroot) break;
                        } while (false);
                        if (subroot)
                        {
                            subroot = false;
                            break;
                        }
                        Cursor = v_2;
                        if (Cursor >= Limit)
                        {
                            subroot = true;
                            break;
                        }
                        Cursor++;
                    }
                    returnn = true;
                    if (subroot)
                    {
                        subroot = false;
                        break;
                    }
                    // <-, line 203
                    slice_from("y");
                    if (returnn)
                    {
                        goto replab0;
                    }
                } while (false);
                Cursor = v_1;
                break;
            }
            return true;
        }


        public bool CanStem()
        {
            var returnn = true;
            var subroot = false;
            int v_1;
            int v_2;
            int v_3;
            int v_4;
            int v_5;
            int v_6;
            int v_7;
            int v_8;
            int v_9;
            int v_10;
            int v_11;
            int v_12;
            int v_13;
            // (, line 205
            // or, line 207
            do
            {
                v_1 = Cursor;
                do
                {
                    // call exception1, line 207
                    if (!r_exception1())
                    {
                        break;
                    }
                    subroot = true;
                    if (subroot) break;
                } while (false);
                if (subroot)
                {
                    subroot = false;
                    break;
                }
                Cursor = v_1;
                do
                {
                    // not, line 208
                    {
                        v_2 = Cursor;
                        do
                        {
                            // hop, line 208
                            {
                                var c = Cursor + 3;
                                if (0 > c || c > Limit)
                                {
                                    break;
                                    ;
                                }
                                Cursor = c;
                            }
                            subroot = true;
                            if (subroot) break;
                        } while (false);
                        if (subroot)
                        {
                            subroot = false;
                            break;
                        }
                        Cursor = v_2;
                    }
                    returnn = true;
                    if (returnn) goto breaklab0;
                } while (false);
                Cursor = v_1;
                // (, line 208
                // do, line 209
                v_3 = Cursor;
                do
                {
                    // call prelude, line 209
                    if (!r_prelude())
                    {
                        break;
                    }
                } while (false);
                Cursor = v_3;
                // do, line 210
                v_4 = Cursor;
                do
                {
                    // call mark_regions, line 210
                    if (!r_mark_regions())
                    {
                        break;
                    }
                } while (false);
                Cursor = v_4;
                // backwards, line 211
                LimitBackward = Cursor;
                Cursor = Limit;
                // (, line 211
                // do, line 213
                v_5 = Limit - Cursor;
                do
                {
                    // call Step_1a, line 213
                    if (!r_Step_1a())
                    {
                        break;
                    }
                } while (false);
                Cursor = Limit - v_5;
                // or, line 215
                do
                {
                    v_6 = Limit - Cursor;
                    do
                    {
                        // call exception2, line 215
                        if (!r_exception2())
                        {
                            break;
                        }
                        subroot = true;
                        if (subroot) break;
                    } while (false);
                    if (subroot)
                    {
                        subroot = false;
                        break;
                    }
                    Cursor = Limit - v_6;
                    // (, line 215
                    // do, line 217
                    v_7 = Limit - Cursor;
                    do
                    {
                        // call Step_1b, line 217
                        if (!r_Step_1b())
                        {
                            break;
                        }
                    } while (false);
                    Cursor = Limit - v_7;
                    // do, line 218
                    v_8 = Limit - Cursor;
                    do
                    {
                        // call Step_1c, line 218
                        if (!r_Step_1c())
                        {
                            break;
                        }
                    } while (false);
                    Cursor = Limit - v_8;
                    // do, line 220
                    v_9 = Limit - Cursor;
                    do
                    {
                        // call Step_2, line 220
                        if (!r_Step_2())
                        {
                            break;
                        }
                    } while (false);
                    Cursor = Limit - v_9;
                    // do, line 221
                    v_10 = Limit - Cursor;
                    do
                    {
                        // call Step_3, line 221
                        if (!r_Step_3())
                        {
                            break;
                        }
                    } while (false);
                    Cursor = Limit - v_10;
                    // do, line 222
                    v_11 = Limit - Cursor;
                    do
                    {
                        // call Step_4, line 222
                        if (!r_Step_4())
                        {
                            break;
                        }
                    } while (false);
                    Cursor = Limit - v_11;
                    // do, line 224
                    v_12 = Limit - Cursor;
                    do
                    {
                        // call Step_5, line 224
                        if (!r_Step_5())
                        {
                            break;
                        }
                    } while (false);
                    Cursor = Limit - v_12;
                } while (false);
                Cursor = LimitBackward; // do, line 227
                v_13 = Cursor;
                do
                {
                    // call postlude, line 227
                    if (!r_postlude())
                    {
                        break;
                    }
                } while (false);
                Cursor = v_13;
            } while (false);
            breaklab0:
            return true;
        }

        public string Stem(string s)
        {
            setCurrent(s.ToLowerInvariant());
            CanStem();
            return getCurrent();
        }
    }
}