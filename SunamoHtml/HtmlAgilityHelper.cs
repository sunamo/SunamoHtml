namespace SunamoHtml;

// HtmlHelperText - for methods which NOT operate on HtmlAgiityHelper!
// HtmlAgilityHelper - getting new nodes
// HtmlAssistant - Only for methods which operate on HtmlAgiityHelper!
public partial class HtmlAgilityHelper
{
    public const string TextNode = "#text";
    // Previously was false but that was incorrect.
    private static bool _trimTexts = true;

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

    public static HtmlNode? Node(HtmlNode node, bool recursive, string tag)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        return Nodes(node, recursive, tag).FirstOrDefault();
    }

    // Return null if not found
    public static HtmlNode? NodeWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool contains = false)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        if (attr == null) throw new ArgumentNullException(nameof(attr));
        if (attrValue == null) throw new ArgumentNullException(nameof(attrValue));
        return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, false, contains).FirstOrDefault();
    }

    public static IList<HtmlNode> NodesWhichContainsInAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool searchAsSingleString = true)
    {
        if (node == null) throw new ArgumentNullException(nameof(node));
        if (tag == null) throw new ArgumentNullException(nameof(tag));
        if (attr == null) throw new ArgumentNullException(nameof(attr));
        if (attrValue == null) throw new ArgumentNullException(nameof(attrValue));
        return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, false, searchAsSingleString);
    }

    [SuppressMessage("Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
    public static string ReplacePlainUriForAnchors(string html)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        var htmlDocument = CreateHtmlDocument();
        return ReplacePlainUriForAnchors(htmlDocument, html);
    }

    [SuppressMessage("Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
    public static string ReplacePlainUriForAnchors(HtmlDocument htmlDocument, string html)
    {
        if (htmlDocument == null) throw new ArgumentNullException(nameof(htmlDocument));
        if (html == null) throw new ArgumentNullException(nameof(html));
        /*
         * Kurvi se mi to tady, pridava se na konec </installedapp></installedapp></installedapp></string></string>.
         * Zde jsem ani po krokovani neobjevil kde to vznika, cimz bude to nejnoduzssi odstranit pri formatu
         */
        html = WrapIntoTagIfNot(html);
        htmlDocument.LoadHtml(html);
        var textNodes = TextNodes(htmlDocument.DocumentNode, "a");
        //RegexHelper.rUri = rUri;
        for (var i = textNodes.Count - 1; i >= 0; i--)
        {
            var item = textNodes[i];
            if (item.ParentNode.Name == "pre")
                continue;
            var data = SHSplit.SplitByWhiteSpaces(item.InnerText);
            var changed = CAChangeContent.ChangeContentWithCondition(new ChangeContentArgsHtml(), data, RegexHelper.IsUri, HtmlGenerator2.Anchor);
            item.InnerHtml = string.Empty;
            InsertGroup(item, data);
        //item.ParentNode.ReplaceChild(CreateNode(item.InnerHtml), item);
        // must be last because use ParentNode above
        //item.ParentNode.RemoveChild(item);
        //new HtmlNode(HtmlNodeType.Element, htmlDocument, 0);
        //    var ret = item.ParentNode.ReplaceChild(newNode, item);
        //newNode.ParentNode.InsertAfter(HtmlNode.CreateNode(data[1]), newNode);
        //int x = 0;
        //}
        }

        var output = htmlDocument.DocumentNode.OuterHtml;
        return output;
    }

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

    public static void InsertGroup(HtmlNode insertAfter, List<string> list)
    {
        if (insertAfter == null) throw new ArgumentNullException(nameof(insertAfter));
        if (list == null) throw new ArgumentNullException(nameof(list));
        foreach (var item in list)
            insertAfter.InnerHtml += SH.WrapWithChar(item, ' ');
        //insertAfter = insertAfter.ParentNode.InsertAfter(CreateNode(item), insertAfter);
        insertAfter.InnerHtml = SHReplace.ReplaceAllDoubleSpaceToSingle(insertAfter.InnerHtml).Trim();
    }

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
                if (!dontHaveAsParentTag.Any(data => data != item.ParentNode.Name) /*!CAG.IsEqualToAnyElement<string>(item.ParentNode.Name, dontHaveAsParentTag)*/)
                    result.Add(item);
        return result;
    }

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

    public static List<HtmlNode> TrimTexts(List<HtmlNode> nodes)
    {
        return TrimTextsInternal(nodes, true);
    }
}
