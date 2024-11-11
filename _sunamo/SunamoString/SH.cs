namespace SunamoHtml._sunamo.SunamoString;

internal class SH
{
    internal static string JoinNL(List<string> l)
    {
        StringBuilder sb = new();
        foreach (var item in l) sb.AppendLine(item);
        var r = string.Empty;
        r = sb.ToString();
        return r;
    }

    internal static List<string> SplitCharMore(string s, params char[] dot)
    {
        return s.Split(dot, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitMore(string s, params string[] dot)
    {
        return s.Split(dot, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitNone(string text, params string[] deli)
    {
        return text.Split(deli, StringSplitOptions.None).ToList();
    }

    internal static string NullToStringOrDefault(object n)
    {
        return n == null ? " " + "(null)" : " " + n;
    }

    internal static string TrimEnd(string name, string ext)
    {
        while (name.EndsWith(ext)) return name.Substring(0, name.Length - ext.Length);
        return name;
    }


    internal static string ShortForLettersCountThreeDotsReverse(string p, int p_2)
    {
        p = p.Trim();
        var pl = p.Length;
        var jeDelsiA1 = p_2 <= pl;


        if (jeDelsiA1)
        {
            if (IsInLastXCharsTheseLetters(p, p_2, ' '))
            {
                var dexMezery = 0;
                var d = p; //p.Substring(p.Length - zkratitO);
                var to = d.Length;

                var napocitano = 0;
                for (var i = to - 1; i >= 0; i--)
                {
                    napocitano++;

                    if (d[i] == ' ')
                    {
                        if (napocitano >= p_2) break;

                        dexMezery = i;
                    }
                }

                d = d.Substring(dexMezery + 1);
                if (d.Trim() != "") d = " ... " + d;
                return d;
                //}
            }

            return " ... " + p.Substring(p.Length - p_2);
        }

        return p;
    }

    internal static bool IsInLastXCharsTheseLetters(string p, int pl, params char[] letters)
    {
        for (var i = p.Length - 1; i >= pl; i--)
            foreach (var item in letters)
                if (p[i] == item)
                    return true;
        return false;
    }

    internal static List<FromToWord> ReturnOccurencesOfStringFromToWord(string celyObsah, params string[] hledaneSlova)
    {
        if (hledaneSlova == null || hledaneSlova.Length == 0) return new List<FromToWord>();
        celyObsah = celyObsah.ToLower();
        var vr = new List<FromToWord>();
        var l = celyObsah.Length;
        for (var i = 0; i < l; i++)
            foreach (var item in hledaneSlova)
            {
                var vsechnoStejne = true;
                var pridat = 0;
                while (pridat < item.Length)
                {
                    var dex = i + pridat;
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
                    var ftw = new FromToWord();
                    ftw.from = i;
                    ftw.to = i + pridat - 1;
                    ftw.word = item;
                    vr.Add(ftw);
                    i += pridat;
                    break;
                }
            }

        return vr;
    }

    internal static string GetFirstPartByLocation(string p1, char deli)
    {
        var dx = p1.IndexOf(deli);

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
                za = text.Substring(pozice + 1);
            else
                za = string.Empty;
        }
    }

    internal static string GetFirstPartByLocation(string p1, int dx)
    {
        string p, z;
        p = p1;

        if (dx < p1.Length) GetPartsByLocation(out p, out z, p1, dx);

        return p;
    }

    private static bool IsInFirstXCharsTheseLetters(string p, int pl, params char[] letters)
    {
        for (var i = 0; i < pl; i++)
            foreach (var item in letters)
                if (p[i] == item)
                    return true;
        return false;
    }

    private static string ShortForLettersCount(string p, int p_2, out bool pridatTriTecky)
    {
        pridatTriTecky = false;
        // Vše tu funguje výborně
        p = p.Trim();
        var pl = p.Length;
        var jeDelsiA1 = p_2 <= pl;


        if (jeDelsiA1)
        {
            if (IsInFirstXCharsTheseLetters(p, p_2, ' '))
            {
                var dexMezery = 0;
                var d = p; //p.Substring(p.Length - zkratitO);
                var to = d.Length;

                var napocitano = 0;
                for (var i = 0; i < to; i++)
                {
                    napocitano++;

                    if (d[i] == ' ')
                    {
                        if (napocitano >= p_2) break;

                        dexMezery = i;
                    }
                }

                d = d.Substring(0, dexMezery + 1);
                if (d.Trim() != "") pridatTriTecky = true;
                //d = d ;
                return d;
                //}
            }

            pridatTriTecky = true;
            return p.Substring(0, p_2);
        }

        return p;
    }

    internal static string ShortForLettersCount(string p, int p_2)
    {
        var pridatTriTecky = false;
        return ShortForLettersCount(p, p_2, out pridatTriTecky);
    }

    internal static string GetToFirst(string input, string searchFor)
    {
        var indexOfChar = input.IndexOf(searchFor);
        if (indexOfChar != -1) return input.Substring(0, indexOfChar + 1);
        return input;
    }

    internal static string GetTextBetweenSimple(string p, string after, string before,
        bool throwExceptionIfNotContains = true)
    {
        var dxOfFounded = int.MinValue;
        var t = GetTextBetween(p, after, before, out dxOfFounded, 0, throwExceptionIfNotContains);
        return t;
    }

    internal static string GetTextBetween(string p, string after, string before, out int dxOfFounded,
        int startSearchingAt, bool throwExceptionIfNotContains = true)
    {
        string vr = null;
        dxOfFounded = p.IndexOf(after, startSearchingAt);
        var p3 = p.IndexOf(before, dxOfFounded + after.Length);
        var b2 = dxOfFounded != -1;
        var b3 = p3 != -1;
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
                ThrowEx.NotContains(p, after, before);
            else
                // 24-1-21 return null instead of p
                return null;
            //vr = p;
        }

        return vr.Trim();
    }

    internal static string XCharsBeforeAndAfterWholeWords(string celyObsah, int stred, int naKazdeStrane)
    {
        var prava = new StringBuilder();
        var slovo = new StringBuilder();

        // Teď to samé udělám i pro levou stranu
        var leva = new StringBuilder();
        for (var i = stred - 1; i >= 0; i--)
        {
            var ch = celyObsah[i];
            if (ch == ' ')
            {
                var ts = slovo.ToString();
                slovo.Clear();
                if (ts != "")
                {
                    leva.Insert(0, ts + " ");
                    if (leva.Length + " ".Length + ts.Length > naKazdeStrane) break;
                }
            }
            else
            {
                slovo.Insert(0, ch);
            }
        }

        var l = slovo + " " + leva.ToString().TrimEnd(' ');
        l = l.TrimEnd(' ');
        naKazdeStrane += naKazdeStrane - l.Length;
        slovo.Clear();
        // Počítám po pravé straně započítám i to středové písmenko
        for (var i = stred; i < celyObsah.Length; i++)
        {
            var ch = celyObsah[i];
            if (ch == ' ')
            {
                var ts = slovo.ToString();
                slovo.Clear();
                if (ts != "")
                {
                    prava.Append(" " + ts);
                    if (prava.Length + " ".Length + ts.Length > naKazdeStrane) break;
                }
            }
            else
            {
                slovo.Append(ch);
            }
        }

        var p = prava.ToString().TrimStart(' ') + " " + slovo;
        p = p.TrimStart(' ');
        var vr = "";
        if (celyObsah.Contains(l + " ") && celyObsah.Contains(" " + p))
            vr = l + "" + p;
        else
            vr = l + p;
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
        var i = 0;

        foreach (var item in v)
            if (char.IsNumber(item))
            {
                i++;
            }
            else if (item == '.')
            {
                if (i > 0) return true;
            }
            else
            {
                return false;
            }

        return false;
    }

    internal static string FirstCharUpper(string nazevPP)
    {
        if (nazevPP.Length == 1) return nazevPP.ToUpper();

        var sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + sb;
    }

    internal static string FirstCharOfEveryWordUpperDash(string v)
    {
        return FirstCharOfEveryWordUpper(v, '-');
    }


    /// <summary>
    ///     Return joined with space
    /// </summary>
    /// <param name="v"></param>
    /// <param name="dash"></param>
    private static string FirstCharOfEveryWordUpper(string v, char dash)
    {
        var p = SHSplit.SplitCharMore(v, dash);
        for (var i = 0; i < p.Count; i++) p[i] = FirstCharUpper(p[i]);
        //p = CAChangeContent.ChangeContent0(null, p, FirstCharUpper);
        return string.Join(" ", p);
    }

    internal static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, '?', '*');
    }

