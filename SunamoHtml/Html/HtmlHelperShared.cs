namespace SunamoHtml.Html;

/// <summary>
/// EN: Shared HTML helper methods (mix of various utilities - consider splitting into more specific classes).
/// CZ: Sdílené HTML pomocné metody (mix různých utilit - zvažte rozdělení do specifičtějších tříd).
/// </summary>
public static partial class HtmlHelper
{
    /// <summary>
    /// Replaces non-pair HTML tags with XML-valid equivalents (adds self-closing slash).
    /// Problematic with auto translate.
    /// </summary>
    /// <param name="html">The HTML input string.</param>
    /// <returns>HTML with XML-valid non-pair tags.</returns>
    public static string ReplaceHtmlNonPairTagsWithXmlValid(string html)
    {
        var alreadyReplaced = new List<string>();
        var mc = Regex.Matches(html, RegexHelper.RNonPairXmlTagsUnvalid.ToString());
        var col = new List<string>(AllLists.HtmlNonPairTags);
        foreach (Match item in mc)
        {
            var data = item.Value.Replace(" >", ">");
            var tag = "";
            if (item.Value.Contains(" "))
                tag = SH.GetFirstPartByLocation(item.Value, ' ');
            else
                tag = data.Replace("/", "").Replace(">", "");
            tag = tag.TrimStart('<').Trim().ToLower();
            if (col.Contains(tag))
                if (!item.Value.Contains("/>"))
                    if (!alreadyReplaced.Contains(item.Value))
                    {
                        alreadyReplaced.Add(item.Value);
                        var nc = item.Value.Substring(0, item.Value.Length - 1) + " />";
                        html = html.Replace(item.Value, nc);
                    }
        }

        return html;
    }

    /// <summary>
    /// Converts plain text to HTML by replacing newlines with BR tags.
    /// </summary>
    /// <param name="text">The text to convert.</param>
    /// <returns>HTML with BR tags instead of newlines.</returns>
    public static string ConvertTextToHtml(string text)
    {
        text = text.Replace(Environment.NewLine, "<br />");
        text = text.Replace("\n", "<br />");
        return text;
    }

    /// <summary>
    /// Prepares text for use in HTML attribute by replacing double quotes with single quotes.
    /// </summary>
    /// <param name="text">The text to prepare.</param>
    /// <returns>Text with double quotes replaced by single quotes.</returns>
    public static string PrepareToAttribute(string text)
    {
        return text.Replace('"', '\'');
    }

    /// <summary>
    /// Replaces all case variations of BR tag with standard lowercased BR tag.
    /// </summary>
    /// <param name="html">The HTML string to process.</param>
    /// <returns>HTML with standardized BR tags.</returns>
    public static string ReplaceAllFontCase(string html)
    {
        var replacement = "<br />";
        html = html.Replace("<BR />", replacement);
        html = html.Replace("<bR />", replacement);
        html = html.Replace("<Br />", replacement);
        html = html.Replace("<br/>", replacement);
        html = html.Replace("<BR/>", replacement);
        html = html.Replace("<bR/>", replacement);
        html = html.Replace("<Br/>", replacement);
        html = html.Replace("<br>", replacement);
        html = html.Replace("<BR>", replacement);
        html = html.Replace("<bR>", replacement);
        html = html.Replace("<Br>", replacement);
        return html;
    }

    /// <summary>
    /// Clears all space characters (nbsp and regular spaces) from text.
    /// </summary>
    /// <param name="text">The text to clear spaces from.</param>
    /// <returns>Text without spaces.</returns>
    public static string ClearSpaces(string text)
    {
        return text.Replace("&nbsp;", "").Replace(" ", "");
    }

