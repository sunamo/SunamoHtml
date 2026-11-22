namespace SunamoHtml.Html;

public static partial class HtmlHelper
{
    // Converts HTML to final XML format by replacing non-pair tags with XML-valid versions and removing XML declarations.
    /// <summary>
    /// To xml final.
    /// </summary>
    /// <param name="xml">The xml.</param>
    /// <returns>The to xml final result.</returns>
    public static string ToXmlFinal(string xml)
    {
        xml = ReplaceHtmlNonPairTagsWithXmlValid(xml);
        xml = XH.RemoveXmlDeclaration(xml);
        return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + ReplaceHtmlNonPairTagsWithXmlValid(XH.RemoveXmlDeclaration(xml.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-2\" />", "", StringComparison.Ordinal).Replace("<?xml version=\"1.0\" encoding=\"utf-8\" />", "", StringComparison.Ordinal).Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" />", "", StringComparison.Ordinal)));
    }

    /// <summary>
    /// Delete attributes from all nodes.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    public static void DeleteAttributesFromAllNodes(IList<HtmlNode> nodes)
    {
        if (nodes == null) throw new ArgumentNullException(nameof(nodes));
        foreach (var node in nodes)
            for (var i = node.Attributes.Count - 1; i >= 0; i--)
                node.Attributes.RemoveAt(i);
    }

    // Converts HTML to XML format, optionally removing XML declaration.
    // Already calls ReplaceHtmlNonPairTagsWithXmlValid.
    /// <summary>
    /// To xml.
    /// </summary>
    /// <param name="xml">The xml.</param>
    /// <param name="isRemoveXmlDeclaration">The is remove xml declaration.</param>
    /// <returns>The to xml result.</returns>
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

    // Converts HTML to XML format, removing XML declaration.
    // Already calls RemoveXmlDeclaration and ReplaceHtmlNonPairTagsWithXmlValid.
    /// <summary>
    /// To xml.
    /// </summary>
    /// <param name="xml">The xml.</param>
    /// <returns>The to xml result.</returns>
    public static string ToXml(string xml)
    {
        return ToXml(xml, true);
    }

    // Strips all HTML tags from text and returns individual words as a list.
    // Use RemoveAllNodes when need to remove also inner HTML.
    /// <summary>
    /// Strip all tags list.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The strip all tags list result.</returns>
    public static IList<string> StripAllTagsList(string text)
    {
        var replaced = StripAllTags(text, " ");
        return SHSplit.Split(replaced, " ");
    }

    // Strips all HTML tags from text, replacing them with a space.
    // Replaces every tag <*> with a space. Inner non-XML content is left as is.
    /// <summary>
    /// Strip all tags space.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The strip all tags space result.</returns>
    public static string StripAllTagsSpace(string text)
    {
        return Regex.Replace(text, @"<[^>]*>", " ");
    }

    // Removes all HTML tags from text. Just calls StripAllTags method.
    // Replaces every tag <*> with a period. Inner non-XML content is left as is.
    /// <summary>
    /// Remove all tags.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The remove all tags result.</returns>
    public static string RemoveAllTags(string text)
    {
        return StripAllTags(text);
    }

    /// <summary>
    /// Has tag attr contains.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="value">The value.</param>
    /// <returns>The has tag attr contains result.</returns>
    public static bool HasTagAttrContains(HtmlNode htmlNode, string delimiter, string attributeName, string value)
    {
        var attrValue = GetValueOfAttribute(attributeName, htmlNode);
        var spl = SHSplit.Split(attrValue, delimiter);
        return spl.Contains(value);
    }

    /// <summary>
    /// Has child tag.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The has child tag result.</returns>
    public static bool HasChildTag(HtmlNode htmlNode, string tagName)
    {
        return ReturnTags(htmlNode, tagName).Count != 0;
    }

    // Returns HTML with all tags of specified type modified by the handler.
    // Not suitable for returning content of entire page.
    /// <summary>
    /// Return apply to all tags.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="handler">The handler.</param>
    /// <param name="value">The value.</param>
    /// <returns>The return apply to all tags result.</returns>
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

    // Recursively applies a handler to all tags matching specified tag name.
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

    // Parses the style attribute of an HTML node and returns it as a dictionary.
    /// <summary>
    /// Get values of style.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <returns>The get values of style result.</returns>
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

    /// <summary>
    /// Get tag.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The get tag result.</returns>
    public static HtmlNode? GetTag(HtmlNode htmlNode, string tagName)
    {
        if (htmlNode == null) throw new ArgumentNullException(nameof(htmlNode));
        foreach (var item in htmlNode.ChildNodes)
            if (item.OriginalName == tagName)
                return item;
        return null;
    }

    // Recursively returns the first tag matching specified tag name.
    /// <summary>
    /// Return tag rek.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The return tag rek result.</returns>
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

    // Returns all immediate child tags matching the specified tag name (non-recursive).
    // If tag is the specified name, doesn't apply recursion on that.
    /// <summary>
    /// Return all tags img.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The return all tags img result.</returns>
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

    // Returns all immediate child tags matching the specified tag name (non-recursive).
    // Wildcard "*" can be passed but wouldn't make much sense.
    /// <summary>
    /// Return tags.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The return tags result.</returns>
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
