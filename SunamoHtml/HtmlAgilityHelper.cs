namespace SunamoHtml;

// HtmlHelperText - for methods which NOT operate on HtmlAgiityHelper!
// HtmlAgilityHelper - getting new nodes
// HtmlAssistant - Only for methods which operate on HtmlAgiityHelper!
/// <summary>
/// Represents the html agility helper type.
/// </summary>
public partial class HtmlAgilityHelper
{
    /// <summary>
    /// The text node constant.
    /// </summary>
    public const string TextNode = "#text";
    // Previously was false but that was incorrect.
    private static bool _trimTexts = true;

    /// <summary>
    /// Pairs dd dt.
    /// </summary>
    /// <param name="dl">The dl.</param>
    /// <param name="recursive">The recursive.</param>
    /// <param name="replaceHtmLForText">The replace htm l for text.</param>
    /// <returns>The pairs dd dt result.</returns>
    public static Dictionary<string, string> PairsDdDt(HtmlNode dl, bool recursive, Dictionary<string, string> replaceHtmLForText)
    {
        if (dl == null) throw new ArgumentNullException(nameof(dl));
        if (replaceHtmLForText == null) throw new ArgumentNullException(nameof(replaceHtmLForText));
        var dd = Nodes(dl, recursive, "dd");
        var dt = Nodes(dl, recursive, "dt");
        if (dd.Count == 0 && dt.Count == 0)
            throw new InvalidOperationException("dd && dt is zero");
        ThrowEx.IsEmpty(dt, "dd");
        ThrowEx.IsEmpty(dt, "dt");
        ThrowEx.DifferentCountInLists("dd", dd, "dt", dt);
        var result = new Dictionary<string, string>();
        for (var i = 0; i < dd.Count; i++)
        {
            // Text is necessary here
            var key = JoinHtmlElementsToOneString(dt[i]);
            var val = JoinHtmlElementsToOneString(dd[i]);
            foreach (var item in replaceHtmLForText)
            {
                key = key.Replace(item.Key, item.Value, StringComparison.Ordinal);
                val = val.Replace(item.Key, item.Value, StringComparison.Ordinal);
            }

            // Default replaces with " "
            // Here using "" because for area we don't want "63 m 2". If needed differently, add parameter here
            try
            {
                result.Add(HtmlHelper.StripAllTags(key, "").Trim(), HtmlHelper.StripAllTags(val, "").Trim());
            }
            catch
            {
                throw;
            }
        }

        return result;
    }

    private static string JoinHtmlElementsToOneString(HtmlNode htmlNode, string delimiter = ", ")
    {
        string result = "";
        HtmlAssistant.RemoveComments(htmlNode);
        var nodes = HtmlAgilityHelper.Nodes(htmlNode, false, "*");
        if (nodes.Count == 0)
        {
            result = htmlNode.InnerText;
        }
        else
        {
            var previousInnerText = htmlNode.InnerText;
            htmlNode = GetNodeWithoutInnerHtmlNodes(htmlNode);
            var nodesNew = HtmlAgilityHelper.Nodes(htmlNode, false, "*");
            if (nodesNew.Count != 0)
            {
                nodes = nodesNew;
            }

            if (nodes.Count != 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in nodes)
                {
                    stringBuilder.Append(item.InnerText + delimiter);
                }

                result = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
                if (string.IsNullOrEmpty(result))
                {
                    result = previousInnerText.Trim();
                }
            }
        }

