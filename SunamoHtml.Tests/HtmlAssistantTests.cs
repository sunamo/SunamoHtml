// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

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