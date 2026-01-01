namespace SunamoHtml;

/// <summary>
/// Represents an XML document using HtmlAgilityPack for manipulation.
/// </summary>
public class XmlAgilityDocument
{
    /// <summary>
    /// Gets or sets the HTML document instance.
    /// </summary>
    public HtmlDocument HtmlDocument { get; set; } = null!;

    /// <summary>
    /// Gets or sets the file path of the document.
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// Loads an XML/HTML document from the specified file path.
    /// </summary>
    /// <param name="filePath">The path to the file to load.</param>
    public
#if ASYNC
        async Task
#else
void
#endif
        Load(string filePath)
    {
        Path = filePath;
        HtmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        var htmlContent =
#if ASYNC
            await
#endif
                File.ReadAllTextAsync(filePath);
        htmlContent = XH.RemoveXmlDeclaration(htmlContent);
        HtmlDocument.LoadHtml(htmlContent);
    }

    /// <summary>
    /// Saves the current document to the file path.
    /// </summary>
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
            File.WriteAllTextAsync(Path, XmlTemplates.xml + "\r\n" + HtmlDocument.DocumentNode.OuterHtml);
    }
}