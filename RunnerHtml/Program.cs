// variables names: ok
using SunamoHtml.Tests;

namespace RunnerHtml;

internal class Program
{
    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }
    static async Task MainAsync()
    {
        HtmlAgilityHelperTests t = new HtmlAgilityHelperTests();
        await t.CreateHtmlDocumentTest();
        Console.WriteLine();
    }
}