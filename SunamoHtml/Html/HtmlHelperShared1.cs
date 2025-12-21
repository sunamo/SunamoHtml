namespace SunamoHtml.Html;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
/// <summary>
/// Je tu mix všeho, rozdělit to pomocí AI
/// </summary>
public static partial class HtmlHelper
{
    public static string ConvertHtmlToText(string h)
    {
        h = WebUtility.HtmlDecode(h);
        h = SHReplace.ReplaceAllArray(h, Environment.NewLine, "<br>", "<br />", "<br/>");
        h = StripAllTags(h);
        return h;
    }

    /// <summary>
    ///     Nahradí každý text < *> za . Vnitřní ne-xml obsah nechá být.
    /// </summary>
    /// <param name = "p"></param>
    public static string StripAllTags(string p)
    {
        return StripAllTags(p, " ");
    }

    public static string StripAllTags(string p, string replaceFor)
    {
        var vr = Regex.Replace(p, @"<[^>]*>", replaceFor);
        vr = SHReplace.ReplaceAllDoubleSpaceToSingle(vr);
        return vr;
    }

    public static HtmlNode TrimNode(HtmlNode hn2)
    {
        if (hn2.FirstChild == null)
            return hn2;
        if (string.IsNullOrWhiteSpace(hn2.FirstChild.InnerHtml))
            return hn2;
        hn2.InnerHtml = hn2.InnerHtml.Trim();
        hn2.FirstChild.InnerHtml = hn2.FirstChild.InnerHtml.Trim();
        hn2.InnerHtml = hn2.InnerHtml.Trim();
        return hn2;
    }

