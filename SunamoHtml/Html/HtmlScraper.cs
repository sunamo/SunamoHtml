namespace SunamoHtml.Html;

public class HtmlScraper
{
    private static readonly StringBuilder s_sb = new();

    public static string AttributeValuesOfTag(HtmlNode hd, bool recursive, string tag, string attr)
    {
        var nodes = HtmlAgilityHelper.Nodes(hd, recursive, tag);
        foreach (var item in nodes) s_sb.AppendLine(HtmlAssistant.GetValueOfAttribute(attr, item));
        return s_sb.ToString();
    }

    public override string ToString()
    {
        return s_sb.ToString();
    }
}