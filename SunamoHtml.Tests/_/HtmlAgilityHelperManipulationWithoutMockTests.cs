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
        nodes = HtmlAgilityHelper.Nodes(documentNode, true, HtmlTags.span);
        Assert.Equal(5, nodes.Count);
        // Non-recursively 
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.Nodes(bodyNode, false, HtmlTags.span);
            Assert.Equal(2, nodes.Count);
        }




        // Recursively
        nodes = HtmlAgilityHelper.Nodes(bodyNode, true, "*");
        Assert.Equal(10, nodes.Count);
        // Non-recursively 
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.Nodes(bodyNode, false, "*");
            Assert.Equal(7, nodes.Count);
        }

    }

    //[Fact]
    public void NodesWithAttrTest()
    {
        List<HtmlNode> nodes = null!;

        // Recursively
        nodes = HtmlAgilityHelper.NodesWithAttr(documentNode, true, HtmlTags.span, HtmlAttrs.C, cssClassC);
        Assert.Equal(3, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWithAttr(bodyNode, false, HtmlTags.span, HtmlAttrs.C, cssClassC);
            Assert.Single(nodes);
        }

        // Recursively
        nodes = HtmlAgilityHelper.NodesWithAttr(bodyNode, true, "*", HtmlAttrs.C, cssClassC);
        Assert.Equal(4, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWithAttr(bodyNode, false, "*", HtmlAttrs.C, cssClassC);
            Assert.Equal(2, nodes.Count);
        }

        // Recursively
        nodes = HtmlAgilityHelper.NodesWithAttr(bodyNode, true, "*", HtmlAttrs.C, cssClassA, true);
        Assert.Equal(3, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWithAttr(bodyNode, false, "*", HtmlAttrs.C, cssClassC, true);
            Assert.Equal(2, nodes.Count);
        }

        // Recursively
        nodes = HtmlAgilityHelper.NodesWithAttr(bodyNode, true, "*", HtmlAttrs.C, "*", true);
        Assert.Equal(10, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWithAttr(bodyNode, false, "*", HtmlAttrs.C, "*", true);
            Assert.Equal(7, nodes.Count);
        }
    }

    //[Fact]
    public void NodesWhichContainsInAttrTest()
    {
        List<HtmlNode> nodes = null!;

        // Recursively
        nodes = HtmlAgilityHelper.NodesWhichContainsInAttr(documentNode, true, HtmlTags.span, HtmlAttrs.C, cssClassA);
        Assert.Equal(2, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWhichContainsInAttr(bodyNode, false, HtmlTags.span, HtmlAttrs.C, cssClassA);
            Assert.Single(nodes);
        }

        // Recursively
        nodes = HtmlAgilityHelper.NodesWhichContainsInAttr(documentNode, true, "*", HtmlAttrs.C, cssClassA);
        Assert.Equal(3, nodes.Count);
        // Non-recursively
        if (noRecursive)
        {
            nodes = HtmlAgilityHelper.NodesWhichContainsInAttr(bodyNode, false, "*", HtmlAttrs.C, cssClassA);
            Assert.Equal(2, nodes.Count);
        }
    }

    //[Fact]
    public void NodeTest()
    {
        HtmlNode node = null!;

        // Recursively
        node = HtmlAgilityHelper.Node(documentNode, true, HtmlTags.span);
        Assert.NotNull(node);

        node = HtmlAgilityHelper.Node(documentNode, true, HtmlTags.img);
        Assert.Null(node);

        node = HtmlAgilityHelper.Node(documentNode, true, "*");
        Assert.NotNull(node);

        // Non-recursively 
        if (noRecursive)
        {
            node = HtmlAgilityHelper.Node(bodyNode, false, HtmlTags.span);
            Assert.NotNull(node);

            node = HtmlAgilityHelper.Node(bodyNode, false, HtmlTags.img);
            Assert.Null(node);

            node = HtmlAgilityHelper.Node(bodyNode, false, "*");
            Assert.NotNull(node);
        }
    }

    //[Fact]
    public void NodeWithAttrTest()
    {
        HtmlNode node = null!;

        // Recursively
        node = HtmlAgilityHelper.NodeWithAttr(documentNode, true, HtmlTags.span, HtmlAttrs.C, cssClassC);
        Assert.NotNull(node);

        // exists but "a b"
        node = HtmlAgilityHelper.NodeWithAttr(documentNode, true, HtmlTags.span, HtmlAttrs.C, cssClassA);
        Assert.Null(node);

        node = HtmlAgilityHelper.NodeWithAttr(documentNode, true, "*", HtmlAttrs.CAttr, cssClassA);
        Assert.Null(node);

        node = HtmlAgilityHelper.NodeWithAttr(documentNode, true, HtmlTags.img, HtmlAttrs.CAttr, cssClassA);
        Assert.Null(node);

        // Non-recursively
        if (noRecursive)
        {
            node = HtmlAgilityHelper.NodeWithAttr(bodyNode, false, HtmlTags.span, HtmlAttrs.C, cssClassC);
            Assert.NotNull(node);

            // exists but "a b"
            node = HtmlAgilityHelper.NodeWithAttr(bodyNode, false, HtmlTags.span, HtmlAttrs.C, cssClassA);
            Assert.Null(node);

            node = HtmlAgilityHelper.NodeWithAttr(bodyNode, false, "*", HtmlAttrs.C, cssClassC);
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
