namespace SunamoHtml;

public class HtmlAssistant
{
    /// <summary>
    /// return se if wont be found
    /// return (null) Consts.nulled when attr exists without value (input readonly atc.)
    /// </summary>
    /// <param name="p"></param>
    /// <param name="divMain"></param>
    /// <param name="_trim"></param>
    public static string GetValueOfAttribute(string p, HtmlNode divMain, bool _trim = false)
    {
        object o = divMain.Attributes[p]; // divMain.GetAttributeValue(p, null);//
        if (o != null)
        {
            string st = ((HtmlAttribute)o).Value;
            if (_trim)
            {
                st = st.Trim();
            }

            if (st == string.Empty)
            {
                return Consts.nulled;
            }

            return st;
        }

        return string.Empty;
    }

    public static string TrimInnerHtml(string value)
    {
        HtmlDocument hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(value);
        foreach (var item in hd.DocumentNode.DescendantsAndSelf())
        {
            if (item.NodeType == HtmlNodeType.Element)
            {
                item.InnerHtml = item.InnerHtml.Trim();
            }
        }
        return hd.DocumentNode.OuterHtml;
    }

    public static List<string> SplitByBr(string input)
    {
        return SplitByTag(input, "br");
    }

    static void RemoveComments(HtmlNode node)
    {
        if (!node.HasChildNodes)
        {
            return;
        }

        for (int i = 0; i < node.ChildNodes.Count; i++)
        {
            if (node.ChildNodes[i].NodeType == HtmlNodeType.Comment)
            {
                node.ChildNodes.RemoveAt(i);
                --i;
            }
        }

        foreach (HtmlNode subNode in node.ChildNodes)
        {
            RemoveComments(subNode);
        }
    }


    public static List<string> SplitByTag(string input, string d)
    {
        var ih = input;
        ih = HtmlHelper.ReplaceHtmlNonPairTagsWithXmlValid(ih);
        var lines = SHSplit.Split(ih, HtmlTagTemplates.br);
        return lines;
    }

    public static void SetAttribute(HtmlNode node, string atr, string hod)
    {
        object o = null;
        while (true)
        {
            o = node.Attributes.FirstOrDefault(a => a.Name == atr);
            if (o != null)
            {
                node.Attributes.Remove((HtmlAttribute)o);
            }
            else
            {
                break;
            }
        }

        var atr2 = node.OwnerDocument.CreateAttribute(atr, hod);

        node.Attributes.Add(atr2);

        var html = node.OuterHtml;
    }

    public static string InnerText(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool contains = false)
    {
        return InnerContentWithAttr(node, recursive, tag, attr, attrValue, false, contains);
    }

    public static string InnerHtmlWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool contains = false)
    {
        return InnerContentWithAttr(node, recursive, tag, attr, attrValue, true, contains);
    }

    public static string InnerContentWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool html, bool contains = false)
    {
        HtmlNode node2 = HtmlAgilityHelper.NodeWithAttr(node, true, tag, attr, attrValue, contains);
        if (node2 != null)
        {
            var c = string.Empty;
            if (html)
            {
                c = node2.InnerHtml;
            }
            else
            {
                c = node2.InnerText;
            }

            return HtmlAssistant.HtmlDecode(c.Trim());
        }

        return string.Empty;
    }

    public static string HtmlDecode(string v)
    {
        return WebUtility.HtmlDecode(v);
    }

    public static List<HtmlNode> GetAnyHeader(HtmlNode docs, bool rec, bool stopAfterFirst)
    {
        List<HtmlNode> hd2 = new List<HtmlNode>();
        for (int i = 1; i < 7; i++)
        {
            var hd = HtmlAgilityHelper.Node(docs, rec, "h" + i);

            if (hd != null)
            {
                hd2.Add(hd);
                if (stopAfterFirst)
                {
                    break;
                }
            }
        }

        return hd2;
    }

    public static HtmlNode RemoveAllAttrs(HtmlNode img)
    {
        var tagL = img.Name.ToLower();
        string html = "";
        if (AllLists.HtmlNonPairTags.Contains(tagL))
        {
            html = "<" + tagL + " />";
        }
        else
        {
            html = "<" + tagL + "></" + tagL + ">";
        }

        var hn = HtmlNode.CreateNode(html);
        return img.ParentNode.ReplaceChild(hn, img);
    }

    public static List<string> AttrsValues(List<HtmlNode> anchors, string v)
    {
        List<string> result = new List<string>();

        foreach (var item in anchors)
        {
            result.Add(HtmlAssistant.GetValueOfAttribute(v, item));
        }

        return result;
    }

    static Type type = typeof(HtmlAssistant);

    public static string InnerTextDecodeTrim(HtmlNode n)
    {
        var r = n.InnerText.Trim();
        r = SHReplace.ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(r, AllStrings.space);
        r = WebUtility.HtmlDecode(r);
        r = SHReplace.ReplaceAllDoubleSpaceToSingle(r);
        return r;
    }
    public static string InnerText(HtmlNode item, bool recursive, string tag)
    {
        var node = HtmlAgilityHelper.Node(item, recursive, tag);
        if (node == null)
        {
            return string.Empty;
        }
        return node.InnerText;
    }

    public static string InnerHtml(HtmlNode item, bool recursive, string tag)
    {
        var node = HtmlAgilityHelper.Node(item, recursive, tag);
        if (node == null)
        {
            return string.Empty;
        }
        return node.InnerHtml;
    }

    public static Dictionary<string, string> GetAttributesPairs(string s)
    {
        if (!s.Contains("<"))
        {
            s = "<img " + s + "/>";
        }

        Dictionary<string, string> result = new Dictionary<string, string>();

        HtmlNode node = HtmlNode.CreateNode(s);
        foreach (var item in node.Attributes)
        {
            result.Add(item.Name, item.Value);
        }

        return result;
    }
}

