namespace SunamoHtml.Html;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
/// <summary>
/// Je tu mix všeho, rozdělit to pomocí AI
/// </summary>
public static partial class HtmlHelper
{
    private static Type type = typeof(HtmlHelper);
    /// <summary>
    ///     Problematic with auto translate
    /// </summary>
    /// <param name = "vstup"></param>
    public static string ReplaceHtmlNonPairTagsWithXmlValid(string vstup)
    {
        var jizNahrazeno = new List<string>();
        var mc = Regex.Matches(vstup, RegexHelper.rNonPairXmlTagsUnvalid.ToString());
        var col = new List<string>(AllLists.HtmlNonPairTags);
        foreach (Match item in mc)
        {
            var data = item.Value.Replace(" >", ">");
            var tag = "";
            if (item.Value.Contains(" "))
                tag = SH.GetFirstPartByLocation(item.Value, ' ');
            else
                tag = data.Replace("/", "").Replace(">", "");
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

    public static string ReplaceAllFontCase(string result)
    {
        var za = "<br />";
        result = result.Replace("<BR />", za);
        result = result.Replace("<bR />", za);
        result = result.Replace("<Br />", za);
        result = result.Replace("<br/>", za);
        result = result.Replace("<BR/>", za);
        result = result.Replace("<bR/>", za);
        result = result.Replace("<Br/>", za);
        result = result.Replace("<br>", za);
        result = result.Replace("<BR>", za);
        result = result.Replace("<bR>", za);
        result = result.Replace("<Br>", za);
        return result;
    }

    public static string ClearSpaces(string dd)
    {
        return dd.Replace("&nbsp;", "").Replace(" ", "");
    }

    private static void RecursiveReturnTagWithAttr(List<HtmlNode> vr, HtmlNode htmlNode, string tag, string attr, string value)
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

    private static string GetValueOfAttribute(string p, HtmlNode divMain, bool _trim = false)
    {
        return HtmlAssistant.GetValueOfAttribute(p, divMain, _trim);
    }

    /// <summary>
    ///     Pokud bude nalezen alespoň jeden tag, vrátí ho, pokud žádný, GN
    /// </summary>
    /// <param name = "htmlNode"></param>
    /// <param name = "tag"></param>
    /// <param name = "attr"></param>
    /// <param name = "value"></param>
    public static HtmlNode ReturnTagWithAttr(HtmlNode htmlNode, string tag, string attr, string value)
    {
        var vr = new List<HtmlNode>();
        RecursiveReturnTagWithAttr(vr, htmlNode, tag, attr, value);
        if (vr.Count > 0)
            return vr[0];
        return null;
    }

    public static List<HtmlNode> GetWithoutTextNodes(HtmlNode htmlNode)
    {
        var vr = new List<HtmlNode>();
        foreach (var item in htmlNode.ChildNodes)
        {
            var dd = item.ToString();
            if (dd != "HtmlAgilityPack.HtmlTextNode")
                vr.Add(item);
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
                if (GetValueOfAttribute(nameOfAtr, item2) == valueOfAtr)
                    return item2;
                var hn3 = GetTagOfAtributeRek(item2, nameOfTag, nameOfAtr, valueOfAtr);
                if (hn3 != null)
                    return hn3;
            }

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

    /// <param name = "html"></param>
    /// <param name = "nameOfTag"></param>
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
    /// <param name = "celyObsah"></param>
    /// <param name = "maxPocetPismenNaVetu"></param>
    /// <param name = "pocetVet"></param>
    /// <param name = "hledaneSlova"></param>
    public static string HighlightingWords(string celyObsah, int maxPocetPismenNaVetu, int pocetVet, List<string> hledaneSlova)
    {
        //hledaneSlova = CA.ToLower(hledaneSlova);
        for (var i = 0; i < hledaneSlova.Count; i++)
            hledaneSlova[i] = hledaneSlova[i].ToLower();
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
                    if (dd.Count == pocetVet)
                        break;
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
                var veta = SH.XCharsBeforeAndAfterWholeWords(SHReplace.ReplaceAllArray(celyObsah, " ", whitespaceChar.whiteSpaceChars.ConvertAll(data => data.ToString()).ToArray()), stred, naKazdeStrane);
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
}