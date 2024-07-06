namespace SunamoHtml;

public class XmlAgilityDocument
{
    public HtmlDocument hd = null;
    public string path = null;

    public
#if ASYNC
    async Task
#else
void
#endif
    Load(string file)
    {
        path = file;
        hd = HtmlAgilityHelper.CreateHtmlDocument();
        var c =
#if ASYNC
        await
#endif
        File.ReadAllTextAsync(file);
        c = XH.RemoveXmlDeclaration(c);
        hd.LoadHtml(c);
    }

    public
#if ASYNC
    async Task
#else
void
#endif
    Save()
    {

#if ASYNC
        await
#endif
        File.WriteAllTextAsync(path, XmlTemplates.xml + "\r\n" + hd.DocumentNode.OuterHtml);
    }
}
