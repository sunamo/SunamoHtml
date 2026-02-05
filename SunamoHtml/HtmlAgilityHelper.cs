namespace SunamoHtml;

/// <summary>
///     HtmlHelperText - for methods which NOT operate on HtmlAgiityHelper!
///     HtmlAgilityHelper - getting new nodes
///     HtmlAssistant - Only for methods which operate on HtmlAgiityHelper!
/// </summary>
public partial class HtmlAgilityHelper
{
    /// <summary>
    /// Constant representing the text node type in HTML DOM.
    /// </summary>
    public const string TextNode = "#text";
    /// <summary>
    /// Previously was false but that was incorrect.
    /// </summary>
    private static bool _trimTexts = true;

    /// <summary>
    /// Extracts key-value pairs from HTML definition list (DL) by pairing DT (term) and DD (definition) elements.
    /// </summary>
    /// <param name="dl">The DL (definition list) HTML node to parse.</param>
    /// <param name="recursive">Whether to search recursively in child nodes.</param>
    /// <param name="replaceHtmLForText">Dictionary of HTML replacements to apply to extracted text.</param>
    /// <returns>Dictionary with DT text as keys and DD text as values.</returns>
    public static Dictionary<string, string> PairsDdDt(HtmlNode dl, bool recursive, Dictionary<string, string> replaceHtmLForText)
    {
        var dd = Nodes(dl, recursive, "dd");
        var dt = Nodes(dl, recursive, "dt");
        if (dd.Count == 0 && dt.Count == 0)
            throw new Exception("dd && dt is zero");
        ThrowEx.IsEmpty(dt, "dd");
        ThrowEx.IsEmpty(dt, "dt");
        ThrowEx.DifferentCountInLists("dd", dd, "dt", dt);
        var result = new Dictionary<string, string>();
        for (var i = 0; i < dd.Count; i++)
        {
            // Text is necessary here
            var key = JoinHtmlElementsToOneString(dt[i]);
#if DEBUG
            if (key == "Plocha:")
            {
            }
#endif
            var val = JoinHtmlElementsToOneString(dd[i]);
            foreach (var item in replaceHtmLForText)
            {
                key = key.Replace(item.Key, item.Value);
                val = val.Replace(item.Key, item.Value);
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
                if (result == "")
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
    /// Finds the first HTML node matching the specified tag within the given node.
    /// </summary>
    /// <param name="node">The parent HTML node to search within.</param>
    /// <param name="recursive">Whether to search recursively in child nodes.</param>
    /// <param name="tag">The HTML tag name to search for.</param>
    /// <returns>The first matching HTML node, or null if not found.</returns>
    public static HtmlNode Node(HtmlNode node, bool recursive, string tag)
    {
        return Nodes(node, recursive, tag).FirstOrDefault();
    }

    /// <summary>
    ///     Return null if not found
    /// </summary>
    /// <param name = "node"></param>
    /// <param name = "recursive"></param>
    /// <param name = "tag"></param>
    /// <param name = "attr"></param>
    /// <param name = "attrValue"></param>
    /// <param name = "contains"></param>
    /// <returns></returns>
    public static HtmlNode NodeWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool contains = false)
    {
        return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, false, contains).FirstOrDefault();
    }

    /// <summary>
    /// Finds all HTML nodes where the specified attribute contains the given value.
    /// </summary>
    /// <param name = "node">The parent HTML node to search within.</param>
    /// <param name = "recursive">Whether to search recursively in child nodes.</param>
    /// <param name = "tag">The HTML tag name to search for.</param>
    /// <param name = "attr">The attribute name to check.</param>
    /// <param name = "attrValue">The value to search for within the attribute.</param>
    /// <param name = "searchAsSingleString">Whether to search the attribute value as a single string (true) or split by whitespace (false).</param>
    /// <returns>List of matching HTML nodes.</returns>
    public static List<HtmlNode> NodesWhichContainsInAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool searchAsSingleString = true)
    {
        return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, false, searchAsSingleString);
    }

    /// <summary>
    /// Replaces plain URIs in text with HTML anchor tags.
    /// </summary>
    /// <param name="html">The HTML string to process.</param>
    /// <returns>HTML string with plain URIs converted to anchor tags.</returns>
    public static string ReplacePlainUriForAnchors(string html)
    {
        var htmlDocument = CreateHtmlDocument();
        return ReplacePlainUriForAnchors(htmlDocument, html);
    }

    /// <summary>
    /// Replaces plain URIs in text with HTML anchor tags using the provided HtmlDocument.
    /// </summary>
    /// <param name="htmlDocument">The HtmlDocument to use for parsing.</param>
    /// <param name="html">The HTML string to process.</param>
    /// <returns>HTML string with plain URIs converted to anchor tags.</returns>
    public static string ReplacePlainUriForAnchors(HtmlDocument htmlDocument, string html)
    {
        /*
         * Kurví se mi to tady, přidává se na konec </installedapp></installedapp></installedapp></string></string>.
         * Zde jsem ani po krokování neobjevil kde to vzniká, čímž bude to nejnodušší odstranit při formátu
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
            var changed = CAChangeContent.ChangeContentWithCondition(null, data, RegexHelper.IsUri, HtmlGenerator2.Anchor);
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

    /// <summary>
    /// Wraps the input string in an HTML tag if it doesn't already start with a tag.
    /// </summary>
    /// <param name="html">The string to potentially wrap.</param>
    /// <param name="tag">The HTML tag to use for wrapping (default is div).</param>
    /// <returns>The wrapped HTML string.</returns>
    public static string WrapIntoTagIfNot(string html, string tag = HtmlTags.Div)
    {
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
    /// Inserts a group of strings as inner HTML of the specified node, wrapping each string with spaces.
    /// </summary>
    /// <param name="insertAfter">The HTML node to insert content into.</param>
    /// <param name="list">List of strings to insert as inner HTML.</param>
    public static void InsertGroup(HtmlNode insertAfter, List<string> list)
    {
        foreach (var item in list)
            insertAfter.InnerHtml += SH.WrapWithChar(item, ' ');
        //insertAfter = insertAfter.ParentNode.InsertAfter(CreateNode(item), insertAfter);
        insertAfter.InnerHtml = SHReplace.ReplaceAllDoubleSpaceToSingle(insertAfter.InnerHtml).Trim();
    }

    /// <summary>
    /// Creates an HTML node from the given HTML string, wrapping non-tag content with spaces.
    /// </summary>
    /// <param name="html">The HTML string to create a node from.</param>
    /// <returns>The created HTML node.</returns>
    public static HtmlNode CreateNode(string html)
    {
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

    /// <summary>
    /// Removes text nodes from an HTML node collection, keeping everything else.
    /// </summary>
    /// <param name="htmlNodeCollection">The HTML node collection to trim.</param>
    /// <returns>List of nodes with text nodes removed.</returns>
    public static List<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
    {
        if (!_trimTexts)
            return htmlNodeCollection.ToList();
        var result = new List<HtmlNode>();
        foreach (var item in htmlNodeCollection)
            if (item.Name != TextNode)
                result.Add(item);
        return result;
    }

    /// <summary>
    /// Finds an ancestor parent node with the specified tag name.
    /// </summary>
    /// <param name="node">The starting HTML node.</param>
    /// <param name="tagName">The tag name to search for in ancestors.</param>
    /// <returns>The ancestor node with matching tag name, or null if not found.</returns>
    public static HtmlNode FindAncestorParentNode(HtmlNode node, string tagName)
    {
        while (node != null)
        {
            if (node.Name == tagName)
                return node;
            node = node.ParentNode;
        }

        return null;
    }

    /// <summary>
    /// Checks if the node has an ancestor with the specified tag name.
    /// </summary>
    /// <param name="node">The starting HTML node.</param>
    /// <param name="tagName">The tag name to search for in ancestors.</param>
    /// <returns>True if an ancestor with the tag name exists, false otherwise.</returns>
    public static bool HasAncestorParentNode(HtmlNode node, string tagName)
    {
        while (node != null)
        {
            if (node.Name == tagName)
                return true;
            node = node.ParentNode;
        }

        return false;
    }

    /// <summary>
    /// Removes text nodes but not comment nodes from a list of HTML nodes.
    /// </summary>
    /// <param name="nodes">The list of HTML nodes to trim.</param>
    /// <returns>List of nodes with text nodes removed.</returns>
    public static List<HtmlNode> TrimTexts(List<HtmlNode> nodes)
    {
        return TrimTextsInternal(nodes, true);
    }
}