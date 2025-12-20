// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoHtml;
/// <summary>
///     HtmlHelperText - for methods which NOT operate on HtmlAgiityHelper!
///     HtmlAgilityHelper - getting new nodes
///     HtmlAssistant - Only for methods which operate on HtmlAgiityHelper!
/// </summary>
public partial class HtmlAgilityHelper
{
    /// <summary>
    ///     A2 =remove #text
    ///     A3 = remove #comment
    /// </summary>
    /// <param name = "c2"></param>
    /// <param name = "texts"></param>
    /// <param name = "comments"></param>
    public static List<HtmlNode> TrimTexts(List<HtmlNode> c2, bool texts, bool comments = false)
    {
        if (!_trimTexts)
            return c2;
        var vr = new List<HtmlNode>();
        var add = true;
        foreach (var item in c2)
        {
            add = true;
            if (texts)
                if (item.Name == textNode)
                    add = false;
            if (comments)
                if (item.Name == "#comment")
                    add = false;
            if (add)
                vr.Add(item);
        }

        return vr;
    }

    public static List<HtmlNode> TrimComments(List<HtmlNode> n)
    {
        var vr = new List<HtmlNode>();
        var startWith = false;
        var endsWith = false;
        var toTranslate = true;
        foreach (var item in n)
        {
            startWith = false;
            endsWith = false;
            toTranslate = true;
            var html = item.InnerHtml.Trim();
            // contains whole html comment
            endsWith = html.Contains(ConstsAspx.endHtmlComment);
            startWith = html.Contains(ConstsAspx.startHtmlComment);
            if (startWith && endsWith) //item.NodeType == HtmlNodeType.Comment)
            {
                toTranslate = false;
            }
            else if (true)
            {
                if (html == string.Empty)
                    continue;
                endsWith = html.Contains(ConstsAspx.endAspxComment);
                startWith = html.Contains(ConstsAspx.startAspxComment);
                if (startWith || endsWith)
                    if (startWith && endsWith)
                        // contains whole aspx comment
                        toTranslate = false;
                if (!toTranslate)
                    continue;
                if (html.StartsWith("<%"))
                    continue;
                //var hd = HtmlAgilityHelper.CreateHtmlDocument();
                //hd.LoadHtml(html);
                var count = item.ChildNodes.Count;
                var textCount = TrimTexts(item.ChildNodes).Count;
                if (textCount == count && html == string.Empty)
                    continue;
                //if (textCount != 0)
                //{
                //    continue;
                //}
                vr.Add(item);
            }
        }

        return vr;
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
        var result = hn.Name == tag;
        //if (!result && hn.Name != "a")
        //{
        //    Debugger.Break();
        //}
        return result;
    }

    private static bool HasTagAttr(HtmlNode item, string atribut, string hodnotaAtributu, bool isWildCard, bool enoughIsContainsAttribute, bool searchAsSingleString)
    {
        if (hodnotaAtributu == "*")
            return true;
        var contains = false;
        var attrValue = HtmlAssistant.GetValueOfAttribute(atribut, item);
        if (enoughIsContainsAttribute)
        {
            if (searchAsSingleString)
            {
                if (isWildCard)
                    contains = SH.MatchWildcard(attrValue, hodnotaAtributu);
                else
                    contains = attrValue.Contains(hodnotaAtributu);
            //
            }
            else
            {
                var cont = true;
                var parameter = SHSplit.Split(hodnotaAtributu, " ");
                foreach (var item2 in parameter)
                    if (!attrValue.Contains(item2))
                    {
                        cont = false;
                        break;
                    }

                contains = cont;
            }
        }
        else
        {
            contains = attrValue == hodnotaAtributu;
        }

        return contains;
    }

    /// <summary>
    ///     A4 = if add one, return. Like Node vs Nodes
    ///     It's calling by others
    ///     Do A5 se může vložit *
    /// </summary>
    /// <param name = "vr"></param>
    /// <param name = "html"></param>
    /// <param name = "p"></param>
    public static void RecursiveReturnTags(List<HtmlNode> vr, HtmlNode html, bool recursive, bool single, string parameter)
    {
        if (html == null)
            return;
        foreach (var item in html.ChildNodes)
            if (HasTagName(item, parameter))
            {
                //RecursiveReturnTags(vr, item, parameter);
                vr.Add(item);
                if (single)
                    return;
                if (recursive)
                    RecursiveReturnTags(vr, item, recursive, single, parameter);
            }
            else
            {
                if (recursive)
                    RecursiveReturnTags(vr, item, recursive, single, parameter);
            }
    }

