namespace SunamoHtml._sunamo;
internal class SHGetLines
{
    internal static List<string?> GetLines(string text)
    {
        return textv.Split(new string[] { v.Contains("\r\n") ? "\r\n" : "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
