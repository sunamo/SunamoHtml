namespace
#if SunamoWikipedia
SunamoWikipedia
#else
SunamoHtml
#endif
;
public class HtmlGenerator : XmlGenerator
{
    public void WriteBr()
    {
        WriteNonPairTag("br");
    }
}