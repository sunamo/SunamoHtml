namespace SunamoHtml.Generators;

public class HtmlGenerator2 : HtmlGenerator
{
    public static string Calendar(List<string> htmlBoxesEveryDay, int year, int mesic)
    {
        var colors = new List<string>(htmlBoxesEveryDay.Count);
        foreach (var item in htmlBoxesEveryDay) colors.Add(null);
        return Calendar(htmlBoxesEveryDay, colors, year, mesic);
    }

    public static string GenerateHtmlCheckBoxesFromFiles(string path, string masc, SearchOption so)
    {
        var hg = new HtmlGenerator();

        var files = Directory.GetFiles(path, masc, so);
        foreach (var item in files)
        {
            hg.WriteTagWithAttrs("input", "type", "checkbox");
            hg.WriteRaw(Path.GetFileName(item));
            hg.WriteBr();
        }

        return hg.ToString();
    }

    public static string Calendar(List<string> htmlBoxesEveryDay, List<string> colors, int year, int mesic)
    {
        var hg = new HtmlGenerator();
        hg.WriteTagWith2Attrs("table", "class", "tabulkaNaStredAutoSirka", "style", "width: 600px");
        hg.WriteTag("tr");

        #region Zapíšu vrchní řádky - názvy dnů

        var ppp = DTConstants.daysInWeekEN;
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
        var dt = new DateTime(year, mesic, 1);
        var prazdneNaZacatku = 0;
        var dow = dt.DayOfWeek;
        switch (dow)
        {
            case DayOfWeek.Friday:
                prazdneNaZacatku = 4;
                break;
            case DayOfWeek.Monday:
                break;
            case DayOfWeek.Saturday:
                prazdneNaZacatku = 5;
                break;
            case DayOfWeek.Sunday:
                prazdneNaZacatku = 6;
                break;
            case DayOfWeek.Thursday:
                prazdneNaZacatku = 3;
                break;
            case DayOfWeek.Tuesday:
                prazdneNaZacatku = 1;
                break;
            case DayOfWeek.Wednesday:
                prazdneNaZacatku = 2;
                break;
        }

        for (var i2 = 0; i2 < prazdneNaZacatku; i2++)
        {
            var pt2 = "";
            if (i2 == 0) pt2 = "bunkaTabulkyKalendareLeft";
            hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare " + pt2);
            hg.WriteRaw("&nbsp;");
            hg.TerminateTag("td");
        }

        var radku2 = prazdneNaZacatku + htmlBoxesEveryDay.Count / 7;
        if (prazdneNaZacatku != 0) radku2++;


        var prazdneNaZacatku2 = prazdneNaZacatku;
        var radku = 1;
        for (var i = 1; i < htmlBoxesEveryDay.Count + 1; i++, prazdneNaZacatku++)
        {
            var pridatTridu = "";

            if (prazdneNaZacatku % 7 == 0)
            {
                pridatTridu = "bunkaTabulkyKalendareLeft";
                radku++;
                hg.TerminateTag("tr");
                hg.WriteTag("tr");
            }
            else if (prazdneNaZacatku % 7 == 6)
            {
                pridatTridu = "bunkaTabulkyKalendareRight";
            }

            var color = colors[i - 1];
            var appendStyle = "";
            if (color == "#030") appendStyle = "color:white;";
            var datum = i + "." + mesic + ".";
            hg.WriteTagWith2Attrs("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTridu, "style",
                appendStyle + "background-color:" + colors[i - 1]);
            //hg.WriteTag("td");
            hg.WriteRaw("<b>" + datum + "</b>");
            hg.WriteBr();
            hg.WriteRaw(htmlBoxesEveryDay[i - 1]);
            hg.TerminateTag("td");
        }

        if (prazdneNaZacatku2 == 0) radku--;

        var bunekZbyva = radku * 7 - prazdneNaZacatku2 - htmlBoxesEveryDay.Count;
        for (var i2 = 0; i2 < bunekZbyva; i2++)
        {
            var pt = "";
            if (bunekZbyva - 1 == i2) pt = "bunkaTabulkyKalendareRight";
            hg.WriteTagWithAttr("td", "class", /*bunkaTabulkyKalendareBottom */ "bunkaTabulkyKalendare " + pt);
            hg.WriteRaw("&nbsp;");
            hg.TerminateTag("td");
        }

        hg.TerminateTag("tr");
        hg.TerminateTag("table");
        return hg.ToString();
    }

