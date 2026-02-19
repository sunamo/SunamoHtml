namespace SunamoHtml.Html;

/// <summary>
/// Static helper class for loading and working with HTML documents using HtmlAgilityPack.
/// </summary>
public static class HtmlDocumentS
{
    private static string s_htmlContent = string.Empty;

    /// <summary>
    /// Loads HTML from string and returns the document node.
    /// HTML is automatically decoded before loading.
    /// </summary>
    /// <param name="html">The HTML string to load.</param>
    /// <returns>The document node of the loaded HTML.</returns>
    public static HtmlNode LoadHtml(string html)
    {
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();

        html = WebUtility.HtmlDecode(html);
        s_htmlContent = html;
        htmlDocument.LoadHtml(html);
        return htmlDocument.DocumentNode;
    }

    /// <summary>
    /// Loads HTML from a file and returns the document node.
    /// HTML is automatically decoded after loading from file.
    /// </summary>
    /// <param name="path">The file path to load HTML from.</param>
    /// <returns>The document node of the loaded HTML.</returns>
    public static
#if ASYNC
        async Task<HtmlNode>
#else
HtmlNode
#endif
        Load(string path)
    {
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        s_htmlContent =
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(path).ConfigureAwait(false);
        s_htmlContent = WebUtility.HtmlDecode(s_htmlContent);
        htmlDocument.LoadHtml(s_htmlContent);
        return htmlDocument.DocumentNode;
    }

    /// <summary>
    /// Extracts the title from an HTML document.
    /// </summary>
    /// <param name="htmlNode">The HTML document node.</param>
    /// <returns>The title text, or empty string if not found.</returns>
    public static string Title(HtmlNode htmlNode)
    {
        var titleNode = HtmlAgilityHelper.Node(htmlNode, true, HtmlTags.Title);
        return InnerHtmlToStringEmpty(titleNode);
    }

    /// <summary>
    /// Gets the trimmed inner HTML of a node, or empty string if node is null.
    /// </summary>
    /// <param name="htmlNode">The HTML node to get inner HTML from.</param>
    /// <returns>Trimmed inner HTML, or empty string if node is null.</returns>
    public static string InnerHtmlToStringEmpty(HtmlNode? htmlNode)
    {
        if (htmlNode == null)
            return string.Empty;

        return htmlNode.InnerHtml.Trim();
    }
}