    public static List<HtmlNode> Nodes(HtmlNode node, bool recursive /*, bool single*/, string tag)
    {
        tag = tag.ToLower();
        var vr = new List<HtmlNode>();
        RecursiveReturnTags(vr, node, recursive, false, tag);
        if (tag != textNode)
            vr = TrimTexts(vr);
        return vr;
    }

    private static List<HtmlNode> NodesWithAttrWorker(HtmlNode node, bool recursive, string tag, string atribut, string hodnotaAtributu, bool isWildCard, bool enoughIsContainsAttribute, bool searchAsSingleString = true)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(vr, node, recursive, tag, atribut, hodnotaAtributu, isWildCard, enoughIsContainsAttribute, searchAsSingleString);
        if (tag != textNode)
            vr = TrimTexts(vr);
        return vr;
    }

    public static HtmlDocument CreateHtmlDocument(CreateHtmlDocumentInitData data = null)
    {
        if (data == null)
        {
            data = new();
        }

        var hd = new HtmlDocument();
        hd.OptionOutputOriginalCase = data.OptionOutputOriginalCase;
        // false - i přesto mi tag ukončený na / převede na </Page>. Musí se ještě tagy jež nechci ukončovat vymazat z HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("form"); před načetním XML https://html-agility-pack.net/knowledge-base/7104652/htmlagilitypack-close-form-tag-automatically
        hd.OptionAutoCloseOnEnd = false;
        hd.OptionOutputAsXml = false;
        hd.OptionFixNestedTags = false;
        //when OptionCheckSyntax = false, raise NullReferenceException in Load/LoadHtml
        //hd.OptionCheckSyntax = false;
        return hd;
    }

    public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode htmlNode, bool recursively, string parameter, string atribut, string hodnotaAtributu, bool enoughIsContainsAttribute, bool searchAsSingleString = true)
    {
        RecursiveReturnTagsWithContainsAttr(vr, htmlNode, recursively, parameter, atribut, hodnotaAtributu, false, enoughIsContainsAttribute, searchAsSingleString);
    }

    /// <summary>
    ///     Do A3 se může zadat * pro vrácení všech tagů
    /// </summary>
    /// <param name = "vr"></param>
    /// <param name = "htmlNode"></param>
    /// <param name = "p"></param>
    /// <param name = "atribut"></param>
    /// <param name = "hodnotaAtributu"></param>
    public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode htmlNode, bool recursively, string parameter, string atribut, string hodnotaAtributu, bool isWildCard, bool enoughIsContainsAttribute, bool searchAsSingleString = true)
    {
        /*
isWildCard -
         */
#if DEBUG
        //StringBuilder stringBuilder = new StringBuilder();
        //stringBuilder.AppendLine("Text nodes:");
        //stringBuilder.AppendLine();
        //foreach (var item in htmlNode.ChildNodes)
        //{
        //    if (item.Name != "#text")
        //    {
        //        continue;
        //    }
        //    stringBuilder.AppendLine(item.OuterHtml);
        //    stringBuilder.AppendLine();
        //    stringBuilder.AppendLine();
        //    stringBuilder.AppendLine();
        //    stringBuilder.AppendLine();
        //    stringBuilder.AppendLine();
        //    stringBuilder.AppendLine();
        //    stringBuilder.AppendLine();
        //    stringBuilder.AppendLine();
        //    stringBuilder.AppendLine();
        //    stringBuilder.AppendLine();
        //    stringBuilder.AppendLine();
        //    stringBuilder.AppendLine();
        //}
        //ClipboardService.SetText(stringBuilder.ToString());
#endif
        var childNodeWithoutText = TrimTexts(htmlNode.ChildNodes);
        parameter = parameter.ToLower();
        //atribut = atribut.ToLower();
        //hodnotaAtributu = atribut.ToLower();
        if (htmlNode == null)
            return;
        foreach (var item in childNodeWithoutText)
        {
            var attrValue = HtmlAssistant.GetValueOfAttribute(atribut, item);
            if (HasTagName(item, parameter))
            {
                if (HasTagAttr(item, atribut, hodnotaAtributu, isWildCard, enoughIsContainsAttribute, searchAsSingleString))
                    vr.Add(item);
                if (recursively)
                    RecursiveReturnTagsWithContainsAttr(vr, item, recursively, parameter, atribut, hodnotaAtributu, isWildCard, enoughIsContainsAttribute, searchAsSingleString);
            }
            else
            {
                if (recursively)
                    RecursiveReturnTagsWithContainsAttr(vr, item, recursively, parameter, atribut, hodnotaAtributu, isWildCard, enoughIsContainsAttribute, searchAsSingleString);
            }
        }
    }

    public static List<HtmlNode> NodesWithAttrWildCard(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool contains = false)
    {
        return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, true, contains);
    }

    public static List<HtmlNode> NodesWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue, bool contains = false)
    {
        return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, false, contains);
    }
}