// variables names: ok
using HtmlAgilityPack;
using SunamoHtml;
using SunamoHtml.Html;

namespace sunamo.Tests.Helpers.Html;

/// <summary>
/// Unit test for right skipping text nodes
/// </summary>
public class HtmlHelperManipulationWithoutMockGoogleFormTests
{
    const string testFile = @"E:\vs\Projects\PlatformIndependentNuGetPackages.Tests\sunamo.Tests\HtmlHelperGoogleFormTestPage.html";
    HtmlNode hd = null!;
    const string mainQuestionTitle = "ss-q-title";
    const string additionalQuestionTitle = "ss-q-help ss-secondary-text";
    const string possibleAnswerTitle = "ss-choice-label";
    const string cssClassMainQuestions = "mainQuestions";
    const string cssClassAdditionalQuestion = "additionalQuestion";
    public HtmlHelperManipulationWithoutMockGoogleFormTests()
    {
        GetHtmlDocumentTestFile();
    }
    void GetHtmlDocumentTestFile()
    {
        HtmlDocument hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.Load(testFile);
        this.hd = hd.DocumentNode;
    }
    //[Fact]
    public void ReturnAllTags()
    {
        var listAllQuestions = HtmlHelper.ReturnTagsWithAttrRek(hd, "ol", "class", "ss-question-list");
        var nodes = HtmlHelper.ReturnAllTags(listAllQuestions[0], "div");
        Assert.Equal(17, nodes.Count);
    }
}