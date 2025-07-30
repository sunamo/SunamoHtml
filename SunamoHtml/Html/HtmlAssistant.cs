namespace SunamoHtml.Html;

/// <summary>
/// Je tu mix všeho, rozdělit to pomocí AI
/// </summary>
public class HtmlAssistant
{
    private static Type type = typeof(HtmlAssistant);

    public static List<string> ParseInnerTextOfEveryTd(HtmlNode tr)
    {
        var tds = HtmlAgilityHelper.Nodes(tr, false, "td");

        var r = new List<string>();
        foreach (var item in tds) r.Add(item.InnerText.Trim());

        return r;
    }

    public static string RemoveStyleTagsText(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        // Odebrání všech <style> tagů
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
    ///     return se if wont be found
    ///     return (null) "(null)" when attr exists without value (input readonly atc.)
    /// </summary>
    /// <param name="p"></param>
    /// <param name="divMain"></param>
    /// <param name="_trim"></param>
    public static string GetValueOfAttribute(string p, HtmlNode divMain, bool _trim = false)
    {
        object o = divMain.Attributes[p]; // divMain.GetAttributeValue(p, null);//
        if (o != null)
        {
            var st = ((HtmlAttribute)o).Value;
            if (_trim) st = st.Trim();

            if (st == string.Empty) return "(null)";

            return st;
        }

        return string.Empty;
    }

    public static string TrimInnerHtml(string value)
    {
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(value);
        foreach (var item in hd.DocumentNode.DescendantsAndSelf())
            if (item.NodeType == HtmlNodeType.Element)
                item.InnerHtml = item.InnerHtml.Trim();
        return hd.DocumentNode.OuterHtml;
    }

    public static List<string> SplitByBr(string input)
    {
        return SplitByTag(input, "br");
    }

    public static void RemoveComments(HtmlNode node)
    {
        if (!node.HasChildNodes) return;

        for (var i = 0; i < node.ChildNodes.Count; i++)
            if (node.ChildNodes[i].NodeType == HtmlNodeType.Comment)
            {
                node.ChildNodes.RemoveAt(i);
                --i;
            }

        foreach (var subNode in node.ChildNodes) RemoveComments(subNode);
    }


    public static List<string> SplitByTag(string input, string d)
    {
        var ih = input;
        ih = HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(ih);
        var lines = SHSplit.Split(ih, d);
        return lines;
    }

    public static void SetAttribute(HtmlNode node, string atr, string hod)
    {
        object o = null;
        while (true)
        {
            o = node.Attributes.FirstOrDefault(a => a.Name == atr);
            if (o != null)
                node.Attributes.Remove((HtmlAttribute)o);
            else
                break;
        }

        var atr2 = node.OwnerDocument.CreateAttribute(atr, hod);

        node.Attributes.Add(atr2);

        var html = node.OuterHtml;
    }

    public static string InnerText(HtmlNode node, bool recursive, string tag, string attr, string attrValue,
        bool contains = false)
    {
        return InnerContentWithAttr(node, recursive, tag, attr, attrValue, false, contains);
    }

    public static string InnerHtmlWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue,
        bool contains = false)
    {
        return InnerContentWithAttr(node, recursive, tag, attr, attrValue, true, contains);
    }

    public static string InnerContentWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue,
        bool html, bool contains = false)
    {
        var node2 = HtmlAgilityHelper.NodeWithAttr(node, recursive, tag, attr, attrValue, contains);
        if (node2 != null)
        {
            var c = string.Empty;
            if (html)
                c = node2.InnerHtml;
            else
                c = node2.InnerText;

            return HtmlDecode(c.Trim());
        }

        return string.Empty;
    }

    public static string HtmlDecode(string v)
    {
        return WebUtility.HtmlDecode(v);
    }

    public static List<HtmlNode> GetAnyHeader(HtmlNode docs, bool rec, bool stopAfterFirst)
    {
        var hd2 = new List<HtmlNode>();
        for (var i = 1; i < 7; i++)
        {
            var hd = HtmlAgilityHelper.Node(docs, rec, "h" + i);

            if (hd != null)
            {
                hd2.Add(hd);
                if (stopAfterFirst) break;
            }
        }

        return hd2;
    }

    public static HtmlNode RemoveAllAttrs(HtmlNode img)
    {
        var tagL = img.Name.ToLower();
        var html = "";
        if (AllLists.HtmlNonPairTags.Contains(tagL))
            html = "<" + tagL + " />";
        else
            html = "<" + tagL + "></" + tagL + ">";

        var hn = HtmlNode.CreateNode(html);
        return img.ParentNode.ReplaceChild(hn, img);
    }

    public static List<string> AttrsValues(List<HtmlNode> anchors, string v)
    {
        var result = new List<string>();

        foreach (var item in anchors) result.Add(GetValueOfAttribute(v, item));

        return result;
    }

    public static string InnerTextDecodeTrim(string r)
    {
        r = SHReplace.ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(r, " ");
        r = WebUtility.HtmlDecode(r);
        r = SHReplace.ReplaceAllDoubleSpaceToSingle(r);
        return r;
    }

    public static string InnerTextDecodeTrim(HtmlNode n)
    {
        var r = n.InnerText.Trim();
        return InnerTextDecodeTrim(r);
    }

    public static string InnerText(HtmlNode item, bool recursive, string tag)
    {
        var node = HtmlAgilityHelper.Node(item, recursive, tag);
        if (node == null) return string.Empty;
        return node.InnerText;
    }

    public static string InnerHtml(HtmlNode item, bool recursive, string tag)
    {
        var node = HtmlAgilityHelper.Node(item, recursive, tag);
        if (node == null) return string.Empty;
        return node.InnerHtml;
    }

    public static Dictionary<string, string> GetAttributesPairs(string s)
    {
        if (!s.Contains("<")) s = "<img " + s + "/>";

        var result = new Dictionary<string, string>();

        var node = HtmlNode.CreateNode(s);
        foreach (var item in node.Attributes) result.Add(item.Name, item.Value);

        return result;
    }
}