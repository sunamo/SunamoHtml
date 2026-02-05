namespace SunamoHtml.Html;

/// <summary>
/// Helper class for HTML text manipulation (tag replacement, tag parsing, syntax detection, etc.).
/// </summary>
public class HtmlHelperText
{
    private const string regexHtmlTag = "<[^<>]+>";

    /// <summary>
    /// Replaces all occurrences of a pair tag with another tag.
    /// </summary>
    /// <param name="text">The HTML input string.</param>
    /// <param name="tag">The tag to replace.</param>
    /// <param name="replacement">The replacement tag name.</param>
    /// <returns>HTML with replaced tags.</returns>
    public static string ReplacePairTag(string text, string tag, string replacement)
    {
        text = text.Replace("<" + tag + ">", "<" + replacement + ">");
        text = text.Replace("<" + tag + " ", "<" + replacement + " ");
        text = text.Replace("</" + tag + ">", "</" + replacement + ">");
        return text;
    }

    /// <summary>
    /// Inserts missing ending tags for unclosed start tags.
    /// </summary>
    /// <param name="text">The HTML text to fix.</param>
    /// <param name="tag">The tag name to check for missing ending tags.</param>
    /// <returns>HTML with missing ending tags inserted.</returns>
    public static string InsertMissingEndingTags(string text, string tag)
    {
        var textBuilder = new StringBuilder(text);

        var start = SH.ReturnOccurencesOfString(text, "<" + tag);
        var endingTag = "</" + tag + ">";
        var ends = SH.ReturnOccurencesOfString(text, endingTag);

        var startC = start.Count;
        var endsC = ends.Count;

        if (start.Count > ends.Count)
        {
            // In keys are start, in value end. If end isnt, then -1
            var se = new Dictionary<int, int>();

            for (var i = start.Count - 1; i >= 0; i--)
            {
                var startActual = start[i];

                var endDx = -1;
                if (ends.Count != 0)
                    endDx = ends.Count - 1;
                var endActual = -1;
                if (endDx != -1)
                    endActual = ends[endDx];
                if (startActual > endActual)
                {
                    se.Add(startActual, -1);
                }
                else
                {
                    se.Add(startActual, endActual);
                    ends.RemoveAt(endDx);
                }
            }

            foreach (var item in se)
                if (item.Value == -1)
                {
                    var dexEndOfStart = textBuilder.ToString().IndexOf('>', item.Key);

                    var space = textBuilder.ToString().IndexOf(' ', dexEndOfStart);

                    if (space != -1)
                        textBuilder.Insert(space, endingTag);
                }
        }

        return textBuilder.ToString();
    }

    /// <summary>
    /// Creates H2 tags from numbered list items in the lines.
    /// </summary>
    /// <param name="lines">List of lines to process.</param>
    /// <returns>List with numbered items wrapped in H2 tags.</returns>
    public static List<string> CreateH2FromNumberedList(List<string> lines)
    {
        for (var i = 0; i < lines.Count; i++)
            lines[i] = lines[i].Trim();

        for (var i = 0; i < lines.Count; i++)
            if (SH.IsNumbered(lines[i]))
                lines[i] = WrapWith(lines[i], "h2");

        return lines;
    }

    /// <summary>
    /// Gets all equivalent variations of a non-pairing tag.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <returns>List of tag variations: &lt;tag>, &lt;tag />, &lt;tag/>.</returns>
    public static List<string> GetAllEquvivalentsOfNonPairingTag(string tagName)
    {
        return new List<string>(["<" + tagName + ">", "<" + tagName + " />", "<" + tagName + "/>"]);
    }

    /// <summary>
    /// Wraps text with specified HTML tag.
    /// </summary>
    /// <param name="text">The text to wrap.</param>
    /// <param name="tagName">The tag name (e.g., "p", "title", etc.).</param>
    /// <returns>Text wrapped in opening and closing tags.</returns>
    private static string WrapWith(string text, string tagName)
    {
        return "<" + tagName + ">" + text + "</" + tagName + ">";
    }

    /// <summary>
    /// Removes all HTML nodes from the HTML string, leaving only text nodes.
    /// </summary>
    /// <param name="htmlText">The HTML string to process.</param>
    /// <returns>HTML with all element nodes removed.</returns>
    public static string RemoveAllNodes(string htmlText)
    {
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        htmlDocument.LoadHtml(htmlText);

        var nodes = htmlDocument.DocumentNode.Descendants().ToList();
        for (var i = 0; i < nodes.Count(); i++)
        {
            var node = nodes[i];

            if (node.NodeType != HtmlNodeType.Text)
            {
                if (node.ParentNode.NodeType != HtmlNodeType.Document)
                    node.ParentNode.Remove();
                else
                    node.Remove();
            }
        }

        return htmlDocument.DocumentNode.OuterHtml;
    }

