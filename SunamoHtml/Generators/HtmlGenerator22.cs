namespace SunamoHtml.Generators;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public partial class HtmlGenerator2 : HtmlGenerator
{
    public static string GetForCheckBoxListWoCheckDuplicate(string idClassCheckbox, string idClassSpan, List<string> idCheckBoxes, List<string> list)
    {
        var hg = new HtmlGenerator();
        if (idCheckBoxes.Count != list.Count)
            throw new Exception("Nestejn\u00FD po\u010Det parametr\u016F v metod\u011B GetForCheckBoxListWoCheckDuplicate " + idCheckBoxes.Count + ":" + list.Count);
        for (var i = 0; i < idCheckBoxes.Count; i++)
        {
            var f = idCheckBoxes[i];
            hg.WriteNonPairTagWithAttrs("input", "type", "checkbox", "id", idClassCheckbox + f, "class", idClassCheckbox);
            hg.WriteTagWith2Attrs("span", "id", idClassSpan + f, "class", idClassSpan);
            hg.WriteRaw(list[i]);
            hg.TerminateTag("span");
            hg.WriteBr();
        }

        return hg.ToString();
    }

    public static string HtmlGeneratorToString(Action<HtmlGenerator> d)
    {
        var hg = new HtmlGenerator();
        d.Invoke(hg);
        var result = hg.ToString();
        return result;
    }

    public static string Italic(string p)
    {
        return "<i>" + p + "</i>";
    }

    public static void ButtonDelete(HtmlGenerator hg, string text, string a1, string v1)
    {
        hg.WriteTagWithAttr("button", a1, v1);
        hg.WriteTagWithAttr("i", "class", "icon-remove");
        hg.TerminateTag("i");
        hg.WriteRaw(text);
        hg.TerminateTag("button");
    }

    public static string Bold(string p)
    {
        return "<b>" + p + "</b>";
    }

    public static string AnchorWithCustomLabel(string uri, string text)
    {
        return "<a href=\"" + uri + ">" + text + "</a>";
    }

    public static string AllMonthsTable(List<string> AllYearsHtmlBoxes, List<string> AllMonthsBoxColors)
    {
        if (AllYearsHtmlBoxes.Count != 12)
            throw new Exception("D\u00E9lka AllMonthsHtmlBoxes nen\u00ED 12.");
        if (AllMonthsBoxColors.Count != 12)
            throw new Exception("D\u00E9lka AllMonthsBoxColors nen\u00ED 12.");
        var hg = new HtmlGenerator();
        hg.WriteTagWith2Attrs("table", "class", "tabulkaNaStredAutoSirka", "style", "width: 100%");
        hg.WriteTag("tr");
#region Zapíšu vrchní řádky - názvy dnů
        var ppp = DTConstants.monthsInYearEN;
        hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareLeft bunkaTabulkyKalendareTop");
        hg.WriteElement("b", ppp[0]);
        hg.TerminateTag("td");
        for (var i = 1; i < ppp.Count - 1; i++)
        {
            hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareTop");
            hg.WriteElement("b", ppp[i]);
            hg.TerminateTag("td");
        }

        hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareRight bunkaTabulkyKalendareTop");
        hg.WriteElement("b", ppp[ppp.Count - 1]);
        hg.TerminateTag("td");
#endregion
        hg.TerminateTag("tr");
        hg.WriteTag("tr");
        for (var i = 0; i < AllYearsHtmlBoxes.Count; i++)
        {
            var pridatTridu = "";
            if (i == 0)
                pridatTridu = "bunkaTabulkyKalendareLeft";
            else if (i == 11)
                pridatTridu = "bunkaTabulkyKalendareRight";
            var color = AllMonthsBoxColors[i];
            var appendStyle = "";
            if (color == "#030")
                appendStyle = "color:white;";
            hg.WriteTagWith2Attrs("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTridu, "style", appendStyle + "background-color:" + color);
            hg.WriteRaw("<b>" + AllYearsHtmlBoxes[i] + "</b>");
            hg.TerminateTag("td");
        }

        hg.TerminateTag("tr");
        hg.TerminateTag("table");
        return hg.ToString();
    }

    public static string AllYearsTable(List<string> years, List<string> AllYearsHtmlBoxes, List<string> AllYearsBoxColors)
    {
        var yearsCount = years.Count;
        if (AllYearsHtmlBoxes.Count != yearsCount)
            throw new Exception("Po\u010Det prvk\u016F v AllYearsHtmlBoxes nen\u00ED stejn\u00FD jako v kolekci years");
        if (AllYearsBoxColors.Count != yearsCount)
            throw new Exception("Po\u010Det prvk\u016F v AllYearsBoxColors nen\u00ED stejn\u00FD jako v kolekci years");
        var hg = new HtmlGenerator();
        hg.WriteTagWith2Attrs("table", "class", "tabulkaNaStredAutoSirka", "style", "width: 200px");
#region Zapíšu vrchní řádky - názvy dnů
#endregion
        for (var i = 0; i < yearsCount; i++)
        {
            var pridatTridu = "";
            hg.WriteTag("tr");
            var pridatTriduTop = "";
            if (i == 0)
                pridatTriduTop = "bunkaTabulkyKalendareTop ";
            pridatTridu = "bunkaTabulkyKalendareLeft";
            hg.WriteTagWithAttr("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTriduTop + pridatTridu);
            hg.WriteRaw("<b>" + years[i] + "</b>");
            hg.TerminateTag("td");
            pridatTridu = "bunkaTabulkyKalendareRight";
            var color = AllYearsBoxColors[i];
            var appendStyle = "";
            if (color == "#030")
                appendStyle = "color:white;";
            hg.WriteTagWith2Attrs("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTriduTop + pridatTridu, "style", appendStyle + "background-color:" + color);
            //hg.WriteRaw("<b>" + AllMonthsHtmlBoxes[i] + "</b>");
            hg.WriteRaw(AllYearsHtmlBoxes[i]);
            hg.TerminateTag("td");
        }

        hg.TerminateTag("tr");
        hg.TerminateTag("table");
        return hg.ToString();
    }

    public static string GenerateTreeWithCheckBoxes(NTreeHtml<string> tree)
    {
        var hg = new HtmlGenerator();
        //hg.WriteTag(HtmlTags.ol);
        var inner = 0;
        AddTree(ref inner, hg, tree);
        //hg.TerminateTag(HtmlTags.ol);
        return hg.ToString();
    }

    private static void AddTree(ref int inner, HtmlGenerator hg, NTreeHtml<string> tree)
    {
        inner++;
        //hg.WriteTag(HtmlTags.li);
        hg.WriteTag(HtmlTags.ol);
        hg.WriteRaw(CheckBox(tree.data));
        foreach (var item in tree.children)
        {
            hg.WriteTag(HtmlTags.li);
            hg.WriteRaw(CheckBox(item.data));
            foreach (var item2 in item.children)
                AddTree(ref inner, hg, item2);
            hg.TerminateTag(HtmlTags.li);
        }

        hg.TerminateTag(HtmlTags.ol);
    //hg.TerminateTag(HtmlTags.li);
    }

    public static string CheckBox(string data)
    {
        if (!string.IsNullOrEmpty(data))
            return "<input type=\"checkbox\" />" + data + "<br />";
        return string.Empty;
    }

    /// <summary>
    ///     Do A1 doplň třeba EditMister.aspx?mid= - co za toto si automaticky doplní a A2 jsou texty do inner textu a
    ///     Nehodí se tedy proto vždy, například, když máš přehozené IDčka v DB
    ///     When uri args and titles are the same
    /// </summary>
    /// <param name = "baseAnchor"></param>
    /// <param name = "to"></param>
    public static string GetForUlWoCheckDuplicate(string baseAnchor, List<string> to)
    {
        return GetForUlWoCheckDuplicate(baseAnchor, to, to);
    }

    /// <summary>
    ///     Automatically replace A3 for A4
    /// </summary>
    /// <param name = "baseAnchor"></param>
    /// <param name = "idcka"></param>
    /// <param name = "najitVTextu"></param>
    /// <param name = "nahraditVTextu"></param>
    /// <param name = "pripona"></param>
    public static string GetForUlWoCheckDuplicate(string baseAnchor, List<string> idcka, string najitVTextu, string nahraditVTextu, string pripona = "")
    {
        var hg = new HtmlGenerator();
        for (var i = 0; i < idcka.Count; i++)
        {
            var text = idcka[i];
            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", baseAnchor + text + pripona);
            if (!string.IsNullOrEmpty(najitVTextu) && !string.IsNullOrEmpty(nahraditVTextu))
                hg.WriteRaw(text.Replace(najitVTextu, nahraditVTextu));
            else
                hg.WriteRaw(text);
            hg.TerminateTag("a");
            hg.TerminateTag("li");
        }

        return hg.ToString();
    }

    public static string GetForUl(string baseAnchor, string[] idcka, string[] texty, bool skipDuplicates)
    {
        return GetForUl(baseAnchor, idcka.ToList(), texty.ToList(), skipDuplicates);
    }

    public static string GetForUl(string baseAnchor, List<string> idcka, List<string> texty, bool skipDuplicates)
    {
        if (idcka.Count != texty.Count)
            return "Nastala chyba, program poslal na render nejm\u00E9n\u011B v jednom poli m\u00E9n\u011B prvk\u016F ne\u017E se o\u010Dek\u00E1valo";
        var hg = new HtmlGenerator();
        if (skipDuplicates)
            texty = texty.Distinct().ToList(); //CAG.RemoveDuplicitiesList(texty);
        for (var i = 0; i < texty.Count; i++)
        {
            var text = texty[i];
            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", baseAnchor + idcka[i]);
            hg.WriteRaw(texty[i]);
            hg.TerminateTag("a");
            hg.TerminateTag("li");
        }

        return hg.ToString();
    }
}