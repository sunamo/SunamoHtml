namespace SunamoHtml.Html;

public static partial class HtmlHelper
{
    // Returns the first tag with specified name and attribute value.
    /// <summary>
    /// Get tag of atribute.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <returns>The get tag of atribute result.</returns>
    public static HtmlNode? GetTagOfAtribute(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        htmlNode = TrimNode(htmlNode);
        foreach (var childNode in htmlNode.ChildNodes)
        {
            var currentNode = childNode;
            if (currentNode.Name == tagName)
            {
                if (GetValueOfAttribute(attributeName, currentNode) == attributeValue)
                    return currentNode;
                foreach (var grandChild in currentNode.ChildNodes)
                    if (GetValueOfAttribute(attributeName, grandChild) == attributeValue)
                        return grandChild;
            }
        }

        return null;
    }

    // Returns all tags with specified name and attribute value, recursively searching the node tree.
    // Originally from HtmlDocument.
    /// <summary>
    /// Return tags with attr rek 2.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <returns>The return tags with attr rek 2 result.</returns>
    public static IList<HtmlNode> ReturnTagsWithAttrRek2(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        RecursiveReturnAllTags(result, htmlNode, tagName);
        for (var i = result.Count - 1; i >= 0; i--)
            if (GetValueOfAttribute(attributeName, result[i]) != attributeValue)
                result.RemoveAt(i);
        return result;
    }

    // Returns all immediate child tags with specified name and attribute value.
    /// <summary>
    /// Get tags of atribute.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <returns>The get tags of atribute result.</returns>
    public static IList<HtmlNode> GetTagsOfAtribute(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        foreach (var childNode in htmlNode.ChildNodes)
            if (childNode.Name == tagName)
                if (GetValueOfAttribute(attributeName, childNode) == attributeValue)
                    result.Add(childNode);
        return result;
    }

    // Recursively searches for tags with attribute value containing specified text.
    private static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        RecursiveReturnTagsWithContainsAttr(result, htmlNode, tagName, attributeName, attributeValue, true, true);
    }

    // Recursively searches for tags with attribute value matching specified criteria.
    // Supports wildcard "*" for tag name to match all tags.
    /// <summary>
    /// Recursive return tags with contains attr.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <param name="isContains">The is contains.</param>
    /// <param name="isRecursively">The is recursively.</param>
    public static void RecursiveReturnTagsWithContainsAttr(IList<HtmlNode> result, HtmlNode htmlNode, string tagName, string attributeName, string attributeValue, bool isContains, bool isRecursively)
    {
        if (result == null) throw new ArgumentNullException(nameof(result));
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        foreach (var item in htmlNode.ChildNodes)
        {
            var attrValue = GetValueOfAttribute(attributeName, item);
            if (isContains)
                isContains = attrValue.Contains(attributeValue, StringComparison.Ordinal);
            else
                isContains = attrValue == attributeValue;
            if (HasTagName(item, tagName) && isContains)
            {
                if (!result.Contains(item))
                    result.Add(item);
            }
            else
            {
                if (isRecursively)
                    RecursiveReturnTagsWithContainsAttr(result, item, tagName, attributeName, attributeValue, isContains, isRecursively);
            }
        }
    }

    // Recursively searches for tags with attribute value containing specified text after splitting by delimiter.
    // Supports wildcard "*" for tag name to match all tags.
    private static void RecursiveReturnTagsWithContainsAttrWithSplittedElement(List<HtmlNode> result, HtmlNode htmlNode, string tagName, string attributeName, string attributeValue, string delimiter)
    {
        foreach (var item in htmlNode.ChildNodes)
            if (HasTagName(item, tagName) && HasTagAttrContains(item, delimiter, attributeName, attributeValue))
            {
                if (!result.Contains(item))
                    result.Add(item);
            }
            else
            {
                RecursiveReturnTagsWithContainsAttrWithSplittedElement(result, item, tagName, attributeName, attributeValue, delimiter);
            }
    }

    // Returns all tags with attribute value containing specified text, recursively searching the node tree.
    // Supports wildcard "*" for tag name to match all tags.
    /// <summary>
    /// Return tags with contains attr rek.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <returns>The return tags with contains attr rek result.</returns>
    public static IList<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, htmlNode, tagName, attributeName, attributeValue);
        return result;
    }

    // Returns all tags with attribute value matching specified criteria, recursively searching the node tree.
    /// <summary>
    /// Return tags with contains attr rek.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <param name="isContains">The is contains.</param>
    /// <param name="isRecursively">The is recursively.</param>
    /// <returns>The return tags with contains attr rek result.</returns>
    public static IList<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue, bool isContains, bool isRecursively)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, htmlNode, tagName, attributeName, attributeValue, isContains, isRecursively);
        return result;
    }

    // Returns all tags with class attribute containing specified class name, recursively searching the node tree.
    // Supports wildcard "*" for tag name to match all tags.
    /// <summary>
    /// Return tags with contains class rek.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="className">The class name.</param>
    /// <returns>The return tags with contains class rek result.</returns>
    public static IList<HtmlNode> ReturnTagsWithContainsClassRek(HtmlNode htmlNode, string tagName, string className)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttrWithSplittedElement(result, htmlNode, tagName, "class", className, " ");
        return result;
    }

    /// <summary>
    /// Return tag rek.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The return tag rek result.</returns>
    public static HtmlNode ReturnTagRek(HtmlNode htmlNode, object tagName)
    {
        throw new NotImplementedException();
    }
}
