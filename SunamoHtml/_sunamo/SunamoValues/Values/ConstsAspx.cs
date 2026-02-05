namespace SunamoHtml._sunamo.SunamoValues.Values;

internal class ConstsAspx
{
    internal static readonly string StartAspxComment = "<%--";
    internal static readonly string EndAspxComment = "--%>";
    internal static readonly string StartHtmlComment = "<!--";
    internal static readonly string EndHtmlComment = "-->";

    internal static readonly List<string> All = new List<string>([StartAspxComment, EndAspxComment, StartHtmlComment,
        EndHtmlComment, ">", "<"]);
}