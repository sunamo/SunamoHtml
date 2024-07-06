namespace SunamoHtml.Generators;
public class HtmlGenerator : XmlGeneratorHtml
{
    public void WriteBr()
    {
        WriteNonPairTag("br");
    }
}
