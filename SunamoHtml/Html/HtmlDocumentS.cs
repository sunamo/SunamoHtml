namespace SunamoHtml.Html;

/// <summary>
/// EN: Static helper class for loading and working with HTML documents using HtmlAgilityPack.
/// CZ: Statická pomocná třída pro načítání a práci s HTML dokumenty pomocí HtmlAgilityPack.
/// </summary>
public static class HtmlDocumentS
{
    private static string s_htmlContent;

    /// <summary>
    /// Loads HTML from string and returns the document node.
    /// HTML is automatically decoded before loading.
    /// </summary>
    /// <param name="html">The HTML string to load.</param>
    /// <returns>The document node of the loaded HTML.</returns>
    public static HtmlNode LoadHtml(string html)
    {
        var hd = HtmlAgilityHelper.CreateHtmlDocument();

        html = WebUtility.HtmlDecode(html);
        s_htmlContent = html;
        hd.LoadHtml(html);
        return hd.DocumentNode;
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
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        s_htmlContent =
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(path);
        s_htmlContent = WebUtility.HtmlDecode(s_htmlContent);
        hd.LoadHtml(s_htmlContent);
        return hd.DocumentNode;
    }

    /// <summary>
    /// Extracts the title from an HTML document.
    /// </summary>
    /// <param name="hd">The HTML document node.</param>
    /// <returns>The title text, or empty string if not found.</returns>
    public static string Title(HtmlNode hd)
    {
        return InnerHtmlToStringEmpty(HtmlAgilityHelper.Node(hd, true, HtmlTags.title));
    }

    /// <summary>
    /// Gets the trimmed inner HTML of a node, or empty string if node is null.
    /// </summary>
    /// <param name="htmlNode">The HTML node to get inner HTML from.</param>
    /// <returns>Trimmed inner HTML, or empty string if node is null.</returns>
    public static string InnerHtmlToStringEmpty(HtmlNode htmlNode)
    {
        if (htmlNode == null)
            return string.Empty;

        return htmlNode.InnerHtml.Trim();
    }
}
