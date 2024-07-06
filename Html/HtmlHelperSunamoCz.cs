namespace SunamoHtml.Html;

public class HtmlHelperSunamoCz
{
    public static string ConvertTextToHtmlWithAnchors(string p, ref string error)
    {
        const string li = "li";
        p = p.Replace(AllStrings.dash + li, AllStrings.space + li);

        p = HtmlHelper.ConvertTextToHtml(p);

        p = p.Replace("<", " <");
        var d = SHSplit.SplitAndKeepDelimiters(p, new List<char>([AllChars.space, AllChars.lt, AllChars.gt]).ConvertAll(d => d.ToString()));

        for (int i = 0; i < d.Count; i++)
        {
            var item = d[i].Trim();
            if (item.StartsWith("https://") || item.StartsWith("https://") || item.StartsWith("www."))
            {
                var res = item;
                res = HtmlGenerator2.AnchorWithHttp(res);
                d[i] = AllStrings.space + res + AllStrings.space;
            }
        }

        p = string.Join("", d);

        var bold = new List<int>();
        bold.AddRange(SH.IndexesOfChars(p, '*'));

        var italic = SH.IndexesOfChars(p, '_');
        var strike = SH.IndexesOfChars(p, '-');

        SHSplit.RemoveWhichHaveWhitespaceAtBothSides(p, bold);
        SHSplit.RemoveWhichHaveWhitespaceAtBothSides(p, italic);
        SHSplit.RemoveWhichHaveWhitespaceAtBothSides(p, strike);

        var isOdd = false;

        foreach (var item in new List<List<int>>([bold, italic, strike]))
        {
            if (item.Count % 2 == 1)
            {
                isOdd = true;
            }
        }

        if (isOdd)
        {
            var exc = Exc.GetStackTrace();
            var cm = Exc.CallingMethod();
            var b2 = Exceptions.IsOdd(string.Empty, "bold", bold);
            var i2 = Exceptions.IsOdd(string.Empty, "italic", italic);
            var s2 = Exceptions.IsOdd(string.Empty, "strike", strike);

            List<string> ls = new List<string>();
            if (b2 != null)
            {
                ls.Add("bold");
            }
            if (i2 != null)
            {
                ls.Add("italic");
            }
            if (s2 != null)
            {
                ls.Add("strike");
            }

            error = StatusPrefixes.info + string.Join(",", ls) + " was odd count of elements. ";
            return p; //HtmlAgilityHelper.WrapIntoTagIfNot(t, "b") + p;
        }

        Dictionary<int, string> bold2 = new Dictionary<int, string>();
        //Dictionary<int, int> italic2 = new Dictionary<int, int>();
        //Dictionary<int, int> strike2 = new Dictionary<int, int>();

        AddToDict(bold2, bold, "b");
        AddToDict(bold2, italic, "i");
        AddToDict(bold2, strike, "s");

        var ie = bold2.OrderBy(d2 => d2.Key);
        var id = ie.OrderByDescending(d2 => d2.Key);

        var end = true;
        foreach (var item in id)
        {


            p = p.Remove(item.Key, 1);
            if (end)
            {
                p = p.Insert(item.Key, HtmlEndingTags.Get(item.Value));
            }
            else
            {
                p = p.Insert(item.Key, HtmlStartingTags.Get(item.Value));
            }

            end = !end;
        }



        return p;
    }

    public static string ConvertTextToHtmlWithAnchors(string p)
    {
        var d = SHSplit.SplitNoneChar(HtmlHelper.ConvertTextToHtml(p), AllChars.space);
        for (int i = 0; i < d.Count; i++)
        {
            if (d[i].StartsWith("http://") || d[i].StartsWith("https://"))
            {
                d[i] = HtmlGenerator2.AnchorWithHttp(d[i]);
            }
        }
        return string.Join(AllChars.space, d);
    }

    static Type type = typeof(HtmlHelperSunamoCz);



    private static void AddToDict(Dictionary<int, string> italic2, List<int> italic, string v)
    {
        foreach (var item in italic)
        {
            italic2.Add(item, v);
        }
    }
}
