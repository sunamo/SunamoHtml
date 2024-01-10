namespace SunamoHtml.Generators;

public class HtmlGenerator : XmlGenerator
{
    public void WriteBr()
    {
        WriteNonPairTag("br");
    }
}