    /// <summary>
    ///     Vratilo 15 namisto 10
    ///     Používá metodu RecursiveReturnTags
    ///     Do A2 se může vložit *
    /// </summary>
    /// <param name = "htmlNode"></param>
    /// <param name = "tag"></param>
    public static List<HtmlNode> ReturnTagsRek(HtmlNode htmlNode, string tag)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnTags(vr, htmlNode, tag);
        vr = TrimTexts(vr);
        return vr;
    }

    /// <summary>
    ///     G null když tag nebude nalezen
    /// </summary>
    /// <param name = "htmlNode"></param>
    /// <param name = "tag"></param>
    /// <param name = "attr"></param>
    /// <param name = "value"></param>
    public static HtmlNode ReturnTagWithAttrRek(HtmlNode htmlNode, string tag, string attr, string value)
    {
        return ReturnTagWithAttr(htmlNode, tag, attr, value);
    }

    /// <summary>
    ///     Do A2 se může zadat * pro získaní všech tagů
    ///     Do A4 se může vložit * pro vrácení tagů text hledaným atributem text jakoukoliv hodnotou
    /// </summary>
    /// <param name = "htmlNode"></param>
    /// <param name = "tag"></param>
    /// <param name = "atribut"></param>
    /// <param name = "hodnotaAtributu"></param>
    public static List<HtmlNode> ReturnTagsWithAttrRek(HtmlNode htmlNode, string tag, string atribut, string hodnotaAtributu)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnTagsWithAttr(vr, htmlNode, tag, atribut, hodnotaAtributu);
        return vr;
    }

    /// <summary>
    ///     Vratilo 0 misto 10
    ///     A1 je uzel který se bude rekurzivně porovnávat
    ///     A2 je název tagu(div, a, atd.) které chci vrátit.
    /// </summary>
    /// <param name = "htmlNode"></param>
    /// <param name = "p"></param>
    public static List<HtmlNode> ReturnAllTags(HtmlNode htmlNode, params string[] p)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnAllTags(vr, htmlNode, p);
        return vr;
    }

    /// <summary>
    ///     Have also override with List<HtmlNode>
    /// </summary>
    /// <param name = "htmlNodeCollection"></param>
    public static List<HtmlNode> TrimTexts(HtmlNodeCollection htmlNodeCollection)
    {
        return HtmlAgilityHelper.TrimTexts(htmlNodeCollection);
    }

    public static List<HtmlNode> TrimTexts(List<HtmlNode> c2)
    {
        return HtmlAgilityHelper.TrimTexts(c2, true);
    }

    public static List<HtmlNode> TrimTexts(List<HtmlNode> c2, bool text, bool comments = false)
    {
        return HtmlAgilityHelper.TrimTexts(c2, text, comments);
    }

    /// <summary>
    ///     It's calling by others
    ///     Do A3 se může vložit *
    /// </summary>
    /// <param name = "vr"></param>
    /// <param name = "html"></param>
    /// <param name = "p"></param>
    private static void RecursiveReturnTags(List<HtmlNode> vr, HtmlNode html, string p)
    {
        foreach (var item in html.ChildNodes)
            if (HasTagName(item, p))
            {
                //RecursiveReturnTags(vr, item, p);
                vr.Add(item);
                RecursiveReturnTags(vr, item, p);
            }
            else
            {
                RecursiveReturnTags(vr, item, p);
            }
    }

    /// <summary>
    ///     Rekurzivně volá metodu RecursiveReturnAllTags
    /// </summary>
    /// <param name = "vr"></param>
    /// <param name = "html"></param>
    /// <param name = "p"></param>
    private static void RecursiveReturnAllTags(List<HtmlNode> vr, HtmlNode html, params string[] p)
    {
        foreach (var item in html.ChildNodes)
        {
            var contains = false;
            if (p.Length == 1)
            {
                if (item.Name == p[0])
                    contains = true;
            }
            else
            {
                foreach (var t in p)
                    if (item.Name == t)
                        contains = true;
            }

            if (contains)
            {
                RecursiveReturnAllTags(vr, item, p);
                if (!vr.Contains(item))
                    vr.Add(item);
            }
            else
            {
                RecursiveReturnAllTags(vr, item, p);
            }
        }
    }

    /// <summary>
    ///     Do A2 se může zadat *
    /// </summary>
    /// <param name = "hn"></param>
    /// <param name = "tag"></param>
    private static bool HasTagName(HtmlNode hn, string tag)
    {
        if (tag == "*")
            return true;
        return hn.Name == tag;
    }

    /// <summary>
    ///     Do A3 se může zadat * pro vrácení všech tagů
    ///     Do A5 se může vložit * pro vrácení tagů text hledaným atributem text jakoukoliv hodnotou
    /// </summary>
    /// <param name = "vr"></param>
    /// <param name = "htmlNode"></param>
    /// <param name = "p"></param>
    /// <param name = "atribut"></param>
    /// <param name = "hodnotaAtributu"></param>
    private static void RecursiveReturnTagsWithAttr(List<HtmlNode> vr, HtmlNode htmlNode, string p, string atribut, string hodnotaAtributu)
    {
        foreach (var item in htmlNode.ChildNodes)
            if (HasTagName(item, p))
            {
                if (HasTagAttr(item, atribut, hodnotaAtributu, false))
                    //RecursiveReturnTagsWithAttr(vr, item, p);
                    if (!vr.Contains(item))
                        vr.Add(item);
            }
            else
            {
                RecursiveReturnTagsWithAttr(vr, item, p, atribut, hodnotaAtributu);
            }
    }

    private static bool HasTagAttr(HtmlNode item, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute)
    {
        if (hodnotaAtributu == "*")
            return true;
        var contains = false;
        var attrValue = GetValueOfAttribute(atribut, item);
        if (enoughIsContainsAttribute)
            contains = attrValue.Contains(hodnotaAtributu);
        else
            contains = attrValue == hodnotaAtributu;
        return contains;
    }

    /// <summary>
    ///     Prochází děti A1 a pokud některý má název A2, G
    ///     Vrátí null pokud se takový tag nepodaří najít
    /// </summary>
    /// <param name = "body"></param>
    /// <param name = "nazevTagu"></param>
    public static HtmlNode ReturnTag(HtmlNode body, string nazevTagu)
    {
        //List<HtmlNode> html = new List<HtmlNode>();
        foreach (var item in body.ChildNodes)
            if (item.Name == nazevTagu)
                return item;
        return null;
    }

    /// <summary>
    ///     Replace A2 by A3
    /// </summary>
    /// <param name = "parentNode"></param>
    /// <param name = "o2"></param>
    /// <param name = "nc"></param>
    public static void ReplaceChildNodeByOuterHtml(HtmlNode parentNode, string o2, HtmlNode nc)
    {
        for (var i = 0; i < parentNode.ChildNodes.Count; i++)
        {
            var item = parentNode.ChildNodes[i];
            if (item.OuterHtml == o2)
            {
                // First is new, Second is old!!!
                parentNode.ReplaceChild(nc, item);
                break;
            }
        }
    }
}