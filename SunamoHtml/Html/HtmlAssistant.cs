namespace SunamoHtml.Html;

public static class HtmlAssistant
{
    public static IList<string> ParseInnerTextOfEveryTd(HtmlNode tr)
    {
        var tds = HtmlAgilityHelper.Nodes(tr, false, "td");

        var result = new List<string>();
        foreach (var item in tds)
            result.Add(item.InnerText.Trim());

        return result;
    }

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

    public static string TrimInnerHtml(string value)
    {
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        htmlDocument.LoadHtml(value);
        foreach (var item in htmlDocument.DocumentNode.DescendantsAndSelf())
            if (item.NodeType == HtmlNodeType.Element)
                item.InnerHtml = item.InnerHtml.Trim();
        return htmlDocument.DocumentNode.OuterHtml;
    }

    public static IList<string> SplitByBr(string html)
    {
        return SplitByTag(html, "br");
    }

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

    public static IList<string> SplitByTag(string html, string tagName)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        var validatedHtml = html;
        validatedHtml = HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(validatedHtml);
        var lines = SHSplit.Split(validatedHtml, tagName);
        return lines;
    }

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

    public static string InnerText(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue,
        bool isContains = false)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        return InnerContentWithAttr(node, isRecursive, tag, attributeName, attributeValue, false, isContains);
    }

    public static string InnerHtmlWithAttr(HtmlNode node, bool isRecursive, string tag, string attributeName, string attributeValue,
        bool isContains = false)
    {
        return InnerContentWithAttr(node, isRecursive, tag, attributeName, attributeValue, true, isContains);
    }

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

    public static string HtmlDecode(string text)
    {
        return WebUtility.HtmlDecode(text);
    }

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

    public static IList<string> AttrsValues(IList<HtmlNode> anchors, string attributeName)
    {
        if (anchors == null) throw new ArgumentNullException(nameof(anchors));
        var result = new List<string>();

        foreach (var item in anchors)
            result.Add(GetValueOfAttribute(attributeName, item));

        return result;
    }

    public static string InnerTextDecodeTrim(string result)
    {
        result = SHReplace.ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(result, " ");
        result = WebUtility.HtmlDecode(result);
        result = SHReplace.ReplaceAllDoubleSpaceToSingle(result);
        return result;
    }

    public static string InnerTextDecodeTrim(HtmlNode node)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        var result = node.InnerText.Trim();
        return InnerTextDecodeTrim(result);
    }

    public static string InnerText(HtmlNode node, bool isRecursive, string tag)
    {
        var foundNode = HtmlAgilityHelper.Node(node, isRecursive, tag);
        if (foundNode == null)
            return string.Empty;
        return foundNode.InnerText;
    }

    public static string InnerHtml(HtmlNode node, bool isRecursive, string tag)
    {
        var foundNode = HtmlAgilityHelper.Node(node, isRecursive, tag);
        if (foundNode == null)
            return string.Empty;
        return foundNode.InnerHtml;
    }

    // If text doesn't contain HTML tags, wraps it in an img tag first.
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
