namespace SunamoHtml.Html;

/// <summary>
/// Helper class with various HTML manipulation methods (parsing, attribute handling, HTML decoding, etc.).
/// Note: This is a mix of various HTML utilities - consider splitting into more specific classes.
/// </summary>
public static class HtmlAssistant
{
    /// <summary>
    /// Parses the inner text of every TD element in a table row.
    /// </summary>
    /// <param name="tr">The table row (TR) HTML node.</param>
    /// <returns>List of trimmed inner text values from all TD elements.</returns>
    public static IList<string> ParseInnerTextOfEveryTd(HtmlNode tr)
    {
        var tds = HtmlAgilityHelper.Nodes(tr, false, "td");

        var result = new List<string>();
        foreach (var item in tds)
            result.Add(item.InnerText.Trim());

        return result;
    }

    /// <summary>
    /// Removes all style tags from HTML text.
    /// </summary>
    /// <param name="html">The HTML text to process.</param>
    /// <returns>HTML with all style tags removed.</returns>
    public static string RemoveStyleTagsText(string html)
    {
        ArgumentNullException.ThrowIfNull(html);
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var styleNodes = doc.DocumentNode.SelectNodes("//style");
        if (styleNodes != null)
        {
            foreach (var node in styleNodes)
            {
                node.Remove();
            }
        }

        return doc.DocumentNode.OuterHtml;
    }

    /// <summary>
    /// Gets the value of an HTML attribute from a node.
    /// Returns empty string if attribute is not found.
    /// Returns "(null)" when attribute exists without a value (e.g., input readonly).
    /// </summary>
    /// <param name="attributeName">The name of the attribute to get.</param>
    /// <param name="node">The HTML node to get the attribute from.</param>
    /// <param name="isTrim">Whether to trim the attribute value.</param>
    /// <returns>Attribute value, empty string if not found, or "(null)" if attribute exists without value.</returns>
    public static string GetValueOfAttribute(string attributeName, HtmlNode node, bool isTrim = false)
    {
        ArgumentNullException.ThrowIfNull(node);
        object o = node.Attributes[attributeName];
        if (o != null)
        {
            var st = ((HtmlAttribute)o).Value;
            if (isTrim)
                st = st.Trim();

            if (string.IsNullOrEmpty(st))
                return "(null)";

            return st;
        }

        return string.Empty;
    }

    /// <summary>
    /// Trims the inner HTML of all elements in the HTML value.
    /// </summary>
    /// <param name="value">The HTML string to process.</param>
    /// <returns>HTML with trimmed inner HTML for all elements.</returns>
    public static string TrimInnerHtml(string value)
    {
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        htmlDocument.LoadHtml(value);
        foreach (var item in htmlDocument.DocumentNode.DescendantsAndSelf())
            if (item.NodeType == HtmlNodeType.Element)
                item.InnerHtml = item.InnerHtml.Trim();
        return htmlDocument.DocumentNode.OuterHtml;
    }

    /// <summary>
    /// Splits HTML input by BR tags.
    /// </summary>
    /// <param name="html">The HTML input to split.</param>
    /// <returns>List of HTML segments split by BR tags.</returns>
    public static IList<string> SplitByBr(string html)
    {
        return SplitByTag(html, "br");
    }

    /// <summary>
    /// Removes all HTML comment nodes from the given node and its children recursively.
    /// </summary>
    /// <param name="node">The HTML node to remove comments from.</param>
    public static void RemoveComments(HtmlNode node)
    {
        ArgumentNullException.ThrowIfNull(node);
        if (!node.HasChildNodes)
            return;

        for (var i = 0; i < node.ChildNodes.Count; i++)
            if (node.ChildNodes[i].NodeType == HtmlNodeType.Comment)
            {
                node.ChildNodes.RemoveAt(i);
                --i;
            }

        foreach (var subNode in node.ChildNodes)
            RemoveComments(subNode);
    }

