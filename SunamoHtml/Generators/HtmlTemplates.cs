namespace SunamoHtml.Generators;

/// <summary>
/// Represents the html templates type.
/// </summary>
public static class HtmlTemplates
{
    /// <summary>
    /// The html start title constant.
    /// </summary>
    public const string HtmlStartTitle =
        "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"https://www.w3.org/1999/xhtml\" ><head><title>";

    // Use this only when you don't need to add anything to the head section, otherwise use the separate constants.
    /// <summary>
    /// The html end title body constant.
    /// </summary>
    public const string HtmlEndTitleBody = "</title></head><body>";

    /// <summary>
    /// The html end title constant.
    /// </summary>
    public const string HtmlEndTitle = "</title>";

    /// <summary>
    /// The html end head body constant.
    /// </summary>
    public const string HtmlEndHeadBody = "</head><body>";

    /// <summary>
    /// The html end constant.
    /// </summary>
    public const string HtmlEnd = "</body></html>";

    /// <summary>
    /// Img.
    /// </summary>
    /// <param name="src">The src.</param>
    /// <param name="alt">The alt.</param>
    /// <returns>The img result.</returns>
    public static string Img(string src, string alt)
    {
        return $"<img src=\"{src}\" alt=\"{alt}\" />";
    }

    /// <summary>
    /// Mail.
    /// </summary>
    /// <param name="src">The src.</param>
    /// <param name="alt">The alt.</param>
    /// <returns>The mail result.</returns>
    public static void Mail(HtmlGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        generator.WriteTagWithAttrs("a", "href", "mailto:radek.jancik@sunamo.cz");
        generator.WriteRaw("radek.jancik@sunamo.cz");
        generator.TerminateTag("a");
    }

    /// <summary>
    /// Hidden field.
    /// </summary>
    /// <param name="src">The src.</param>
    /// <param name="alt">The alt.</param>
    /// <returns>The hidden field result.</returns>
    public static string HiddenField(string id, string value)
    {
        return $"<input type='hidden' id='{id}' value='{value}' />";
    }

    /// <summary>
    /// Get h 2.
    /// </summary>
    /// <param name="generator">The generator.</param>
    /// <returns>The get h 2 result.</returns>
    public static string GetH2(string title)
    {
        return "<h2 static class=\"velkaPismena tl\">" + title + "</h2>";
    }

    /// <summary>
    /// Initializes a new instance of the name value br class.
    /// </summary>
    /// <param name=""a"">The a.</param>
    /// <param name=""href"">The href.</param>
    /// <param name=""mailto:radek.jancik@sunamo.cz"">The mailto radek jancik sunamo cz.</param>
    public static string NameValueBr(string name, string value)
    {
        return "<b>" + name + "</b>: " + value;
    }

    /// <summary>
    /// Tr.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <returns>The tr result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Tr(string name, string value, bool isAddColon)
    {
        if (isAddColon)
            return "<tr><td>" + name + ": </td><td>" + value + "</td></tr>";
        return "<tr><td>" + name + " </td><td>" + value + "</td></tr>";
    }

    /// <summary>
    /// Anchor.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <returns>The anchor result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Anchor(string href, string displayText)
    {
        return "<a href=\"" + href + ">" + displayText + "</a>";
    }

    /// <summary>
    /// Initializes a new instance of the tr colspan 2 class.
    /// </summary>
    /// <param name="MethodImplOptions.AggressiveInlining">The method impl options aggressive inlining.</param>
    public static string TrColspan2(string name, string value, bool isAddColon)
    {
        if (isAddColon)
            return "<tr><td colspan='2'><b>" + name + ": </b></td></tr><tr><td colspan='2'>" + value + "</td></tr>";
        return "<tr><td colspan='2'><b>" + name + " </b></td></tr><tr><td colspan='2'>" + value + "</td></tr>";
    }
}
