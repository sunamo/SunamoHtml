namespace SunamoHtml.Html;

public class HtmlScraper
{
    private static readonly StringBuilder StringBuilder = new();

    public static string AttributeValuesOfTag(HtmlNode node, bool isRecursive, string tag, string attributeName)
    {
        var nodes = HtmlAgilityHelper.Nodes(node, isRecursive, tag);
        foreach (var item in nodes)
            StringBuilder.AppendLine(HtmlAssistant.GetValueOfAttribute(attributeName, item));
        return StringBuilder.ToString();
    }

    public override string ToString()
    {
        return StringBuilder.ToString();
    }
}