    /// <summary>
    /// Splits HTML input by specified tag.
    /// Converts non-pair tags to XML-valid format before splitting.
    /// </summary>
    /// <param name="html">The HTML input to split.</param>
    /// <param name="tagName">The tag name to split by.</param>
    /// <returns>List of HTML segments split by the specified tag.</returns>
    public static IList<string> SplitByTag(string html, string tagName)
    {
        ArgumentNullException.ThrowIfNull(html);
        var validatedHtml = html;
        validatedHtml = HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(validatedHtml);
        var lines = SHSplit.Split(validatedHtml, tagName);
        return lines;
    }

    /// <summary>
    /// Sets an attribute on an HTML node, removing any existing attributes with the same name first.
    /// </summary>
    /// <param name="node">The HTML node to set the attribute on.</param>
    /// <param name="attributeName">The name of the attribute.</param>
    /// <param name="value">The value for the attribute.</param>
    public static void SetAttribute(HtmlNode node, string attributeName, string value)
    {
        ArgumentNullException.ThrowIfNull(node);
        HtmlAttribute? o = null;
        while (true)
        {
            o = node.Attributes.FirstOrDefault(a => a.Name == attributeName);
            if (o != null)
                node.Attributes.Remove(o);
            else
                break;
        }

        var atr2 = node.OwnerDocument.CreateAttribute(attributeName, value);
        node.Attributes.Add(atr2);
    }

    /// <summary>
    /// Gets the inner text of a node that matches specified tag and attribute criteria.
    /// </summary>
    /// <param name="node">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <param name="isContains">Whether to use contains matching for attribute value.</param>
    /// <returns>HTML-decoded and trimmed inner text, or empty string if not found.</returns>
    public static string InnerText(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue,
        bool isContains = false)
    {
        ArgumentNullException.ThrowIfNull(node);
        return InnerContentWithAttr(node, isRecursive, tag, attributeName, attributeValue, false, isContains);
    }

    /// <summary>
    /// Gets the inner HTML of a node that matches specified tag and attribute criteria.
    /// </summary>
    /// <param name="node">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <param name="isContains">Whether to use contains matching for attribute value.</param>
    /// <returns>HTML-decoded and trimmed inner HTML, or empty string if not found.</returns>
    public static string InnerHtmlWithAttr(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue,
        bool isContains = false)
    {
        return InnerContentWithAttr(node, isRecursive, tag, attributeName, attributeValue, true, isContains);
    }

    /// <summary>
    /// Core method for getting inner content (HTML or text) of a node matching attribute criteria.
    /// </summary>
    /// <param name="node">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to match.</param>
    /// <param name="attributeValue">The attribute value to match.</param>
    /// <param name="isHtml">True to return InnerHtml, false to return InnerText.</param>
    /// <param name="isContains">Whether to use contains matching for attribute value.</param>
    /// <returns>HTML-decoded and trimmed content, or empty string if not found.</returns>
    public static string InnerContentWithAttr(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue,
        bool isHtml, bool isContains = false)
    {
        var node2 = HtmlAgilityHelper.NodeWithAttr(node, isRecursive, tag, attributeName, attributeValue, isContains);
        if (node2 != null)
        {
            var content = string.Empty;
            if (isHtml)
                content = node2.InnerHtml;
            else
                content = node2.InnerText;

            return HtmlDecode(content.Trim());
        }

        return string.Empty;
    }

    /// <summary>
    /// Decodes HTML-encoded text.
    /// </summary>
    /// <param name="text">The HTML-encoded text.</param>
    /// <returns>Decoded text.</returns>
    public static string HtmlDecode(string text)
    {
        return WebUtility.HtmlDecode(text);
    }

    /// <summary>
    /// Gets any header element (H1-H6) from the document.
    /// </summary>
    /// <param name="node">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="isStopAfterFirst">Whether to stop after finding the first header.</param>
    /// <returns>List of found header nodes.</returns>
    public static IList<HtmlNode> GetAnyHeader(HtmlNode node, bool isRecursive, bool isStopAfterFirst)
    {
        var headers = new List<HtmlNode>();
        for (var i = 1; i < 7; i++)
        {
            var headerNode = HtmlAgilityHelper.Node(node, isRecursive, "h" + i);

            if (headerNode != null)
            {
                headers.Add(headerNode);
                if (isStopAfterFirst)
                    break;
            }
        }

        return headers;
    }

