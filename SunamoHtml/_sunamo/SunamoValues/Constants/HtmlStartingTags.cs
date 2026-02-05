namespace SunamoHtml._sunamo.SunamoValues.Constants;

/// <summary>
/// HTML starting (opening) tag constants.
/// </summary>
internal class HtmlStartingTags
{
    internal const string B = "<b>";
    internal const string I = "<i>";
    internal const string S = "<s>";
    private static Type Type = typeof(HtmlStartingTags);

    /// <summary>
    /// Gets the opening tag for the specified tag name.
    /// </summary>
    /// <param name="value">The tag name.</param>
    /// <returns>The opening tag string.</returns>
    internal static string Get(string value)
    {
        return "<" + value + ">";
    }
}