namespace SunamoHtml._sunamo.SunamoValues.Values;

internal sealed class ConstsAspx
{
    internal const string StartAspxComment = "<%--";
    internal const string EndAspxComment = "--%>";
    internal const string StartHtmlComment = "<!--";
    internal const string EndHtmlComment = "-->";

    internal static readonly List<string> All = new List<string>([StartAspxComment, EndAspxComment, StartHtmlComment,
        EndHtmlComment, ">", "<"]);
}