namespace SunamoHtml.Html;

public static partial class HtmlHelper
{
    /// <summary>
    /// Convert html to text.
    /// </summary>
    /// <param name="htmlContent">The html content.</param>
    /// <returns>The convert html to text result.</returns>
    public static string ConvertHtmlToText(string htmlContent)
    {
        if (htmlContent == null) throw new ArgumentNullException(nameof(htmlContent));
        htmlContent = WebUtility.HtmlDecode(htmlContent);
        htmlContent = SHReplace.ReplaceAllArray(htmlContent, Environment.NewLine, "<br>", "<br />", "<br/>");
        htmlContent = StripAllTags(htmlContent);
        return htmlContent;
    }

    // Strips all HTML tags from text, replacing them with a single space.
    /// <summary>
    /// Strip all tags.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The strip all tags result.</returns>
    public static string StripAllTags(string text)
    {
        return StripAllTags(text, " ");
    }

    // Strips all HTML tags from text, replacing them with a specified replacement string.
    /// <summary>
    /// Strip all tags.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="replacement">The replacement.</param>
    /// <returns>The strip all tags result.</returns>
    public static string StripAllTags(string text, string replacement)
    {
        var result = Regex.Replace(text, @"<[^>]*>", replacement);
        result = SHReplace.ReplaceAllDoubleSpaceToSingle(result);
        return result;
    }