    /// <summary>
    /// Gets text between HTML tags and extracts tag attributes.
    /// </summary>
    /// <param name="html">The HTML string to search in.</param>
    /// <param name="scriptS">The starting tag.</param>
    /// <param name="scriptE">The ending tag.</param>
    /// <param name="isThrowExceptionIfNotContains">Whether to throw exception if tags not found.</param>
    /// <returns>Tuple with text content and attributes string.</returns>
    public static Tuple<string, string> GetTextBetweenTags(string html, string scriptS, string scriptE,
        bool isThrowExceptionIfNotContains = true)
    {
        if (scriptS.EndsWith(">"))
            return new Tuple<string, string>(SH.GetTextBetweenSimple(html, scriptS, scriptE, isThrowExceptionIfNotContains),
                "");
        var sc = html.IndexOf(scriptS);
        if (sc == -1)
            return new Tuple<string, string>(" ", "");

        var ending = html.IndexOf('>', sc);
        var element = html.IndexOf(scriptE, ending);

        var result = SH.GetTextBetweenTwoCharsInts(html, ending, element);

        var from = sc + scriptS.Length - 1;
        var to = ending;

        var attrs = string.Empty;

        if (from < to)
        {
            attrs = SH.GetTextBetweenTwoCharsInts(html, from, to);
        }

        return new Tuple<string, string>(result, attrs);
    }

    /// <summary>
    /// Gets all HTML tags from the input using regex.
    /// </summary>
    /// <param name="text">The HTML input string.</param>
    /// <returns>List of all HTML tags found.</returns>
    public static List<string> GetAllTags(string text)
    {
        var tags = Regex.Matches(text, regexHtmlTag);
        var sourceList = new List<string>();
        foreach (Match item in tags)
            sourceList.Add(item.Value);
        return sourceList;
    }

    /// <summary>
    /// Removes all HTML tags from text.
    /// </summary>
    /// <param name="clipboardText">The text with HTML tags.</param>
    /// <returns>Text with all HTML tags and spaces removed.</returns>
    public static string RemoveHtmlTags(string clipboardText)
    {
        return SHReplace.ReplaceAll(HtmlHelper.RemoveAllTags(clipboardText), " ", "");
    }

    /// <summary>
    /// Removes ASP.NET comments from HTML.
    /// </summary>
    /// <param name="html">The HTML content.</param>
    /// <returns>HTML with ASPX comments removed.</returns>
    public static string RemoveAspxComments(string html)
    {
        html = Regex.Replace(html, ConstsAspx.StartAspxComment + ".*?" + ConstsAspx.EndAspxComment, string.Empty,
            RegexOptions.Singleline);
        return html;
    }

    /// <summary>
    /// Checks if text contains any of the specified HTML tags.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <param name="allHtmlTagsWithLeftArrow">List of HTML tags to check for (with &lt;).</param>
    /// <returns>True if text contains any of the specified tags.</returns>
    public static bool ContainsTag(string text, List<string> allHtmlTagsWithLeftArrow)
    {
        foreach (var item in allHtmlTagsWithLeftArrow)
            if (text.Contains(item))
                return true;
        return false;
    }

    /// <summary>
    /// Gets the syntax type of an HTML tag (paired ended, paired not ended, non-paired).
    /// </summary>
    /// <param name="tag">The tag string to analyze (will be modified to clean tag name).</param>
    /// <returns>The HTML tag syntax type.</returns>
    public static HtmlTagSyntax GetSyntax(ref string tag)
    {
        ThrowEx.InvalidParameter((string)tag, "tag");

        tag = SH.GetToFirst((string)tag, " ");
        tag = tag.Trim().TrimStart('<').TrimEnd('>').ToLower();

        if (AllLists.HtmlNonPairTags.Contains((string)tag))
            return HtmlTagSyntax.NonPairingNotEnded;
        tag = tag.TrimEnd('/');
        if (AllLists.HtmlNonPairTags.Contains((string)tag))
            return HtmlTagSyntax.NonPairingEnded;
        if (tag[tag.Length - 1] == '/')
            return HtmlTagSyntax.End;
        return HtmlTagSyntax.Start;
    }

