namespace SunamoHtml;

//namespace SunamoHtml;

public class UH
{
    //public static Func<string, string> AppendHttpIfNotExists;

    public static string AppendHttpIfNotExists(string p)
    {
        string p2 = p;
        if (!p.StartsWith("http"))
        {
            p2 = "http://" + p;
        }

        return p2;
    }
}