    public static string GalleryZoomInProfilePhoto(List<string> membersName, List<string> memberProfilePicture,
        List<string> memberAnchors)
    {
        var hg = new HtmlGenerator();
        hg.WriteTag("ul");
        for (var i = 0; i < membersName.Count; i++)
        {
            hg.WriteTag("li");

            hg.WriteTagWithAttr("a", "href", memberAnchors[i]);

            hg.WriteTag("p");
            hg.WriteRaw(membersName[i]);
            hg.TerminateTag("p");

            hg.WriteTagWithAttr("div", "style", "background-image: url(" + memberProfilePicture[i] + ");");
            hg.TerminateTag("div");

            hg.TerminateTag("a");
            hg.TerminateTag("li");
        }

        hg.TerminateTag("ul");
        return hg.ToString();
    }

    public static string GetSelect(string id, object def, IList list)
    {
        var gh = new HtmlGenerator();
        gh.WriteTagWithAttr("select", "name", "select" + id);
        foreach (var item2 in list)
        {
            var item = item2.ToString();
            if (item != def.ToString())
            {
                gh.WriteElement("option", item);
            }
            else
            {
                gh.WriteTagWithAttr("option", "selected", "selected");
                gh.WriteRaw(item);
                gh.TerminateTag("option");
            }
        }

        gh.TerminateTag("select");
        return gh.ToString();
    }

    public static string GetInputText(string id, string value)
    {
        var gh = new HtmlGenerator();
        gh.WriteTagWithAttrs("input", "type", "text", "name", "inputText" + id, "value", value);
        return gh.ToString();
    }

    /// <summary>
    ///     Jedná se o divy pod sebou, nikoliv o ol/ul>li
    /// </summary>
    /// <param name="hg"></param>
    /// <param name="odkazyPhoto"></param>
    /// <param name="odkazyText"></param>
    /// <param name="innerHtmlText"></param>
    /// <param name="srcPhoto"></param>
    public static string TopListWithImages(HtmlGenerator hg, int widthImage, int heightImage, string initialImageUri,
        List<string> odkazyPhoto, List<string> odkazyText, List<string> innerHtmlText, List<string> srcPhoto,
        string nameJsArray)
    {
        var count = odkazyPhoto.Count;
        if (count == 0)
            //throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto nemá žádný prvek");
            return "";
        if (count != odkazyText.Count)
            throw new Exception(
                "Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem odkazyText");
        if (count != innerHtmlText.Count)
            throw new Exception(
                "Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem innerHtmlText");
        if (count != srcPhoto.Count)
            throw new Exception(
                "Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem srcPhoto");

        //HtmlGenerator hg = new HtmlGenerator();
        var nt = 0;
        var animated = int.TryParse(srcPhoto[0], out nt);

        for (var i = 0; i < count; i++)
        {
            hg.WriteTagWithAttr("div", "style", "padding: 5px;");
            hg.WriteTagWithAttr("a", "href", odkazyPhoto[i]);
            hg.WriteTagWithAttr("div", "style", "display: inline-block;");
            if (animated)
                hg.WriteNonPairTagWithAttrs("img", "style",
                    "margin-left: auto; margin-right: auto; vertical-align-middle; width: " + widthImage +
                    "px;height:" + heightImage + "px", "id", nameJsArray + srcPhoto[i], "class", "alternatingImage",
                    "src", initialImageUri, "alt", odkazyText[i]);
            else
                hg.WriteNonPairTagWithAttrs("img", "src", srcPhoto[i], "alt", odkazyText[i]);
            hg.TerminateTag("div");

            hg.TerminateTag("a");
            hg.WriteTagWithAttr("a", "href", odkazyText[i]);
            hg.WriteRaw(innerHtmlText[i]);
            hg.TerminateTag("a");
            hg.TerminateTag("div");
        }

        return hg.ToString();
    }

