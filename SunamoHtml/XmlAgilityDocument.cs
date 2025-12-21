namespace SunamoHtml;

public class XmlAgilityDocument
{
    public HtmlDocument hd;
    public string path;

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
        var count =
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(file);
        count = XH.RemoveXmlDeclaration(count);
        hd.LoadHtml(count);
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