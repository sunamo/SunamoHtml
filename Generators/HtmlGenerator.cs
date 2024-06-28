
namespace SunamoHtml;
public class HtmlGenerator : XmlGenerator
{
    public void WriteBr()
    {
        WriteNonPairTag("br");
    }
}
