using HtmlAgilityPack;
using SunamoHtml.Html;

namespace SunamoHtml.Tests;

public class HtmlAssistantTests
{
    private static HtmlNode Load(string html)
    {
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        htmlDocument.LoadHtml(html);
        return htmlDocument.DocumentNode;
    }

    [Fact]
    public void InnerTextDecodeTrimTest()
    {
        var data = HtmlAssistant.InnerTextDecodeTrim("chaty/chalupy 66 m² s pozemkem 1 489 m²");
    }

    [Fact]
    public void GetValueOfAttribute_EmptyAttribute_ReturnsEmptyString()
    {
        var input = Load("<input readonly value=\"\" name=\"a\">").ChildNodes[0];

        Assert.Equal(string.Empty, HtmlAssistant.GetValueOfAttribute("readonly", input));
        Assert.Equal(string.Empty, HtmlAssistant.GetValueOfAttribute("value", input));
        Assert.Equal(string.Empty, HtmlAssistant.GetValueOfAttribute("missing", input));
        Assert.Equal("a", HtmlAssistant.GetValueOfAttribute("name", input));
    }

    [Fact]
    public void Nodes_NullNode_ReturnsEmptyInsteadOfThrowing()
    {
        Assert.Empty(HtmlAgilityHelper.Nodes(null, true, "li"));
        Assert.Null(HtmlAgilityHelper.Node(null, true, "li"));
        Assert.Empty(HtmlAgilityHelper.NodesWithAttr(null, true, "a", "class", "x"));
        Assert.Null(HtmlAgilityHelper.NodeWithAttr(null, true, "a", "class", "x"));
        Assert.Empty(HtmlAgilityHelper.NodesWithAttrToken(null, true, "a", "class", "x"));
        Assert.Null(HtmlAgilityHelper.NodeWithAttrToken(null, true, "a", "class", "x"));
        Assert.Equal(string.Empty, HtmlAssistant.InnerText(null, true, "p", "itemprop", "description"));
        Assert.Equal(string.Empty, HtmlAssistant.InnerTextWithAttrToken(null, true, "p", "itemprop", "description"));
    }

    [Fact]
    public void NodesWithAttrToken_MatchesWholeValueOrOneToken()
    {
        var root = Load("<div><a class=\"toDetail big\" href=\"/1\">1</a><a class=\"toDetail\" href=\"/2\">2</a><a class=\"toDetailX\" href=\"/3\">3</a><a href=\"/4\">4</a></div>");

        var token = HtmlAgilityHelper.NodesWithAttrToken(root, true, "a", "class", "toDetail");
        Assert.Equal(new[] { "/1", "/2" }, token.Select(node => HtmlAssistant.GetValueOfAttribute("href", node)));

        var whole = HtmlAgilityHelper.NodesWithAttrToken(root, true, "a", "class", "toDetail big");
        Assert.Single(whole);

        // The exact comparison keeps its behaviour: only the node whose whole attribute equals the value.
        var exact = HtmlAgilityHelper.NodesWithAttr(root, true, "a", "class", "toDetail");
        Assert.Single(exact);
    }

    [Fact]
    public void InnerTextWithAttrToken_TrimsAndDecodes()
    {
        var root = Load("<div><p itemprop=\"description text\">  Tom &amp; Jerry&nbsp;s.r.o. </p></div>");

        Assert.Equal("Tom & Jerry s.r.o.", HtmlAssistant.InnerTextWithAttrToken(root, true, "p", "itemprop", "description"));
        Assert.Equal(string.Empty, HtmlAssistant.InnerTextWithAttrToken(root, true, "p", "itemprop", "missing"));
    }
}
