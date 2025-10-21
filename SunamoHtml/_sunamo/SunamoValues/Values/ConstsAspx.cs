// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoHtml._sunamo.SunamoValues.Values;

internal class ConstsAspx
{
    internal static readonly string startAspxComment = "<%--";
    internal static readonly string endAspxComment = "--%>";
    internal static readonly string startHtmlComment = "<!--";
    internal static readonly string endHtmlComment = "-->";

    internal static readonly List<string> all = new List<string>([startAspxComment, endAspxComment, startHtmlComment,
        endHtmlComment, ">", "<"]);
}