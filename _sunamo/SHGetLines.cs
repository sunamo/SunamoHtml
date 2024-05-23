namespace SunamoHtml;
public class SHGetLines
{
    public static List<string?> GetLines(string text)
    {
        return text.Split(new string[] { text.Contains("\r\n") ? "\r\n" : "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
