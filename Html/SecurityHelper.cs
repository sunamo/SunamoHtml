namespace SunamoHtml.Html;

public static class SecurityHelper
{
    public static string TreatHtmlCode(string r)
    {
        r = RemoveJsAttributesFromEveryNode(r);
        r = r.Replace(" ", ""); //SHReplace.ReplaceAll2(r, " ", "");
        r = RegexHelper.rHtmlScript.Replace(r, "");
        r = RegexHelper.rHtmlComment.Replace(r, "");

        return r;
    }

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
                    else if (item.Value.ToLower().Trim().StartsWith("javascript:")) item.Remove();
            html = document.DocumentNode.OuterHtml;
        }

        return html;
    }
}