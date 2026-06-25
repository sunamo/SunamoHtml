namespace SunamoHtml.Html;

public static partial class HtmlHelper
{
    // EN: Converts HTML to final XML format by replacing non-pair tags with XML-valid versions and removing XML declarations.
    // CZ: Převede HTML do finálního XML formátu nahrazením nepárových tagů XML-validními verzemi a odstraněním XML deklarací.
    public static string ToXmlFinal(string xml)
    {
        xml = ReplaceHtmlNonPairTagsWithXmlValid(xml);
        xml = XH.RemoveXmlDeclaration(xml);
        return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + ReplaceHtmlNonPairTagsWithXmlValid(XH.RemoveXmlDeclaration(xml.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-2\" />", "", StringComparison.Ordinal).Replace("<?xml version=\"1.0\" encoding=\"utf-8\" />", "", StringComparison.Ordinal).Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" />", "", StringComparison.Ordinal)));
    }

    public static void DeleteAttributesFromAllNodes(IList<HtmlNode> nodes)
    {
        if (nodes == null) throw new ArgumentNullException(nameof(nodes));
        foreach (var node in nodes)
            for (var i = node.Attributes.Count - 1; i >= 0; i--)
                node.Attributes.RemoveAt(i);
    }

    // EN: Converts HTML to XML format, optionally removing XML declaration.
    // CZ: Převede HTML do XML formátu, volitelně odstraní XML deklaraci.
    // Already calls ReplaceHtmlNonPairTagsWithXmlValid.
    public static string ToXml(string xml, bool isRemoveXmlDeclaration)
    {
        var doc = HtmlAgilityHelper.CreateHtmlDocument();
        doc.LoadHtml(xml);
        using var sw = new StringWriter();
        using var tw = XmlWriter.Create(sw);
        doc.DocumentNode.WriteTo(tw);
        tw.Flush();
        sw.Flush();
        var result = sw.ToString();
        if (isRemoveXmlDeclaration)
            result = XH.RemoveXmlDeclaration(result);
        result = ReplaceHtmlNonPairTagsWithXmlValid(result);
        return result;
    }

    // EN: Converts HTML to XML format, removing XML declaration.
    // CZ: Převede HTML do XML formátu, odstraní XML deklaraci.
    // Already calls RemoveXmlDeclaration and ReplaceHtmlNonPairTagsWithXmlValid.
    public static string ToXml(string xml)
    {
        return ToXml(xml, true);
    }

    // EN: Strips all HTML tags from text and returns individual words as a list.
    // CZ: Odstraní všechny HTML tagy z textu a vrátí jednotlivá slova jako seznam.
    // Use RemoveAllNodes when need to remove also inner HTML.
    public static IList<string> StripAllTagsList(string text)
    {
        var replaced = StripAllTags(text, " ");
        return SHSplit.Split(replaced, " ");
    }

    // EN: Strips all HTML tags from text, replacing them with a space.
    // CZ: Odstraní všechny HTML tagy z textu, nahradí je mezerou.
    // Replaces every tag <*> with a space. Inner non-XML content is left as is.
    public static string StripAllTagsSpace(string text)
    {
        return Regex.Replace(text, @"<[^>]*>", " ");
    }

    // EN: Removes all HTML tags from text. Just calls StripAllTags method.
    // CZ: Odstraní všechny HTML tagy z textu. Pouze volá metodu StripAllTags.
    // Replaces every tag <*> with a period. Inner non-XML content is left as is.
    public static string RemoveAllTags(string text)
    {
        return StripAllTags(text);
    }

    public static bool HasTagAttrContains(HtmlNode htmlNode, string delimiter, string attributeName, string value)
    {
        var attrValue = GetValueOfAttribute(attributeName, htmlNode);
        var spl = SHSplit.Split(attrValue, delimiter);
        return spl.Contains(value);
    }

    public static bool HasChildTag(HtmlNode htmlNode, string tagName)
    {
        return ReturnTags(htmlNode, tagName).Count != 0;
    }

    // EN: Returns HTML with all tags of specified type modified by the handler.
    // CZ: Vrátí HTML se všemi tagy zadaného typu upravenými handlerem.
    // Not suitable for returning content of entire page.
    public static string ReturnApplyToAllTags(string text, string tagName, EditHtmlWidthHandler handler, string value)
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));
        var result = new List<HtmlNode>();
        var doc = HtmlAgilityHelper.CreateHtmlDocument();
        doc.LoadHtml(text);
        var htmlNode = doc.DocumentNode;
        RecursiveApplyToAllTags(result, ref htmlNode, tagName, handler, value);
        return htmlNode.OuterHtml;
    }

    // EN: Recursively applies a handler to all tags matching specified tag name.
    // CZ: Rekurzivně aplikuje handler na všechny tagy odpovídající zadanému názvu.
    private static void RecursiveApplyToAllTags(List<HtmlNode> result, ref HtmlNode htmlNode, string tagName, EditHtmlWidthHandler handler, string value)
    {
        for (var i = 0; i < htmlNode.ChildNodes.Count; i++)
        {
            var item = htmlNode.ChildNodes[i];
            if (item.Name == tagName)
            {
                RecursiveApplyToAllTags(result, ref item, tagName, handler, value);
                if (!result.Contains(item))
                {
                    result.Add(item);
                    var _ = handler.Invoke(ref item, value);
                }
            }
            else
            {
                RecursiveApplyToAllTags(result, ref item, tagName, handler, value);
            }
        }
    }

    // EN: Parses the style attribute of an HTML node and returns it as a dictionary.
    // CZ: Naparsuje style atribut HTML uzlu a vrátí ho jako slovník.
    public static Dictionary<string, string> GetValuesOfStyle(HtmlNode htmlNode)
    {
        var result = new Dictionary<string, string>();
        var styleAttribute = GetValueOfAttribute("style", htmlNode);
        if (styleAttribute.Contains(";", StringComparison.Ordinal))
        {
            var data = SHSplit.Split(styleAttribute, ";");
            foreach (var item in data)
                if (item.Contains(":", StringComparison.Ordinal))
                {
                    var keyValue = SHSplit.SplitNone(item, ":");
                    result.Add(keyValue[0].Trim().ToUpperInvariant(), keyValue[1].Trim().ToUpperInvariant());
                }
        }

        return result;
    }

    public static HtmlNode? GetTag(HtmlNode htmlNode, string tagName)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        foreach (var item in htmlNode.ChildNodes)
            if (item.OriginalName == tagName)
                return item;
        return null;
    }

    // EN: Recursively returns the first tag matching specified tag name.
    // CZ: Rekurzivně vrátí první tag odpovídající zadanému názvu tagu.
    public static HtmlNode? ReturnTagRek(HtmlNode htmlNode, string tagName)
    {
        htmlNode = TrimNode(htmlNode);
        foreach (var childNode in htmlNode.ChildNodes)
        {
            var currentNode = childNode;
            foreach (var item in childNode.ChildNodes)
            {
                if (item.Name == tagName)
                    return item;
                var foundNode = ReturnTagRek(item, tagName);
                if (foundNode != null)
                    return foundNode;
            }

            if (currentNode.Name == tagName)
                return currentNode;
        }

        return null;
    }

    // EN: Returns all immediate child tags matching the specified tag name (non-recursive).
    // CZ: Vrátí všechny přímé podřízené tagy odpovídající zadanému názvu (nerekurzivně).
    // If tag is the specified name, doesn't apply recursion on that.
    public static IList<HtmlNode> ReturnAllTagsImg(HtmlNode htmlNode, string tagName)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        foreach (var item in htmlNode.ChildNodes)
            if (item.Name == tagName)
            {
                var node = item.ParentNode;
                if (node != null)
                    result.Add(item);
            }
            else
            {
                result.AddRange(ReturnAllTags(item, tagName));
            }

        return result;
    }

    // EN: Returns all immediate child tags matching the specified tag name (non-recursive).
    // CZ: Vrátí všechny přímé podřízené tagy odpovídající zadanému názvu (nerekurzivně).
    // Wildcard "*" can be passed but wouldn't make much sense.
    public static IList<HtmlNode> ReturnTags(HtmlNode htmlNode, string tagName)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        var result = new List<HtmlNode>();
        foreach (var item in htmlNode.ChildNodes)
            if (HasTagName(item, tagName))
                result.Add(item);
        return result;
    }
}
