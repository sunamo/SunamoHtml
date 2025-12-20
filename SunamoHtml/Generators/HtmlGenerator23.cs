// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoHtml.Generators;
public partial class HtmlGenerator2 : HtmlGenerator
{
    public static string GetForUlWoCheckDuplicate(string baseAnchor, List<string> idcka, List<string> texty)
    {
        return GetForUl(baseAnchor, idcka, texty, false);
    }

    /// <summary>
    ///     Počet v A1 a A2 musí být stejný.
    ///     Mohl bych udělat tu samou metodu text ABC/ABC ale tam je jako druhý parametr object a to se mi nehodí do krámu
    /// </summary>
    /// <param name = "anchors"></param>
    /// <param name = "to"></param>
    public static string GetForUlWoCheckDuplicate(List<string> anchors, List<string> to)
    {
        return GetForUl("", anchors, to, false);
    }

    /// <summary>
    ///     Tato metoda se používá pokud v Ul nepoužíváš žádné odkazy
    /// </summary>
    /// <param name = "to"></param>
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

    public static string GetUlWoCheckDuplicate(string baseAnchor, List<string> to)
    {
        //return "<ul static class=\"textVlevo\">";
        HtmlGenerator hg = new HtmlGenerator();
        for (int i = 0; i < to.Count; i++)
        {
            string text = to[i];
            hg.WriteTag("li");
            hg.WriteTagWithAttr("a", "href", baseAnchor + (i + 1).ToString());
            hg.WriteRaw(text);
            hg.TerminateTag("a");
            hg.TerminateTag("li");
        }

        return hg.ToString() + "//ul>";
    }

    /// <summary>
    ///     Bere si pouze jeden parametr, tedy je bez odkazů
    /// </summary>
    /// <param name = "list"></param>
    public static string GetUlWoCheckDuplicate(List<string> list, string appendClass)
    {
        return "<ul static class=\"textVlevo " + appendClass + ">" + GetForUlWoCheckDuplicate(list) + "//ul>";
    }

    /// <param name = "anchors"></param>
    /// <param name = "texts"></param>
    public static string GetUlWoCheckDuplicate(List<string> anchors, List<string> texts)
    {
        return "<ul static class=\"textVlevo\">" + GetForUlWoCheckDuplicate(anchors, texts) + "//ul>";
    }

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
            var text = to[i];
            hg.WriteTag("li");
            hg.WriteRaw(text);
            hg.TerminateTag("li");
        }

        hg.TerminateTag("ol");
        return hg.ToString();
    }
}