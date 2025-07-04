namespace SunamoHtml.Html;

public static class HtmlHelper
{
    private static Type type = typeof(HtmlHelper);

    /// <summary>
    ///     Problematic with auto translate
    /// </summary>
    /// <param name="vstup"></param>
    public static string ReplaceHtmlNonPairTagsWithXmlValid(string vstup)
    {
        var jizNahrazeno = new List<string>();

        var mc = Regex.Matches(vstup, RegexHelper.rNonPairXmlTagsUnvalid.ToString());
        var col = new List<string>(AllLists.HtmlNonPairTags);

        foreach (Match item in mc)
        {
            var d = item.Value.Replace(" >", ">");
            var tag = "";
            if (item.Value.Contains(" "))
                tag = SH.GetFirstPartByLocation(item.Value, ' ');
            else
                tag = d.Replace("/", "").Replace(">", "");

            tag = tag.TrimStart('<').Trim().ToLower();
            if (col.Contains(tag))
                if (!item.Value.Contains("/>"))
                    if (!jizNahrazeno.Contains(item.Value))
                    {
                        jizNahrazeno.Add(item.Value);
                        var nc = item.Value.Substring(0, item.Value.Length - 1) + " />";
                        vstup = vstup.Replace(item.Value, nc);
                    }
        }

        return vstup;
    }

    public static string ConvertTextToHtml(string p)
    {
        p = p.Replace(Environment.NewLine, "<br />");
        p = p.Replace("\n", "<br />");
        return p;
    }

    public static string PrepareToAttribute(string title)
    {
        return title.Replace('"', '\'');
    }

    public static string ReplaceAllFontCase(string r)
    {
        var za = "<br />";
        r = r.Replace("<BR />", za);
        r = r.Replace("<bR />", za);
        r = r.Replace("<Br />", za);

        r = r.Replace("<br/>", za);
        r = r.Replace("<BR/>", za);
        r = r.Replace("<bR/>", za);
        r = r.Replace("<Br/>", za);

        r = r.Replace("<br>", za);
        r = r.Replace("<BR>", za);
        r = r.Replace("<bR>", za);
        r = r.Replace("<Br>", za);
        return r;
    }

    public static string ClearSpaces(string dd)
    {
        return dd.Replace("&nbsp;", "").Replace(" ", "");
    }

    private static void RecursiveReturnTagWithAttr(List<HtmlNode> vr, HtmlNode htmlNode, string tag, string attr,
        string value)
    {
        foreach (var item in htmlNode.ChildNodes)
            if (item.Name == tag && GetValueOfAttribute(attr, item) == value)
            {
                //RecursiveReturnTagWithAttr(vr, item, tag, attr, value);
                vr.Add(item);
                return;
            }
            else
            {
                RecursiveReturnTagWithAttr(vr, item, tag, attr, value);
            }
    }

    public static string GetValueOfAttribute(string p, HtmlNode divMain, bool _trim = false)
    {
        return HtmlAssistant.GetValueOfAttribute(p, divMain, _trim);
    }

