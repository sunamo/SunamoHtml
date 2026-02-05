namespace SunamoHtml._sunamo.SunamoBts;

/// <summary>
/// EN: Basic Type String parsing utilities.
/// CZ: Základní utility pro parsování typů ze stringů.
/// </summary>
internal class BTS
{
    /// <summary>
    /// EN: Tries to parse a string to int, returns default value if parsing fails.
    /// CZ: Zkusí naparsovat string na int, vrátí výchozí hodnotu pokud parsování selže.
    /// </summary>
    /// <param name="text">The text to parse.</param>
    /// <param name="defaultValue">The default value to return if parsing fails.</param>
    /// <returns>Parsed integer or default value.</returns>
    internal static int TryParseInt(string text, int defaultValue)
    {
        return TryParseInt(text, defaultValue, false);
    }

    /// <summary>
    /// EN: Tries to parse a string to int, optionally throws exception on failure.
    /// CZ: Zkusí naparsovat string na int, volitelně vyhodí výjimku při selhání.
    /// </summary>
    /// <param name="text">The text to parse.</param>
    /// <param name="defaultValue">The default value to return if parsing fails.</param>
    /// <param name="isThrowEx">Whether to throw exception on parsing failure.</param>
    /// <returns>Parsed integer or default value.</returns>
    internal static int TryParseInt(string text, int defaultValue, bool isThrowEx)
    {
        var parsedValue = 0;
        if (int.TryParse(text, out parsedValue))
        {
            return parsedValue;
        }

        if (isThrowEx) ThrowEx.NotInt(text, null);
        return defaultValue;
    }
}