// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoHtml.Generators;
public partial class HtmlGenerator2 : HtmlGenerator
{
    /// <summary>
    ///     Jedná se o divy pod sebou, nikoliv o ol/ul>li
    /// </summary>
    /// <param name = "hg"></param>
    /// <param name = "odkazyPhoto"></param>
    /// <param name = "odkazyText"></param>
    /// <param name = "innerHtmlText"></param>
    /// <param name = "srcPhoto"></param>
    public static string TopListWithImages(HtmlGenerator hg, int widthImage, int heightImage, string initialImageUri, List<string> odkazyPhoto, List<string> odkazyText, List<string> innerHtmlText, List<string> srcPhoto, List<string> idBadges, string nameJsArray)
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
        if (count != idBadges.Count)
            throw new Exception(Translate.FromKey(XlfKeys.MetodaHtmlGenerator2TopListWithImagesOdkazyPhoto) + " " + count + " se nerovn\u00E1 po\u010Dtem idBadges " + idBadges.Count);
        //HtmlGenerator hg = new HtmlGenerator();
        var nt = 0;
        var animated = int.TryParse(srcPhoto[0], out nt);
        for (var i = 0; i < count; i++)
        {
            hg.WriteTagWith2Attrs("div", "style", "padding: 5px;", "class", "hoverable");
            hg.WriteTagWithAttr("a", "href", odkazyPhoto[i]);
            hg.WriteTagWithAttrs("div", "style", "display: inline-block;", "id", "iosBadge" + idBadges[i], "class", "iosbRepair");
            if (animated)
                hg.WriteNonPairTagWithAttrs("img", "style", "margin-left: auto; margin-right: auto; vertical-align-middle; width: " + widthImage + "px;height:" + heightImage + "px", "id", nameJsArray + srcPhoto[i], "class", "alternatingImage", "src", initialImageUri, HtmlAttrs.alt, odkazyText[i]);
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

    public static string GetForUlWCheckDuplicate(List<string> to)
    {
        var hg = new HtmlGenerator();
        var zapsane = new List<string>();
        for (var i = 0; i < to.Count; i++)
        {
            var text = to[i];
            if (!zapsane.Contains(text))
            {
                zapsane.Add(text);
                hg.WriteTag("li");
                //hg.ZapisTagSAtributem("a", "href", "ZobrazText.aspx?sid=" + text.id.ToString());
                hg.WriteRaw(text);
                hg.TerminateTag("li");
            }
        }

        return hg.ToString();
    }

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
    /// <param name = "www"></param>
    public static string Anchor(string www)
    {
        if (www.Contains("=\""))
            return www;
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
    /// <param name = "www"></param>
    public static string AnchorWithHttp(string www)
    {
        // Prvně ho přidám pokud tam není
        if (!www.StartsWith("http"))
            www = "http://" + www;
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
        if (targetBlank)
            return "<a href=\"" + http + "\" target=\"_blank\">" + text + "</a>";
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

    public static string GetRadioButtonsWoCheckDuplicate(string nameOfRBs, List<string> idcka, List<string> nazvy, string eventHandlerSelected)
    {
        var hg = new HtmlGenerator();
        for (var i = 0; i < idcka.Count; i++)
        {
            hg.WriteTagWithAttrs("input", "type", "radio", "name", nameOfRBs, "value", idcka[i], "onclick", eventHandlerSelected);
            hg.WriteRaw(nazvy[i]);
            hg.WriteBr();
        }

        return hg.ToString();
    }

    /// <summary>
    ///     Generátor pro třídu jquery.tagcloud.js
    /// </summary>
    /// <param name = "dWordCount"></param>
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

    private static string GetWordsForTagCloud(Dictionary<string, short> dWordCount, string nameJavascriptMethod, string prefixWithDot)
    {
        var hg = new HtmlGenerator();
        foreach (var item in dWordCount)
        {
            var bezmezer = item.Key.Replace(" ", "");
            hg.WriteTagWithAttrs("a", "id", "tag" + bezmezer, "href", "javascript:" + prefixWithDot + nameJavascriptMethod + "($('#tag" + bezmezer + "'), '" + item.Key + "');", "rel", item.Value.ToString());
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
            whatLeave += "<span static class='showonhover'><a href='#'> ... </a><span static class='hovertext'>" + new Regex(whatLeave).Replace(p1, "", 1) + "</span></span>";
            return whatLeave;
        }

        return p1;
    }

    public static string LiI(string p)
    {
        return "<li><i>" + p + "//i></li>";
    }
}