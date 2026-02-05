namespace SunamoHtml;

/// <summary>
///     HtmlHelperText - for methods which NOT operate on HtmlAgiityHelper!
///     HtmlAgilityHelper - getting new nodes
///     HtmlAssistant - Only for methods which operate on HtmlAgiityHelper!
/// </summary>
public partial class HtmlAgilityHelper
{
    /// <summary>
    /// Trims text nodes and optionally comments from a list of HTML nodes.
    /// </summary>
    /// <param name="nodes">The list of HTML nodes to trim.</param>
    /// <param name="isRemovingTextNodes">Whether to remove text nodes.</param>
    /// <param name="isRemovingComments">Whether to remove comment nodes.</param>
    /// <returns>List of nodes with text nodes and/or comments removed.</returns>
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

    /// <summary>
    /// Removes comment nodes from a list of HTML nodes.
    /// </summary>
    /// <param name="nodes">The list of HTML nodes to process.</param>
    /// <returns>List of nodes with comments removed.</returns>
    public static List<HtmlNode> TrimComments(List<HtmlNode> nodes)
    {
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
            endsWithComment = html.Contains(ConstsAspx.EndHtmlComment);
            startsWithComment = html.Contains(ConstsAspx.StartHtmlComment);
            if (startsWithComment && endsWithComment)
            {
                shouldTranslate = false;
            }
            else
            {
                if (html == string.Empty)
                    continue;
                endsWithComment = html.Contains(ConstsAspx.EndAspxComment);
                startsWithComment = html.Contains(ConstsAspx.StartAspxComment);
                if (startsWithComment || endsWithComment)
                    if (startsWithComment && endsWithComment)
                        // contains whole aspx comment
                        shouldTranslate = false;
                if (!shouldTranslate)
                    continue;
                if (html.StartsWith("<%"))
                    continue;
                var count = item.ChildNodes.Count;
                var textCount = TrimTexts(item.ChildNodes).Count;
                if (textCount == count && html == string.Empty)
                    continue;
                result.Add(item);
            }
        }

