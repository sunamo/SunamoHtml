namespace SunamoHtml.Html;

/// <summary>
/// Represents the html assistant type.
/// </summary>
public static class HtmlAssistant
{
    /// <summary>
    /// Parse inner text of every td.
    /// </summary>
    /// <param name="tr">The tr.</param>
    /// <returns>The parse inner text of every td result.</returns>
    public static IList<string> ParseInnerTextOfEveryTd(HtmlNode tr)
    {
        var tds = HtmlAgilityHelper.Nodes(tr, false, "td");

        var result = new List<string>();
        foreach (var item in tds)
            result.Add(item.InnerText.Trim());

        return result;
    }

    /// <summary>
    /// Remove style tags text.
    /// </summary>
    /// <param name="html">The html.</param>
    /// <returns>The remove style tags text result.</returns>
    public static string RemoveStyleTagsText(string html)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
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

    // Returns empty string if attribute is not found.
    // Returns "(null)" when attribute exists without a value (e.g., input readonly).
    /// <summary>
    /// Get value of attribute.
    /// </summary>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="node">The node.</param>
    /// <param name="isTrim">The is trim.</param>
    /// <returns>The get value of attribute result.</returns>
    public static string GetValueOfAttribute(string attributeName, HtmlNode node, bool isTrim = false)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
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
    /// Trim inner html.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The trim inner html result.</returns>
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
    /// Split by br.
    /// </summary>
    /// <param name="html">The html.</param>
    /// <returns>The split by br result.</returns>
    public static IList<string> SplitByBr(string html)
    {
        return SplitByTag(html, "br");
    }

    /// <summary>
    /// Remove comments.
    /// </summary>
    /// <param name="node">The node.</param>
    public static void RemoveComments(HtmlNode node)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
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
    /// Split by tag.
    /// </summary>
    /// <param name="html">The html.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The split by tag result.</returns>
    public static IList<string> SplitByTag(string html, string tagName)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        var validatedHtml = html;
        validatedHtml = HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(validatedHtml);
        var lines = SHSplit.Split(validatedHtml, tagName);
        return lines;
    }

    /// <summary>
    /// Set attribute.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="value">The value.</param>
    public static void SetAttribute(HtmlNode node, string attributeName, string value)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
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
    /// Inner text.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="isRecursive">The is recursive.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <param name="isContains">The is contains.</param>
    /// <returns>The inner text result.</returns>
    public static string InnerText(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue,
        bool isContains = false)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        return InnerContentWithAttr(node, isRecursive, tag, attributeName, attributeValue, false, isContains);
    }

    /// <summary>
    /// Inner html with attr.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="isRecursive">The is recursive.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <param name="isContains">The is contains.</param>
    /// <returns>The inner html with attr result.</returns>
    public static string InnerHtmlWithAttr(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue,
        bool isContains = false)
    {
        return InnerContentWithAttr(node, isRecursive, tag, attributeName, attributeValue, true, isContains);
    }

    /// <summary>
    /// Inner content with attr.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="isRecursive">The is recursive.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    /// <param name="isHtml">The is html.</param>
    /// <param name="isContains">The is contains.</param>
    /// <returns>The inner content with attr result.</returns>
    public static string InnerContentWithAttr(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue,
        bool isHtml, bool isContains = false)
    {
        var node2 = HtmlAgilityHelper.NodeWithAttr(node, isRecursive, tag, attributeName, attributeValue, isContains);
        if (node2 != null)
        {
            var content = isHtml ? node2.InnerHtml : node2.InnerText;
            return HtmlDecode(content.Trim());
        }

        return string.Empty;
    }

    /// <summary>
    /// Html decode.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The html decode result.</returns>
    public static string HtmlDecode(string text)
    {
        return WebUtility.HtmlDecode(text);
    }

    /// <summary>
    /// Get any header.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="isRecursive">The is recursive.</param>
    /// <param name="isStopAfterFirst">The is stop after first.</param>
    /// <returns>The get any header result.</returns>
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
    /// Remove all attrs.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns>The remove all attrs result.</returns>
    public static HtmlNode RemoveAllAttrs(HtmlNode node)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
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
    /// Attrs values.
    /// </summary>
    /// <param name="anchors">The anchors.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <returns>The attrs values result.</returns>
    public static IList<string> AttrsValues(IList<HtmlNode> anchors, string attributeName)
    {
        if (anchors == null) throw new ArgumentNullException(nameof(anchors));
        var result = new List<string>();

        foreach (var item in anchors)
            result.Add(GetValueOfAttribute(attributeName, item));

        return result;
    }

    /// <summary>
    /// Inner text decode trim.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>The inner text decode trim result.</returns>
    public static string InnerTextDecodeTrim(string result)
    {
        result = SHReplace.ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(result, " ");
        result = WebUtility.HtmlDecode(result);
        result = SHReplace.ReplaceAllDoubleSpaceToSingle(result);
        return result;
    }

    /// <summary>
    /// Inner text decode trim.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns>The inner text decode trim result.</returns>
    public static string InnerTextDecodeTrim(HtmlNode node)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        var result = node.InnerText.Trim();
        return InnerTextDecodeTrim(result);
    }

    /// <summary>
    /// Inner text.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="isRecursive">The is recursive.</param>
    /// <param name="tag">The tag.</param>
    /// <returns>The inner text result.</returns>
    public static string InnerText(HtmlNode node, bool isRecursive, string tag)
    {
        var foundNode = HtmlAgilityHelper.Node(node, isRecursive, tag);
        if (foundNode == null)
            return string.Empty;
        return foundNode.InnerText;
    }

    /// <summary>
    /// Inner html.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="isRecursive">The is recursive.</param>
    /// <param name="tag">The tag.</param>
    /// <returns>The inner html result.</returns>
    public static string InnerHtml(HtmlNode node, bool isRecursive, string tag)
    {
        var foundNode = HtmlAgilityHelper.Node(node, isRecursive, tag);
        if (foundNode == null)
            return string.Empty;
        return foundNode.InnerHtml;
    }

    // If text doesn't contain HTML tags, wraps it in an img tag first.
    /// <summary>
    /// Get attributes pairs.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The get attributes pairs result.</returns>
    public static Dictionary<string, string> GetAttributesPairs(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        if (!text.Contains('<'))
            text = "<img " + text + "/>";

        var result = new Dictionary<string, string>();

        var node = HtmlNode.CreateNode(text);
        foreach (var item in node.Attributes)
            result.Add(item.Name, item.Value);

        return result;
    }
}
