namespace SunamoHtml._sunamo.SunamoValues.Constants;

internal class HtmlEndingTags
{
    internal const string B = "</b>";
    internal const string I = "</i>";
    internal const string S = "</s>";
    internal static string Get(string value) => "</" + value + ">";
}