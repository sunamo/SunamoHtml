namespace SunamoHtml.Html;

public static partial class HtmlHelper
{
    // EN: Returns the first tag with specified name and attribute value.
    // CZ: Vrátí první tag se zadaným názvem a hodnotou atributu.
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

    // EN: Returns all tags with specified name and attribute value, recursively searching the node tree.
    // CZ: Vrátí všechny tagy se zadaným názvem a hodnotou atributu, rekurzivně prohledá strom uzlů.
    // Originally from HtmlDocument.
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

    // EN: Returns all immediate child tags with specified name and attribute value.
    // CZ: Vrátí všechny přímé podřízené tagy se zadaným názvem a hodnotou atributu.
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

    // EN: Recursively searches for tags with attribute value containing specified text.
    // CZ: Rekurzivně vyhledává tagy s hodnotou atributu obsahující zadaný text.
    private static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        RecursiveReturnTagsWithContainsAttr(result, htmlNode, tagName, attributeName, attributeValue, true, true);
    }

    // EN: Recursively searches for tags with attribute value matching specified criteria.
    // CZ: Rekurzivně vyhledává tagy s hodnotou atributu odpovídající zadaným kritériím.
    // Supports wildcard "*" for tag name to match all tags.
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

    // EN: Recursively searches for tags with attribute value containing specified text after splitting by delimiter.
    // CZ: Rekurzivně vyhledává tagy s hodnotou atributu obsahující zadaný text po rozdělení pomocí oddělovače.
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

    // EN: Returns all tags with attribute value containing specified text, recursively searching the node tree.
    // CZ: Vrátí všechny tagy s hodnotou atributu obsahující zadaný text, rekurzivně prohledá strom uzlů.
    // Supports wildcard "*" for tag name to match all tags.
    public static IList<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, htmlNode, tagName, attributeName, attributeValue);
        return result;
    }

    // EN: Returns all tags with attribute value matching specified criteria, recursively searching the node tree.
    // CZ: Vrátí všechny tagy s hodnotou atributu odpovídající zadaným kritériím, rekurzivně prohledá strom uzlů.
    public static IList<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue, bool isContains, bool isRecursively)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, htmlNode, tagName, attributeName, attributeValue, isContains, isRecursively);
        return result;
    }

    // EN: Returns all tags with class attribute containing specified class name, recursively searching the node tree.
    // CZ: Vrátí všechny tagy s atributem class obsahujícím zadaný název třídy, rekurzivně prohledá strom uzlů.
    // Supports wildcard "*" for tag name to match all tags.
    public static IList<HtmlNode> ReturnTagsWithContainsClassRek(HtmlNode htmlNode, string tagName, string className)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttrWithSplittedElement(result, htmlNode, tagName, "class", className, " ");
        return result;
    }

    public static HtmlNode ReturnTagRek(HtmlNode htmlNode, object tagName)
    {
        throw new NotImplementedException();
    }
}
