namespace SunamoHtml.Generators;

/// <summary>
/// Represents the html generator type.
/// </summary>
public class HtmlGenerator : XmlGeneratorHtml
{
    /// <summary>
    /// Write br.
    /// </summary>
    public void WriteBr()
    {
        WriteNonPairTag("br");
    }
}
