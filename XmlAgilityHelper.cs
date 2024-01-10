namespace SunamoHtml;

/// <summary>
/// With ci I have already tried
/// Every method which I need from HtmlAgilityHelper => make same header and call from HtmlAgilityHelper
/// </summary>
public class XmlAgilityHelper // : HtmlAgilityHelper - NO, see class comment
{


    public static HtmlNode Node(HtmlNode node, bool recursive, string tag)
    {
        return HtmlAgilityHelper.Node(node, recursive, tag);
    }

    public static List<HtmlNode> Nodes(HtmlNode node, bool recursive, string tag)
    {
        return HtmlAgilityHelper.Nodes(node, recursive, tag);
    }
}
