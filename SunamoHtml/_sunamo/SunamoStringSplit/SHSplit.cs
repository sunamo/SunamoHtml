namespace SunamoHtml._sunamo.SunamoStringSplit;

internal class SHSplit
{
    internal static List<string> SplitBySpaceAndPunctuationCharsLeave(string veta)
    {
        var vr = new List<string>();
        vr.Add("");
        foreach (var item in veta)
        {
            var jeMezeraOrPunkce = false;
            foreach (var item2 in SHData.spaceAndPuntactionChars)
                if (item == item2)
                {
                    jeMezeraOrPunkce = true;
                    break;
                }

            if (jeMezeraOrPunkce)
            {
                if (vr[vr.Count - 1] == "")
                    vr[vr.Count - 1] += item.ToString();
                else
                    vr.Add(item.ToString());

                vr.Add("");
            }
            else
            {
                vr[vr.Count - 1] += item.ToString();
            }
        }

        return vr;
    }

    internal static List<string> SplitAndKeepDelimiters(string originalString, List<string> ienu)
    {
        //var ienu = (IList)deli;
        var vr = Regex.Split(originalString, @"(?<=[" + string.Join("", ienu) + "])");
        return vr.ToList();
    }

    internal static void RemoveWhichHaveWhitespaceAtBothSides(string s, List<int> bold)
    {
        for (var i = bold.Count - 1; i >= 0; i--)
            if (char.IsWhiteSpace(s[bold[i] - 1]) && char.IsWhiteSpace(s[bold[i] + 1]))
                bold.RemoveAt(i);
    }

    internal static List<string> Split(string p, params string[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitNone(string p, params string[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.None).ToList();
    }

    internal static List<string> SplitNoneChar(string p, params char[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.None).ToList();
    }


    internal static List<string> SplitChar(string p, params char[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitByWhiteSpaces(string innerText)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();

        return innerText.Split(whitespaceChar.whiteSpaceChars.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    //    //internal static Func<string, IList, List<string>> SplitAndKeepDelimiters;
    //    //internal static Func<string, char, List<string>> SplitNoneChar;
    //    //internal static Func<string, String[], List<string>> Split;
    //    //internal static Func<string, String, List<string>> SplitNoneString;
    //    //internal static Func<string, List<string>> SplitByWhiteSpaces;
    //    //internal static Func<string, List<string>> SplitBySpaceAndPunctuationCharsLeave;
}