namespace SunamoHtml.Html;

/// <summary>
/// EN: Shared HTML helper methods (partial class - part 1).
/// CZ: Sdílené HTML pomocné metody (partial class - část 1).
/// </summary>
public static partial class HtmlHelper
{
    /// <summary>
    /// Converts HTML to plain text by decoding HTML entities, replacing BR tags with newlines, and stripping all tags.
    /// </summary>
    /// <param name="htmlContent">The HTML content to convert.</param>
    /// <returns>Plain text without HTML tags.</returns>
    public static string ConvertHtmlToText(string htmlContent)
    {
        ArgumentNullException.ThrowIfNull(htmlContent);
        htmlContent = WebUtility.HtmlDecode(htmlContent);
        htmlContent = SHReplace.ReplaceAllArray(htmlContent, Environment.NewLine, "<br>", "<br />", "<br/>");
        htmlContent = StripAllTags(htmlContent);
        return htmlContent;
    }

    /// <summary>
    /// EN: Strips all HTML tags from text, replacing them with a single space.
    /// CZ: Odstraní všechny HTML tagy z textu, nahradí je jednou mezerou.
    /// </summary>
    /// <param name="text">The text to strip tags from.</param>
    /// <returns>Text without HTML tags.</returns>
    public static string StripAllTags(string text)
    {
        return StripAllTags(text, " ");
    }

    /// <summary>
    /// EN: Strips all HTML tags from text, replacing them with a specified replacement string.
    /// CZ: Odstraní všechny HTML tagy z textu, nahradí je zadaným řetězcem.
    /// </summary>
    /// <param name="text">The text to strip tags from.</param>
    /// <param name="replacement">The string to replace tags with.</param>
    /// <returns>Text without HTML tags.</returns>
    public static string StripAllTags(string text, string replacement)
    {
        var result = Regex.Replace(text, @"<[^>]*>", replacement);
        result = SHReplace.ReplaceAllDoubleSpaceToSingle(result);
        return result;
    }

    /// <summary>
    /// Trims whitespace from an HTML node's inner content.
    /// </summary>
    /// <param name="htmlNode">The HTML node to trim.</param>
    /// <returns>The trimmed HTML node.</returns>
    public static HtmlNode TrimNode(HtmlNode htmlNode)
    {
        ArgumentNullException.ThrowIfNull(htmlNode);
        if (htmlNode.FirstChild == null)
            return htmlNode;
        if (string.IsNullOrWhiteSpace(htmlNode.FirstChild.InnerHtml))
            return htmlNode;
        htmlNode.InnerHtml = htmlNode.InnerHtml.Trim();
        htmlNode.FirstChild.InnerHtml = htmlNode.FirstChild.InnerHtml.Trim();
        htmlNode.InnerHtml = htmlNode.InnerHtml.Trim();
        return htmlNode;
    }

    /// <summary>
    /// EN: Returns all tags matching the specified tag name, recursively searching the node tree.
    /// CZ: Vrátí všechny tagy odpovídající zadanému názvu tagu, rekurzivně prohledá strom uzlů.
    /// Supports wildcard "*" to match all tags.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for, or "*" for all tags.</param>
    /// <returns>List of matching HTML nodes with trimmed text.</returns>
    public static IList<HtmlNode> ReturnTagsRek(HtmlNode htmlNode, string tagName)
    {
        ArgumentNullException.ThrowIfNull(htmlNode);
        var result = new List<HtmlNode>();
        RecursiveReturnTags(result, htmlNode, tagName);
        return TrimTexts(result);
    }