    /// <summary>
    /// Trim node.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <returns>The trim node result.</returns>
    public static HtmlNode TrimNode(HtmlNode htmlNode)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        if (htmlNode.FirstChild == null)
            return htmlNode;
        if (string.IsNullOrWhiteSpace(htmlNode.FirstChild.InnerHtml))
            return htmlNode;
        htmlNode.InnerHtml = htmlNode.InnerHtml.Trim();
        htmlNode.FirstChild.InnerHtml = htmlNode.FirstChild.InnerHtml.Trim();
        htmlNode.InnerHtml = htmlNode.InnerHtml.Trim();
        return htmlNode;
    }

    // Returns all tags matching the specified tag name, recursively searching the node tree.
    // Supports wildcard "*" to match all tags.
    /// <summary>
    /// Return tags rek.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The return tags rek result.</returns>
    public static IList<HtmlNode> ReturnTagsRek(HtmlNode htmlNode, string tagName)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        RecursiveReturnTags(result, htmlNode, tagName);
        return TrimTexts(result);
    }

    // Returns the first tag with specified name and attribute value, recursively searching the node tree.
    // Returns null if tag is not found.
    /// <summary>
    /// Return tag with attr rek.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <returns>The return tag with attr rek result.</returns>
    public static HtmlNode? ReturnTagWithAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        return ReturnTagWithAttr(htmlNode, tagName, attributeName, attributeValue);
    }

    // Returns all tags matching specified name and attribute value, recursively searching the node tree.
    // Supports wildcard "*" for tag name to match all tags.
    // Supports wildcard "*" for attribute value to match any value.
    /// <summary>
    /// Return tags with attr rek.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <returns>The return tags with attr rek result.</returns>
    public static IList<HtmlNode> ReturnTagsWithAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithAttr(result, htmlNode, tagName, attributeName, attributeValue);
        return result;
    }

    // Returns all child tags matching specified tag names.
    /// <summary>
    /// Return all tags.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagNames">The tag names.</param>
    /// <returns>The return all tags result.</returns>
    public static IList<HtmlNode> ReturnAllTags(HtmlNode htmlNode, params string[] tagNames)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        if (tagNames == null) throw new ArgumentNullException(nameof(tagNames));
        var result = new List<HtmlNode>();
        RecursiveReturnAllTags(result, htmlNode, tagNames);
        return result;
    }

    /// <summary>
    /// Trim texts.
    /// </summary>
    /// <param name="htmlNodeCollection">The html node collection.</param>
    /// <returns>The trim texts result.</returns>
    public static IList<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
    {
        return HtmlAgilityHelper.TrimTexts(htmlNodeCollection);
    }

    /// <summary>
    /// Trim texts.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <returns>The trim texts result.</returns>
    public static IList<HtmlNode> TrimTexts(IList<HtmlNode> nodes)
    {
        return HtmlAgilityHelper.TrimTexts(nodes as List<HtmlNode> ?? new List<HtmlNode>(nodes));
    }

    /// <summary>
    /// Trim texts.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <param name="isRemoveTextNodes">The is remove text nodes.</param>
    /// <param name="isRemoveComments">The is remove comments.</param>
    /// <returns>The trim texts result.</returns>
    public static IList<HtmlNode> TrimTexts(IList<HtmlNode> nodes, bool isRemoveTextNodes, bool isRemoveComments = false)
    {
        if (nodes == null) throw new ArgumentNullException(nameof(nodes));
        return HtmlAgilityHelper.TrimTextsInternal(nodes as List<HtmlNode> ?? new List<HtmlNode>(nodes), isRemoveTextNodes, isRemoveComments);
    }

    // Recursively searches for tags matching specified tag name.
    // Supports wildcard "*" to match all tags.
    private static void RecursiveReturnTags(List<HtmlNode> result, HtmlNode htmlNode, string tagName)
    {
        foreach (var item in htmlNode.ChildNodes)
            if (HasTagName(item, tagName))
            {
                result.Add(item);
                RecursiveReturnTags(result, item, tagName);
            }
            else
            {
                RecursiveReturnTags(result, item, tagName);
            }
    }

    // Recursively searches for all tags matching any of the specified tag names.
    private static void RecursiveReturnAllTags(List<HtmlNode> result, HtmlNode htmlNode, params string[] tagNames)
    {
        foreach (var item in htmlNode.ChildNodes)
        {
            var contains = false;
            if (tagNames.Length == 1)
            {
                if (item.Name == tagNames[0])
                    contains = true;
            }
            else
            {
                foreach (var tagName in tagNames)
                    if (item.Name == tagName)
                        contains = true;
            }

            if (contains)
            {
                RecursiveReturnAllTags(result, item, tagNames);
                if (!result.Contains(item))
                    result.Add(item);
            }
            else
            {
                RecursiveReturnAllTags(result, item, tagNames);
            }
        }
    }

    // Checks if an HTML node has the specified tag name.
    // Supports wildcard "*" to match any tag name.
    private static bool HasTagName(HtmlNode htmlNode, string tagName)
    {
        if (tagName == "*")
            return true;
        return htmlNode.Name == tagName;
    }

    // Recursively searches for tags matching specified tag name and attribute value.
    // Supports wildcard "*" for tag name to match all tags.
    // Supports wildcard "*" for attribute value to match any value.
    private static void RecursiveReturnTagsWithAttr(List<HtmlNode> result, HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        foreach (var item in htmlNode.ChildNodes)
            if (HasTagName(item, tagName))
            {
                if (HasTagAttr(item, attributeName, attributeValue, false))
                    if (!result.Contains(item))
                        result.Add(item);
            }
            else
            {
                RecursiveReturnTagsWithAttr(result, item, tagName, attributeName, attributeValue);
            }
    }

    // Checks if an HTML node has an attribute with the specified name and value.
    // Supports wildcard "*" for attribute value to match any value.
    private static bool HasTagAttr(HtmlNode htmlNode, string attributeName, string attributeValue, bool isEnoughContains)
    {
        if (attributeValue == "*")
            return true;
        var attrValue = GetValueOfAttribute(attributeName, htmlNode);
        bool contains;
        if (isEnoughContains)
            contains = attrValue.Contains(attributeValue, StringComparison.Ordinal);
        else
            contains = attrValue == attributeValue;
        return contains;
    }

    // Returns the first child tag matching the specified tag name.
    // Returns null if tag is not found.
    /// <summary>
    /// Return tag.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The return tag result.</returns>
    public static HtmlNode? ReturnTag(HtmlNode htmlNode, string tagName)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        foreach (var item in htmlNode.ChildNodes)
            if (item.Name == tagName)
                return item;
        return null;
    }

    // Replaces a child node by matching its OuterHtml with a new node.
    /// <summary>
    /// Replace child node by outer html.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="oldOuterHtml">The old outer html.</param>
    /// <param name="newNode">The new node.</param>
    public static void ReplaceChildNodeByOuterHtml(HtmlNode htmlNode, string oldOuterHtml, HtmlNode newNode)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        for (var i = 0; i < htmlNode.ChildNodes.Count; i++)
        {
            var item = htmlNode.ChildNodes[i];
            if (item.OuterHtml == oldOuterHtml)
            {
                // First is new, Second is old!!!
                htmlNode.ReplaceChild(newNode, item);
                break;
            }
        }
    }
}
