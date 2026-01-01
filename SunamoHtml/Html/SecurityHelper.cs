namespace SunamoHtml.Html;

/// <summary>
/// EN: Helper class for treating HTML code by removing dangerous scripts and JavaScript attributes.
/// CZ: Pomocná třída pro ošetření HTML kódu odstraněním nebezpečných skriptů a JavaScript atributů.
/// </summary>
public static class SecurityHelper
{
    /// <summary>
    /// Treats HTML code by removing dangerous elements:
    /// - JavaScript attributes (onclick, onload, etc.)
    /// - Script tags
    /// - HTML comments
    /// - Non-breaking spaces
    /// </summary>
    /// <param name="html">The HTML code to treat.</param>
    /// <returns>Sanitized HTML code.</returns>
    public static string TreatHtmlCode(string html)
    {
        html = RemoveJsAttributesFromEveryNode(html);
        html = html.Replace(" ", "");
        html = RegexHelper.rHtmlScript.Replace(html, "");
        html = RegexHelper.rHtmlComment.Replace(html, "");

        return html;
    }

    /// <summary>
    /// Removes all JavaScript-related attributes from every HTML node.
    /// Removes attributes starting with "on" (onclick, onload, etc.) and attributes with "javascript:" values.
    /// </summary>
    /// <param name="html">The HTML code to process.</param>
    /// <returns>HTML with JavaScript attributes removed.</returns>
    public static string RemoveJsAttributesFromEveryNode(string html)
    {
        var document = new HtmlDocument();
        document.LoadHtml(html);
        var nodes = document.DocumentNode.SelectNodes("//*");
        if (nodes != null)
        {
            foreach (var eachNode in nodes)
                foreach (var item in eachNode.Attributes)
                    if (item.Name.ToLower().StartsWith("on"))
                        item.Remove();
                    else if (item.Value.ToLower().Trim().StartsWith("javascript:"))
                        item.Remove();
            html = document.DocumentNode.OuterHtml;
        }

        return html;
    }
}
