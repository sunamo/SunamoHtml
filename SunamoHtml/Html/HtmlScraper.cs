namespace SunamoHtml.Html;

/// <summary>
/// EN: Helper class for scraping HTML attribute values from tags.
/// CZ: Pomocná třída pro získávání hodnot HTML atributů z tagů.
/// </summary>
public class HtmlScraper
{
    private static readonly StringBuilder StringBuilder = new();

    /// <summary>
    /// Gets all attribute values of a specific tag and appends them to internal string builder.
    /// </summary>
    /// <param name="node">The HTML node to search in.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <param name="attributeName">The attribute name to get values from.</param>
    /// <returns>String with all attribute values, each on a new line.</returns>
    public static string AttributeValuesOfTag(HtmlNode node, bool isRecursive, string tag, string attributeName)
    {
        var nodes = HtmlAgilityHelper.Nodes(node, isRecursive, tag);
        foreach (var item in nodes)
            StringBuilder.AppendLine(HtmlAssistant.GetValueOfAttribute(attributeName, item));
        return StringBuilder.ToString();
    }

    /// <summary>
    /// Returns the accumulated string from the internal string builder.
    /// </summary>
    /// <returns>The accumulated string.</returns>
    public override string ToString()
    {
        return StringBuilder.ToString();
    }
}