namespace SunamoHtml;

// Wrapper class for HtmlAgilityHelper methods for XML manipulation.
// Every method which is needed from HtmlAgilityHelper should have the same header and call from HtmlAgilityHelper.
// Note: This class does not inherit from HtmlAgilityHelper by design.
public static class XmlAgilityHelper // : HtmlAgilityHelper - NO, see class comment
{
    public static HtmlNode? Node(HtmlNode node, bool isRecursive, string tag)
        => HtmlAgilityHelper.Node(node, isRecursive, tag);

    public static IList<HtmlNode> Nodes(HtmlNode node, bool isRecursive, string tag)
        => HtmlAgilityHelper.Nodes(node, isRecursive, tag);
}