    /// <summary>
    ///     Pokud bude nalezen alespoň jeden tag, vrátí ho, pokud žádný, GN
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="attr"></param>
    /// <param name="value"></param>
    public static HtmlNode ReturnTagWithAttr(HtmlNode htmlNode, string tag, string attr, string value)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnTagWithAttr(vr, htmlNode, tag, attr, value);
        if (vr.Count > 0) return vr[0];
        return null;
    }

    public static List<HtmlNode> GetWithoutTextNodes(HtmlNode htmlNode)
    {
        var vr = new List<HtmlNode>();
        foreach (var item in htmlNode.ChildNodes)
        {
            var dd = item.ToString();
            if (dd != "HtmlAgilityPack.HtmlTextNode") vr.Add(item);
        }

        return vr;
    }

    public static HtmlNode? GetTagOfAtributeRek(HtmlNode hn, string nameOfTag, string nameOfAtr, string valueOfAtr)
    {
        hn = TrimNode(hn);
        foreach (var var in hn.ChildNodes)
        {
            //var.InnerHtml = var.InnerHtml.Trim();
            var hn2 = var; //.FirstChild;
            foreach (var item2 in var.ChildNodes)
            {
                if (GetValueOfAttribute(nameOfAtr, item2) == valueOfAtr) return item2;
                var hn3 = GetTagOfAtributeRek(item2, nameOfTag, nameOfAtr, valueOfAtr);
                if (hn3 != null) return hn3;
            }

            if (hn2.Name == nameOfTag)
            {
                if (GetValueOfAttribute(nameOfAtr, hn2) == valueOfAtr) return hn2;
                foreach (var var2 in hn2.ChildNodes)
                    if (GetValueOfAttribute(nameOfAtr, var2) == valueOfAtr)
                        return var2;

                //}
            }
        }

        return null;
    }

    /// <param name="html"></param>
    /// <param name="nameOfTag"></param>
    public static string TrimOpenAndEndTags(string html, string nameOfTag)
    {
        html = html.Replace("<" + nameOfTag + ">", "");
        html = html.Replace("</" + nameOfTag + ">", "");
        return html;
    }



    /// <summary>
    ///     Před zavoláním této metody musí být v A1 převedeny bílé znaky na mezery - pouze tak budou označeny všechny výskyty
    ///     daných slov
    /// </summary>
    /// <param name="celyObsah"></param>
    /// <param name="maxPocetPismenNaVetu"></param>
    /// <param name="pocetVet"></param>
    /// <param name="hledaneSlova"></param>
    public static string HighlightingWords(string celyObsah, int maxPocetPismenNaVetu, int pocetVet,
        List<string> hledaneSlova)
    {
        //hledaneSlova = CA.ToLower(hledaneSlova);
        for (var i = 0; i < hledaneSlova.Count; i++) hledaneSlova[i] = hledaneSlova[i].ToLower();
        celyObsah = celyObsah.Trim();
        var ftw = SH.ReturnOccurencesOfStringFromToWord(celyObsah, hledaneSlova.ToArray());
        if (ftw.Count > 0)
        {
            var dd = new List<List<FromToWord>>();
            var fromtw = new List<FromToWord>();
            fromtw.Add(ftw[0]);
            var indexDDNaKteryVkladat = 0;
            var indexFromNaposledyVlozeneho = ftw[0].from;
            dd.Add(fromtw);

            for (var i = 1; i < ftw.Count; i++)
            {
                var item = ftw[i];
                if (item.to - indexFromNaposledyVlozeneho < maxPocetPismenNaVetu)
                {
                    dd[indexDDNaKteryVkladat].Add(item);
                }
                else
                {
                    var ftw2 = new List<FromToWord>();
                    ftw2.Add(item);
                    dd.Add(ftw2);
                    if (dd.Count == pocetVet) break;
                    indexDDNaKteryVkladat++;
                }

                indexFromNaposledyVlozeneho = item.from;
            }

            // Teď vypočtu pro každou kolekci v DD prostřední prvek a od toho vezmu vždy znaky nalevo i napravo
            var final = new StringBuilder();
            foreach (var item in dd)
            {
                var stred = 0;
                if (item.Count % 2 == 0)
                {
                    // Zjistím 2 prostřední slova
                    var from = item[item.Count / 2].from;
                    var to = 0;
                    if (item.Count != 2)
                        to = item[item.Count / 2 + 1].to;
                    else
                        to = item[item.Count / 2].to;

                    stred = from + (to - from) / 2;
                }
                else if (item.Count == 1)
                {
                    stred = item[0].from + (item[0].to - item[0].from) / 2;
                }
                else
                {
                    stred = item.Count / 2;
                    stred++;
                    stred = item[stred].from + (item[stred].to - item[stred].from) / 2;
                }

                var naKazdeStrane = maxPocetPismenNaVetu / 2;

                WhitespaceCharService whitespaceChar = new();

                var veta = SH.XCharsBeforeAndAfterWholeWords(
                    SHReplace.ReplaceAllArray(celyObsah, " ", whitespaceChar.whiteSpaceChars.ConvertAll(d => d.ToString()).ToArray()),
                    stred, naKazdeStrane);

                // Teď zvýrazním nalezené slova
                var slova = SHSplit.SplitBySpaceAndPunctuationCharsLeave(veta);
                var vetaSeZvyraznenimiCastmi = new StringBuilder();
                foreach (var item2 in slova)
                {
                    var jeToHledaneSlovo = false;
                    var i2l = item2.ToLower();
                    foreach (var item3 in hledaneSlova)
                        if (i2l == item3)
                            jeToHledaneSlovo = true;

                    if (jeToHledaneSlovo)
                        vetaSeZvyraznenimiCastmi.Append("<b>" + item2 + "</b>");
                    else
                        vetaSeZvyraznenimiCastmi.Append(item2);
                }

                final.Append(vetaSeZvyraznenimiCastmi + " ... ");
            }

            return final.ToString();
        }

        return SH.ShortForLettersCountThreeDotsReverse(celyObsah, pocetVet * maxPocetPismenNaVetu);
    }


    public static string ConvertHtmlToText(string h)
    {
        h = WebUtility.HtmlDecode(h);
        h = SHReplace.ReplaceAllArray(h, Environment.NewLine, "<br>", "<br />", "<br/>");
        h = StripAllTags(h);
        return h;
    }

    /// <summary>
    ///     Nahradí každý text <*> za . Vnitřní ne-xml obsah nechá být.
    /// </summary>
    /// <param name="p"></param>
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
        if (hn2.FirstChild == null) return hn2;
        if (string.IsNullOrWhiteSpace(hn2.FirstChild.InnerHtml)) return hn2;
        hn2.InnerHtml = hn2.InnerHtml.Trim();
        hn2.FirstChild.InnerHtml = hn2.FirstChild.InnerHtml.Trim();
        hn2.InnerHtml = hn2.InnerHtml.Trim();
        return hn2;
    }

    /// <param name="node"></param>
    /// <param name="atr"></param>
    /// <param name="hod"></param>
    public static void SetAttribute(HtmlNode node, string atr, string hod)
    {
        HtmlAssistant.SetAttribute(node, atr, hod);
    }

    /// <summary>
    ///     Vratilo 15 namisto 10
    ///     Používá metodu RecursiveReturnTags
    ///     Do A2 se může vložit *
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
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
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="attr"></param>
    /// <param name="value"></param>
    public static HtmlNode ReturnTagWithAttrRek(HtmlNode htmlNode, string tag, string attr, string value)
    {
        return ReturnTagWithAttr(htmlNode, tag, attr, value);
    }

    /// <summary>
    ///     Do A2 se může zadat * pro získaní všech tagů
    ///     Do A4 se může vložit * pro vrácení tagů s hledaným atributem s jakoukoliv hodnotou
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    public static List<HtmlNode> ReturnTagsWithAttrRek(HtmlNode htmlNode, string tag, string atribut,
        string hodnotaAtributu)
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
    /// <param name="htmlNode"></param>
    /// <param name="p"></param>
    public static List<HtmlNode> ReturnAllTags(HtmlNode htmlNode, params string[] p)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnAllTags(vr, htmlNode, p);
        return vr;
    }

    /// <summary>
    ///     Have also override with List<HtmlNode>
    /// </summary>
    /// <param name="htmlNodeCollection"></param>
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
    /// <param name="vr"></param>
    /// <param name="html"></param>
    /// <param name="p"></param>
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
    /// <param name="vr"></param>
    /// <param name="html"></param>
    /// <param name="p"></param>
    private static void RecursiveReturnAllTags(List<HtmlNode> vr, HtmlNode html, params string[] p)
    {
        foreach (var item in html.ChildNodes)
        {
            var contains = false;

            if (p.Length == 1)
            {
                if (item.Name == p[0]) contains = true;
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
                if (!vr.Contains(item)) vr.Add(item);
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
    /// <param name="hn"></param>
    /// <param name="tag"></param>
    private static bool HasTagName(HtmlNode hn, string tag)
    {
        if (tag == "*") return true;
        return hn.Name == tag;
    }

    /// <summary>
    ///     Do A3 se může zadat * pro vrácení všech tagů
    ///     Do A5 se může vložit * pro vrácení tagů s hledaným atributem s jakoukoliv hodnotou
    /// </summary>
    /// <param name="vr"></param>
    /// <param name="htmlNode"></param>
    /// <param name="p"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    private static void RecursiveReturnTagsWithAttr(List<HtmlNode> vr, HtmlNode htmlNode, string p, string atribut,
        string hodnotaAtributu)
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

    private static bool HasTagAttr(HtmlNode item, string atribut, string hodnotaAtributu,
        bool enoughIsContainsAttribute)
    {
        if (hodnotaAtributu == "*") return true;
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
    /// <param name="body"></param>
    /// <param name="nazevTagu"></param>
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
    /// <param name="parentNode"></param>
    /// <param name="o2"></param>
    /// <param name="nc"></param>
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

    public static string ToXmlFinal(string xml)
    {
        xml = ReplaceHtmlNonPairTagsWithXmlValid(xml);
        xml = XH.RemoveXmlDeclaration(xml);
        return "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + ReplaceHtmlNonPairTagsWithXmlValid(
            XH.RemoveXmlDeclaration(xml.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-2\" />", "")
                .Replace("<?xml version=\"1.0\" encoding=\"utf-8\" />", "")
                .Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" />", "")));
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
    /// <param name="xml"></param>
    /// <param name="odstranitXmlDeklaraci"></param>
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
        if (odstranitXmlDeklaraci) vr = XH.RemoveXmlDeclaration(vr);
        vr = ReplaceHtmlNonPairTagsWithXmlValid(vr);
        return vr;
    }

    /// <summary>
    ///     Již volá RemoveXmlDeclaration i ReplaceHtmlNonPairTagsWithXmlValid
    /// </summary>
    /// <param name="xml"></param>
    public static string ToXml(string xml)
    {
        return ToXml(xml, true);
    }

    /// <summary>
    ///     Strip all tags and return only
    ///     Use RemoveAllNodes when need remove also with innerhtml
    /// </summary>
    /// <param name="d"></param>
    public static List<string> StripAllTagsList(string d)
    {
        var replaced = StripAllTags(d, " ");
        return SHSplit.Split(replaced, " ");
    }

    /// <summary>
    ///     Nahradí každý text <*> za mezeru. Vnitřní ne-xml obsah nechá být.
    /// </summary>
    /// <param name="p"></param>
    public static string StripAllTagsSpace(string p)
    {
        return Regex.Replace(p, @"<[^>]*>", " ");
    }

    /// <summary>
    ///     Jen volá metodu StripAllTags
    ///     Nahradí každý text <*> za . Vnitřní ne-xml obsah nechá být.
    /// </summary>
    /// <param name="p"></param>
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
    /// <param name="dd2"></param>
    /// <param name="v"></param>
    //public static string ReturnInnerTextOfTagsRek(HtmlNode dd2, string v)
    //{
    //    ThrowEx.NotImplementedMethod();
    //    return null;
    //}

    /// <summary>
    ///     Nehodí se na vrácení obsahu celé stránky
    ///     A1 je zdrojový kód celé stránky
    /// </summary>
    /// <param name="s"></param>
    /// <param name="p"></param>
    /// <param name="ssh"></param>
    /// <param name="value"></param>
    public static string ReturnApplyToAllTags(string s, string p, EditHtmlWidthHandler ssh, string value)
    {
        var vr = new List<HtmlNode>();
        var doc = HtmlAgilityHelper.CreateHtmlDocument();
        //hd.Encoding = Encoding.UTF8;
        doc.LoadHtml(s);
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
    /// <param name="vr"></param>
    /// <param name="html"></param>
    /// <param name="p"></param>
    /// <param name="ssh"></param>
    /// <param name="value"></param>
    private static void RecursiveApplyToAllTags(List<HtmlNode> vr, ref HtmlNode html, string p,
        EditHtmlWidthHandler ssh, string value)
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
                    var d = ssh.Invoke(ref item, value);
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
            var d = SHSplit.Split(at, ";");
            foreach (var item2 in d)
                if (item2.Contains(":"))
                {
                    var r = SHSplit.SplitNone(item2, ":");
                    vr.Add(r[0].Trim().ToLower(), r[1].Trim().ToLower());
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
                if (item2.Name == nameOfTag) return item2;
                var hn3 = ReturnTagRek(item2, nameOfTag);
                if (hn3 != null) return hn3;
            }

            if (hn2.Name == nameOfTag) return hn2;
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
    /// <param name="html"></param>
    /// <param name="p"></param>
    public static List<HtmlNode> ReturnAllTagsImg(HtmlNode html, string p)
    {
        var vr = new List<HtmlNode>();
        foreach (var item in html.ChildNodes)
            if (item.Name == p)
            {
                var node = item.ParentNode;
                if (node != null) vr.Add(item);
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
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    public static List<HtmlNode> ReturnTags(HtmlNode parent, string tag)
    {
        var vr = new List<HtmlNode>();
        foreach (var item in parent.ChildNodes)
            if (HasTagName(item, tag))
                vr.Add(item);
        return vr;
    }

    public static HtmlNode GetTagOfAtribute(HtmlNode hn, string nameOfTag, string nameOfAtr, string valueOfAtr)
    {
        hn = TrimNode(hn);
        foreach (var var in hn.ChildNodes)
        {
            //var.InnerHtml = var.InnerHtml.Trim();
            var hn2 = var; //.FirstChild;
            if (hn2.Name == nameOfTag)
            {
                if (GetValueOfAttribute(nameOfAtr, hn2) == valueOfAtr) return hn2;
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
    /// <param name="htmlNode"></param>
    /// <param name="tagName"></param>
    /// <param name="attrName"></param>
    /// <param name="attrValue"></param>
    public static List<HtmlNode> ReturnTagsWithAttrRek2(HtmlNode htmlNode, string tagName, string attrName,
        string attrValue)
    {
        var node = new List<HtmlNode>();
        RecursiveReturnAllTags(node, htmlNode, tagName);
        for (var i = node.Count - 1; i >= 0; i--)
            if (GetValueOfAttribute(attrName, node[i]) != attrValue)
                node.RemoveAt(i);
        return node;
    }

    /// <param name="hn"></param>
    /// <param name="nameOfTag"></param>
    /// <param name="nameOfAtr"></param>
    /// <param name="valueOfAtr"></param>
    public static List<HtmlNode> GetTagsOfAtribute(HtmlNode hn, string nameOfTag, string nameOfAtr, string valueOfAtr)
    {
        var vr = new List<HtmlNode>();
        foreach (var var in hn.ChildNodes)
            if (var.Name == nameOfTag)
                if (GetValueOfAttribute(nameOfAtr, var) == valueOfAtr)
                    vr.Add(var);
        return vr;
    }

    private static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode node, string p, string atribut,
        string hodnotaAtributu)
    {
        RecursiveReturnTagsWithContainsAttr(vr, node, p, atribut, hodnotaAtributu, true, true);
    }

    /// <summary>
    ///     Do A3 se může zadat * pro vrácení všech tagů
    /// </summary>
    /// <param name="vr"></param>
    /// <param name="htmlNode"></param>
    /// <param name="p"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    public static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> vr, HtmlNode htmlNode, string p,
        string atribut, string hodnotaAtributu, bool contains, bool recursively)
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
                if (!vr.Contains(item)) vr.Add(item);
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
    /// <param name="vr"></param>
    /// <param name="htmlNode"></param>
    /// <param name="p"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    private static void RecursiveReturnTagsWithContainsAttrWithSplittedElement(List<HtmlNode> vr, HtmlNode htmlNode,
        string p, string atribut, string hodnotaAtributu, string delimiter)
    {
        foreach (var item in htmlNode.ChildNodes)
            if (HasTagName(item, p) && HasTagAttrContains(item, delimiter, atribut, hodnotaAtributu))
            {
                //RecursiveReturnTagsWithContainsAttrWithSplittedElement(vr, item, p, atribut, hodnotaAtributu, delimiter);
                if (!vr.Contains(item)) vr.Add(item);
            }
            else
            {
                RecursiveReturnTagsWithContainsAttrWithSplittedElement(vr, item, p, atribut, hodnotaAtributu,
                    delimiter);
            }
    }

    /// <summary>
    ///     Do A2 se může zadat * pro získaní všech tagů
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    public static List<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tag, string atribut,
        string hodnotaAtributu)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(vr, htmlNode, tag, atribut, hodnotaAtributu);
        return vr;
    }

    public static List<HtmlNode> ReturnTagsWithContainsAttrRek(HtmlNode htmlNode, string tag, string atribut,
        string hodnotaAtributu, bool contains, bool recursively)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(vr, htmlNode, tag, atribut, hodnotaAtributu, contains, recursively);
        return vr;
    }

    /// <summary>
    ///     Do A2 se může zadat * pro získaní všech tagů
    /// </summary>
    /// <param name="htmlNode"></param>
    /// <param name="tag"></param>
    /// <param name="atribut"></param>
    /// <param name="hodnotaAtributu"></param>
    public static List<HtmlNode> ReturnTagsWithContainsClassRek(HtmlNode htmlNode, string tag, string t)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttrWithSplittedElement(vr, htmlNode, tag, "class", t, " ");
        return vr;
    }
}