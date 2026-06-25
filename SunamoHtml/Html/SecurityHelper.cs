namespace SunamoHtml.Html;

public static class SecurityHelper
{
    public static string TreatHtmlCode(string html)
    {
        if (html == null) throw new ArgumentNullException(nameof(html));
        html = RemoveJsAttributesFromEveryNode(html);
        html = html.Replace(" ", "", StringComparison.Ordinal);
        html = RegexHelper.RHtmlScript.Replace(html, "");
        html = RegexHelper.RHtmlComment.Replace(html, "");

        return html;
    }

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
