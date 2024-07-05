namespace SunamoHtml._sunamo.SunamoString;

//namespace SunamoHtml;

internal class SH
{
    internal static string ShortForLettersCountThreeDotsReverse(string p, int p_2)
    {
        p = p.Trim();
        int pl = p.Length;
        bool jeDelsiA1 = p_2 <= pl;


        if (jeDelsiA1)
        {
            if (SH.IsInLastXCharsTheseLetters(p, p_2, AllChars.space))
            {
                int dexMezery = 0;
                string d = p; //p.Substring(p.Length - zkratitO);
                int to = d.Length;

                int napocitano = 0;
                for (int i = to - 1; i >= 0; i--)
                {
                    napocitano++;

                    if (d[i] == AllChars.space)
                    {
                        if (napocitano >= p_2)
                        {
                            break;
                        }

                        dexMezery = i;
                    }
                }
                d = d.Substring(dexMezery + 1);
                if (d.Trim() != "")
                {
                    d = " ... " + d;
                }
                return d;
                //}
            }
            else
            {
                return " ... " + p.Substring(p.Length - p_2);
            }
        }

        return p;
    }

    internal static bool IsInLastXCharsTheseLetters(string p, int pl, params char[] letters)
    {
        for (int i = p.Length - 1; i >= pl; i--)
        {
            foreach (var item in letters)
            {
                if (p[i] == item)
                {
                    return true;
                }
            }
        }
        return false;
    }

    internal static List<FromToWord> ReturnOccurencesOfStringFromToWord(string celyObsah, params string[] hledaneSlova)
    {
        if (hledaneSlova == null || hledaneSlova.Length == 0)
        {
            return new List<FromToWord>();
        }
        celyObsah = celyObsah.ToLower();
        List<FromToWord> vr = new List<FromToWord>();
        int l = celyObsah.Length;
        for (int i = 0; i < l; i++)
        {
            foreach (string item in hledaneSlova)
            {
                bool vsechnoStejne = true;
                int pridat = 0;
                while (pridat < item.Length)
                {
                    int dex = i + pridat;
                    if (l > dex)
                    {
                        if (celyObsah[dex] != item[pridat])
                        {
                            vsechnoStejne = false;
                            break;
                        }
                    }
                    else
                    {
                        vsechnoStejne = false;
                        break;
                    }
                    pridat++;
                }
                if (vsechnoStejne)
                {
                    FromToWord ftw = new FromToWord();
                    ftw.from = i;
                    ftw.to = i + pridat - 1;
                    ftw.word = item;
                    vr.Add(ftw);
                    i += pridat;
                    break;
                }
            }
        }
        return vr;
    }

    internal static string GetFirstPartByLocation(string p1, char deli)
    {
        int dx = p1.IndexOf(deli);

        return GetFirstPartByLocation(p1, dx);
    }

    internal static void GetPartsByLocation(out string pred, out string za, string text, int pozice)
    {
        if (pozice == -1)
        {
            pred = text;
            za = "";
        }
        else
        {
            pred = text.Substring(0, pozice);
            if (text.Length > pozice + 1)
            {
                za = text.Substring(pozice + 1);
            }
            else
            {
                za = string.Empty;
            }
        }
    }

