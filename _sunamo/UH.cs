namespace SunamoHtml;

//namespace SunamoHtml._sunamo;

internal class UH
{
    //internal static Func<string, string> AppendHttpIfNotExists;

    internal static string AppendHttpIfNotExists(string p)
    {
        string p2 = p;
        if (!p.StartsWith("http"))
        {
            p2 = "http://" + p;
        }

        return p2;
    }
}
