// variables names: ok
using HtmlAgilityPack;
using SunamoHtml;
using SunamoHtml.Html;

namespace sunamo.Tests.Html;

public class HtmlHelperBaseTests
{
    protected HtmlNode DocumentNode = null!;
    protected HtmlNode BodyNode = null!;
    public const string TestFile = @"E:\vs\Projects\PlatformIndependentNuGetPackages.Tests\sunamo.Tests\HtmlHelperTestPage.html";
    public readonly string CssClassC = "c";
    public readonly string CssClassA = "a";
    public readonly string CssClassHello = "hello";
    public readonly string DivFirstId = "first";
    public HtmlHelperBaseTests()
    {
        GetHtmlDocumentTestFile();
    }
    void GetHtmlDocumentTestFile()
    {
        HtmlDocument hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.Load(TestFile);
        this.DocumentNode = hd.DocumentNode;
        this.BodyNode = HtmlHelper.ReturnTagRek(DocumentNode, "body");
    }
}