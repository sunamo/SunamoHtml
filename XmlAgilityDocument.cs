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
        TF.ReadAllText(file);
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
        TF.WriteAllText(path, XmlTemplates.xml + Consts.nl2 + hd.DocumentNode.OuterHtml);
    }
}
