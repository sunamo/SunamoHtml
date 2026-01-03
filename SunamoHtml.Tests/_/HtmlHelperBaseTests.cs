using HtmlAgilityPack;
using SunamoHtml;
using SunamoHtml.Html;

namespace sunamo.Tests.Html;

public class HtmlHelperBaseTests
{
    protected HtmlNode documentNode = null!;
    protected HtmlNode bodyNode = null!;
    public const string testFile = @"E:\vs\Projects\PlatformIndependentNuGetPackages.Tests\sunamo.Tests\HtmlHelperTestPage.html";
    public readonly string cssClassC = "c";
    public readonly string cssClassA = "a";
    public readonly string cssClassHello = "hello";
    public readonly string divFirstId = "first";
    public HtmlHelperBaseTests()
    {
        GetHtmlDocumentTestFile();
    }
    void GetHtmlDocumentTestFile()
    {
        HtmlDocument hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.Load(testFile);
        this.documentNode = hd.DocumentNode;
        this.bodyNode = HtmlHelper.ReturnTagRek(documentNode, "body");
    }
}