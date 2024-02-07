namespace SunamoHtml._sunamo;
internal class SHGetLines
{
    internal static List<string?> GetLines(string text)
    {
        return text.Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
