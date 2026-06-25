namespace SunamoHtml;

// HtmlHelperText - for methods which NOT operate on HtmlAgiityHelper!
// HtmlAgilityHelper - getting new nodes
// HtmlAssistant - Only for methods which operate on HtmlAgiityHelper!
public partial class HtmlAgilityHelper
{
    internal static List<HtmlNode> TrimTextsInternal(List<HtmlNode> nodes, bool isRemovingTextNodes, bool isRemovingComments = false)
    {
        if (!_trimTexts)
            return nodes;
        var result = new List<HtmlNode>();
        var shouldAdd = true;
        foreach (var item in nodes)
        {
            shouldAdd = true;
            if (isRemovingTextNodes)
                if (item.Name == TextNode)
                    shouldAdd = false;
            if (isRemovingComments)
                if (item.Name == "#comment")
                    shouldAdd = false;
            if (shouldAdd)
                result.Add(item);
        }

        return result;
    }

    public static IList<HtmlNode> TrimComments(IList<HtmlNode> nodes)
    {
        if (nodes == null) throw new ArgumentNullException(nameof(nodes));
        var result = new List<HtmlNode>();
        var startsWithComment = false;
        var endsWithComment = false;
        var shouldTranslate = true;
        foreach (var item in nodes)
        {
            startsWithComment = false;
            endsWithComment = false;
            shouldTranslate = true;
            var html = item.InnerHtml.Trim();
            // contains whole html comment
            endsWithComment = html.Contains(ConstsAspx.EndHtmlComment, StringComparison.Ordinal);
            startsWithComment = html.Contains(ConstsAspx.StartHtmlComment, StringComparison.Ordinal);
            if (startsWithComment && endsWithComment)
            {
                shouldTranslate = false;
            }
            else
            {
                if (string.IsNullOrEmpty(html))
                    continue;
                endsWithComment = html.Contains(ConstsAspx.EndAspxComment, StringComparison.Ordinal);
                startsWithComment = html.Contains(ConstsAspx.StartAspxComment, StringComparison.Ordinal);
                if (startsWithComment || endsWithComment)
                    if (startsWithComment && endsWithComment)
                        // contains whole aspx comment
                        shouldTranslate = false;
                if (!shouldTranslate)
                    continue;
                if (html.StartsWith("<%", StringComparison.Ordinal))
                    continue;
                var count = item.ChildNodes.Count;
                var textCount = TrimTexts(item.ChildNodes).Count;
                if (textCount == count && string.IsNullOrEmpty(html))
                    continue;
                result.Add(item);
            }
        }

        return result;
    }

    // Use "*" for any tag.
    private static bool HasTagName(HtmlNode node, string tag)
    {
        if (tag == "*")
            return true;
        var result = node.Name == tag;
        return result;
    }

    private static bool HasTagAttr(HtmlNode node, string attributeName, string attributeValue, bool isWildCard, bool isEnoughContainsAttribute, bool isSearchAsSingleString)
    {
        if (attributeValue == "*")
            return true;
        var hasMatchingAttribute = false;
        var actualAttributeValue = HtmlAssistant.GetValueOfAttribute(attributeName, node);
        if (isEnoughContainsAttribute)
        {
            if (isSearchAsSingleString)
            {
                if (isWildCard)
                    hasMatchingAttribute = SH.MatchWildcard(actualAttributeValue, attributeValue);
                else
                    hasMatchingAttribute = actualAttributeValue.Contains(attributeValue, StringComparison.Ordinal);
            }
            else
            {
                var allParametersMatch = true;
                var parameters = SHSplit.Split(attributeValue, " ");
                foreach (var parameter in parameters)
                    if (!actualAttributeValue.Contains(parameter, StringComparison.Ordinal))
                    {
                        allParametersMatch = false;
                        break;
                    }

                hasMatchingAttribute = allParametersMatch;
            }
        }
        else
        {
            hasMatchingAttribute = actualAttributeValue == attributeValue;
        }

        return hasMatchingAttribute;
    }

    // Recursively returns HTML tags matching the specified tag name.
    // If single is true, returns only the first match (like Node vs Nodes).
    // Use "*" in parameter to match any tag.
    public static void RecursiveReturnTags(List<HtmlNode> result, HtmlNode htmlNode, bool isRecursive, bool isSingle, string tagName)
    {
        if (result == null) throw new ArgumentNullException(nameof(result));
        if (htmlNode == null)
            return;
        foreach (var item in htmlNode.ChildNodes)
            if (HasTagName(item, tagName))
            {
                result.Add(item);
                if (isSingle)
                    return;
                if (isRecursive)
                    RecursiveReturnTags(result, item, isRecursive, isSingle, tagName);
            }
            else
            {
                if (isRecursive)
                    RecursiveReturnTags(result, item, isRecursive, isSingle, tagName);
            }
    }