    /// <summary>
    ///     Jedná se o divy pod sebou, nikoliv o ol/ul>li
    /// </summary>
    /// <param name="hg"></param>
    /// <param name="odkazyPhoto"></param>
    /// <param name="odkazyText"></param>
    /// <param name="innerHtmlText"></param>
    /// <param name="srcPhoto"></param>
    public static string TopListWithImages(HtmlGenerator hg, int widthImage, int heightImage, string initialImageUri,
        List<string> odkazyPhoto, List<string> odkazyText, List<string> innerHtmlText, List<string> srcPhoto,
        List<string> idBadges, string nameJsArray)
    {
        var count = odkazyPhoto.Count;
        if (count == 0)
            //throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto nemá žádný prvek");
            return "";
        if (count != odkazyText.Count)
            throw new Exception(
                "Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem odkazyText");
        if (count != innerHtmlText.Count)
            throw new Exception(
                "Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem innerHtmlText");
        if (count != srcPhoto.Count)
            throw new Exception(
                "Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem srcPhoto");
        if (count != idBadges.Count)
            throw new Exception(Translate.FromKey(XlfKeys.MetodaHtmlGenerator2TopListWithImagesOdkazyPhoto) + " " + count +
                                " se nerovn\u00E1 po\u010Dtem idBadges " + idBadges.Count);

        //HtmlGenerator hg = new HtmlGenerator();
        var nt = 0;
        var animated = int.TryParse(srcPhoto[0], out nt);
        for (var i = 0; i < count; i++)
        {
            hg.WriteTagWith2Attrs("div", "style", "padding: 5px;", "class", "hoverable");
            hg.WriteTagWithAttr("a", "href", odkazyPhoto[i]);
            hg.WriteTagWithAttrs("div", "style", "display: inline-block;", "id", "iosBadge" + idBadges[i], "class",
                "iosbRepair");

            if (animated)
                hg.WriteNonPairTagWithAttrs("img", "style",
                    "margin-left: auto; margin-right: auto; vertical-align-middle; width: " + widthImage +
                    "px;height:" + heightImage + "px", "id", nameJsArray + srcPhoto[i], "class", "alternatingImage",
                    "src", initialImageUri, HtmlAttrs.alt, odkazyText[i]);
            else
                hg.WriteNonPairTagWithAttrs("img", "src", srcPhoto[i], HtmlAttrs.alt, odkazyText[i]);

            hg.TerminateTag("div");
            hg.TerminateTag("a");

            hg.WriteTagWithAttr("a", "href", odkazyText[i]);
            hg.WriteRaw(innerHtmlText[i]);
            hg.TerminateTag("a");
            hg.TerminateTag("div");
        }

        return hg.ToString();
    }

    #region GetForUlWCheckDuplicate

    public static string GetForUlWCheckDuplicate(List<string> to)
    {
        var hg = new HtmlGenerator();
        var zapsane = new List<string>();
        for (var i = 0; i < to.Count; i++)
        {
            var s = to[i];
            if (!zapsane.Contains(s))
            {
                zapsane.Add(s);
                hg.WriteTag("li");
                //hg.ZapisTagSAtributem("a", "href", "ZobrazText.aspx?sid=" + s.id.ToString());
                hg.WriteRaw(s);
                hg.TerminateTag("li");
            }
        }

        return hg.ToString();
    }

    #endregion

    public static string Success(string p)
    {
        var hg = new HtmlGenerator();
        hg.WriteTagWithAttr("div", "class", "success");
        hg.WriteRaw(p);
        hg.TerminateTag("div");
        return hg.ToString();
    }

    /// <summary>
    ///     Zadávej A1 bez https://, do odkazu se doplní samo, do textu nikoliv
    /// </summary>
    /// <param name="www"></param>
    public static string Anchor(string www)
    {
        if (www.Contains("=\"")) return www;

        var http = UH.AppendHttpIfNotExists(www);
        return "<a href=\"" + http + "\"" + ">" + www + "</a>";
    }

    public static string AnchorMailto(string t)
    {
        return "<a href=\"mailto:" + t + ">" + t + "</a>";
    }

    /// <summary>
    ///     A1 je text bez https:// / https://, který se doplní do odkazu sám pokud tam nebude.
    ///     V textu se ale vždy nahradí pokud tam bude.
    /// </summary>
    /// <param name="www"></param>
    public static string AnchorWithHttp(string www)
    {
        // Prvně ho přidám pokud tam není
        if (!www.StartsWith("http")) www = "http://" + www;
        var http = www; // UH.AppendHttpIfNotExists(www);
        // pak ho odstraním aby tam nebyl 2x
        var sh = new Regex("https://").Replace(www, "", 1); //SHReplace.ReplaceOnce(www, , "");

        return "<a href=\"" + http + "\"" + ">" + sh + "</a>";
    }

