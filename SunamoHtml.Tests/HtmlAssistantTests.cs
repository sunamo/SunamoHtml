// variables names: ok

using SunamoHtml.Html;

namespace SunamoHtml.Tests;

public class HtmlAssistantTests
{
    [Fact]
    public void InnerTextDecodeTrimTest()
    {
        var data = HtmlAssistant.InnerTextDecodeTrim("chaty/chalupy 66 m² s pozemkem 1 489 m²");
    }
}