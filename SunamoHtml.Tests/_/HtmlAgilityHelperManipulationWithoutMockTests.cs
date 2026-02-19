// variables names: ok
using HtmlAgilityPack;
using SunamoHtml;

namespace sunamo.Tests.Html;

public class HtmlAgilityHelperManipulationWithoutMockTests : HtmlHelperBaseTests
{
    public HtmlAgilityHelperManipulationWithoutMockTests()
    {
        //GetHtml
    }
    bool noRecursive = true;

    //[Fact]
    public void NodesTest()
    {
        List<HtmlNode> nodes = null!;

        noRecursive = true;

        //Recursively
        nodes = HtmlAgilityHelper.Nodes(DocumentNode, true, HtmlTags.Span);
        Assert.Equal(5, nodes.Count);
        // Non-recursively 
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.Nodes(BodyNode, false, HtmlTags.Span);
            Assert.Equal(2, nodes.Count);
        }

        // Recursively
        nodes = HtmlAgilityHelper.Nodes(BodyNode, true, "*");
        Assert.Equal(10, nodes.Count);
        // Non-recursively 
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.Nodes(BodyNode, false, "*");
            Assert.Equal(7, nodes.Count);
        }

    }

    //[Fact]
    public void NodesWithAttrTest()
    {
        List<HtmlNode> nodes = null!;

        // Recursively
        nodes = HtmlAgilityHelper.NodesWithAttr(DocumentNode, true, HtmlTags.Span, HtmlAttrs.C, CssClassC);
        Assert.Equal(3, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWithAttr(BodyNode, false, HtmlTags.Span, HtmlAttrs.C, CssClassC);
            Assert.Single(nodes);
        }

        // Recursively
        nodes = HtmlAgilityHelper.NodesWithAttr(BodyNode, true, "*", HtmlAttrs.C, CssClassC);
        Assert.Equal(4, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWithAttr(BodyNode, false, "*", HtmlAttrs.C, CssClassC);
            Assert.Equal(2, nodes.Count);
        }

        // Recursively
        nodes = HtmlAgilityHelper.NodesWithAttr(BodyNode, true, "*", HtmlAttrs.C, CssClassA, true);
        Assert.Equal(3, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWithAttr(BodyNode, false, "*", HtmlAttrs.C, CssClassC, true);
            Assert.Equal(2, nodes.Count);
        }

        // Recursively
        nodes = HtmlAgilityHelper.NodesWithAttr(BodyNode, true, "*", HtmlAttrs.C, "*", true);
        Assert.Equal(10, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWithAttr(BodyNode, false, "*", HtmlAttrs.C, "*", true);
            Assert.Equal(7, nodes.Count);
        }
    }

    //[Fact]
    public void NodesWhichContainsInAttrTest()
    {
        IList<HtmlNode> nodes = null!;

        // Recursively
        nodes = HtmlAgilityHelper.NodesWhichContainsInAttr(DocumentNode, true, HtmlTags.Span, HtmlAttrs.C, CssClassA);
        Assert.Equal(2, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWhichContainsInAttr(BodyNode, false, HtmlTags.Span, HtmlAttrs.C, CssClassA);
            Assert.Single(nodes);
        }

        // Recursively
        nodes = HtmlAgilityHelper.NodesWhichContainsInAttr(DocumentNode, true, "*", HtmlAttrs.C, CssClassA);
        Assert.Equal(3, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWhichContainsInAttr(BodyNode, false, "*", HtmlAttrs.C, CssClassA);
            Assert.Equal(2, nodes.Count);
        }
    }

    //[Fact]
    public void NodeTest()
    {
        HtmlNode node = null!;

        // Recursively
        node = HtmlAgilityHelper.Node(DocumentNode, true, HtmlTags.Span);
        Assert.NotNull(node);

        node = HtmlAgilityHelper.Node(DocumentNode, true, HtmlTags.Img);
        Assert.Null(node);

        node = HtmlAgilityHelper.Node(DocumentNode, true, "*");
        Assert.NotNull(node);

        // Non-recursively 
        if (noRecursive)
        {
            node = HtmlAgilityHelper.Node(BodyNode, false, HtmlTags.Span);
            Assert.NotNull(node);

            node = HtmlAgilityHelper.Node(BodyNode, false, HtmlTags.Img);
            Assert.Null(node);

            node = HtmlAgilityHelper.Node(BodyNode, false, "*");
            Assert.NotNull(node);
        }
    }

    //[Fact]
    public void NodeWithAttrTest()
    {
        HtmlNode node = null!;

        // Recursively
        node = HtmlAgilityHelper.NodeWithAttr(DocumentNode, true, HtmlTags.Span, HtmlAttrs.C, CssClassC);
        Assert.NotNull(node);

        // exists but "a b"
        node = HtmlAgilityHelper.NodeWithAttr(DocumentNode, true, HtmlTags.Span, HtmlAttrs.C, CssClassA);
        Assert.Null(node);

        node = HtmlAgilityHelper.NodeWithAttr(DocumentNode, true, "*", HtmlAttrs.CAttr, CssClassA);
        Assert.Null(node);

        node = HtmlAgilityHelper.NodeWithAttr(DocumentNode, true, HtmlTags.Img, HtmlAttrs.CAttr, CssClassA);
        Assert.Null(node);

        // Non-recursively
        if (noRecursive)
        {
            node = HtmlAgilityHelper.NodeWithAttr(BodyNode, false, HtmlTags.Span, HtmlAttrs.C, CssClassC);
            Assert.NotNull(node);

            // exists but "a b"
            node = HtmlAgilityHelper.NodeWithAttr(BodyNode, false, HtmlTags.Span, HtmlAttrs.C, CssClassA);
            Assert.Null(node);

            node = HtmlAgilityHelper.NodeWithAttr(BodyNode, false, "*", HtmlAttrs.C, CssClassC);
            Assert.NotNull(node);
        }
    }

    //[Fact]
    public void ReplacePlainUriForAnchors()
    {
        string actual = "I tried https://www.nuget.org/p/ because <a href=\"http://jepsano.net/\">http://jepsano.net/</a> another text";
        string excepted = "I tried <a href=\"https://www.nuget.org/p/\">https://www.nuget.org/p/</a> because <a href=\"http://jepsano.net/\">http://jepsano.net/</a> another text";

        string result = HtmlAgilityHelper.ReplacePlainUriForAnchors(actual);
        Assert.Equal(excepted, result);
    }
}
