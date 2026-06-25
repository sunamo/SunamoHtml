namespace SunamoHtml.Html;

// Problematic with auto translate.
public static partial class HtmlHelper
{
    public static string ReplaceHtmlNonPairTagsWithXmlValid(string html)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        var alreadyReplaced = new List<string>();
        var mc = Regex.Matches(html, RegexHelper.RNonPairXmlTagsUnvalid.ToString());
        var col = new List<string>(AllLists.HtmlNonPairTags);
        foreach (Match item in mc)
        {
            var data = item.Value.Replace(" >", ">", StringComparison.Ordinal);
            var tag = "";
            if (item.Value.Contains(" ", StringComparison.Ordinal))
                tag = SH.GetFirstPartByLocation(item.Value, ' ');
            else
                tag = data.Replace("/", "", StringComparison.Ordinal).Replace(">", "", StringComparison.Ordinal);
            tag = tag.TrimStart('<').Trim();
            if (col.Contains(tag, StringComparer.OrdinalIgnoreCase))
                if (!item.Value.Contains("/>", StringComparison.Ordinal))
                    if (!alreadyReplaced.Contains(item.Value))
                    {
                        alreadyReplaced.Add(item.Value);
                        var nc = item.Value.Substring(0, item.Value.Length - 1) + " />";
                        html = html.Replace(item.Value, nc, StringComparison.Ordinal);
                    }
        }

        return html;
    }

    public static string ConvertTextToHtml(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        text = text.Replace(Environment.NewLine, "<br />", StringComparison.Ordinal);
        text = text.Replace("\n", "<br />", StringComparison.Ordinal);
        return text;
    }

    public static string PrepareToAttribute(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        return text.Replace('"', '\'');
    }

    public static string ReplaceAllFontCase(string html)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        var replacement = "<br />";
        html = html.Replace("<BR />", replacement, StringComparison.Ordinal);
        html = html.Replace("<bR />", replacement, StringComparison.Ordinal);
        html = html.Replace("<Br />", replacement, StringComparison.Ordinal);
        html = html.Replace("<br/>", replacement, StringComparison.Ordinal);
        html = html.Replace("<BR/>", replacement, StringComparison.Ordinal);
        html = html.Replace("<bR/>", replacement, StringComparison.Ordinal);
        html = html.Replace("<Br/>", replacement, StringComparison.Ordinal);
        html = html.Replace("<br>", replacement, StringComparison.Ordinal);
        html = html.Replace("<BR>", replacement, StringComparison.Ordinal);
        html = html.Replace("<bR>", replacement, StringComparison.Ordinal);
        html = html.Replace("<Br>", replacement, StringComparison.Ordinal);
        return html;
    }

    public static string ClearSpaces(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        return text.Replace("&nbsp;", "", StringComparison.Ordinal).Replace(" ", "", StringComparison.Ordinal);
    }

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

    private static string GetValueOfAttribute(string attributeName, HtmlNode htmlNode, bool isTrim = false)
    {
        return HtmlAssistant.GetValueOfAttribute(attributeName, htmlNode, isTrim);
    }

    // EN: Returns the first tag with specified attribute name and value. Returns null if not found.
    // CZ: Vrátí první tag se zadaným názvem atributu a hodnotou. Vrátí null pokud není nalezen.
    public static HtmlNode? ReturnTagWithAttr(HtmlNode htmlNode, string tag, string attributeName, string value)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        RecursiveReturnTagWithAttr(result, htmlNode, tag, attributeName, value);
        if (result.Count > 0)
            return result[0];
        return null;
    }

    public static IList<HtmlNode> GetWithoutTextNodes(HtmlNode htmlNode)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        foreach (var item in htmlNode.ChildNodes)
        {
            var itemType = item.ToString();
            if (itemType != "HtmlAgilityPack.HtmlTextNode")
                result.Add(item);
        }

        return result;
    }

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

    public static string TrimOpenAndEndTags(string html, string nameOfTag)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        html = html.Replace("<" + nameOfTag + ">", "", StringComparison.Ordinal);
        html = html.Replace("</" + nameOfTag + ">", "", StringComparison.Ordinal);
        return html;
    }

    // EN: Highlights searched words in text content with bold tags, returning sentence snippets.
    // CZ: Zvýrazní hledaná slova v textovém obsahu tučnými tagy, vrátí úryvky vět.
    // Before calling, white space characters must be converted to spaces in the content.
    public static string HighlightingWords(string entireContent, int maxLettersPerSentence, int sentenceCount, IList<string> searchedWords)
    {
        if (entireContent == null) throw new ArgumentNullException(nameof(entireContent));
        if (searchedWords == null) throw new ArgumentNullException(nameof(searchedWords));
        for (var i = 0; i < searchedWords.Count; i++)
            searchedWords[i] = searchedWords[i].ToUpperInvariant();
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
                    var wordLower = word.ToUpperInvariant();
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
