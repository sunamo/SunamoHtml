namespace SunamoHtml.Html;

/// <summary>
/// EN: Helper class for converting plain text to HTML with automatic anchor detection and markdown-like formatting.
/// CZ: Pomocná třída pro konverzi prostého textu do HTML s automatickou detekcí odkazů a markdown-like formátováním.
/// </summary>
public class HtmlHelperSunamoCz
{
    /// <summary>
    /// Converts text to HTML with automatic anchor links and markdown-like formatting (*bold*, _italic_, -strike-).
    /// </summary>
    /// <param name="text">The text to convert.</param>
    /// <param name="error">Output error message if conversion fails.</param>
    /// <returns>Converted HTML string.</returns>
    public static string ConvertTextToHtmlWithAnchors(string text, ref string error)
    {
        const string li = "li";
        text = text.Replace("-" + li, "" + li);

        text = HtmlHelper.ConvertTextToHtml(text);

        text = text.Replace("<", " <");
        var data = SHSplit.SplitAndKeepDelimiters(text, new List<char>([' ', '<', '>'])
            .ConvertAll(data => data.ToString()));

        for (var i = 0; i < data.Count; i++)
        {
            var item = data[i].Trim();
            if (item.StartsWith("https://") || item.StartsWith("https://") || item.StartsWith("www."))
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

    /// <summary>
    /// Converts text to HTML with automatic anchor links for URLs.
    /// </summary>
    /// <param name="text">The text to convert.</param>
    /// <returns>Converted HTML string with anchor links.</returns>
    public static string ConvertTextToHtmlWithAnchors(string text)
    {
        var data = SHSplit.SplitNoneChar(HtmlHelper.ConvertTextToHtml(text), ' ');
        for (var i = 0; i < data.Count; i++)
            if (data[i].StartsWith("http://") || data[i].StartsWith("https://"))
                data[i] = HtmlGenerator2.AnchorWithHttp(data[i]);
        return string.Join(' ', data);
    }

    /// <summary>
    /// Adds all integer positions to the dictionary with the specified tag value.
    /// </summary>
    /// <param name="tagsDict">The dictionary to add to.</param>
    /// <param name="positions">List of positions to add.</param>
    /// <param name="tagName">The tag name value to associate with each position.</param>
    private static void AddToDict(Dictionary<int, string> tagsDict, List<int> positions, string tagName)
    {
        foreach (var item in positions)
            tagsDict.Add(item, tagName);
    }
}