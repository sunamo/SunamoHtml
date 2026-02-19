namespace SunamoHtml;

/// <summary>
/// Wrapper class for HtmlAgilityHelper methods for XML manipulation.
/// Every method which is needed from HtmlAgilityHelper should have the same header and call from HtmlAgilityHelper.
/// Note: This class does not inherit from HtmlAgilityHelper by design.
/// </summary>
public static class XmlAgilityHelper // : HtmlAgilityHelper - NO, see class comment
{
    /// <summary>
    /// Finds a single HTML node with the specified tag.
    /// </summary>
    /// <param name="node">The parent node to search within.</param>
    /// <param name="isRecursive">Whether to search recursively through child nodes.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <returns>The found HTML node, or null if not found.</returns>
    public static HtmlNode? Node(HtmlNode node, bool isRecursive, string tag)
    {
        return HtmlAgilityHelper.Node(node, isRecursive, tag);
    }

    /// <summary>
    /// Finds all HTML nodes with the specified tag.
    /// </summary>
    /// <param name="node">The parent node to search within.</param>
    /// <param name="isRecursive">Whether to search recursively through child nodes.</param>
    /// <param name="tag">The tag name to search for.</param>
    /// <returns>A list of found HTML nodes.</returns>
    public static IList<HtmlNode> Nodes(HtmlNode node, bool isRecursive, string tag)
    {
        return HtmlAgilityHelper.Nodes(node, isRecursive, tag);
    }
}
