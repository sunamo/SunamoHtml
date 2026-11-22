namespace SunamoHtml.Html;

/// <summary>
/// Represents the html scraper type.
/// </summary>
public class HtmlScraper
{
    private static readonly StringBuilder StringBuilder = new();

    /// <summary>
    /// Attribute values of tag.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="isRecursive">The is recursive.</param>
    /// <param name="tag">The tag.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <returns>The attribute values of tag result.</returns>
    public static string AttributeValuesOfTag(HtmlNode node, bool isRecursive, string tag, string attributeName)
    {
        var nodes = HtmlAgilityHelper.Nodes(node, isRecursive, tag);
        foreach (var item in nodes)
            StringBuilder.AppendLine(HtmlAssistant.GetValueOfAttribute(attributeName, item));
        return StringBuilder.ToString();
    }

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns>The to string result.</returns>
    public override string ToString()
    {
        return StringBuilder.ToString();
    }
}
