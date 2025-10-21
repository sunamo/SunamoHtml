// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoHtml.Generators;

public static class HtmlGeneratorList
{
    /// <summary>
    ///     If A3 is null, will be used data from A2
    /// </summary>
    /// <param name="baseAnchor"></param>
    /// <param name="to"></param>
    /// <param name="titles"></param>
    /// <param name="checkDuplicates"></param>
    /// <param name="tag"></param>
    public static string GetFor(string baseAnchor, List<string> to, List<string> titles, bool checkDuplicates,
        string tag)
    {
        var hg = new HtmlGenerator();
        List<string> zapsane = null;


        if (titles == null) titles = to;


        zapsane = new List<string>();
        hg.WriteTag(tag);
        for (var i = 0; i < to.Count; i++)
        {
            var text = to[i];
            if (!zapsane.Contains(text))
            {
                if (checkDuplicates) zapsane.Add(text);

                hg.WriteTag("li");

                hg.WriteTagWithAttr("a", "href", baseAnchor + to[i]);
                //hg.ZapisTagSAtributem("a", "href", "ZobrazText.aspx?sid=" + text.id.ToString());
                hg.WriteRaw(titles[i]);
                hg.TerminateTag("a");
                hg.TerminateTag("li");
            }
        }

        hg.TerminateTag(tag);
        return hg.ToString();
    }

    public static string Ul(string baseAnchor, List<string> to, List<string> titles, bool checkDuplicates)
    {
        return GetFor(baseAnchor, to, titles, checkDuplicates, HtmlTags.ul);
    }

    public static string Ol(string baseAnchor, List<string> to, List<string> titles, bool checkDuplicates)
    {
        return GetFor(baseAnchor, to, titles, checkDuplicates, HtmlTags.ol);
    }
}