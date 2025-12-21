namespace SunamoHtml._sunamo.SunamoString;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
internal partial class SH
{
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
                    if (leva.Length + " ".Length + ts.Length > naKazdeStrane)
                        break;
                }
            }
            else
            {
                slovo.Insert(0, ch);
            }
        }

        var list = slovo + " " + leva.ToString().TrimEnd(' ');
        list = list.TrimEnd(' ');
        naKazdeStrane += naKazdeStrane - list.Length;
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
                    if (prava.Length + " ".Length + ts.Length > naKazdeStrane)
                        break;
                }
            }
            else
            {
                slovo.Append(ch);
            }
        }

        var parameter = prava.ToString().TrimStart(' ') + " " + slovo;
        parameter = parameter.TrimStart(' ');
        var vr = "";
        if (celyObsah.Contains(list + " ") && celyObsah.Contains(" " + parameter))
            vr = list + "" + parameter;
        else
            vr = list + parameter;
        return vr;
    }

    internal static string GetTextBetweenTwoCharsInts(string parameter, int begin, int end)
    {
        if (end > begin)
            // a(1) - 1,3
            return parameter.Substring(begin + 1, end - begin - 1);
        // originally
        //return parameter.Substring(begin+1, end - begin - 1);
        return parameter;
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
                if (i > 0)
                    return true;
            }
            else
            {
                return false;
            }

        return false;
    }

    internal static string FirstCharUpper(string nazevPP)
    {
        if (nazevPP.Length == 1)
            return nazevPP.ToUpper();
        var stringBuilder = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToUpper() + stringBuilder;
    }

    internal static string FirstCharOfEveryWordUpperDash(string v)
    {
        return FirstCharOfEveryWordUpper(v, '-');
    }

    /// <summary>
    ///     Return joined with space
    /// </summary>
    /// <param name = "v"></param>
    /// <param name = "dash"></param>
    private static string FirstCharOfEveryWordUpper(string v, char dash)
    {
        var parameter = SHSplit.SplitChar(v, dash);
        for (var i = 0; i < parameter.Count; i++)
            parameter[i] = FirstCharUpper(parameter[i]);
        //p = CAChangeContent.ChangeContent0(null, parameter, FirstCharUpper);
        return string.Join(" ", parameter);
    }

    internal static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, '?', '*');
    }

    private static bool IsMatchRegex(string str, string pat, char singleWildcard, char multipleWildcard)
    {
        // If I compared .vs with .vs, return false before
        if (str == pat)
            return true;
        var escapedSingle = Regex.Escape(new string (singleWildcard, 1));
        var escapedMultiple = Regex.Escape(new string (multipleWildcard, 1));
        pat = Regex.Escape(pat);
        pat = pat.Replace(escapedSingle, ".");
        pat = "^" + pat.Replace(escapedMultiple, ".*") + "$";
        var reg = new Regex(pat);
        return reg.IsMatch(str);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWithChar(string value, char v, bool _trimWrapping = false, bool alsoIfIsWhitespaceOrEmpty = true)
    {
        if (string.IsNullOrWhiteSpace(value) && !alsoIfIsWhitespaceOrEmpty)
            return string.Empty;
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
    /// <param name = "input"></param>
    /// <param name = "whiteSpaceChars"></param>
    /// <returns></returns>
    internal static List<int> IndexesOfCharsList(string input, List<char> whiteSpaceChars)
    {
        var dx = new List<int>();
        foreach (var item in whiteSpaceChars)
            dx.AddRange(ReturnOccurencesOfString(input, item.ToString()));
        dx.Sort();
        return dx;
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