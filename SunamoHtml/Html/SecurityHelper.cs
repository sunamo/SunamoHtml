namespace SunamoHtml.Html;

/// <summary>
/// Represents the security helper type.
/// </summary>
public static class SecurityHelper
{
    /// <summary>
    /// Treat html code.
    /// </summary>
    /// <param name="html">The html.</param>
    /// <returns>The treat html code result.</returns>
    public static string TreatHtmlCode(string html)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        html = RemoveJsAttributesFromEveryNode(html);
        html = html.Replace(" ", "", StringComparison.Ordinal);
        html = RegexHelper.RHtmlScript.Replace(html, "");
        html = RegexHelper.RHtmlComment.Replace(html, "");

        return html;
    }

    /// <summary>
    /// Remove js attributes from every node.
    /// </summary>
    /// <param name="html">The html.</param>
    /// <returns>The remove js attributes from every node result.</returns>
    public static string RemoveJsAttributesFromEveryNode(string html)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        var document = new HtmlDocument();
        document.LoadHtml(html);
        var nodes = document.DocumentNode.SelectNodes("//*");
        if (nodes != null)
        {
            foreach (var eachNode in nodes)
                foreach (var item in eachNode.Attributes)
                    if (item.Name.ToLowerInvariant().StartsWith("on", StringComparison.OrdinalIgnoreCase))
                        item.Remove();
                    else if (item.Value.ToLowerInvariant().Trim().StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
                        item.Remove();
            html = document.DocumentNode.OuterHtml;
        }

        return html;
    }
}
