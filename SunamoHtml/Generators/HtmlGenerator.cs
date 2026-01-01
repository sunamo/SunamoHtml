namespace SunamoHtml.Generators;

/// <summary>
/// HTML generator for creating HTML markup.
/// </summary>
public class HtmlGenerator : XmlGeneratorHtml
{
    /// <summary>
    /// Writes an HTML break tag.
    /// </summary>
    public void WriteBr()
    {
        WriteNonPairTag("br");
    }
}