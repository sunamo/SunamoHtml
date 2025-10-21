// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoHtml._sunamo.SunamoUri;

//namespace SunamoHtml;

internal class UH
{
    //internal static Func<string, string> AppendHttpIfNotExists;

    internal static string AppendHttpIfNotExists(string p)
    {
        var p2 = p;
        if (!p.StartsWith("http")) p2 = "http://" + p;

        return p2;
    }
}