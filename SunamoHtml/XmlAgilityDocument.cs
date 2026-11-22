namespace SunamoHtml;

/// <summary>
/// Represents the xml agility document type.
/// </summary>
public class XmlAgilityDocument
{
    /// <summary>
    /// Gets or sets the html document.
    /// </summary>
    public HtmlDocument HtmlDocument { get; set; } = null!;

    /// <summary>
    /// Gets or sets the path.
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Load.
    /// </summary>
    /// <param name="filePath">The file path.</param>
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

    /// <summary>
    /// Save.
    /// </summary>
    public
        async Task
        Save()
    {
        await
            FileAsync.WriteAllTextAsync(Path, XmlTemplates.Xml + "\r\n" + HtmlDocument.DocumentNode.OuterHtml).ConfigureAwait(false);
    }
}