    public static string AnchorWithHttp(string www, string text)
    {
        var http = UH.AppendHttpIfNotExists(www);
        return "<a href=\"" + http + "\"" + ">" + text + "</a>";
    }

    public static string AnchorWithHttp(bool targetBlank, string www, string text)
    {
        string http = null;
        http = UH.AppendHttpIfNotExists(www);
        return AnchorWithHttpCore(targetBlank, text, http);
    }

    public static string AnchorWithHttpCore(bool targetBlank, string text, string http)
    {
        if (targetBlank) return "<a href=\"" + http + "\" target=\"_blank\">" + text + "</a>";
        return "<a href=\"" + http + ">" + text + "</a>";
    }

    public static string GetRadioButtonsWoCheckDuplicate(string nameOfRBs, List<string> idcka, List<string> nazvy)
    {
        var hg = new HtmlGenerator();

        for (var i = 0; i < idcka.Count; i++)
        {
            hg.WriteNonPairTagWithAttrs("input", "type", "radio", "name", nameOfRBs, "value", idcka[i]);
            hg.WriteRaw(nazvy[i]);
            hg.WriteBr();
        }

        return hg.ToString();
    }

    public static string GetRadioButtonsWoCheckDuplicate(string nameOfRBs, List<string> idcka, List<string> nazvy,
        string eventHandlerSelected)
    {
        var hg = new HtmlGenerator();

        for (var i = 0; i < idcka.Count; i++)
        {
            hg.WriteTagWithAttrs("input", "type", "radio", "name", nameOfRBs, "value", idcka[i], "onclick",
                eventHandlerSelected);
            hg.WriteRaw(nazvy[i]);
            hg.WriteBr();
        }

        return hg.ToString();
    }

    /// <summary>
    ///     Generátor pro třídu jquery.tagcloud.js
    /// </summary>
    /// <param name="dWordCount"></param>
    public static string GetWordsForTagCloud(Dictionary<string, short> dWordCount, string prefixWithDot)
    {
        var nameJavascriptMethod = "AfterWordCloudClick";
        return GetWordsForTagCloud(dWordCount, nameJavascriptMethod, prefixWithDot);
    }

    public static string GetWordsForTagCloudManageTags(Dictionary<string, short> dWordCount, string prefixWithDot)
    {
        var nameJavascriptMethod = "AfterWordCloudClick2";
        return GetWordsForTagCloud(dWordCount, nameJavascriptMethod, prefixWithDot);
    }

    private static string GetWordsForTagCloud(Dictionary<string, short> dWordCount, string nameJavascriptMethod,
        string prefixWithDot)
    {
        var hg = new HtmlGenerator();
        foreach (var item in dWordCount)
        {
            var bezmezer = item.Key.Replace(" ", "");
            hg.WriteTagWithAttrs("a", "id", "tag" + bezmezer, "href",
                "javascript:" + prefixWithDot + nameJavascriptMethod + "($('#tag" + bezmezer + "'), '" + item.Key +
                "');", "rel", item.Value.ToString());
            hg.WriteRaw(SH.FirstCharOfEveryWordUpperDash(item.Key));
            hg.TerminateTag("a");
            hg.WriteRaw(" &nbsp; ");
        }

        return hg.ToString();
    }

    public void Detail(string name, object value)
    {
        WriteRaw("<b>" + name + ":</b> " + value);
        WriteBr();
    }

    public void DetailIfNotEmpty(string name, string value)
    {
        if (value != "")
        {
            WriteRaw("<b>" + name + ":</b> " + value);
            WriteBr();
        }
    }

    public static string DetailStatic(string name, object value)
    {
        return "<b>" + name + ":</b> " + value + "<br />";
    }

    public static string DetailStatic(string green, string name, object value)
    {
        return "<div style='color:" + green + "'><b>" + name + ":</b> " + value + "//div>";
    }


    public static string ShortForLettersCount(string p1, int p2)
    {
        p1 = p1.Replace(" ", "");
        if (p1.Length > p2)
        {
            var whatLeave = SH.ShortForLettersCount(p1, p2);
            //"<span static class='tooltip'>" +
            whatLeave += "<span static class='showonhover'><a href='#'> ... </a><span static class='hovertext'>" +
                         new Regex(whatLeave).Replace(p1, "", 1) + "</span></span>";
            return whatLeave;
        }

        return p1;
    }

