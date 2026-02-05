namespace SunamoHtml._sunamo.SunamoUri;

internal class UH
{
    //internal static Func<string, string> AppendHttpIfNotExists;

    internal static string AppendHttpIfNotExists(string url)
    {
        var result = url;
        if (!url.StartsWith("http")) result = "http://" + url;

        return result;
    }
}