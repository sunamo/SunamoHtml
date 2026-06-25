namespace SunamoHtml.Html;

public static partial class HtmlHelper
{
    public static string ConvertHtmlToText(string htmlContent)
    {
        if (htmlContent == null) throw new ArgumentNullException(nameof(htmlContent));
        htmlContent = WebUtility.HtmlDecode(htmlContent);
        htmlContent = SHReplace.ReplaceAllArray(htmlContent, Environment.NewLine, "<br>", "<br />", "<br/>");
        htmlContent = StripAllTags(htmlContent);
        return htmlContent;
    }

    // EN: Strips all HTML tags from text, replacing them with a single space.
    // CZ: Odstraní všechny HTML tagy z textu, nahradí je jednou mezerou.
    public static string StripAllTags(string text)
    {
        return StripAllTags(text, " ");
    }

    // EN: Strips all HTML tags from text, replacing them with a specified replacement string.
    // CZ: Odstraní všechny HTML tagy z textu, nahradí je zadaným řetězcem.
    public static string StripAllTags(string text, string replacement)
    {
        var result = Regex.Replace(text, @"<[^>]*>", replacement);
        result = SHReplace.ReplaceAllDoubleSpaceToSingle(result);
        return result;
    }

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

    // EN: Returns all tags matching the specified tag name, recursively searching the node tree.
    // CZ: Vrátí všechny tagy odpovídající zadanému názvu tagu, rekurzivně prohledá strom uzlů.
    // Supports wildcard "*" to match all tags.
    public static IList<HtmlNode> ReturnTagsRek(HtmlNode htmlNode, string tagName)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        RecursiveReturnTags(result, htmlNode, tagName);
        return TrimTexts(result);
    }

    // EN: Returns the first tag with specified name and attribute value, recursively searching the node tree.
    // CZ: Vrátí první tag se zadaným názvem a hodnotou atributu, rekurzivně prohledá strom uzlů.
    // Returns null if tag is not found.
    public static HtmlNode? ReturnTagWithAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        return ReturnTagWithAttr(htmlNode, tagName, attributeName, attributeValue);
    }

    // EN: Returns all tags matching specified name and attribute value, recursively searching the node tree.
    // CZ: Vrátí všechny tagy odpovídající zadanému názvu a hodnotě atributu, rekurzivně prohledá strom uzlů.
    // Supports wildcard "*" for tag name to match all tags.
    // Supports wildcard "*" for attribute value to match any value.
    public static IList<HtmlNode> ReturnTagsWithAttrRek(HtmlNode htmlNode, string tagName, string attributeName, string attributeValue)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithAttr(result, htmlNode, tagName, attributeName, attributeValue);
        return result;
    }

    // EN: Returns all child tags matching specified tag names.
    // CZ: Vrátí všechny podřízené tagy odpovídající zadaným názvům tagů.
    public static IList<HtmlNode> ReturnAllTags(HtmlNode htmlNode, params string[] tagNames)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        if (tagNames == null) throw new ArgumentNullException(nameof(tagNames));
        var result = new List<HtmlNode>();
        RecursiveReturnAllTags(result, htmlNode, tagNames);
        return result;
    }

    public static IList<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
    {
        return HtmlAgilityHelper.TrimTexts(htmlNodeCollection);
    }

    public static IList<HtmlNode> TrimTexts(IList<HtmlNode> nodes)
    {
        return HtmlAgilityHelper.TrimTexts(nodes as List<HtmlNode> ?? new List<HtmlNode>(nodes));
    }

    public static IList<HtmlNode> TrimTexts(IList<HtmlNode> nodes, bool isRemoveTextNodes, bool isRemoveComments = false)
    {
        if (nodes == null) throw new ArgumentNullException(nameof(nodes));
        return HtmlAgilityHelper.TrimTextsInternal(nodes as List<HtmlNode> ?? new List<HtmlNode>(nodes), isRemoveTextNodes, isRemoveComments);
    }

    // EN: Recursively searches for tags matching specified tag name.
    // CZ: Rekurzivně vyhledává tagy odpovídající zadanému názvu tagu.
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

    // EN: Recursively searches for all tags matching any of the specified tag names.
    // CZ: Rekurzivně vyhledává všechny tagy odpovídající některému ze zadaných názvů tagů.
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

    // EN: Checks if an HTML node has the specified tag name.
    // CZ: Zkontroluje zda má HTML uzel zadaný název tagu.
    // Supports wildcard "*" to match any tag name.
    private static bool HasTagName(HtmlNode htmlNode, string tagName)
    {
        if (tagName == "*")
            return true;
        return htmlNode.Name == tagName;
    }

    // EN: Recursively searches for tags matching specified tag name and attribute value.
    // CZ: Rekurzivně vyhledává tagy odpovídající zadanému názvu tagu a hodnotě atributu.
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

    // EN: Checks if an HTML node has an attribute with the specified name and value.
    // CZ: Zkontroluje zda má HTML uzel atribut se zadaným názvem a hodnotou.
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

    // EN: Returns the first child tag matching the specified tag name.
    // CZ: Vrátí první podřízený tag odpovídající zadanému názvu tagu.
    // Returns null if tag is not found.
    public static HtmlNode? ReturnTag(HtmlNode htmlNode, string tagName)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        foreach (var item in htmlNode.ChildNodes)
            if (item.Name == tagName)
                return item;
        return null;
    }

    // EN: Replaces a child node by matching its OuterHtml with a new node.
    // CZ: Nahradí podřízený uzel porovnáním jeho OuterHtml s novým uzlem.
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
