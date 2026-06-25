namespace SunamoHtml.Html;

public static class HtmlDocumentS
{
    private static string s_htmlContent = string.Empty;

    // HTML is automatically decoded before loading.
    public static HtmlNode LoadHtml(string html)
    {
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();

        html = WebUtility.HtmlDecode(html);
        s_htmlContent = html;
        htmlDocument.LoadHtml(html);
        return htmlDocument.DocumentNode;
    }

    // HTML is automatically decoded after loading from file.
    public static
        async Task<HtmlNode>
        Load(string path)
    {
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        s_htmlContent =
            await FileAsync.ReadAllTextAsync(path).ConfigureAwait(false);
        s_htmlContent = WebUtility.HtmlDecode(s_htmlContent);
        htmlDocument.LoadHtml(s_htmlContent);
        return htmlDocument.DocumentNode;
    }

    public static string Title(HtmlNode htmlNode)
    {
        var titleNode = HtmlAgilityHelper.Node(htmlNode, true, HtmlTags.Title);
        return InnerHtmlToStringEmpty(titleNode);
    }

    public static string InnerHtmlToStringEmpty(HtmlNode? htmlNode)
    {
        if (htmlNode == null)
            return string.Empty;

        return htmlNode.InnerHtml.Trim();
    }
}
