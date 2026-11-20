namespace SunamoHtml;

// Wrapper class for HtmlAgilityHelper methods for XML manipulation.
// Every method which is needed from HtmlAgilityHelper should have the same header and call from HtmlAgilityHelper.
// Note: This class does not inherit from HtmlAgilityHelper by design.
/// <summary>
/// Represents the xml agility helper type.
/// </summary>
public static class XmlAgilityHelper // : HtmlAgilityHelper - NO, see class comment
{
    /// <summary>
    /// Node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="isRecursive">The is recursive.</param>
    /// <param name="tag">The tag.</param>
    /// <returns>The node result.</returns>
    public static HtmlNode? Node(HtmlNode node, bool isRecursive, string tag)
        => HtmlAgilityHelper.Node(node, isRecursive, tag);

    /// <summary>
    /// Nodes.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="isRecursive">The is recursive.</param>
    /// <param name="tag">The tag.</param>
    /// <returns>The nodes result.</returns>
    public static IList<HtmlNode> Nodes(HtmlNode node, bool isRecursive, string tag)
        => HtmlAgilityHelper.Nodes(node, isRecursive, tag);
}