    public static List<HtmlNode> Nodes(HtmlNode node, bool isRecursive, string tag)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        tag = tag.ToLowerInvariant();
        var result = new List<HtmlNode>();
        RecursiveReturnTags(result, node, isRecursive, false, tag);
        if (tag != TextNode)
            result = TrimTexts(result);
        return result;
    }

    private static List<HtmlNode> NodesWithAttrWorker(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue, bool isWildCard, bool isEnoughContainsAttribute, bool isSearchAsSingleString = true)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, node, isRecursive, tag, attributeName, attributeValue, isWildCard, isEnoughContainsAttribute, isSearchAsSingleString);
        if (tag != TextNode)
            result = TrimTexts(result);
        return result;
    }

    public static HtmlDocument CreateHtmlDocument(CreateHtmlDocumentInitData? data = null)
    {
        if (data == null)
        {
            data = new();
        }

        var htmlDocument = new HtmlDocument();
        htmlDocument.OptionOutputOriginalCase = data.OptionOutputOriginalCase;
        // false - even though tags ending with / are converted to </Page>. Tags that shouldn't be closed must be removed from HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("form"); before loading XML https://html-agility-pack.net/knowledge-base/7104652/htmlagilitypack-close-form-tag-automatically
        htmlDocument.OptionAutoCloseOnEnd = false;
        htmlDocument.OptionOutputAsXml = false;
        htmlDocument.OptionFixNestedTags = false;
        // when OptionCheckSyntax = false, raises NullReferenceException in Load/LoadHtml
        return htmlDocument;
    }

    public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode htmlNode, bool isRecursive, string tagName, string attributeName, string attributeValue, bool isEnoughContainsAttribute, bool isSearchAsSingleString = true)
    {
        RecursiveReturnTagsWithContainsAttr(result, htmlNode, isRecursive, tagName, attributeName, attributeValue, false, isEnoughContainsAttribute, isSearchAsSingleString);
    }

    // Recursively returns tags with attribute containing specified value.
    // Use "*" in tagName to return all tags.
    public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode htmlNode, bool isRecursive, string tagName, string attributeName, string attributeValue, bool isWildCard, bool isEnoughContainsAttribute, bool isSearchAsSingleString = true)
    {
        if (result == null) throw new ArgumentNullException(nameof(result));
        if (tagName == null) throw new ArgumentNullException(nameof(tagName));
        if (attributeName == null) throw new ArgumentNullException(nameof(attributeName));
        if (attributeValue == null) throw new ArgumentNullException(nameof(attributeValue));
        var childNodesWithoutText = TrimTexts(htmlNode.ChildNodes);
        tagName = tagName.ToLowerInvariant();
        if (htmlNode == null)
            return;
        foreach (var item in childNodesWithoutText)
        {
            var actualAttributeValue = HtmlAssistant.GetValueOfAttribute(attributeName, item);
            if (HasTagName(item, tagName))
            {
                if (HasTagAttr(item, attributeName, attributeValue, isWildCard, isEnoughContainsAttribute, isSearchAsSingleString))
                    result.Add(item);
                if (isRecursive)
                    RecursiveReturnTagsWithContainsAttr(result, item, isRecursive, tagName, attributeName, attributeValue, isWildCard, isEnoughContainsAttribute, isSearchAsSingleString);
            }
            else
            {
                if (isRecursive)
                    RecursiveReturnTagsWithContainsAttr(result, item, isRecursive, tagName, attributeName, attributeValue, isWildCard, isEnoughContainsAttribute, isSearchAsSingleString);
            }
        }
    }

    public static IList<HtmlNode> NodesWithAttrWildCard(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue, bool isContains = false)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        if (attributeName == null) throw new ArgumentNullException(nameof(attributeName));
        if (attributeValue == null) throw new ArgumentNullException(nameof(attributeValue));
        return NodesWithAttrWorker(node, isRecursive, tag, attributeName, attributeValue, true, isContains);
    }

    public static IList<HtmlNode> NodesWithAttr(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue, bool isContains = false)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        if (attributeName == null) throw new ArgumentNullException(nameof(attributeName));
        if (attributeValue == null) throw new ArgumentNullException(nameof(attributeValue));
        return NodesWithAttrWorker(node, isRecursive, tag, attributeName, attributeValue, false, isContains);
    }
}
