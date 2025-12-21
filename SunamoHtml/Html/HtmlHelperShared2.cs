namespace SunamoHtml.Html;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
/// <summary>
/// Je tu mix všeho, rozdělit to pomocí AI
/// </summary>
public static partial class HtmlHelper
{
    public static string ToXmlFinal(string xml)
    {
        xml = ReplaceHtmlNonPairTagsWithXmlValid(xml);
        xml = XH.RemoveXmlDeclaration(xml);
        return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + ReplaceHtmlNonPairTagsWithXmlValid(XH.RemoveXmlDeclaration(xml.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-2\" />", "").Replace("<?xml version=\"1.0\" encoding=\"utf-8\" />", "").Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" />", "")));
    }

    public static void DeleteAttributesFromAllNodes(List<HtmlNode> nodes)
    {
        foreach (var node in nodes)
            for (var i = node.Attributes.Count - 1; i >= 0; i--)
                node.Attributes.RemoveAt(i);
    }

    /// <summary>
    ///     Již volá ReplaceHtmlNonPairTagsWithXmlValid
    /// </summary>
    /// <param name = "xml"></param>
    /// <param name = "odstranitXmlDeklaraci"></param>
    public static string ToXml(string xml, bool odstranitXmlDeklaraci)
    {
        var doc = HtmlAgilityHelper.CreateHtmlDocument();
        //doc.Encoding = Encoding.UTF8;
        doc.LoadHtml(xml);
        var sw = new StringWriter();
        var tw = XmlWriter.Create(sw);
        doc.DocumentNode.WriteTo(tw);
        tw.Flush();
        sw.Flush();
        var vr = sw.ToString();
        if (odstranitXmlDeklaraci)
            vr = XH.RemoveXmlDeclaration(vr);
        vr = ReplaceHtmlNonPairTagsWithXmlValid(vr);
        return vr;
    }

    /// <summary>
    ///     Již volá RemoveXmlDeclaration i ReplaceHtmlNonPairTagsWithXmlValid
    /// </summary>
    /// <param name = "xml"></param>
    public static string ToXml(string xml)
    {
        return ToXml(xml, true);
    }

    /// <summary>
    ///     Strip all tags and return only
    ///     Use RemoveAllNodes when need remove also with innerhtml
    /// </summary>
    /// <param name = "d"></param>
    public static List<string> StripAllTagsList(string data)
    {
        var replaced = StripAllTags(data, " ");
        return SHSplit.Split(replaced, " ");
    }

    /// <summary>
    ///     Nahradí každý text < *> za mezeru. Vnitřní ne-xml obsah nechá být.
    /// </summary>
    /// <param name = "p"></param>
    public static string StripAllTagsSpace(string p)
    {
        return Regex.Replace(p, @"<[^>]*>", " ");
    }

    /// <summary>
    ///     Jen volá metodu StripAllTags
    ///     Nahradí každý text < *> za . Vnitřní ne-xml obsah nechá být.
    /// </summary>
    /// <param name = "p"></param>
    public static string RemoveAllTags(string p)
    {
        return StripAllTags(p);
    }

    public static bool HasTagAttrContains(HtmlNode htmlNode, string delimiter, string attr, string value)
    {
        var attrValue = GetValueOfAttribute(attr, htmlNode);
        var spl = SHSplit.Split(attrValue, delimiter);
        return spl.Contains(value);
    }

    public static bool HasChildTag(HtmlNode spanInHeader, string v)
    {
        return ReturnTags(spanInHeader, v).Count != 0;
    }

    /// <summary>
    ///     Used in ParseChromeAPIs. Nowhere is executed
    /// </summary>
    /// <param name = "dd2"></param>
    /// <param name = "v"></param>
     //public static string ReturnInnerTextOfTagsRek(HtmlNode dd2, string v)
    //{
    //    ThrowEx.NotImplementedMethod();
    //    return null;
    //}
    /// <summary>
    ///     Nehodí se na vrácení obsahu celé stránky
    ///     A1 je zdrojový kód celé stránky
    /// </summary>
    /// <param name = "s"></param>
    /// <param name = "p"></param>
    /// <param name = "ssh"></param>
    /// <param name = "value"></param>
    public static string ReturnApplyToAllTags(string text, string p, EditHtmlWidthHandler ssh, string value)
    {
        var vr = new List<HtmlNode>();
        var doc = HtmlAgilityHelper.CreateHtmlDocument();
        //hd.Encoding = Encoding.UTF8;
        doc.LoadHtml(text);
        var htmlNode = doc.DocumentNode;
        RecursiveApplyToAllTags(vr, ref htmlNode, p, ssh, value);
        return htmlNode.OuterHtml;
        ;
    }

    /// <summary>
    ///     A1 je kolekce uzlů na které jsem zavolal A4
    ///     A2 je referencovaný uzel, do kterého se změny přímo projevují
    ///     A3 je název tagu který se hledá(div, a, atd.)
    ///     A4 je samotná metoda která bude provádět změny
    ///     A5 je volitelný parametr do A4
    /// </summary>
    /// <param name = "vr"></param>
    /// <param name = "html"></param>
    /// <param name = "p"></param>
    /// <param name = "ssh"></param>
    /// <param name = "value"></param>
    private static void RecursiveApplyToAllTags(List<HtmlNode> vr, ref HtmlNode html, string p, EditHtmlWidthHandler ssh, string value)
    {
        for (var i = 0; i < html.ChildNodes.Count; i++)
        {
            var item = html.ChildNodes[i];
            if (item.Name == p)
            {
                RecursiveApplyToAllTags(vr, ref item, p, ssh, value);
                if (!vr.Contains(item))
                {
                    vr.Add(item);
                    var data = ssh.Invoke(ref item, value);
                }
            }
            else
            {
                RecursiveApplyToAllTags(vr, ref item, p, ssh, value);
            }
        }
    }

    public static Dictionary<string, string> GetValuesOfStyle(HtmlNode item)
    {
        var vr = new Dictionary<string, string>();
        var at = GetValueOfAttribute("style", item);
        if (at.Contains(";"))
        {
            var data = SHSplit.Split(at, ";");
            foreach (var item2 in data)
                if (item2.Contains(":"))
                {
                    var result = SHSplit.SplitNone(item2, ":");
                    vr.Add(result[0].Trim().ToLower(), result[1].Trim().ToLower());
                }
        }

        return vr;
    }

    public static HtmlNode GetTag(HtmlNode cacheAuthorNode, string p)
    {
        foreach (var item in cacheAuthorNode.ChildNodes)
            if (item.OriginalName == p)
                return item;
        return null;
    }

    //public static HtmlNode ReturnNextSibling(HtmlNode h4Callback, string v)
    //{
    //    ThrowEx.NotImplementedMethod();
    //    return null;
    //}
    public static HtmlNode ReturnTagRek(HtmlNode hn, string nameOfTag)
    {
        hn = TrimNode(hn);
        foreach (var var in hn.ChildNodes)
        {
            var hn2 = var; //.FirstChild;
            foreach (var item2 in var.ChildNodes)
            {
                if (item2.Name == nameOfTag)
                    return item2;
                var hn3 = ReturnTagRek(item2, nameOfTag);
                if (hn3 != null)
                    return hn3;
            }

            if (hn2.Name == nameOfTag)
                return hn2;
        }

        return null;
    }

    ///// <summary>
    /////     Method with just single parameter are used in ParseChromeAPIs
    ///// </summary>
    ///// <param name="dlOuter"></param>
    //public static List<HtmlNode> ReturnTags(HtmlNode dlOuter)
    //{
    //    ThrowEx.NotImplementedMethod();
    //    return null;
    //}
    /// <summary>
    ///     Return 0 instead of 10
    ///     If tag is A2, don't apply recursive on that
    ///     A2 je název tagu, napříkald img
    /// </summary>
    /// <param name = "html"></param>
    /// <param name = "p"></param>
    public static List<HtmlNode> ReturnAllTagsImg(HtmlNode html, string p)
    {
        var vr = new List<HtmlNode>();
        foreach (var item in html.ChildNodes)
            if (item.Name == p)
            {
                var node = item.ParentNode;
                if (node != null)
                    vr.Add(item);
            }
            else
            {
                vr.AddRange(ReturnAllTags(item, p));
            }

        return vr;
    }

    /// <summary>
    ///     Do A2 se může vložit * ale nemělo by to moc smysl
    /// </summary>
    /// <param name = "parent"></param>
    /// <param name = "tag"></param>
    public static List<HtmlNode> ReturnTags(HtmlNode parent, string tag)
    {
        var vr = new List<HtmlNode>();
        foreach (var item in parent.ChildNodes)
            if (HasTagName(item, tag))
                vr.Add(item);
        return vr;
    }
}