    private static bool IsMatchRegex(string str, string pat, char singleWildcard, char multipleWildcard)
    {
        // If I compared .vs with .vs, return false before
        if (str == pat) return true;

        var escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        var escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pat = Regex.Escape(pat);
        pat = pat.Replace(escapedSingle, ".");
        pat = "^" + pat.Replace(escapedMultiple, ".*") + "$";
        var reg = new Regex(pat);
        return reg.IsMatch(str);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWithChar(string value, char v, bool _trimWrapping = false,
        bool alsoIfIsWhitespaceOrEmpty = true)
    {
        if (string.IsNullOrWhiteSpace(value) && !alsoIfIsWhitespaceOrEmpty) return string.Empty;

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
    ///     IndexesOfChars - char
    ///     ReturnOccurencesOfString - string
    /// </summary>
    /// <param name="input"></param>
    /// <param name="whiteSpaceChars"></param>
    /// <returns></returns>
    internal static List<int> IndexesOfCharsList(string input, List<char> whiteSpaceChars)
    {
        var dx = new List<int>();
        foreach (var item in whiteSpaceChars) dx.AddRange(ReturnOccurencesOfString(input, item.ToString()));
        dx.Sort();
        return dx;
    }

    internal static string NormalizeString(string s)
    {
        if (s.Contains((char)160))
        {
            var sb = new StringBuilder();
            foreach (var item in s)
                if (item == (char)160)
                    sb.Append(' ');
                else
                    sb.Append(item);
            return sb.ToString();
        }

        return s;
    }

    internal static List<int> ReturnOccurencesOfString(string vcem, string co)
    {
        var Results = new List<int>();
        for (var Index = 0; Index < vcem.Length - co.Length + 1; Index++)
        {
            var subs = vcem.Substring(Index, co.Length);
            ////////DebugLogger.Instance.WriteLine(subs);
            // non-breaking space. &nbsp; code 160
            // 32 space
            var ch = subs[0];
            var ch2 = co[0];


            if (subs == co)
                Results.Add(Index);
        }

        return Results;
    }
}