    /// <summary>
    /// EN: Returns the first tag with specified name and attribute value, recursively searching the node tree.
    /// CZ: Vrátí první tag se zadaným názvem a hodnotou atributu, rekurzivně prohledá strom uzlů.
    /// Returns null if tag is not found.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <returns>First matching HTML node or null.</returns>
    public static HtmlNode? ReturnTagWithAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        return ReturnTagWithAttr(htmlNode, tagName, attributeName, attributeValue);
    }

    /// <summary>
    /// EN: Returns all tags matching specified name and attribute value, recursively searching the node tree.
    /// CZ: Vrátí všechny tagy odpovídající zadanému názvu a hodnotě atributu, rekurzivně prohledá strom uzlů.
    /// Supports wildcard "*" for tag name to match all tags.
    /// Supports wildcard "*" for attribute value to match any value.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for, or "*" for all tags.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match, or "*" for any value.</param>
    /// <returns>List of matching HTML nodes.</returns>
    public static IList<HtmlNode> ReturnTagsWithAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        ArgumentNullException.ThrowIfNull(htmlNode);
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithAttr(result, htmlNode, tagName, attributeName, attributeValue);
        return result;
    }

    /// <summary>
    /// EN: Returns all child tags matching specified tag names.
    /// CZ: Vrátí všechny podřízené tagy odpovídající zadaným názvům tagů.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagNames">Tag names to search for.</param>
    /// <returns>List of matching HTML nodes.</returns>
    public static IList<HtmlNode> ReturnAllTags(HtmlNode htmlNode, params string[] tagNames)
    {
        ArgumentNullException.ThrowIfNull(htmlNode);
        ArgumentNullException.ThrowIfNull(tagNames);
        var result = new List<HtmlNode>();
        RecursiveReturnAllTags(result, htmlNode, tagNames);
        return result;
    }

    /// <summary>
    /// Trims whitespace from all HTML nodes in a collection.
    /// </summary>
    /// <param name="htmlNodeCollection">The HTML node collection to trim.</param>
    /// <returns>List of trimmed HTML nodes.</returns>
    public static IList<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
    {
        return HtmlAgilityHelper.TrimTexts(htmlNodeCollection);
    }

    /// <summary>
    /// Trims whitespace from all HTML nodes in a list, removing text nodes.
    /// </summary>
    /// <param name="nodes">The list of HTML nodes to trim.</param>
    /// <returns>List of trimmed HTML nodes without text nodes.</returns>
    public static IList<HtmlNode> TrimTexts(IList<HtmlNode> nodes)
    {
        return HtmlAgilityHelper.TrimTexts(nodes as List<HtmlNode> ?? new List<HtmlNode>(nodes));
    }

    /// <summary>
    /// Trims whitespace from all HTML nodes in a list, optionally removing text nodes and comments.
    /// </summary>
    /// <param name="nodes">The list of HTML nodes to trim.</param>
    /// <param name="isRemoveTextNodes">Whether to remove text nodes.</param>
    /// <param name="isRemoveComments">Whether to remove comments.</param>
    /// <returns>List of trimmed HTML nodes.</returns>
    public static IList<HtmlNode> TrimTexts(IList<HtmlNode> nodes, bool isRemoveTextNodes, bool isRemoveComments = false)
    {
        ArgumentNullException.ThrowIfNull(nodes);
        return HtmlAgilityHelper.TrimTextsInternal(nodes as List<HtmlNode> ?? new List<HtmlNode>(nodes), isRemoveTextNodes, isRemoveComments);
    }

    /// <summary>
    /// EN: Recursively searches for tags matching specified tag name.
    /// CZ: Rekurzivně vyhledává tagy odpovídající zadanému názvu tagu.
    /// Supports wildcard "*" to match all tags.
    /// </summary>
    /// <param name="result">The result list to add found nodes to.</param>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for, or "*" for all tags.</param>
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

    /// <summary>
    /// EN: Recursively searches for all tags matching any of the specified tag names.
    /// CZ: Rekurzivně vyhledává všechny tagy odpovídající některému ze zadaných názvů tagů.
    /// </summary>
    /// <param name="result">The result list to add found nodes to.</param>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagNames">Tag names to search for.</param>
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

    /// <summary>
    /// EN: Checks if an HTML node has the specified tag name.
    /// CZ: Zkontroluje zda má HTML uzel zadaný název tagu.
    /// Supports wildcard "*" to match any tag name.
    /// </summary>
    /// <param name="htmlNode">The HTML node to check.</param>
    /// <param name="tagName">The tag name to check for, or "*" for any tag.</param>
    /// <returns>True if the node has the tag name, false otherwise.</returns>
    private static bool HasTagName(HtmlNode htmlNode, string tagName)
    {
        if (tagName == "*")
            return true;
        return htmlNode.Name == tagName;
    }

    /// <summary>
    /// EN: Recursively searches for tags matching specified tag name and attribute value.
    /// CZ: Rekurzivně vyhledává tagy odpovídající zadanému názvu tagu a hodnotě atributu.
    /// Supports wildcard "*" for tag name to match all tags.
    /// Supports wildcard "*" for attribute value to match any value.
    /// </summary>
    /// <param name="result">The result list to add found nodes to.</param>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for, or "*" for all tags.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match, or "*" for any value.</param>
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

    /// <summary>
    /// EN: Checks if an HTML node has an attribute with the specified name and value.
    /// CZ: Zkontroluje zda má HTML uzel atribut se zadaným názvem a hodnotou.
    /// Supports wildcard "*" for attribute value to match any value.
    /// </summary>
    /// <param name="htmlNode">The HTML node to check.</param>
    /// <param name="attributeName">The attribute name to check for.</param>
    /// <param name="attributeValue">The attribute value to match, or "*" for any value.</param>
    /// <param name="isEnoughContains">Whether to use Contains instead of exact match.</param>
    /// <returns>True if the node has the attribute with matching value, false otherwise.</returns>
    private static bool HasTagAttr(HtmlNode htmlNode, string attributeName, string attributeValue, bool isEnoughContains)
    {
        if (attributeValue == "*")
            return true;
        var contains = false;
        var attrValue = GetValueOfAttribute(attributeName, htmlNode);
        if (isEnoughContains)
            contains = attrValue.Contains(attributeValue, StringComparison.Ordinal);
        else
            contains = attrValue == attributeValue;
        return contains;
    }

    /// <summary>
    /// EN: Returns the first child tag matching the specified tag name.
    /// CZ: Vrátí první podřízený tag odpovídající zadanému názvu tagu.
    /// Returns null if tag is not found.
    /// </summary>
    /// <param name="htmlNode">The parent HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <returns>First matching HTML node or null.</returns>
    public static HtmlNode? ReturnTag(HtmlNode htmlNode, string tagName)
    {
        ArgumentNullException.ThrowIfNull(htmlNode);
        foreach (var item in htmlNode.ChildNodes)
            if (item.Name == tagName)
                return item;
        return null;
    }

    /// <summary>
    /// EN: Replaces a child node by matching its OuterHtml with a new node.
    /// CZ: Nahradí podřízený uzel porovnáním jeho OuterHtml s novým uzlem.
    /// </summary>
    /// <param name="htmlNode">The parent node containing the child to replace.</param>
    /// <param name="oldOuterHtml">The OuterHtml of the child node to replace.</param>
    /// <param name="newNode">The new node to replace with.</param>
    public static void ReplaceChildNodeByOuterHtml(HtmlNode htmlNode, string oldOuterHtml, HtmlNode newNode)
    {
        ArgumentNullException.ThrowIfNull(htmlNode);
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