    public static string LiI(string p)
    {
        return "<li><i>" + p + "//i></li>";
    }

    public static string GetForCheckBoxListWoCheckDuplicate(string idClassCheckbox, string idClassSpan,
        List<string> idCheckBoxes, List<string> list)
    {
        var hg = new HtmlGenerator();
        if (idCheckBoxes.Count != list.Count)
            throw new Exception(
                "Nestejn\u00FD po\u010Det parametr\u016F v metod\u011B GetForCheckBoxListWoCheckDuplicate " +
                idCheckBoxes.Count + ":" + list.Count);

        for (var i = 0; i < idCheckBoxes.Count; i++)
        {
            var f = idCheckBoxes[i];
            hg.WriteNonPairTagWithAttrs("input", "type", "checkbox", "id", idClassCheckbox + f, "class",
                idClassCheckbox);
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
        var r = hg.ToString();
        return r;
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
        if (AllYearsHtmlBoxes.Count != 12) throw new Exception("D\u00E9lka AllMonthsHtmlBoxes nen\u00ED 12.");
        if (AllMonthsBoxColors.Count != 12) throw new Exception("D\u00E9lka AllMonthsBoxColors nen\u00ED 12.");
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
            else if (i == 11) pridatTridu = "bunkaTabulkyKalendareRight";

            var color = AllMonthsBoxColors[i];
            var appendStyle = "";
            if (color == "#030") appendStyle = "color:white;";
            hg.WriteTagWith2Attrs("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTridu, "style",
                appendStyle + "background-color:" + color);

            hg.WriteRaw("<b>" + AllYearsHtmlBoxes[i] + "</b>");

            hg.TerminateTag("td");
        }

        hg.TerminateTag("tr");
        hg.TerminateTag("table");
        return hg.ToString();
    }

    public static string AllYearsTable(List<string> years, List<string> AllYearsHtmlBoxes,
        List<string> AllYearsBoxColors)
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
            if (i == 0) pridatTriduTop = "bunkaTabulkyKalendareTop ";
            pridatTridu = "bunkaTabulkyKalendareLeft";
            hg.WriteTagWithAttr("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTriduTop + pridatTridu);
            hg.WriteRaw("<b>" + years[i] + "</b>");
            hg.TerminateTag("td");
            pridatTridu = "bunkaTabulkyKalendareRight";
            var color = AllYearsBoxColors[i];
            var appendStyle = "";
            if (color == "#030") appendStyle = "color:white;";
            hg.WriteTagWith2Attrs("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTriduTop + pridatTridu,
                "style", appendStyle + "background-color:" + color);

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
            foreach (var item2 in item.children) AddTree(ref inner, hg, item2);
            hg.TerminateTag(HtmlTags.li);
        }

