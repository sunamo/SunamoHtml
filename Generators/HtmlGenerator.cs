
namespace SunamoHtml;
public class HtmlGenerator : XmlGeneratorHtml
{
    public void WriteBr()
    {
        WriteNonPairTag("br");
    }
}
