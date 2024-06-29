namespace SunamoHtml;
internal class BTS
{
    internal static int TryParseInt(string entry, int def)
    {
        return TryParseInt(entry, def, false);
    }

    internal static int TryParseInt(string entry, int def, bool throwEx)
    {
        int lastInt = 0;
        if (int.TryParse(entry, out lastInt))
        {
            return lastInt;
        }
        else
        {
            if (throwEx)
            {
                ThrowEx.NotInt(entry, null);
            }
        }
        return def;
    }
}
