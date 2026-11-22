namespace SunamoHtml.Html;

/// <summary>
/// Represents the html helper text type.
/// </summary>
public static class HtmlHelperText
{
    private const string regexHtmlTag = "<[^<>]+>";

    /// <summary>
    /// Replace pair tag.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="replacement">The replacement.</param>
    /// <returns>The replace pair tag result.</returns>
    public static string ReplacePairTag(string text, string tag, string replacement)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        if (replacement == null) throw new ArgumentNullException(nameof(replacement));
        text = text.Replace("<" + tag + ">", "<" + replacement + ">", StringComparison.Ordinal);
        text = text.Replace("<" + tag + " ", "<" + replacement + " ", StringComparison.Ordinal);
        text = text.Replace("</" + tag + ">", "</" + replacement + ">", StringComparison.Ordinal);
        return text;
    }

    /// <summary>
    /// Insert missing ending tags.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="tag">The tag.</param>
    /// <returns>The insert missing ending tags result.</returns>
    public static string InsertMissingEndingTags(string text, string tag)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        if (tag == null) throw new ArgumentNullException(nameof(tag));
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
    /// Create h 2 from numbered list.
    /// </summary>
    /// <param name="lines">The lines.</param>
    /// <returns>The create h 2 from numbered list result.</returns>
    public static IList<string> CreateH2FromNumberedList(IList<string> lines)
    {
        if (lines == null) throw new ArgumentNullException(nameof(lines));
        for (var i = 0; i < lines.Count; i++)
            lines[i] = lines[i].Trim();

        for (var i = 0; i < lines.Count; i++)
            if (SH.IsNumbered(lines[i]))
                lines[i] = WrapWith(lines[i], "h2");

        return lines;
    }

    /// <summary>
    /// Get all equvivalents of non pairing tag.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The get all equvivalents of non pairing tag result.</returns>
    public static IList<string> GetAllEquvivalentsOfNonPairingTag(string tagName)
    {
        if (tagName == null) throw new ArgumentNullException(nameof(tagName));
        return new List<string>(["<" + tagName + ">", "<" + tagName + " />", "<" + tagName + "/>"]);
    }

    private static string WrapWith(string text, string tagName)
    {
        return "<" + tagName + ">" + text + "</" + tagName + ">";
    }

    /// <summary>
    /// Remove all nodes.
    /// </summary>
    /// <param name="htmlText">The html text.</param>
    /// <returns>The remove all nodes result.</returns>
    public static string RemoveAllNodes(string htmlText)
    {
        if (htmlText == null) throw new ArgumentNullException(nameof(htmlText));
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        htmlDocument.LoadHtml(htmlText);

        var nodes = htmlDocument.DocumentNode.Descendants().ToList();
        for (var i = 0; i < nodes.Count; i++)
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
    /// Get text between tags.
    /// </summary>
    /// <param name="html">The html.</param>
    /// <param name="scriptS">The script s.</param>
    /// <param name="scriptE">The script e.</param>
    /// <param name="isThrowExceptionIfNotContains">The is throw exception if not contains.</param>
    /// <returns>The get text between tags result.</returns>
    public static Tuple<string, string> GetTextBetweenTags(string html, string scriptS, string scriptE,
        bool isThrowExceptionIfNotContains = true)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        if (scriptS == null) throw new ArgumentNullException(nameof(scriptS));
        if (scriptE == null) throw new ArgumentNullException(nameof(scriptE));
        if (scriptS.EndsWith('>'))
            return new Tuple<string, string>(SH.GetTextBetweenSimple(html, scriptS, scriptE, isThrowExceptionIfNotContains),
                "");
        var sc = html.IndexOf(scriptS, StringComparison.Ordinal);
        if (sc == -1)
            return new Tuple<string, string>(" ", "");

        var ending = html.IndexOf('>', sc);
        var element = html.IndexOf(scriptE, ending, StringComparison.Ordinal);

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
    /// Get all tags.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The get all tags result.</returns>
    public static IList<string> GetAllTags(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        var tags = Regex.Matches(text, regexHtmlTag);
        var sourceList = new List<string>();
        foreach (Match item in tags)
            sourceList.Add(item.Value);
        return sourceList;
    }

    /// <summary>
    /// Remove html tags.
    /// </summary>
    /// <param name="clipboardText">The clipboard text.</param>
    /// <returns>The remove html tags result.</returns>
    public static string RemoveHtmlTags(string clipboardText)
    {
        if (clipboardText == null) throw new ArgumentNullException(nameof(clipboardText));
        return SHReplace.ReplaceAll(HtmlHelper.RemoveAllTags(clipboardText), " ", "");
    }

    /// <summary>
    /// Remove aspx comments.
    /// </summary>
    /// <param name="html">The html.</param>
    /// <returns>The remove aspx comments result.</returns>
    public static string RemoveAspxComments(string html)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        html = Regex.Replace(html, ConstsAspx.StartAspxComment + ".*?" + ConstsAspx.EndAspxComment, string.Empty,
            RegexOptions.Singleline);
        return html;
    }

    /// <summary>
    /// Contains tag.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="allHtmlTagsWithLeftArrow">The all html tags with left arrow.</param>
    /// <returns>The contains tag result.</returns>
    public static bool ContainsTag(string text, IList<string> allHtmlTagsWithLeftArrow)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        if (allHtmlTagsWithLeftArrow == null) throw new ArgumentNullException(nameof(allHtmlTagsWithLeftArrow));
        foreach (var item in allHtmlTagsWithLeftArrow)
            if (text.Contains(item, StringComparison.Ordinal))
                return true;
        return false;
    }

    /// <summary>
    /// Get syntax.
    /// </summary>
    /// <param name="tag">The tag.</param>
    /// <returns>The get syntax result.</returns>
    public static HtmlTagSyntax GetSyntax(ref string tag)
    {
        ThrowEx.InvalidParameter((string)tag, "tag");

        tag = SH.GetToFirst((string)tag, " ");
        tag = tag.Trim().TrimStart('<').TrimEnd('>').ToLowerInvariant();

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
    /// Trim inner of encoded html.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The trim inner of encoded html result.</returns>
    public static string TrimInnerOfEncodedHtml(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        text = SHReplace.ReplaceAll(text, "&gt;", "&gt; ");
        text = SHReplace.ReplaceAll(text, "&lt;", " &lt;");
        return text;
    }

    /// <summary>
    /// Split by space and lt gt.
    /// </summary>
    /// <param name="shortDescription">The short description.</param>
    /// <returns>The split by space and lt gt result.</returns>
    public static IList<string> SplitBySpaceAndLtGt(string shortDescription)
    {
        if (shortDescription == null) throw new ArgumentNullException(nameof(shortDescription));
        var f = SHSplit.Split(shortDescription, "<", ">", " ");
        return f;
    }

    /// <summary>
    /// Is html entity.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The is html entity result.</returns>
    public static bool IsHtmlEntity(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        text = text.TrimStart('&').TrimEnd(';');
        return AllLists.HtmlEntities.Contains(text);
    }

    /// <summary>
    /// Get content of tags.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The get content of tags result.</returns>
    public static IList<string> GetContentOfTags(string text, string tagName)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        if (tagName == null) throw new ArgumentNullException(nameof(tagName));
        var result = new List<string>();
        var start = $"<{tagName}";
        var end = $"</{tagName}>";
        var dex = text.IndexOf(start, StringComparison.Ordinal);
        while (dex != -1)
        {
            var dexEndLetter = text.IndexOf('>', dex);

            var dex2 = text.IndexOf(start, dex + start.Length, StringComparison.Ordinal);
            var dexEnd = text.IndexOf(end, dex, StringComparison.Ordinal);

            if (dex2 != -1)
                if (dexEnd > dex2)
                    throw new InvalidOperationException($"Another starting tag is before ending <{tagName}>");

            result.Add(SH.GetTextBetweenTwoCharsInts(text, dexEndLetter, dexEnd).Trim());

            dex = text.IndexOf(start, dexEnd, StringComparison.Ordinal);
        }

        return result;
    }

    /// <summary>
    /// Is css declaration name.
    /// </summary>
    /// <param name="decl">The decl.</param>
    /// <returns>The is css declaration name result.</returns>
    public static bool IsCssDeclarationName(string decl)
    {
        if (decl == null) throw new ArgumentNullException(nameof(decl));
        if (AllLists.AllCssKeys.Contains(decl))
            return true;
        return false;
    }

    /// <summary>
    /// Convert text to html.
    /// </summary>
    /// <param name="lines">The lines.</param>
    /// <returns>The convert text to html result.</returns>
    public static string ConvertTextToHtml(IList<string> lines)
    {
        if (lines == null) throw new ArgumentNullException(nameof(lines));
        var filteredLines = lines.Where(d => !string.IsNullOrWhiteSpace(d)).ToList();

        var endP = "</p>";

        CAChangeContent.ChangeContent0(null!, filteredLines, AddIntoParagraph);

        var result = SH.JoinNL(filteredLines);
        result = result.Replace(endP, endP + "\r" + "\n", StringComparison.Ordinal);

        return result;
    }

    private static string AddIntoParagraph(string text)
    {
        const string spaceDash = " -";

        if (text.Contains(spaceDash, StringComparison.Ordinal))
        {
            text = "<b>" + text;
            text = text.Replace(spaceDash, "</b>" + spaceDash, StringComparison.Ordinal);
        }

        if (text[0] == '<')
        {
            var tag = GetFirstTag(text).ToLowerInvariant();

            if (AllLists.PairingTagsDontWrapToParagraph.Contains(tag))
                return text;
            if (tag.StartsWith('/'))
                if (AllLists.PairingTagsDontWrapToParagraph.Contains(tag.Substring(1)))
                    return text;
        }

        return WrapWith(text, "p");
    }

    private static string GetFirstTag(string text)
    {
        var between = SH.GetTextBetweenSimple(text, "<", ">");

        if (between.Contains(' '))
            return SH.GetToFirst(between, " ");
        return between;
    }
}
