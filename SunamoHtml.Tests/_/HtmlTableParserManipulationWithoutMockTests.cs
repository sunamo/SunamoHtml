// variables names: ok

using SunamoHtml;
using SunamoHtml.Html;

public class HtmlTableParserTests
{
    //[Fact]
    public
    async Task
 HtmlTableParserTest()
    {
        var argument = @"D:\_Test\sunamo\sunamo\Html\HtmlTableParserTests\argument.html";
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        htmlDocument.LoadHtml(
    await
 File.ReadAllTextAsync(argument));
        var table = HtmlAgilityHelper.Node(htmlDocument.DocumentNode, true, "table");
        HtmlTableParser parser = new HtmlTableParser(table, false);
        var value = parser.ColumnValues("1", false, false);
    }
}
