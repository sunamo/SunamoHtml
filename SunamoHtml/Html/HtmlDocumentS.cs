namespace SunamoHtml.Html;

/// <summary>
/// Represents the html document s type.
/// </summary>
public static class HtmlDocumentS
{
    private static string s_htmlContent = string.Empty;

    // HTML is automatically decoded before loading.
    /// <summary>
    /// Load html.
    /// </summary>
    /// <param name="html">The html.</param>
    /// <returns>The load html result.</returns>
    public static HtmlNode LoadHtml(string html)
    {
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();

        html = WebUtility.HtmlDecode(html);
        s_htmlContent = html;
        htmlDocument.LoadHtml(html);
        return htmlDocument.DocumentNode;
    }

    // HTML is automatically decoded after loading from file.
    /// <summary>
    /// Load.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The load result.</returns>
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

    /// <summary>
    /// Title.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <returns>The title result.</returns>
    public static string Title(HtmlNode htmlNode)
    {
        var titleNode = HtmlAgilityHelper.Node(htmlNode, true, HtmlTags.Title);
        return InnerHtmlToStringEmpty(titleNode);
    }

    /// <summary>
    /// Inner html to string empty.
    /// </summary>
    /// <param name="htmlNode">The html node.</param>
    /// <returns>The inner html to string empty result.</returns>
    public static string InnerHtmlToStringEmpty(HtmlNode? htmlNode)
    {
        if (htmlNode == null)
            return string.Empty;

        return htmlNode.InnerHtml.Trim();
    }
}
