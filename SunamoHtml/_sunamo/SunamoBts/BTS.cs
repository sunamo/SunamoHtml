namespace SunamoHtml._sunamo.SunamoBts;

internal class BTS
{
    internal static int TryParseInt(string text, int defaultValue)
    {
        return TryParseInt(text, defaultValue, false);
    }

    internal static int TryParseInt(string text, int defaultValue, bool isThrowEx)
    {
        if (int.TryParse(text, out var parsedValue))
        {
            return parsedValue;
        }

        if (isThrowEx) ThrowEx.NotInt(text, null);
        return defaultValue;
    }
}
