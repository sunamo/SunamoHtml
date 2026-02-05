namespace SunamoHtml._sunamo.SunamoValues.Constants;

/// <summary>
/// HTML ending (closing) tag constants.
/// </summary>
internal class HtmlEndingTags
{
    internal const string B = "</b>";
    internal const string I = "</i>";
    internal const string S = "</s>";
    private static Type Type = typeof(HtmlEndingTags);

    /// <summary>
    /// Gets the closing tag for the specified tag name.
    /// </summary>
    /// <param name="value">The tag name.</param>
    /// <returns>The closing tag string.</returns>
    internal static string Get(string value)
    {
        return "</" + value + ">";
    }
}