namespace SunamoHtml.Generators;

// Extended HTML generator with detail display methods for labels, anchors, and mailto links.
/// <summary>
/// Represents the html generator extended type.
/// </summary>
public class HtmlGeneratorExtended : HtmlGenerator
{
    // Only outputs if the name parameter is not empty.
    /// <summary>
    /// Detail anchor.
    /// </summary>
    /// <param name="label">The label.</param>
    /// <param name="uri">The uri.</param>
    /// <param name="name">The name.</param>
    public void DetailAnchor(string label, System.Uri uri, string name)
    {
        DetailAnchor(label, uri?.ToString() ?? string.Empty, name);
    }

    // Only outputs if the name parameter is not empty.
    /// <summary>
    /// Detail anchor.
    /// </summary>
    /// <param name="label">The label.</param>
    /// <param name="url">The url.</param>
    /// <param name="name">The name.</param>
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

    // Only outputs if the value parameter is not empty.
    /// <summary>
    /// Detail.
    /// </summary>
    /// <param name="label">The label.</param>
    /// <param name="htmlContent">The html content.</param>
    public void Detail(string label, string htmlContent)
    {
        if (!string.IsNullOrEmpty(htmlContent))
        {
            WriteElement("b", label + ":");
            WriteRaw(htmlContent);
            WriteBr();
        }
    }

    // Only outputs if the HTML content is not empty.
    /// <summary>
    /// Detail new line.
    /// </summary>
    /// <param name="label">The label.</param>
    /// <param name="htmlContent">The html content.</param>
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

    // Only outputs if the email parameter is not empty.
    /// <summary>
    /// Detail mailto.
    /// </summary>
    /// <param name="label">The label.</param>
    /// <param name="email">The email.</param>
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

    // Currently throws NotImplementedException - needs implementation.
    /// <summary>
    /// Boilerplate start.
    /// </summary>
    /// <param name="args">The args.</param>
    public static
        async Task
        BoilerplateStart(BoilerplateStartArgs args)
    {
        throw new InvalidOperationException();
    }

    // Currently throws NotImplementedException - needs implementation.
    /// <summary>
    /// Boilerplate middle.
    /// </summary>
    /// <param name="args">The args.</param>
    public static void BoilerplateMiddle(BoilerplateMiddleArgs? args = null)
    {
        throw new InvalidOperationException();
    }

    /// <summary>
    /// Boilerplate end.
    /// </summary>
    public static void BoilerplateEnd()
    {
        throw new InvalidOperationException();
    }
}
