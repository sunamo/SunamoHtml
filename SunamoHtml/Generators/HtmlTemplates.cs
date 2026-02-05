namespace SunamoHtml.Generators;

/// <summary>
/// EN: Static class containing HTML template constants and helper methods for common HTML elements.
/// CZ: Statická třída obsahující HTML šablony konstanty a pomocné metody pro běžné HTML elementy.
/// </summary>
public static class HtmlTemplates
{
    /// <summary>
    /// HTML document start with DOCTYPE and opening head/title tags.
    /// </summary>
    public const string HtmlStartTitle =
        "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"https://www.w3.org/1999/xhtml\" ><head><title>";

    /// <summary>
    /// Closing title tag, closing head tag, and opening body tag.
    /// EN: Use this only when you don't need to add anything to the head section, otherwise use the separate constants.
    /// CZ: Použij toto pouze když nepotřebuješ nic přidat do head sekce, jinak použij oddělené konstanty.
    /// </summary>
    public const string HtmlEndTitleBody = "</title></head><body>";

    /// <summary>
    /// Closing title tag.
    /// </summary>
    public const string HtmlEndTitle = "</title>";

    /// <summary>
    /// Closing head tag and opening body tag.
    /// </summary>
    public const string HtmlEndHeadBody = "</head><body>";

    /// <summary>
    /// Closing body and html tags.
    /// </summary>
    public const string HtmlEnd = "</body></html>";

    /// <summary>
    /// Generates an img tag with src and alt attributes.
    /// </summary>
    /// <param name="src">The image source URL.</param>
    /// <param name="alt">The alt text for the image.</param>
    /// <returns>HTML img tag string.</returns>
    public static string Img(string src, string alt)
    {
        return $"<img src=\"{src}\" alt=\"{alt}\" />";
    }

    /// <summary>
    /// Writes a mailto link for radek.jancik@sunamo.cz.
    /// </summary>
    /// <param name="generator">The HTML generator to write to.</param>
    public static void Mail(HtmlGenerator generator)
    {
        generator.WriteTagWithAttrs("a", "href", "mailto:radek.jancik@sunamo.cz");
        generator.WriteRaw("radek.jancik@sunamo.cz");
        generator.TerminateTag("a");
    }

    /// <summary>
    /// Generates a hidden input field with id and value.
    /// </summary>
    /// <param name="id">The id attribute for the hidden field.</param>
    /// <param name="value">The value attribute for the hidden field.</param>
    /// <returns>HTML hidden input field string.</returns>
    public static string HiddenField(string id, string value)
    {
        var format = "<input type='hidden' id='" + id + "' value='" + value + "' />";
        return format;
    }

    /// <summary>
    /// Generates an H2 heading with specific CSS classes.
    /// </summary>
    /// <param name="title">The heading text.</param>
    /// <returns>HTML h2 element string.</returns>
    public static string GetH2(string title)
    {
        return "<h2 static class=\"velkaPismena tl\">" + title + "</h2>";
    }

    /// <summary>
    /// Generates a name-value pair with bold name and line break.
    /// </summary>
    /// <param name="name">The name/label.</param>
    /// <param name="value">The value.</param>
    /// <returns>HTML string with formatted name-value pair.</returns>
    public static string NameValueBr(string name, string value)
    {
        return "<b>" + name + "</b>: " + value;
    }

    /// <summary>
    /// Generates a table row with two cells (name and value).
    /// </summary>
    /// <param name="name">The name/label for the first cell.</param>
    /// <param name="value">The value for the second cell.</param>
    /// <param name="isAddColon">Whether to add a colon after the name.</param>
    /// <returns>HTML tr element string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string tr(string name, string value, bool isAddColon)
    {
        if (isAddColon)
            return "<tr><td>" + name + ": </td><td>" + value + "</td></tr>";
        return "<tr><td>" + name + " </td><td>" + value + "</td></tr>";
    }

    /// <summary>
    /// Generates an anchor link.
    /// </summary>
    /// <param name="href">The href URL.</param>
    /// <param name="displayText">The text to display.</param>
    /// <returns>HTML anchor element string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string a(string href, string displayText)
    {
        return "<a href=\"" + href + ">" + displayText + "</a>";
    }

    /// <summary>
    /// Generates two table rows with colspan='2' cells (header row with bold name, data row with value).
    /// </summary>
    /// <param name="name">The name/label for the header row.</param>
    /// <param name="value">The value for the data row.</param>
    /// <param name="isAddColon">Whether to add a colon after the name.</param>
    /// <returns>HTML string with two tr elements.</returns>
    public static string trColspan2(string name, string value, bool isAddColon)
    {
        if (isAddColon)
            return "<tr><td colspan='2'><b>" + name + ": </b></td></tr><tr><td colspan='2'>" + value + "</td></tr>";
        return "<tr><td colspan='2'><b>" + name + " </b></td></tr><tr><td colspan='2'>" + value + "</td></tr>";
    }
}