        return result;
    }

    private static HtmlNode GetNodeWithoutInnerHtmlNodes(HtmlNode htmlNode)
    {
        var nodes = HtmlAgilityHelper.Nodes(htmlNode, false, "*");
        if (nodes.Count == 0)
        {
            // For area, it returns here already
            return htmlNode;
        }

        // For accessories, it returns here
        return nodes[0];
    }

    /// <summary>
    /// Node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="recursive">The recursive.</param>
    /// <param name="tag">The tag.</param>
    /// <returns>The node result.</returns>
    public static HtmlNode? Node(HtmlNode? node, bool recursive, string tag)
    {
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        return Nodes(node, recursive, tag).FirstOrDefault();
    }

    // Return null if not found
    /// <summary>
    /// Node with attr.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="recursive">The recursive.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="attr">The attr.</param>
    /// <param name="attrValue">The attr value.</param>
    /// <param name="contains">The contains.</param>
    /// <returns>The node with attr result.</returns>
    public static HtmlNode? NodeWithAttr(HtmlNode? node, bool recursive, string tag, string attr, string attrValue, bool contains = false)
    {
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        if (attr == null) throw new ArgumentNullException(nameof(attr));
        if (attrValue == null) throw new ArgumentNullException(nameof(attrValue));
        return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, false, contains).FirstOrDefault();
    }

    /// <summary>
    /// Returns the first node whose attribute equals the token or contains it as one of its whitespace separated tokens; null when the node is null or nothing matches.
    /// </summary>
    /// <param name="node">The node to search in; null gives null.</param>
    /// <param name="isRecursive">Whether all descendants are searched, not only direct children.</param>
    /// <param name="tag">The tag name, "*" for any tag.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="token">The whole attribute value or one of its tokens (e.g. one CSS class).</param>
    /// <returns>The first matching node or null.</returns>
    public static HtmlNode? NodeWithAttrToken(HtmlNode? node, bool isRecursive, string tag, string attributeName, string token)
    {
        return NodesWithAttrToken(node, isRecursive, tag, attributeName, token).FirstOrDefault();
    }

    /// <summary>
    /// Returns the nodes whose attribute equals the token or contains it as one of its whitespace separated tokens (class="a b" matches "a", "b" and "a b"); an empty list when the node is null.
    /// </summary>
    /// <param name="node">The node to search in; null gives an empty list.</param>
    /// <param name="isRecursive">Whether all descendants are searched, not only direct children.</param>
    /// <param name="tag">The tag name, "*" for any tag.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="token">The whole attribute value or one of its tokens (e.g. one CSS class).</param>
    /// <returns>The matching nodes in document order.</returns>
    public static List<HtmlNode> NodesWithAttrToken(HtmlNode? node, bool isRecursive, string tag, string attributeName, string token)
    {
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        if (attributeName == null) throw new ArgumentNullException(nameof(attributeName));
        if (token == null) throw new ArgumentNullException(nameof(token));
        var result = new List<HtmlNode>();
        foreach (var item in Nodes(node, isRecursive, tag))
        {
            var attribute = item.Attributes[attributeName];
            if (attribute == null)
                continue;
            var actual = attribute.Value ?? string.Empty;
            if (actual == token || actual.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Contains(token))
                result.Add(item);
        }

        return result;
    }

    /// <summary>
    /// Nodes which contains in attr.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="recursive">The recursive.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="attr">The attr.</param>
    /// <param name="attrValue">The attr value.</param>
    /// <param name="false">The false.</param>
    /// <param name="contains">The contains.</param>
    /// <returns>The nodes which contains in attr result.</returns>
    public static IList<HtmlNode> NodesWhichContainsInAttr(HtmlNode? node, bool recursive, string tag, string attr, string attrValue, bool searchAsSingleString = true)
    {
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        if (attr == null) throw new ArgumentNullException(nameof(attr));
        if (attrValue == null) throw new ArgumentNullException(nameof(attrValue));
        return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, false, searchAsSingleString);
    }

    /// <summary>
    /// Replace plain uri for anchors.
    /// </summary>
    /// <param name=""Design"">The design.</param>
    /// <param name=""CA1055:UriReturnValuesShouldNotBeStrings"">The CA 1055 uri return values should not be strings.</param>
    /// <returns>The replace plain uri for anchors result.</returns>
    [SuppressMessage("Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
    public static string ReplacePlainUriForAnchors(string html)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        var htmlDocument = CreateHtmlDocument();
        return ReplacePlainUriForAnchors(htmlDocument, html);
    }

    /// <summary>
    /// Replace plain uri for anchors.
    /// </summary>
    /// <param name=""Design"">The design.</param>
    /// <param name=""CA1055:UriReturnValuesShouldNotBeStrings"">The CA 1055 uri return values should not be strings.</param>
    /// <returns>The replace plain uri for anchors result.</returns>
    [SuppressMessage("Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
    public static string ReplacePlainUriForAnchors(HtmlDocument htmlDocument, string html)
    {
        if (htmlDocument == null) throw new ArgumentNullException(nameof(htmlDocument));
        if (html == null) throw new ArgumentNullException(nameof(html));
        /*
         * It gets messed up here, it is appended to the end of </installedapp></installedapp></installedapp></string></string>.
         * Even after stepping through it I did not find where it originates, so the simplest fix is to remove it during formatting
         */
        html = WrapIntoTagIfNot(html);
        htmlDocument.LoadHtml(html);
        var textNodes = TextNodes(htmlDocument.DocumentNode, "a");
        for (var i = textNodes.Count - 1; i >= 0; i--)
        {
            var item = textNodes[i];
            if (item.ParentNode.Name == "pre")
                continue;
            var data = SHSplit.SplitByWhiteSpaces(item.InnerText);
            var changed = CAChangeContent.ChangeContentWithCondition(new ChangeContentArgsHtml(), data, RegexHelper.IsUri, HtmlGenerator2.Anchor);
            item.InnerHtml = string.Empty;
            InsertGroup(item, data);
        // must be last because use ParentNode above
        }

        var output = htmlDocument.DocumentNode.OuterHtml;
        return output;
    }

    /// <summary>
    /// Wrap into tag if not.
    /// </summary>
    /// <param name="html">The html.</param>
    /// <param name="tag">The tag.</param>
    /// <returns>The wrap into tag if not result.</returns>
    public static string WrapIntoTagIfNot(string html, string tag = HtmlTags.Div)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        html = html.Trim();
        if (html[0] != '<')
            html = WrapIntoTag(tag, html);
        return html;
    }

    private static string WrapIntoTag(string tag, string html)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append('<');
        stringBuilder.Append(tag);
        stringBuilder.Append('>');
        stringBuilder.Append(html);
        stringBuilder.Append('<' + string.Empty + '/');
        stringBuilder.Append(tag);
        stringBuilder.Append('>');
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Insert group.
    /// </summary>
    /// <returns>The insert group result.</returns>
    public static void InsertGroup(HtmlNode insertAfter, List<string> list)
    {
        if (insertAfter == null) throw new ArgumentNullException(nameof(insertAfter));
        if (list == null) throw new ArgumentNullException(nameof(list));
        foreach (var item in list)
            insertAfter.InnerHtml += SH.WrapWithChar(item, ' ');
        insertAfter.InnerHtml = SHReplace.ReplaceAllDoubleSpaceToSingle(insertAfter.InnerHtml).Trim();
    }

    /// <summary>
    /// Create node.
    /// </summary>
    /// <param name="insertAfter.InnerHtml">The insert after inner html.</param>
    /// <returns>The create node result.</returns>
    public static HtmlNode CreateNode(string html)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        if (!RegexHelper.RHtmlTag.IsMatch(html))
            html = SH.WrapWithChar(html, ' ');
        return HtmlNode.CreateNode(html);
    }

    private static List<HtmlNode> TextNodes(HtmlNode node, params string[] dontHaveAsParentTag)
    {
        /*
         * I tried https://www.nuget.org/p/ because <a href=\"https://jepsano.net/\">https://jepsano.net/</a> another text https://www.nuget.org/p/ divide into:
         * I tried https://www.nuget.org/p/ because
         * <a href=\"https://jepsano.net/\">
         * https://jepsano.net/ with parent a
         * another text https://www.nuget.org/p/
         *
         */
        var result = new List<HtmlNode>();
        var allNodes = new List<HtmlNode>();
        RecursiveReturnTags(allNodes, node, true, false, "*");
        foreach (var item in allNodes)
            if (item.Name == TextNode)
                if (!dontHaveAsParentTag.Contains(item.ParentNode.Name))
                    result.Add(item);
        return result;
    }

    /// <summary>
    /// Trim texts.
    /// </summary>
    /// <param name="htmlNodeCollection">The html node collection.</param>
    /// <returns>The trim texts result.</returns>
    public static List<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
    {
        if (htmlNodeCollection == null) throw new ArgumentNullException(nameof(htmlNodeCollection));
        if (!_trimTexts)
            return htmlNodeCollection.ToList();
        var result = new List<HtmlNode>();
        foreach (var item in htmlNodeCollection)
            if (item.Name != TextNode)
                result.Add(item);
        return result;
    }

    /// <summary>
    /// Find ancestor parent node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The find ancestor parent node result.</returns>
    public static HtmlNode? FindAncestorParentNode(HtmlNode node, string tagName)
    {
        if (tagName == null) throw new ArgumentNullException(nameof(tagName));
        while (node != null)
        {
            if (node.Name == tagName)
                return node;
            node = node.ParentNode;
        }

        return null;
    }

    /// <summary>
    /// Has ancestor parent node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="tagName">The tag name.</param>
    /// <returns>The has ancestor parent node result.</returns>
    public static bool HasAncestorParentNode(HtmlNode node, string tagName)
    {
        if (tagName == null) throw new ArgumentNullException(nameof(tagName));
        while (node != null)
        {
            if (node.Name == tagName)
                return true;
            node = node.ParentNode;
        }

        return false;
    }

    /// <summary>
    /// Trim texts.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <returns>The trim texts result.</returns>
    public static List<HtmlNode> TrimTexts(List<HtmlNode> nodes)
    {
        return TrimTextsInternal(nodes, true);
    }
}
