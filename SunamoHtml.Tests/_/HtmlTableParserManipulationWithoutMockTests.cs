// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

using SunamoHtml;
using SunamoHtml.Html;

public class HtmlTableParserTests
{
    //[Fact]
    public
#if ASYNC
    async Task
#else
    void
#endif
 HtmlTableParserTest()
    {
        var argument = @"D:\_Test\sunamo\sunamo\Html\HtmlTableParserTests\argument.html";
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(
#if ASYNC
    await
#endif
 File.ReadAllTextAsync(argument));
        var table = HtmlAgilityHelper.Node(hd.DocumentNode, true, "table");
        HtmlTableParser p = new HtmlTableParser(table, false);
        var value = p.ColumnValues("1", false, false);
        int i = 0;
    }
}
