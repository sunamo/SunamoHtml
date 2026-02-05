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
        //t.PairsDdDtTest2();
        await t.CreateHtmlDocumentTest();
        //t.Test1();
        //await t.NodesWithAttrTest();
        //HtmlAssistantTests t = new HtmlAssistantTests();
        //t.InnerTextDecodeTrimTest();
        //Console.WriteLine("Finished");
        Console.WriteLine();
    }
}