    /// <summary>
    /// Trims inner content of HTML-encoded text by adding spaces around &gt; and &lt;.
    /// </summary>
    /// <param name="value">The HTML-encoded text.</param>
    /// <returns>Text with spaces added around encoded brackets.</returns>
    public static string TrimInnerOfEncodedHtml(string value)
    {
        value = SHReplace.ReplaceAll(value, "&gt;", "&gt; ");
        value = SHReplace.ReplaceAll(value, "&lt;", " &lt;");
        return value;
    }

    /// <summary>
    /// Splits text by space and HTML angle brackets.
    /// </summary>
    /// <param name="shortDescription">The text to split.</param>
    /// <returns>List of split segments.</returns>
    public static List<string> SplitBySpaceAndLtGt(string shortDescription)
    {
        var f = SHSplit.Split(shortDescription, "<", ">", " ");
        return f;
    }

    /// <summary>
    /// Checks if a string is a valid HTML entity (without &amp; and ;).
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True if text is a valid HTML entity name.</returns>
    public static bool IsHtmlEntity(string text)
    {
        text = text.TrimStart('&').TrimEnd(';');
        return AllLists.HtmlEntities.Contains(text);
    }

    /// <summary>
    /// Gets the content of all occurrences of a specific tag in the text.
    /// </summary>
    /// <param name="text">The HTML text to search.</param>
    /// <param name="tagName">The tag name to extract content from.</param>
    /// <returns>List of content strings from all found tags.</returns>
    public static List<string> GetContentOfTags(string text, string tagName)
    {
        var result = new List<string>();
        var start = $"<{tagName}";
        var end = $"</{tagName}>";
        var dex = text.IndexOf(start);
        while (dex != -1)
        {
            var dexEndLetter = text.IndexOf('>', dex);

            var dex2 = text.IndexOf(start, dex + start.Length);
            var dexEnd = text.IndexOf(end, dex);

            if (dex2 != -1)
                if (dexEnd > dex2)
                    throw new Exception($"Another starting tag is before ending <{tagName}>");

            result.Add(SH.GetTextBetweenTwoCharsInts(text, dexEndLetter, dexEnd).Trim());

            dex = text.IndexOf(start, dexEnd);
        }

        return result;
    }

    /// <summary>
    /// Checks if a string is a valid CSS declaration name.
    /// </summary>
    /// <param name="decl">The CSS declaration name to check.</param>
    /// <returns>True if it's a valid CSS property name.</returns>
    public static bool IsCssDeclarationName(string decl)
    {
        if (AllLists.AllCssKeys.Contains(decl))
            return true;
        return false;
    }

    /// <summary>
    /// Converts a list of text lines to HTML with paragraphs.
    /// </summary>
    /// <param name="lines">List of text lines to convert.</param>
    /// <returns>HTML string with lines wrapped in paragraphs.</returns>
    public static string ConvertTextToHtml(List<string> lines)
    {
        lines = lines.Where(d => !string.IsNullOrWhiteSpace(d)).ToList();

        var endP = "</p>";

        CAChangeContent.ChangeContent0(null, lines, AddIntoParagraph);

        var result = SH.JoinNL(lines);
        result = result.Replace(endP, endP + "\r" + "\n");

        return result;
    }

    /// <summary>
    /// Adds text into a paragraph tag, handling special formatting for " -" prefix.
    /// </summary>
    /// <param name="text">The text to wrap in paragraph.</param>
    /// <returns>Text wrapped in paragraph or returned as-is if already has certain tags.</returns>
    private static string AddIntoParagraph(string text)
    {
        const string spaceDash = " -";

        if (text.Contains(spaceDash))
        {
            text = "<b>" + text;
            text = text.Replace(spaceDash, "</b>" + spaceDash);
        }

        if (text[0] == '<')
        {
            var tag = GetFirstTag(text).ToLower();

            if (AllLists.PairingTagsDontWrapToParagraph.Contains(tag))
                return text;
            if (tag.StartsWith("/"))
                if (AllLists.PairingTagsDontWrapToParagraph.Contains(tag.Substring(1)))
                    return text;
        }

        return WrapWith(text, "p");
    }

    /// <summary>
    /// Extracts the first tag name from HTML text.
    /// </summary>
    /// <param name="text">The HTML text.</param>
    /// <returns>The first tag name found.</returns>
    private static string GetFirstTag(string text)
    {
        var between = SH.GetTextBetweenSimple(text, "<", ">");

        if (between.Contains(" "))
            return SH.GetToFirst(between, " ");
        return between;
    }
}