    internal static string GetFirstPartByLocation(string p1, int dx)
    {
        string p, z;
        p = p1;

        if (dx < p1.Length)
        {
            GetPartsByLocation(out p, out z, p1, dx);
        }

        return p;
    }
    private static bool IsInFirstXCharsTheseLetters(string p, int pl, params char[] letters)
    {
        for (int i = 0; i < pl; i++)
        {
            foreach (var item in letters)
            {
                if (p[i] == item)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static string ShortForLettersCount(string p, int p_2, out bool pridatTriTecky)
    {
        pridatTriTecky = false;
        // Vše tu funguje výborně
        p = p.Trim();
        int pl = p.Length;
        bool jeDelsiA1 = p_2 <= pl;


        if (jeDelsiA1)
        {
            if (SH.IsInFirstXCharsTheseLetters(p, p_2, AllChars.space))
            {
                int dexMezery = 0;
                string d = p; //p.Substring(p.Length - zkratitO);
                int to = d.Length;

                int napocitano = 0;
                for (int i = 0; i < to; i++)
                {
                    napocitano++;

                    if (d[i] == AllChars.space)
                    {
                        if (napocitano >= p_2)
                        {
                            break;
                        }

                        dexMezery = i;
                    }
                }
                d = d.Substring(0, dexMezery + 1);
                if (d.Trim() != "")
                {
                    pridatTriTecky = true;
                    //d = d ;
                }
                return d;
                //}
            }
            else
            {
                pridatTriTecky = true;
                return p.Substring(0, p_2);
            }
        }

        return p;
    }

    internal static string ShortForLettersCount(string p, int p_2)
    {
        bool pridatTriTecky = false;
        return ShortForLettersCount(p, p_2, out pridatTriTecky);
    }
    internal static string GetToFirst(string input, string searchFor)
    {
        int indexOfChar = input.IndexOf(searchFor);
        if (indexOfChar != -1)
        {
            return input.Substring(0, indexOfChar + 1);
        }
        return input;
    }
    internal static string GetTextBetweenSimple(string p, string after, string before, bool throwExceptionIfNotContains = true)
    {
        int dxOfFounded = int.MinValue;
        var t = GetTextBetween(p, after, before, out dxOfFounded, 0, throwExceptionIfNotContains);
        return t;
    }

    internal static string GetTextBetween(string p, string after, string before, out int dxOfFounded, int startSearchingAt, bool throwExceptionIfNotContains = true)
    {
        string vr = null;
        dxOfFounded = p.IndexOf(after, startSearchingAt);
        int p3 = p.IndexOf(before, dxOfFounded + after.Length);
        bool b2 = dxOfFounded != -1;
        bool b3 = p3 != -1;
        if (b2 && b3)
        {
            dxOfFounded += after.Length;
            p3 -= 1;
            // When I return between ( ), there must be +1
            var length = p3 - dxOfFounded + 1;
            if (length < 1)
            {
                // Takhle to tu bylo předtím ale logicky je to nesmysl.
                //return p;
            }
            vr = p.Substring(dxOfFounded, length).Trim();
        }
        else
        {
            if (throwExceptionIfNotContains)
            {
                ThrowEx.NotContains(p, after, before);
            }
            else
            {
                // 24-1-21 return null instead of p
                return null;
                //vr = p;
            }
        }

        return vr.Trim();
    }

    internal static string XCharsBeforeAndAfterWholeWords(string celyObsah, int stred, int naKazdeStrane)
    {
        StringBuilder prava = new StringBuilder();
        StringBuilder slovo = new StringBuilder();

        // Teď to samé udělám i pro levou stranu
        StringBuilder leva = new StringBuilder();
        for (int i = stred - 1; i >= 0; i--)
        {
            char ch = celyObsah[i];
            if (ch == AllChars.space)
            {
                string ts = slovo.ToString();
                slovo.Clear();
                if (ts != "")
                {
                    leva.Insert(0, ts + AllStrings.space);
                    if (leva.Length + AllStrings.space.Length + ts.Length > naKazdeStrane)
                    {
                        break;
                    }
                    else
                    {
                    }
                }
            }
            else
            {
                slovo.Insert(0, ch);
            }
        }
        string l = slovo.ToString() + AllStrings.space + leva.ToString().TrimEnd(AllChars.space);
        l = l.TrimEnd(AllChars.space);
        naKazdeStrane += naKazdeStrane - l.Length;
        slovo.Clear();
        // Počítám po pravé straně započítám i to středové písmenko
        for (int i = stred; i < celyObsah.Length; i++)
        {
            char ch = celyObsah[i];
            if (ch == AllChars.space)
            {
                string ts = slovo.ToString();
                slovo.Clear();
                if (ts != "")
                {
                    prava.Append(AllStrings.space + ts);
                    if (prava.Length + AllStrings.space.Length + ts.Length > naKazdeStrane)
                    {
                        break;
                    }
                    else
                    {
                    }
                }
            }
            else
            {
                slovo.Append(ch);
            }
        }

        string p = prava.ToString().TrimStart(AllChars.space) + AllStrings.space + slovo.ToString();
        p = p.TrimStart(AllChars.space);
        string vr = "";
        if (celyObsah.Contains(l + AllStrings.space) && celyObsah.Contains(AllStrings.space + p))
        {
            vr = l + AllStrings.space + p;
        }
        else
        {
            vr = l + p;
        }
        return vr;
    }

    internal static string GetTextBetweenTwoCharsInts(string p, int begin, int end)
    {
        if (end > begin)
            // a(1) - 1,3
            return p.Substring(begin + 1, end - begin - 1);
        // originally
        //return p.Substring(begin+1, end - begin - 1);
        return p;
    }

    internal static bool IsNumbered(string v)
    {
        int i = 0;

        foreach (var item in v)
        {
            if (char.IsNumber(item))
            {
                i++;
                continue;
            }
            else if (item == AllChars.dot)
            {
                if (i > 0)
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        return false;
    }
    internal static string FirstCharUpper(string nazevPP)
    {
        if (nazevPP.Length == 1)
        {
            return nazevPP.ToUpper();
        }

        string sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + sb;
    }
    internal static string FirstCharOfEveryWordUpperDash(string v)
    {
        return FirstCharOfEveryWordUpper(v, AllChars.dash);
    }



    /// <summary>
    /// Return joined with space
    /// </summary>
    /// <param name="v"></param>
    /// <param name="dash"></param>
    private static string FirstCharOfEveryWordUpper(string v, char dash)
    {
        var p = SHSplit.SplitChar(v, dash);
        for (int i = 0; i < p.Count; i++)
        {
            p[i] = SH.FirstCharUpper(p[i]);
        }
        //p = CAChangeContent.ChangeContent0(null, p, FirstCharUpper);
        return string.Join(AllStrings.space, p);
    }

    internal static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, AllChars.q, AllChars.asterisk);
    }

    private static bool IsMatchRegex(string str, string pat, char singleWildcard, char multipleWildcard)
    {
        // If I compared .vs with .vs, return false before
        if (str == pat)
        {
            return true;
        }

        string escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        string escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pat = Regex.Escape(pat);
        pat = pat.Replace(escapedSingle, AllStrings.dot);
        pat = "^" + pat.Replace(escapedMultiple, ".*") + "$";
        Regex reg = new Regex(pat);
        return reg.IsMatch(str);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWithChar(string value, char v, bool _trimWrapping = false, bool alsoIfIsWhitespaceOrEmpty = true)
    {
        if (string.IsNullOrWhiteSpace(value) && !alsoIfIsWhitespaceOrEmpty)
        {
            return string.Empty;
        }

        // TODO: Make with StringBuilder, because of WordAfter and so
        return WrapWith(_trimWrapping ? value.Trim() : value, v.ToString());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWith(string value, string h)
    {
        return h + value + h;
    }

    //    internal static Func<string, string, string> ReplaceWhiteSpacesWithoutSpacesWithReplaceWith;
    //    internal static Func<string, string, string, string> ReplaceAll2;

    //    internal static Func<string, List<string>> GetLines;
    //    internal static Func<List<string>, string> JoinNL;
    //    internal static Func<string, int, int, string> GetTextBetweenTwoCharsInts;

    //    internal static Func<string, char, List<int>> IndexesOfChars;
    //    internal static Action<string, List<int>> RemoveWhichHaveWhitespaceAtBothSides;
    //    internal static Func<string, string, List<int>> ReturnOccurencesOfString;
    //    internal static Func<string, bool> IsNumbered;
    //    internal static Func<string, string, string> GetToFirst;
    //    internal static Func<string, string> FirstCharOfEveryWordUpperDash;
    //    internal static Func<string, int, string> ShortForLettersCount;
    //    internal static Func<string, string, string, string> ReplaceOnce;

    //    internal static Func<string, string, string, string> ReplaceAll;
    //    internal static Func<string, string, string[], string> ReplaceAllArray;
    //    internal static Func<string, char, bool, bool, string> WrapWithChar;
    //    internal static Func<string, string> ReplaceAllDoubleSpaceToSingle;

    //    internal static Func<string, int, int, string> XCharsBeforeAndAfterWholeWords;

    //    internal static Func<string, int, string> ShortForLettersCountThreeDotsReverse;
    //    internal static Func<string, string, bool> MatchWildcard;
    //    internal static Func<string, string, List<string>> Split;
    //    internal static Func<string, string[], List<FromToWord>> ReturnOccurencesOfStringFromToWord;
    //    internal static Func<string, char, string> GetFirstPartByLocation;
    //    internal static Func<string, string, string, bool, string> GetTextBetweenSimple;

    internal static List<int> IndexesOfChars(string input, char ch)
    {
        return IndexesOfCharsList(input, new List<char>(ch));
    }

    /// <summary>
    /// IndexesOfChars - char
    /// ReturnOccurencesOfString - string
    /// </summary>
    /// <param name="input"></param>
    /// <param name="whiteSpacesChars"></param>
    /// <returns></returns>
    internal static List<int> IndexesOfCharsList(string input, List<char> whiteSpacesChars)
    {
        var dx = new List<int>();
        foreach (var item in whiteSpacesChars)
        {
            dx.AddRange(SH.ReturnOccurencesOfString(input, item.ToString()));
        }
        dx.Sort();
        return dx;
    }

    internal static string NormalizeString(string s)
    {
        if (s.Contains(AllChars.nbsp))
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in s)
            {
                if (item == AllChars.nbsp)
                {
                    sb.Append(AllChars.space);
                }
                else
                {
                    sb.Append(item);
                }
            }
            return sb.ToString();
        }

        return s;
    }

    internal static List<int> ReturnOccurencesOfString(string vcem, string co)
    {

        List<int> Results = new List<int>();
        for (int Index = 0; Index < (vcem.Length - co.Length) + 1; Index++)
        {
            var subs = vcem.Substring(Index, co.Length);
            ////////DebugLogger.Instance.WriteLine(subs);
            // non-breaking space. &nbsp; code 160
            // 32 space
            char ch = subs[0];
            char ch2 = co[0];
            if (subs == AllStrings.space)
            {
            }
            if (subs == co)
                Results.Add(Index);
        }
        return Results;
    }
}
