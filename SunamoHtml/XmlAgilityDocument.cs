namespace SunamoHtml;

public class XmlAgilityDocument
{
    public HtmlDocument HtmlDocument { get; set; } = null!;

    public string Path { get; set; } = string.Empty;

    public
        async Task
        Load(string filePath)
    {
        Path = filePath;
        HtmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        var htmlContent =
            await FileAsync.ReadAllTextAsync(filePath).ConfigureAwait(false);
        htmlContent = XH.RemoveXmlDeclaration(htmlContent);
        HtmlDocument.LoadHtml(htmlContent);
    }

    public
        async Task
        Save()
    {
        await
            FileAsync.WriteAllTextAsync(Path, XmlTemplates.Xml + "\r\n" + HtmlDocument.DocumentNode.OuterHtml).ConfigureAwait(false);
    }
}
