// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoHtml.Html;

public static class HtmlDocumentS
{
    private static string s_html2;

    public static HtmlNode LoadHtml(string html)
    {
        var hd = HtmlAgilityHelper.CreateHtmlDocument();

        html = WebUtility.HtmlDecode(html);
        s_html2 = html;
        //HtmlHelper.ToXml(html)
        hd.LoadHtml(html);
        return hd.DocumentNode;
    }

    public static
#if ASYNC
        async Task<HtmlNode>
#else
HtmlNode
#endif
        Load(string path)
    {
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        //hd.Encoding = Encoding.UTF8;
        s_html2 =
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(path);
        s_html2 = WebUtility.HtmlDecode(s_html2);
        //string html =HtmlHelper.ToXml();
        hd.LoadHtml(s_html2);
        return hd.DocumentNode;
    }

    public static string Title(HtmlNode hd)
    {
        return InnerHtmlToStringEmpty(HtmlAgilityHelper.Node(hd, true, HtmlTags.title));
    }

    public static string InnerHtmlToStringEmpty(HtmlNode htmlNode)
    {
        if (htmlNode == null) return string.Empty;

        return htmlNode.InnerHtml.Trim();
    }
}