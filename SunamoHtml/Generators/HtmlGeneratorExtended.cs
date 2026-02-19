namespace SunamoHtml.Generators;

/// <summary>
/// EN: Extended HTML generator with detail display methods for labels, anchors, and mailto links.
/// CZ: Rozšířený HTML generátor s metodami pro zobrazení detailů s labely, odkazy a mailto linky.
/// </summary>
public class HtmlGeneratorExtended : HtmlGenerator
{
    /// <summary>
    /// Writes a detail line with label and anchor link.
    /// Only outputs if the name parameter is not empty.
    /// </summary>
    /// <param name="label">The label text to display before the value.</param>
    /// <param name="uri">The URI for the anchor link. If empty, displays plain text.</param>
    /// <param name="name">The name/text to display or link to.</param>
    public void DetailAnchor(string label, System.Uri uri, string name)
    {
        DetailAnchor(label, uri?.ToString() ?? string.Empty, name);
    }

    /// <summary>
    /// Writes a detail line with label and anchor link using a URL string.
    /// Only outputs if the name parameter is not empty.
    /// </summary>
    /// <param name="label">The label text to display before the value.</param>
    /// <param name="url">The URL string for the anchor link. If empty, displays plain text.</param>
    /// <param name="name">The name/text to display or link to.</param>
    public void DetailAnchor(string label, string url, string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            WriteElement("b", label + ":");
            WriteRaw(" ");
            if (string.IsNullOrEmpty(url))
            {
                WriteRaw(name);
            }
            else
            {
                WriteTagWithAttrs("a", "href", url);
                WriteRaw(name);
                TerminateTag("a");
            }

            WriteBr();
        }
    }

    /// <summary>
    /// Writes a detail line with label and value.
    /// Only outputs if the value parameter is not empty.
    /// </summary>
    /// <param name="label">The label text to display before the value.</param>
    /// <param name="htmlContent">The value to display.</param>
    public void Detail(string label, string htmlContent)
    {
        if (!string.IsNullOrEmpty(htmlContent))
        {
            WriteElement("b", label + ":");
            WriteRaw(htmlContent);
            WriteBr();
        }
    }

    /// <summary>
    /// Writes a detail line with label on separate line from value.
    /// Only outputs if the HTML content is not empty.
    /// </summary>
    /// <param name="label">The label text to display.</param>
    /// <param name="htmlContent">The HTML content to display on the next line.</param>
    public void DetailNewLine(string label, string htmlContent)
    {
        if (!string.IsNullOrEmpty(htmlContent))
        {
            WriteElement("b", label);
            WriteBr();
            WriteRaw(htmlContent);
            WriteBr();
        }
    }

    /// <summary>
    /// Writes a detail line with label and mailto link.
    /// Only outputs if the email parameter is not empty.
    /// </summary>
    /// <param name="label">The label text to display before the email.</param>
    /// <param name="email">The email address to create a mailto link for.</param>
    public void DetailMailto(string label, string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            WriteElement("b", label + ":");
            WriteRaw(" ");
            WriteTagWithAttrs("a", "href", "mailto:" + email);
            WriteRaw(email);
            TerminateTag("a");
            WriteBr();
        }
    }

    /// <summary>
    /// Generates HTML5 boilerplate start section with CSS and JavaScript.
    /// Currently throws NotImplementedException - needs implementation.
    /// </summary>
    /// <param name="args">Arguments containing CSS and JS paths/content.</param>
    public static
#if ASYNC
        async Task
#else
void
#endif
        BoilerplateStart(BoilerplateStartArgs args)
    {
        throw new InvalidOperationException();
    }

    /// <summary>
    /// Generates HTML5 boilerplate middle section (body tag with onload).
    /// Currently throws NotImplementedException - needs implementation.
    /// </summary>
    /// <param name="args">Optional arguments containing onload handler.</param>
    public static void BoilerplateMiddle(BoilerplateMiddleArgs? args = null)
    {
        throw new InvalidOperationException();
    }

    /// <summary>
    /// Generates HTML5 boilerplate end section (closing body and html tags).
    /// Currently throws NotImplementedException - needs implementation.
    /// </summary>
    public static void BoilerplateEnd()
    {
        throw new InvalidOperationException();
    }
}