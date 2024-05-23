namespace SunamoHtml;

//namespace SunamoHtml;


public partial class SHSplit
{

    public static List<string> SplitMore(string parametry, params string[] deli)
    {
        return parametry.Split(deli, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
    public static List<string> SplitBySpaceAndPunctuationCharsLeave(string veta)
    {
        List<string> vr = new List<string>();
        vr.Add("");
        foreach (var item in veta)
        {
            bool jeMezeraOrPunkce = false;
            foreach (var item2 in SHData.spaceAndPuntactionChars)
            {
                if (item == item2)
                {
                    jeMezeraOrPunkce = true;
                    break;
                }
            }

            if (jeMezeraOrPunkce)
            {
                if (vr[vr.Count - 1] == "")
                {
                    vr[vr.Count - 1] += item.ToString();
                }
                else
                {
                    vr.Add(item.ToString());
                }

                vr.Add("");
            }
            else
            {
                vr[vr.Count - 1] += item.ToString();
            }
        }
        return vr;
    }
    public static List<string> SplitAndKeepDelimiters(string originalString, List<string> ienu)
    {
        //var ienu = (IList)deli;
        var vr = Regex.Split(originalString, @"(?<=[" + string.Join("", ienu) + "])");
        return vr.ToList();
    }
    public static void RemoveWhichHaveWhitespaceAtBothSides(string s, List<int> bold)
    {
        for (int i = bold.Count - 1; i >= 0; i--)
        {
            if (char.IsWhiteSpace(s[bold[i] - 1]) && char.IsWhiteSpace(s[bold[i] + 1]))
            {
                bold.RemoveAt(i);
            }
        }

    }

    public static List<string> Split(string p, params string[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static List<string> SplitNone(string p, params string[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.None).ToList();
    }

    public static List<string> SplitNoneChar(string p, params char[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.None).ToList();
    }



    public static List<string> SplitChar(string p, params char[] newLine)
    {
        return p.Split(newLine, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static List<string> SplitByWhiteSpaces(string innerText)
    {
        return innerText.Split(AllChars.whiteSpacesChars.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    //    //public static Func<string, IList, List<string>> SplitAndKeepDelimiters;
    //    //public static Func<string, char, List<string>> SplitNoneChar;
    //    //public static Func<string, String[], List<string>> SplitMore;
    //    //public static Func<string, String, List<string>> SplitNoneString;
    //    //public static Func<string, List<string>> SplitByWhiteSpaces;
    //    //public static Func<string, List<string>> SplitBySpaceAndPunctuationCharsLeave;

}
