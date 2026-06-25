namespace SunamoHtml.Generators;

// EN: Extended HTML generator with detail display methods for labels, anchors, and mailto links.
// CZ: Rozšířený HTML generátor s metodami pro zobrazení detailů s labely, odkazy a mailto linky.
public class HtmlGeneratorExtended : HtmlGenerator
{
    // Only outputs if the name parameter is not empty.
    public void DetailAnchor(string label, System.Uri uri, string name)
    {
        DetailAnchor(label, uri?.ToString() ?? string.Empty, name);
    }

    // Only outputs if the name parameter is not empty.
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
    public static
        async Task
        BoilerplateStart(BoilerplateStartArgs args)
    {
        throw new InvalidOperationException();
    }

    // Currently throws NotImplementedException - needs implementation.
    public static void BoilerplateMiddle(BoilerplateMiddleArgs? args = null)
    {
        throw new InvalidOperationException();
    }

    public static void BoilerplateEnd()
    {
        throw new InvalidOperationException();
    }
}
