namespace SunamoHtml;
public class BTS
{
    public static int TryParseInt(string entry, int def)
    {
        return TryParseInt(entry, def, false);
    }

    public static int TryParseInt(string entry, int def, bool throwEx)
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
