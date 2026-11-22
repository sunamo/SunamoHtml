namespace SunamoHtml._sunamo.SunamoUri;

internal class UH
{

    internal static string AppendHttpIfNotExists(string url)
    {
        var result = url;
        if (!url.StartsWith("http", StringComparison.Ordinal)) result = "http://" + url;

        return result;
    }
}