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
        var a = @"D:\_Test\sunamo\sunamo\Html\HtmlTableParserTests\a.html";
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(
#if ASYNC
    await
#endif
 File.ReadAllTextAsync(a));
        var table = HtmlAgilityHelper.Node(hd.DocumentNode, true, "table");
        HtmlTableParser p = new HtmlTableParser(table, false);
        var v = p.ColumnValues("1", false, false);
        int i = 0;
    }
}
