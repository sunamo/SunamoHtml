// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoHtml.Generators;
public partial class HtmlGenerator2 : HtmlGenerator
{
    public static string Calendar(List<string> htmlBoxesEveryDay, int year, int mesic)
    {
        var colors = new List<string>(htmlBoxesEveryDay.Count);
        foreach (var item in htmlBoxesEveryDay)
            colors.Add(null);
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
            if (i2 == 0)
                pt2 = "bunkaTabulkyKalendareLeft";
            hg.WriteTagWithAttr("td", "class", "bunkaTabulkyKalendare " + pt2);
            hg.WriteRaw("&nbsp;");
            hg.TerminateTag("td");
        }

        var radku2 = prazdneNaZacatku + htmlBoxesEveryDay.Count / 7;
        if (prazdneNaZacatku != 0)
            radku2++;
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
            if (color == "#030")
                appendStyle = "color:white;";
            var datum = i + "." + mesic + ".";
            hg.WriteTagWith2Attrs("td", "class", "tableCenter bunkaTabulkyKalendare " + pridatTridu, "style", appendStyle + "background-color:" + colors[i - 1]);
            //hg.WriteTag("td");
            hg.WriteRaw("<b>" + datum + "</b>");
            hg.WriteBr();
            hg.WriteRaw(htmlBoxesEveryDay[i - 1]);
            hg.TerminateTag("td");
        }

        if (prazdneNaZacatku2 == 0)
            radku--;
        var bunekZbyva = radku * 7 - prazdneNaZacatku2 - htmlBoxesEveryDay.Count;
        for (var i2 = 0; i2 < bunekZbyva; i2++)
        {
            var pt = "";
            if (bunekZbyva - 1 == i2)
                pt = "bunkaTabulkyKalendareRight";
            hg.WriteTagWithAttr("td", "class", /*bunkaTabulkyKalendareBottom */ "bunkaTabulkyKalendare " + pt);
            hg.WriteRaw("&nbsp;");
            hg.TerminateTag("td");
        }

        hg.TerminateTag("tr");
        hg.TerminateTag("table");
        return hg.ToString();
    }

    public static string GalleryZoomInProfilePhoto(List<string> membersName, List<string> memberProfilePicture, List<string> memberAnchors)
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
    /// <param name = "hg"></param>
    /// <param name = "odkazyPhoto"></param>
    /// <param name = "odkazyText"></param>
    /// <param name = "innerHtmlText"></param>
    /// <param name = "srcPhoto"></param>
    public static string TopListWithImages(HtmlGenerator hg, int widthImage, int heightImage, string initialImageUri, List<string> odkazyPhoto, List<string> odkazyText, List<string> innerHtmlText, List<string> srcPhoto, string nameJsArray)
    {
        var count = odkazyPhoto.Count;
        if (count == 0)
            //throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto nemá žádný prvek");
            return "";
        if (count != odkazyText.Count)
            throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem odkazyText");
        if (count != innerHtmlText.Count)
            throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem innerHtmlText");
        if (count != srcPhoto.Count)
            throw new Exception("Metoda HtmlGenerator2.TopListWithImages - odkazyPhoto se nerovn\u00E1 po\u010Dtem srcPhoto");
        //HtmlGenerator hg = new HtmlGenerator();
        var nt = 0;
        var animated = int.TryParse(srcPhoto[0], out nt);
        for (var i = 0; i < count; i++)
        {
            hg.WriteTagWithAttr("div", "style", "padding: 5px;");
            hg.WriteTagWithAttr("a", "href", odkazyPhoto[i]);
            hg.WriteTagWithAttr("div", "style", "display: inline-block;");
            if (animated)
                hg.WriteNonPairTagWithAttrs("img", "style", "margin-left: auto; margin-right: auto; vertical-align-middle; width: " + widthImage + "px;height:" + heightImage + "px", "id", nameJsArray + srcPhoto[i], "class", "alternatingImage", "src", initialImageUri, "alt", odkazyText[i]);
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
}