        return result;
    }

    /// <summary>
    /// Checks if an HTML node has the specified tag name. Use "*" for any tag.
    /// </summary>
    /// <param name="node">The HTML node to check.</param>
    /// <param name="tag">The tag name to check for, or "*" for any tag.</param>
    /// <returns>True if the node has the specified tag name or tag is "*".</returns>
    private static bool HasTagName(HtmlNode node, string tag)
    {
        if (tag == "*")
            return true;
        var result = node.Name == tag;
        return result;
    }

    /// <summary>
    /// Checks if an HTML node has an attribute with the specified value.
    /// </summary>
    /// <param name="node">The HTML node to check.</param>
    /// <param name="attributeName">The attribute name to check.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <param name="isWildCard">Whether to use wildcard matching.</param>
    /// <param name="isEnoughContainsAttribute">Whether partial match is sufficient.</param>
    /// <param name="isSearchAsSingleString">Whether to search as a single string or split by spaces.</param>
    /// <returns>True if the node has the attribute with matching value.</returns>
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
                    hasMatchingAttribute = actualAttributeValue.Contains(attributeValue);
            }
            else
            {
                var allParametersMatch = true;
                var parameters = SHSplit.Split(attributeValue, " ");
                foreach (var parameter in parameters)
                    if (!actualAttributeValue.Contains(parameter))
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

    /// <summary>
    /// Recursively returns HTML tags matching the specified tag name.
    /// If single is true, returns only the first match (like Node vs Nodes).
    /// Use "*" in parameter to match any tag.
    /// </summary>
    /// <param name="result">The list to add found nodes to.</param>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="isSingle">Whether to stop after finding first match.</param>
    /// <param name="tagName">The tag name to search for, or "*" for any tag.</param>
    public static void RecursiveReturnTags(List<HtmlNode> result, HtmlNode htmlNode, bool isRecursive, bool isSingle, string tagName)
    {
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

    /// <summary>
    /// Gets all nodes with the specified tag name.
    /// </summary>
    /// <param name="node">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <returns>List of matching HTML nodes with text nodes trimmed.</returns>
    public static List<HtmlNode> Nodes(HtmlNode node, bool isRecursive, string tag)
    {
        tag = tag.ToLower();
        var result = new List<HtmlNode>();
        RecursiveReturnTags(result, node, isRecursive, false, tag);
        if (tag != TextNode)
            result = TrimTexts(result);
        return result;
    }

    /// <summary>
    /// Worker method to get nodes with specified attribute criteria.
    /// </summary>
    /// <param name="node">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <param name="isWildCard">Whether to use wildcard matching.</param>
    /// <param name="isEnoughContainsAttribute">Whether partial match is sufficient.</param>
    /// <param name="isSearchAsSingleString">Whether to search as a single string.</param>
    /// <returns>List of matching HTML nodes.</returns>
    private static List<HtmlNode> NodesWithAttrWorker(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue, bool isWildCard, bool isEnoughContainsAttribute, bool isSearchAsSingleString = true)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, node, isRecursive, tag, attributeName, attributeValue, isWildCard, isEnoughContainsAttribute, isSearchAsSingleString);
        if (tag != TextNode)
            result = TrimTexts(result);
        return result;
    }

    /// <summary>
    /// Creates an HTML document with specific initialization options.
    /// </summary>
    /// <param name="data">Initialization data, or null for default settings.</param>
    /// <returns>Configured HTML document instance.</returns>
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

    /// <summary>
    /// Recursively returns tags with attribute containing specified value.
    /// </summary>
    /// <param name="result">The list to add found nodes to.</param>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <param name="isEnoughContainsAttribute">Whether partial match is sufficient.</param>
    /// <param name="isSearchAsSingleString">Whether to search as a single string.</param>
    public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode htmlNode, bool isRecursive, string tagName, string attributeName, string attributeValue, bool isEnoughContainsAttribute, bool isSearchAsSingleString = true)
    {
        RecursiveReturnTagsWithContainsAttr(result, htmlNode, isRecursive, tagName, attributeName, attributeValue, false, isEnoughContainsAttribute, isSearchAsSingleString);
    }

    /// <summary>
    /// Recursively returns tags with attribute containing specified value.
    /// Use "*" in tagName to return all tags.
    /// </summary>
    /// <param name="result">The list to add found nodes to.</param>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tagName">The tag name to search for, or "*" for all tags.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <param name="isWildCard">Whether to use wildcard matching.</param>
    /// <param name="isEnoughContainsAttribute">Whether partial match is sufficient.</param>
    /// <param name="isSearchAsSingleString">Whether to search as a single string.</param>
    public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode htmlNode, bool isRecursive, string tagName, string attributeName, string attributeValue, bool isWildCard, bool isEnoughContainsAttribute, bool isSearchAsSingleString = true)
    {
        var childNodesWithoutText = TrimTexts(htmlNode.ChildNodes);
        tagName = tagName.ToLower();
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

    /// <summary>
    /// Gets nodes with attribute matching wildcard pattern.
    /// </summary>
    /// <param name="node">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value pattern.</param>
    /// <param name="isContains">Whether to use contains matching.</param>
    /// <returns>List of matching HTML nodes.</returns>
    public static List<HtmlNode> NodesWithAttrWildCard(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue, bool isContains = false)
    {
        return NodesWithAttrWorker(node, isRecursive, tag, attributeName, attributeValue, true, isContains);
    }

    /// <summary>
    /// Gets nodes with exact attribute match.
    /// </summary>
    /// <param name="node">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <param name="isContains">Whether to use contains matching.</param>
    /// <returns>List of matching HTML nodes.</returns>
    public static List<HtmlNode> NodesWithAttr(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue, bool isContains = false)
    {
        return NodesWithAttrWorker(node, isRecursive, tag, attributeName, attributeValue, false, isContains);
    }
}