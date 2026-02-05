namespace SunamoHtml.Html;

/// <summary>
/// EN: Shared HTML helper methods (partial class - part 3).
/// CZ: Sdílené HTML pomocné metody (partial class - část 3).
/// </summary>
public static partial class HtmlHelper
{
    /// <summary>
    /// EN: Returns the first tag with specified name and attribute value.
    /// CZ: Vrátí první tag se zadaným názvem a hodnotou atributu.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <returns>First matching HTML node or null.</returns>
    public static HtmlNode GetTagOfAtribute(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
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

    /// <summary>
    /// EN: Returns all tags with specified name and attribute value, recursively searching the node tree.
    /// CZ: Vrátí všechny tagy se zadaným názvem a hodnotou atributu, rekurzivně prohledá strom uzlů.
    /// Originally from HtmlDocument.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <returns>List of matching HTML nodes.</returns>
    public static List<HtmlNode> ReturnTagsWithAttrRek2(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnAllTags(result, htmlNode, tagName);
        for (var i = result.Count - 1; i >= 0; i--)
            if (GetValueOfAttribute(attributeName, result[i]) != attributeValue)
                result.RemoveAt(i);
        return result;
    }

    /// <summary>
    /// EN: Returns all immediate child tags with specified name and attribute value.
    /// CZ: Vrátí všechny přímé podřízené tagy se zadaným názvem a hodnotou atributu.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <returns>List of matching child HTML nodes.</returns>
    public static List<HtmlNode> GetTagsOfAtribute(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        var result = new List<HtmlNode>();
        foreach (var childNode in htmlNode.ChildNodes)
            if (childNode.Name == tagName)
                if (GetValueOfAttribute(attributeName, childNode) == attributeValue)
                    result.Add(childNode);
        return result;
    }

    /// <summary>
    /// EN: Recursively searches for tags with attribute value containing specified text.
    /// CZ: Rekurzivně vyhledává tagy s hodnotou atributu obsahující zadaný text.
    /// </summary>
    /// <param name="result">The result list to add found nodes to.</param>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to check.</param>
    /// <param name="attributeValue">The attribute value to search for.</param>
    private static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        RecursiveReturnTagsWithContainsAttr(result, htmlNode, tagName, attributeName, attributeValue, true, true);
    }

    /// <summary>
    /// EN: Recursively searches for tags with attribute value matching specified criteria.
    /// CZ: Rekurzivně vyhledává tagy s hodnotou atributu odpovídající zadaným kritériím.
    /// Supports wildcard "*" for tag name to match all tags.
    /// </summary>
    /// <param name="result">The result list to add found nodes to.</param>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for, or "*" for all tags.</param>
    /// <param name="attributeName">The attribute name to check.</param>
    /// <param name="attributeValue">The attribute value to search for.</param>
    /// <param name="isContains">Whether to use Contains instead of exact match.</param>
    /// <param name="isRecursively">Whether to search recursively.</param>
    public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode htmlNode, string tagName, string attributeName, string attributeValue, bool isContains, bool isRecursively)
    {
        foreach (var item in htmlNode.ChildNodes)
        {
            var attrValue = GetValueOfAttribute(attributeName, item);
            if (isContains)
                isContains = attrValue.Contains(attributeValue);
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

    /// <summary>
    /// EN: Recursively searches for tags with attribute value containing specified text after splitting by delimiter.
    /// CZ: Rekurzivně vyhledává tagy s hodnotou atributu obsahující zadaný text po rozdělení pomocí oddělovače.
    /// Supports wildcard "*" for tag name to match all tags.
    /// </summary>
    /// <param name="result">The result list to add found nodes to.</param>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for, or "*" for all tags.</param>
    /// <param name="attributeName">The attribute name to check.</param>
    /// <param name="attributeValue">The attribute value to search for.</param>
    /// <param name="delimiter">The delimiter to split the attribute value by.</param>
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

    /// <summary>
    /// EN: Returns all tags with attribute value containing specified text, recursively searching the node tree.
    /// CZ: Vrátí všechny tagy s hodnotou atributu obsahující zadaný text, rekurzivně prohledá strom uzlů.
    /// Supports wildcard "*" for tag name to match all tags.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for, or "*" for all tags.</param>
    /// <param name="attributeName">The attribute name to check.</param>
    /// <param name="attributeValue">The attribute value to search for.</param>
    /// <returns>List of matching HTML nodes.</returns>
    public static List<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, htmlNode, tagName, attributeName, attributeValue);
        return result;
    }

    /// <summary>
    /// EN: Returns all tags with attribute value matching specified criteria, recursively searching the node tree.
    /// CZ: Vrátí všechny tagy s hodnotou atributu odpovídající zadaným kritériím, rekurzivně prohledá strom uzlů.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to check.</param>
    /// <param name="attributeValue">The attribute value to search for.</param>
    /// <param name="isContains">Whether to use Contains instead of exact match.</param>
    /// <param name="isRecursively">Whether to search recursively.</param>
    /// <returns>List of matching HTML nodes.</returns>
    public static List<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue, bool isContains, bool isRecursively)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, htmlNode, tagName, attributeName, attributeValue, isContains, isRecursively);
        return result;
    }

    /// <summary>
    /// EN: Returns all tags with class attribute containing specified class name, recursively searching the node tree.
    /// CZ: Vrátí všechny tagy s atributem class obsahujícím zadaný název třídy, rekurzivně prohledá strom uzlů.
    /// Supports wildcard "*" for tag name to match all tags.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for, or "*" for all tags.</param>
    /// <param name="className">The class name to search for.</param>
    /// <returns>List of matching HTML nodes.</returns>
    public static List<HtmlNode> ReturnTagsWithContainsClassRek(HtmlNode htmlNode, string tagName, string className)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttrWithSplittedElement(result, htmlNode, tagName, "class", className, " ");
        return result;
    }

    /// <summary>
    /// Recursively returns the first tag matching specified tag name.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <returns>First matching tag or null.</returns>
    public static HtmlNode ReturnTagRek(HtmlNode htmlNode, object tagName)
    {
        throw new NotImplementedException();
    }
}