    /// <summary>
    /// Removes all attributes from an HTML node and replaces it with a clean version.
    /// </summary>
    /// <param name="node">The HTML node to remove attributes from.</param>
    /// <returns>The new clean node that replaced the original.</returns>
    public static HtmlNode RemoveAllAttrs(HtmlNode node)
    {
        ArgumentNullException.ThrowIfNull(node);
        var tagL = node.Name.ToLowerInvariant();
        string html;
        if (AllLists.HtmlNonPairTags.Contains(tagL))
            html = "<" + tagL + " />";
        else
            html = "<" + tagL + "></" + tagL + ">";

        var hn = HtmlNode.CreateNode(html);
        return node.ParentNode.ReplaceChild(hn, node);
    }

    /// <summary>
    /// Gets attribute values from a list of HTML nodes.
    /// </summary>
    /// <param name="anchors">List of HTML nodes.</param>
    /// <param name="attributeName">The attribute name to get values for.</param>
    /// <returns>List of attribute values.</returns>
    public static IList<string> AttrsValues(IList<HtmlNode> anchors, string attributeName)
    {
        ArgumentNullException.ThrowIfNull(anchors);
        var result = new List<string>();

        foreach (var item in anchors)
            result.Add(GetValueOfAttribute(attributeName, item));

        return result;
    }

    /// <summary>
    /// Decodes and trims inner text, replacing whitespace characters and double spaces.
    /// </summary>
    /// <param name="result">The inner text to process.</param>
    /// <returns>Cleaned and decoded text.</returns>
    public static string InnerTextDecodeTrim(string result)
    {
        result = SHReplace.ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(result, " ");
        result = WebUtility.HtmlDecode(result);
        result = SHReplace.ReplaceAllDoubleSpaceToSingle(result);
        return result;
    }

    /// <summary>
    /// Gets the decoded and trimmed inner text from an HTML node.
    /// </summary>
    /// <param name="node">The HTML node.</param>
    /// <returns>Cleaned and decoded inner text.</returns>
    public static string InnerTextDecodeTrim(HtmlNode node)
    {
        ArgumentNullException.ThrowIfNull(node);
        var result = node.InnerText.Trim();
        return InnerTextDecodeTrim(result);
    }

    /// <summary>
    /// Gets the inner text of a child node with specified tag.
    /// </summary>
    /// <param name="node">The parent HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <returns>Inner text of found node, or empty string if not found.</returns>
    public static string InnerText(HtmlNode node, bool isRecursive, string tag)
    {
        var foundNode = HtmlAgilityHelper.Node(node, isRecursive, tag);
        if (foundNode == null)
            return string.Empty;
        return foundNode.InnerText;
    }

    /// <summary>
    /// Gets the inner HTML of a child node with specified tag.
    /// </summary>
    /// <param name="node">The parent HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <returns>Inner HTML of found node, or empty string if not found.</returns>
    public static string InnerHtml(HtmlNode node, bool isRecursive, string tag)
    {
        var foundNode = HtmlAgilityHelper.Node(node, isRecursive, tag);
        if (foundNode == null)
            return string.Empty;
        return foundNode.InnerHtml;
    }

    /// <summary>
    /// Parses HTML attributes from text into a dictionary.
    /// If text doesn't contain HTML tags, wraps it in an img tag first.
    /// </summary>
    /// <param name="text">The HTML text or attributes string.</param>
    /// <returns>Dictionary of attribute name-value pairs.</returns>
    public static Dictionary<string, string> GetAttributesPairs(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        if (!text.Contains('<'))
            text = "<img " + text + "/>";

        var result = new Dictionary<string, string>();

        var node = HtmlNode.CreateNode(text);
        foreach (var item in node.Attributes)
            result.Add(item.Name, item.Value);

        return result;
    }
}