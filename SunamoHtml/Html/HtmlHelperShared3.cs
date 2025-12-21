namespace SunamoHtml.Html;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
/// <summary>
/// Je tu mix všeho, rozdělit to pomocí AI
/// </summary>
public static partial class HtmlHelper
{
    public static HtmlNode GetTagOfAtribute(HtmlNode hn, string nameOfTag, string nameOfAtr, string valueOfAtr)
    {
        hn = TrimNode(hn);
        foreach (var var in hn.ChildNodes)
        {
            //var.InnerHtml = var.InnerHtml.Trim();
            var hn2 = var; //.FirstChild;
            if (hn2.Name == nameOfTag)
            {
                if (GetValueOfAttribute(nameOfAtr, hn2) == valueOfAtr)
                    return hn2;
                foreach (var var2 in hn2.ChildNodes)
                    if (GetValueOfAttribute(nameOfAtr, var2) == valueOfAtr)
                        return var2;
            //}
            }
        }

        return null;
    }

    /// <summary>
    ///     Return 0 instead of 10
    ///     Originally from HtmlDocument
    /// </summary>
    /// <param name = "htmlNode"></param>
    /// <param name = "tagName"></param>
    /// <param name = "attrName"></param>
    /// <param name = "attrValue"></param>
    public static List<HtmlNode> ReturnTagsWithAttrRek2(HtmlNode htmlNode, string tagName, string attrName, string attrValue)
    {
        var node = new List<HtmlNode>();
        RecursiveReturnAllTags(node, htmlNode, tagName);
        for (var i = node.Count - 1; i >= 0; i--)
            if (GetValueOfAttribute(attrName, node[i]) != attrValue)
                node.RemoveAt(i);
        return node;
    }

    /// <param name = "hn"></param>
    /// <param name = "nameOfTag"></param>
    /// <param name = "nameOfAtr"></param>
    /// <param name = "valueOfAtr"></param>
    public static List<HtmlNode> GetTagsOfAtribute(HtmlNode hn, string nameOfTag, string nameOfAtr, string valueOfAtr)
    {
        var vr = new List<HtmlNode>();
        foreach (var var in hn.ChildNodes)
            if (var.Name == nameOfTag)
                if (GetValueOfAttribute(nameOfAtr, var) == valueOfAtr)
                    vr.Add(var);
        return vr;
    }

    private static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode node, string p, string atribut, string hodnotaAtributu)
    {
        RecursiveReturnTagsWithContainsAttr(vr, node, p, atribut, hodnotaAtributu, true, true);
    }

    /// <summary>
    ///     Do A3 se může zadat * pro vrácení všech tagů
    /// </summary>
    /// <param name = "vr"></param>
    /// <param name = "htmlNode"></param>
    /// <param name = "p"></param>
    /// <param name = "atribut"></param>
    /// <param name = "hodnotaAtributu"></param>
    public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode htmlNode, string p, string atribut, string hodnotaAtributu, bool contains, bool recursively)
    {
        foreach (var item in htmlNode.ChildNodes)
        {
            var attrValue = GetValueOfAttribute(atribut, item);
            if (contains)
                contains = attrValue.Contains(hodnotaAtributu);
            else
                contains = attrValue == hodnotaAtributu;
            if (HasTagName(item, p) && contains)
            {
                //RecursiveReturnTagsWithContainsAttr(vr, item, p);
                if (!vr.Contains(item))
                    vr.Add(item);
            }
            else
            {
                if (recursively)
                    RecursiveReturnTagsWithContainsAttr(vr, item, p, atribut, hodnotaAtributu, contains, recursively);
            }
        }
    }

    /// <summary>
    ///     Do A3 se může zadat * pro vrácení všech tagů
    /// </summary>
    /// <param name = "vr"></param>
    /// <param name = "htmlNode"></param>
    /// <param name = "p"></param>
    /// <param name = "atribut"></param>
    /// <param name = "hodnotaAtributu"></param>
    private static void RecursiveReturnTagsWithContainsAttrWithSplittedElement(List<HtmlNode> vr, HtmlNode htmlNode, string p, string atribut, string hodnotaAtributu, string delimiter)
    {
        foreach (var item in htmlNode.ChildNodes)
            if (HasTagName(item, p) && HasTagAttrContains(item, delimiter, atribut, hodnotaAtributu))
            {
                //RecursiveReturnTagsWithContainsAttrWithSplittedElement(vr, item, p, atribut, hodnotaAtributu, delimiter);
                if (!vr.Contains(item))
                    vr.Add(item);
            }
            else
            {
                RecursiveReturnTagsWithContainsAttrWithSplittedElement(vr, item, p, atribut, hodnotaAtributu, delimiter);
            }
    }

    /// <summary>
    ///     Do A2 se může zadat * pro získaní všech tagů
    /// </summary>
    /// <param name = "htmlNode"></param>
    /// <param name = "tag"></param>
    /// <param name = "atribut"></param>
    /// <param name = "hodnotaAtributu"></param>
    public static List<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tag, string atribut, string hodnotaAtributu)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(vr, htmlNode, tag, atribut, hodnotaAtributu);
        return vr;
    }

    public static List<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tag, string atribut, string hodnotaAtributu, bool contains, bool recursively)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(vr, htmlNode, tag, atribut, hodnotaAtributu, contains, recursively);
        return vr;
    }

    /// <summary>
    ///     Do A2 se může zadat * pro získaní všech tagů
    /// </summary>
    /// <param name = "htmlNode"></param>
    /// <param name = "tag"></param>
    /// <param name = "atribut"></param>
    /// <param name = "hodnotaAtributu"></param>
    public static List<HtmlNode> ReturnTagsWithContainsClassRek(HtmlNode htmlNode, string tag, string t)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttrWithSplittedElement(vr, htmlNode, tag, "class", t, " ");
        return vr;
    }

    public static HtmlNode ReturnTagRek(HtmlNode documentNode, object body)
    {
        throw new NotImplementedException();
    }
}