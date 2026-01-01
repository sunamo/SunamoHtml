namespace SunamoHtml.Html;

/// <summary>
/// EN: Shared HTML helper methods (partial class - part 2).
/// CZ: Sdílené HTML pomocné metody (partial class - část 2).
/// </summary>
public static partial class HtmlHelper
{
    /// <summary>
    /// EN: Converts HTML to final XML format by replacing non-pair tags with XML-valid versions and removing XML declarations.
    /// CZ: Převede HTML do finálního XML formátu nahrazením nepárových tagů XML-validními verzemi a odstraněním XML deklarací.
    /// </summary>
    /// <param name="xml">The HTML/XML content to convert.</param>
    /// <returns>XML with UTF-8 declaration and valid non-pair tags.</returns>
    public static string ToXmlFinal(string xml)
    {
        xml = ReplaceHtmlNonPairTagsWithXmlValid(xml);
        xml = XH.RemoveXmlDeclaration(xml);
        return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + ReplaceHtmlNonPairTagsWithXmlValid(XH.RemoveXmlDeclaration(xml.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-2\" />", "").Replace("<?xml version=\"1.0\" encoding=\"utf-8\" />", "").Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" />", "")));
    }

    /// <summary>
    /// Deletes all attributes from all HTML nodes in a list.
    /// </summary>
    /// <param name="nodes">The list of HTML nodes to remove attributes from.</param>
    public static void DeleteAttributesFromAllNodes(List<HtmlNode> nodes)
    {
        foreach (var node in nodes)
            for (var i = node.Attributes.Count - 1; i >= 0; i--)
                node.Attributes.RemoveAt(i);
    }

    /// <summary>
    /// EN: Converts HTML to XML format, optionally removing XML declaration.
    /// CZ: Převede HTML do XML formátu, volitelně odstraní XML deklaraci.
    /// Already calls ReplaceHtmlNonPairTagsWithXmlValid.
    /// </summary>
    /// <param name="xml">The HTML content to convert.</param>
    /// <param name="isRemoveXmlDeclaration">Whether to remove the XML declaration.</param>
    /// <returns>XML-formatted content.</returns>
    public static string ToXml(string xml, bool isRemoveXmlDeclaration)
    {
        var doc = HtmlAgilityHelper.CreateHtmlDocument();
        doc.LoadHtml(xml);
        var sw = new StringWriter();
        var tw = XmlWriter.Create(sw);
        doc.DocumentNode.WriteTo(tw);
        tw.Flush();
        sw.Flush();
        var result = sw.ToString();
        if (isRemoveXmlDeclaration)
            result = XH.RemoveXmlDeclaration(result);
        result = ReplaceHtmlNonPairTagsWithXmlValid(result);
        return result;
    }

    /// <summary>
    /// EN: Converts HTML to XML format, removing XML declaration.
    /// CZ: Převede HTML do XML formátu, odstraní XML deklaraci.
    /// Already calls RemoveXmlDeclaration and ReplaceHtmlNonPairTagsWithXmlValid.
    /// </summary>
    /// <param name="xml">The HTML content to convert.</param>
    /// <returns>XML-formatted content without declaration.</returns>
    public static string ToXml(string xml)
    {
        return ToXml(xml, true);
    }

    /// <summary>
    /// EN: Strips all HTML tags from text and returns individual words as a list.
    /// CZ: Odstraní všechny HTML tagy z textu a vrátí jednotlivá slova jako seznam.
    /// Use RemoveAllNodes when need to remove also inner HTML.
    /// </summary>
    /// <param name="text">The HTML text to process.</param>
    /// <returns>List of words without HTML tags.</returns>
    public static List<string> StripAllTagsList(string text)
    {
        var replaced = StripAllTags(text, " ");
        return SHSplit.Split(replaced, " ");
    }

    /// <summary>
    /// EN: Strips all HTML tags from text, replacing them with a space.
    /// CZ: Odstraní všechny HTML tagy z textu, nahradí je mezerou.
    /// Replaces every tag &lt;*&gt; with a space. Inner non-XML content is left as is.
    /// </summary>
    /// <param name="text">The text to strip tags from.</param>
    /// <returns>Text without HTML tags.</returns>
    public static string StripAllTagsSpace(string text)
    {
        return Regex.Replace(text, @"<[^>]*>", " ");
    }

    /// <summary>
    /// EN: Removes all HTML tags from text. Just calls StripAllTags method.
    /// CZ: Odstraní všechny HTML tagy z textu. Pouze volá metodu StripAllTags.
    /// Replaces every tag &lt;*&gt; with a period. Inner non-XML content is left as is.
    /// </summary>
    /// <param name="text">The text to remove tags from.</param>
    /// <returns>Text without HTML tags.</returns>
    public static string RemoveAllTags(string text)
    {
        return StripAllTags(text);
    }

    /// <summary>
    /// Checks if an HTML node has an attribute whose value, when split by delimiter, contains the specified value.
    /// </summary>
    /// <param name="htmlNode">The HTML node to check.</param>
    /// <param name="delimiter">The delimiter to split the attribute value by.</param>
    /// <param name="attributeName">The attribute name to check.</param>
    /// <param name="value">The value to search for in the split parts.</param>
    /// <returns>True if the attribute value contains the value after splitting, false otherwise.</returns>
    public static bool HasTagAttrContains(HtmlNode htmlNode, string delimiter, string attributeName, string value)
    {
        var attrValue = GetValueOfAttribute(attributeName, htmlNode);
        var spl = SHSplit.Split(attrValue, delimiter);
        return spl.Contains(value);
    }

    /// <summary>
    /// Checks if an HTML node has a child tag with the specified tag name.
    /// </summary>
    /// <param name="htmlNode">The HTML node to check.</param>
    /// <param name="tagName">The tag name to search for in children.</param>
    /// <returns>True if the node has a child tag with the specified name, false otherwise.</returns>
    public static bool HasChildTag(HtmlNode htmlNode, string tagName)
    {
        return ReturnTags(htmlNode, tagName).Count != 0;
    }

    /// <summary>
    /// EN: Returns HTML with all tags of specified type modified by the handler.
    /// CZ: Vrátí HTML se všemi tagy zadaného typu upravenými handlerem.
    /// Not suitable for returning content of entire page.
    /// </summary>
    /// <param name="text">The source code of the entire page.</param>
    /// <param name="tagName">The tag name to search for (div, a, etc.).</param>
    /// <param name="handler">The handler method to apply to each tag.</param>
    /// <param name="value">Optional parameter passed to the handler.</param>
    /// <returns>Modified HTML content.</returns>
    public static string ReturnApplyToAllTags(string text, string tagName, EditHtmlWidthHandler handler, string value)
    {
        var result = new List<HtmlNode>();
        var doc = HtmlAgilityHelper.CreateHtmlDocument();
        doc.LoadHtml(text);
        var htmlNode = doc.DocumentNode;
        RecursiveApplyToAllTags(result, ref htmlNode, tagName, handler, value);
        return htmlNode.OuterHtml;
    }

    /// <summary>
    /// EN: Recursively applies a handler to all tags matching specified tag name.
    /// CZ: Rekurzivně aplikuje handler na všechny tagy odpovídající zadanému názvu.
    /// </summary>
    /// <param name="result">The collection of nodes to which the handler was applied.</param>
    /// <param name="htmlNode">The referenced node into which changes are directly reflected.</param>
    /// <param name="tagName">The tag name to search for (div, a, etc.).</param>
    /// <param name="handler">The method that performs the changes.</param>
    /// <param name="value">Optional parameter passed to the handler.</param>
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
                    var data = handler.Invoke(ref item, value);
                }
            }
            else
            {
                RecursiveApplyToAllTags(result, ref item, tagName, handler, value);
            }
        }
    }

    /// <summary>
    /// EN: Parses the style attribute of an HTML node and returns it as a dictionary.
    /// CZ: Naparsuje style atribut HTML uzlu a vrátí ho jako slovník.
    /// </summary>
    /// <param name="htmlNode">The HTML node to get style values from.</param>
    /// <returns>Dictionary with style property names as keys and values as values.</returns>
    public static Dictionary<string, string> GetValuesOfStyle(HtmlNode htmlNode)
    {
        var result = new Dictionary<string, string>();
        var styleAttribute = GetValueOfAttribute("style", htmlNode);
        if (styleAttribute.Contains(";"))
        {
            var data = SHSplit.Split(styleAttribute, ";");
            foreach (var item in data)
                if (item.Contains(":"))
                {
                    var keyValue = SHSplit.SplitNone(item, ":");
                    result.Add(keyValue[0].Trim().ToLower(), keyValue[1].Trim().ToLower());
                }
        }

        return result;
    }

    /// <summary>
    /// Returns the first child tag with the specified original name.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The original tag name to search for.</param>
    /// <returns>First matching child tag or null.</returns>
    public static HtmlNode GetTag(HtmlNode htmlNode, string tagName)
    {
        foreach (var item in htmlNode.ChildNodes)
            if (item.OriginalName == tagName)
                return item;
        return null;
    }

    /// <summary>
    /// EN: Recursively returns the first tag matching specified tag name.
    /// CZ: Rekurzivně vrátí první tag odpovídající zadanému názvu tagu.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <returns>First matching tag or null.</returns>
    public static HtmlNode ReturnTagRek(HtmlNode htmlNode, string tagName)
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

    /// <summary>
    /// EN: Returns all immediate child tags matching the specified tag name (non-recursive).
    /// CZ: Vrátí všechny přímé podřízené tagy odpovídající zadanému názvu (nerekurzivně).
    /// If tag is the specified name, doesn't apply recursion on that.
    /// </summary>
    /// <param name="htmlNode">The HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for (e.g., img).</param>
    /// <returns>List of matching child tags.</returns>
    public static List<HtmlNode> ReturnAllTagsImg(HtmlNode htmlNode, string tagName)
    {
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

    /// <summary>
    /// EN: Returns all immediate child tags matching the specified tag name (non-recursive).
    /// CZ: Vrátí všechny přímé podřízené tagy odpovídající zadanému názvu (nerekurzivně).
    /// Wildcard "*" can be passed but wouldn't make much sense.
    /// </summary>
    /// <param name="parentNode">The parent HTML node to search in.</param>
    /// <param name="tagName">The tag name to search for.</param>
    /// <returns>List of matching child tags.</returns>
    public static List<HtmlNode> ReturnTags(HtmlNode parentNode, string tagName)
    {
        var result = new List<HtmlNode>();
        foreach (var item in parentNode.ChildNodes)
            if (HasTagName(item, tagName))
                result.Add(item);
        return result;
    }
}