    /// <summary>
    /// Recursively finds HTML nodes matching tag and attribute criteria.
    /// </summary>
    /// <param name="result">The result list to add found nodes to.</param>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="value">The attribute value to match.</param>
    private static void RecursiveReturnTagWithAttr(List<HtmlNode> result, HtmlNode htmlNode, string tag, string attributeName, string value)
    {
        foreach (var item in htmlNode.ChildNodes)
            if (item.Name == tag && GetValueOfAttribute(attributeName, item) == value)
            {
                result.Add(item);
                return;
            }
            else
            {
                RecursiveReturnTagWithAttr(result, item, tag, attributeName, value);
            }
    }

    /// <summary>
    /// Gets the value of an HTML attribute from a node.
    /// </summary>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="divMain">The HTML node.</param>
    /// <param name="isTrim">Whether to trim the value.</param>
    /// <returns>Attribute value or empty string if not found.</returns>
    private static string GetValueOfAttribute(string attributeName, HtmlNode divMain, bool isTrim = false)
    {
        return HtmlAssistant.GetValueOfAttribute(attributeName, divMain, isTrim);
    }

    /// <summary>
    /// EN: Returns the first tag with specified attribute name and value. Returns null if not found.
    /// CZ: Vrátí první tag se zadaným názvem atributu a hodnotou. Vrátí null pokud není nalezen.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="value">The attribute value to match.</param>
    /// <returns>First matching HTML node or null.</returns>
    public static HtmlNode ReturnTagWithAttr(HtmlNode htmlNode, string tag, string attributeName, string value)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagWithAttr(result, htmlNode, tag, attributeName, value);
        if (result.Count > 0)
            return result[0];
        return null;
    }

    /// <summary>
    /// Gets all child nodes excluding text nodes.
    /// </summary>
    /// <param name="htmlNode">The HTML node to get children from.</param>
    /// <returns>List of non-text child nodes.</returns>
    public static List<HtmlNode> GetWithoutTextNodes(HtmlNode htmlNode)
    {
        var result = new List<HtmlNode>();
        foreach (var item in htmlNode.ChildNodes)
        {
            var itemType = item.ToString();
            if (itemType != "HtmlAgilityPack.HtmlTextNode")
                result.Add(item);
        }

        return result;
    }

    /// <summary>
    /// Recursively searches for a tag with specified attribute name and value.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="nameOfTag">The tag name to search for.</param>
    /// <param name="nameOfAttribute">The attribute name to match.</param>
    /// <param name="valueOfAttribute">The attribute value to match.</param>
    /// <returns>Found HTML node or null.</returns>
    public static HtmlNode? GetTagOfAtributeRek(HtmlNode htmlNode, string nameOfTag, string nameOfAttribute, string valueOfAttribute)
    {
        htmlNode = TrimNode(htmlNode);
        foreach (var childNode in htmlNode.ChildNodes)
        {
            var currentNode = childNode;
            foreach (var nestedNode in childNode.ChildNodes)
            {
                if (GetValueOfAttribute(nameOfAttribute, nestedNode) == valueOfAttribute)
                    return nestedNode;
                var foundNode = GetTagOfAtributeRek(nestedNode, nameOfTag, nameOfAttribute, valueOfAttribute);
                if (foundNode != null)
                    return foundNode;
            }

            if (currentNode.Name == nameOfTag)
            {
                if (GetValueOfAttribute(nameOfAttribute, currentNode) == valueOfAttribute)
                    return currentNode;
                foreach (var nestedChildNode in currentNode.ChildNodes)
                    if (GetValueOfAttribute(nameOfAttribute, nestedChildNode) == valueOfAttribute)
                        return nestedChildNode;
            }
        }

        return null;
    }

    /// <summary>
    /// Removes opening and closing tags from HTML string.
    /// </summary>
    /// <param name="html">The HTML string.</param>
    /// <param name="nameOfTag">The tag name to remove.</param>
    /// <returns>HTML without specified opening and closing tags.</returns>
    public static string TrimOpenAndEndTags(string html, string nameOfTag)
    {
        html = html.Replace("<" + nameOfTag + ">", "");
        html = html.Replace("</" + nameOfTag + ">", "");
        return html;
    }

    /// <summary>
    /// EN: Highlights searched words in text content with bold tags, returning sentence snippets.
    /// CZ: Zvýrazní hledaná slova v textovém obsahu tučnými tagy, vrátí úryvky vět.
    /// Before calling, white space characters must be converted to spaces in the content.
    /// </summary>
    /// <param name="entireContent">The entire content to search in.</param>
    /// <param name="maxLettersPerSentence">Maximum letters per sentence snippet.</param>
    /// <param name="sentenceCount">Number of sentence snippets to return.</param>
    /// <param name="searchedWords">List of words to search for and highlight.</param>
    /// <returns>HTML string with highlighted words in sentence snippets.</returns>
    public static string HighlightingWords(string entireContent, int maxLettersPerSentence, int sentenceCount, List<string> searchedWords)
    {
        for (var i = 0; i < searchedWords.Count; i++)
            searchedWords[i] = searchedWords[i].ToLower();
        entireContent = entireContent.Trim();
        var occurrences = SH.ReturnOccurencesOfStringFromToWord(entireContent, searchedWords.ToArray());
        if (occurrences.Count > 0)
        {
            var sentenceGroups = new List<List<FromToWord>>();
            var currentGroup = new List<FromToWord>();
            currentGroup.Add(occurrences[0]);
            var currentGroupIndex = 0;
            var lastInsertedFromIndex = occurrences[0].From;
            sentenceGroups.Add(currentGroup);
            for (var i = 1; i < occurrences.Count; i++)
            {
                var item = occurrences[i];
                if (item.To - lastInsertedFromIndex < maxLettersPerSentence)
                {
                    sentenceGroups[currentGroupIndex].Add(item);
                }
                else
                {
                    var newGroup = new List<FromToWord>();
                    newGroup.Add(item);
                    sentenceGroups.Add(newGroup);
                    if (sentenceGroups.Count == sentenceCount)
                        break;
                    currentGroupIndex++;
                }

                lastInsertedFromIndex = item.From;
            }
            var final = new StringBuilder();
            foreach (var item in sentenceGroups)
            {
                var middle = 0;
                if (item.Count % 2 == 0)
                {
                    var from = item[item.Count / 2].From;
                    var to = 0;
                    if (item.Count != 2)
                        to = item[item.Count / 2 + 1].To;
                    else
                        to = item[item.Count / 2].To;
                    middle = from + (to - from) / 2;
                }
                else if (item.Count == 1)
                {
                    middle = item[0].From + (item[0].To - item[0].From) / 2;
                }
                else
                {
                    middle = item.Count / 2;
                    middle++;
                    middle = item[middle].From + (item[middle].To - item[middle].From) / 2;
                }

                var charsPerSide = maxLettersPerSentence / 2;
                WhitespaceCharService whitespaceChar = new();
                var sentence = SH.XCharsBeforeAndAfterWholeWords(SHReplace.ReplaceAllArray(entireContent, " ", whitespaceChar.WhiteSpaceChars.ConvertAll(data => data.ToString()).ToArray()), middle, charsPerSide);
                var words = SHSplit.SplitBySpaceAndPunctuationCharsLeave(sentence);
                var sentenceWithHighlightedParts = new StringBuilder();
                foreach (var word in words)
                {
                    var isSearchedWord = false;
                    var wordLower = word.ToLower();
                    foreach (var searchedWord in searchedWords)
                        if (wordLower == searchedWord)
                            isSearchedWord = true;
                    if (isSearchedWord)
                        sentenceWithHighlightedParts.Append("<b>" + word + "</b>");
                    else
                        sentenceWithHighlightedParts.Append(word);
                }

                final.Append(sentenceWithHighlightedParts + " ... ");
            }

            return final.ToString();
        }

        return SH.ShortForLettersCountThreeDotsReverse(entireContent, sentenceCount * maxLettersPerSentence);
    }
}