        hg.TerminateTag(HtmlTags.ol);
        //hg.TerminateTag(HtmlTags.li);
    }

    public static string CheckBox(string data)
    {
        if (!string.IsNullOrEmpty(data)) return "<input type=\"checkbox\" />" + data + "<br />";
        return string.Empty;
    }


    #region GetForUlWoCheckDuplicate

    /// <summary>
    ///     Do A1 doplň třeba EditMister.aspx?mid= - co za toto si automaticky doplní a A2 jsou texty do inner textu a
    ///     Nehodí se tedy proto vždy, například, když máš přehozené IDčka v DB
    ///     When uri args and titles are the same
    /// </summary>
    /// <param name="baseAnchor"></param>
    /// <param name="to"></param>
    public static string GetForUlWoCheckDuplicate(string baseAnchor, List<string> to)
    {
        return GetForUlWoCheckDuplicate(baseAnchor, to, to);
    }

    /// <summary>
    ///     Automatically replace A3 for A4
    /// </summary>
    /// <param name="baseAnchor"></param>
    /// <param name="idcka"></param>
    /// <param name="najitVTextu"></param>
    /// <param name="nahraditVTextu"></param>
    /// <param name="pripona"></param>
    public static string GetForUlWoCheckDuplicate(string baseAnchor, List<string> idcka, string najitVTextu,
        string nahraditVTextu, string pripona = "")
    {
        var hg = new HtmlGenerator();

        for (var i = 0; i < idcka.Count; i++)
        {
            var s = idcka[i];

            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", baseAnchor + s + pripona);
            if (!string.IsNullOrEmpty(najitVTextu) && !string.IsNullOrEmpty(nahraditVTextu))
                hg.WriteRaw(s.Replace(najitVTextu, nahraditVTextu));
            else
                hg.WriteRaw(s);

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
            return
                "Nastala chyba, program poslal na render nejm\u00E9n\u011B v jednom poli m\u00E9n\u011B prvk\u016F ne\u017E se o\u010Dek\u00E1valo";
        var hg = new HtmlGenerator();

        if (skipDuplicates) texty = texty.Distinct().ToList(); //CAG.RemoveDuplicitiesList(texty);

        for (var i = 0; i < texty.Count; i++)
        {
            var s = texty[i];
            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", baseAnchor + idcka[i]);

            hg.WriteRaw(texty[i]);


            hg.TerminateTag("a");

            hg.TerminateTag("li");
        }

        return hg.ToString();
    }

    public static string GetForUlWoCheckDuplicate(string baseAnchor, List<string> idcka, List<string> texty)
    {
        return GetForUl(baseAnchor, idcka, texty, false);
    }

    /// <summary>
    ///     Počet v A1 a A2 musí být stejný.
    ///     Mohl bych udělat tu samou metodu s ABC/ABC ale tam je jako druhý parametr object a to se mi nehodí do krámu
    /// </summary>
    /// <param name="anchors"></param>
    /// <param name="to"></param>
    public static string GetForUlWoCheckDuplicate(List<string> anchors, List<string> to)
    {
        return GetForUl("", anchors, to, false);
    }

    /// <summary>
    ///     Tato metoda se používá pokud v Ul nepoužíváš žádné odkazy
    /// </summary>
    /// <param name="to"></param>
    public static string GetForUlWoCheckDuplicate(List<string> to)
    {
        var hg = new HtmlGenerator();

        for (var i = 0; i < to.Count; i++)
        {
            hg.WriteTag("li");
            hg.WriteRaw(to[i]);
            hg.TerminateTag("li");
        }

        return hg.ToString();
    }

    #endregion

    #region Ul

    public static string GetUlWoCheckDuplicate(string baseAnchor, List<string> to)
    {
        return "<ul static class=\"textVlevo\">";
        //HtmlGenerator hg = new HtmlGenerator();

        //for (int i = 0; i < to.Count; i++)
        //{
        //    string s = to[i];

        //    hg.WriteTag("li");
        //    hg.WriteTagWithAttr("a", "href", baseAnchor + (i + 1).ToString());
        //    hg.WriteRaw(s);
        //    hg.TerminateTag("a");

        //    hg.TerminateTag("li");
        //}

        //return hg.ToString() + "//ul>";
    }

    /// <summary>
    ///     Bere si pouze jeden parametr, tedy je bez odkazů
    /// </summary>
    /// <param name="list"></param>
    public static string GetUlWoCheckDuplicate(List<string> list, string appendClass)
    {
        return "<ul static class=\"textVlevo " + appendClass + ">" + GetForUlWoCheckDuplicate(list) + "//ul>";
    }

    /// <param name="anchors"></param>
    /// <param name="texts"></param>
    public static string GetUlWoCheckDuplicate(List<string> anchors, List<string> texts)
    {
        return "<ul static class=\"textVlevo\">" + GetForUlWoCheckDuplicate(anchors, texts) + "//ul>";
    }

    #endregion

    #region Ol

    public static string GetOl(List<string> possibleAnswers, bool checkDuplicates = false)
    {
        return HtmlGeneratorList.GetFor("", possibleAnswers, possibleAnswers, checkDuplicates, HtmlTags.ol);
    }

    private static Type type = typeof(HtmlGenerator2);


    public static string GetOlWoCheckDuplicate(List<string> anchors, List<string> to)
    {
        if (anchors.Count != to.Count)
            throw new Exception("Po\u010Dty odr\u00E1\u017Eek a odkaz\u016F se li\u0161\u00ED");

        var hg = new HtmlGenerator();
        hg.WriteTag("ol");
        for (var i = 0; i < to.Count; i++)
        {
            var s = to[i];

            hg.WriteTag("li");

            hg.WriteRaw(s);


            hg.TerminateTag("li");
        }

        hg.TerminateTag("ol");
        return hg.ToString();
    }

    #endregion
}