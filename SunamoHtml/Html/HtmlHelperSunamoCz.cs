namespace SunamoHtml.Html;

public static class HtmlHelperSunamoCz
{
    public static string ConvertTextToHtmlWithAnchors(string text, ref string error)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        const string li = "li";
        text = text.Replace("-" + li, "" + li, StringComparison.Ordinal);

        text = HtmlHelper.ConvertTextToHtml(text);

        text = text.Replace("<", " <", StringComparison.Ordinal);
        var data = SHSplit.SplitAndKeepDelimiters(text, new List<char>([' ', '<', '>'])
            .ConvertAll(data => data.ToString()));

        for (var i = 0; i < data.Count; i++)
        {
            var item = data[i].Trim();
            if (item.StartsWith("https://", StringComparison.Ordinal) || item.StartsWith("https://", StringComparison.Ordinal) || item.StartsWith("www.", StringComparison.Ordinal))
            {
                var res = item;
                res = HtmlGenerator2.AnchorWithHttp(res);
                data[i] = " " + res + " ";
            }
        }

        text = string.Join("", data);

        var bold = new List<int>();
        bold.AddRange(SH.IndexesOfChars(text, '*'));

        var italic = SH.IndexesOfChars(text, '_');
        var strike = SH.IndexesOfChars(text, '-');

        SHSplit.RemoveWhichHaveWhitespaceAtBothSides(text, bold);
        SHSplit.RemoveWhichHaveWhitespaceAtBothSides(text, italic);
        SHSplit.RemoveWhichHaveWhitespaceAtBothSides(text, strike);

        var isOdd = false;

        foreach (var item in new List<List<int>>([bold, italic, strike]))
        {
            if (item.Count % 2 == 1)
                isOdd = true;
        }

        if (isOdd)
        {
            var cm = Exceptions.CallingMethod();
            var b2 = Exceptions.HasOddNumberOfElements(string.Empty, "bold", bold);
            var i2 = Exceptions.HasOddNumberOfElements(string.Empty, "italic", italic);
            var s2 = Exceptions.HasOddNumberOfElements(string.Empty, "strike", strike);

            var sourceList = new List<string>();
            if (b2 != null)
                sourceList.Add("bold");
            if (i2 != null)
                sourceList.Add("italic");
            if (s2 != null)
                sourceList.Add("strike");

            error = StatusPrefixes.Info + string.Join(",", sourceList) + " was odd count of elements. ";
            return text;
        }

        var bold2 = new Dictionary<int, string>();

        AddToDict(bold2, bold, "b");
        AddToDict(bold2, italic, "i");
        AddToDict(bold2, strike, "s");

        var ie = bold2.OrderBy(d2 => d2.Key);
        var id = ie.OrderByDescending(d2 => d2.Key);

        var end = true;
        foreach (var item in id)
        {
            text = text.Remove(item.Key, 1);
            if (end)
                text = text.Insert(item.Key, HtmlEndingTags.Get(item.Value));
            else
                text = text.Insert(item.Key, HtmlStartingTags.Get(item.Value));

            end = !end;
        }

        return text;
    }

    public static string ConvertTextToHtmlWithAnchors(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        var data = SHSplit.SplitNoneChar(HtmlHelper.ConvertTextToHtml(text), ' ');
        for (var i = 0; i < data.Count; i++)
            if (data[i].StartsWith("http://", StringComparison.Ordinal) || data[i].StartsWith("https://", StringComparison.Ordinal))
                data[i] = HtmlGenerator2.AnchorWithHttp(data[i]);
        return string.Join(" ", data);
    }

    private static void AddToDict(Dictionary<int, string> tagsDict, List<int> positions, string tagName)
    {
        foreach (var item in positions)
            tagsDict.Add(item, tagName);
    }
}
