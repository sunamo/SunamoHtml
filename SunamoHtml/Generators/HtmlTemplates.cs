namespace SunamoHtml.Generators;

public static class HtmlTemplates
{
    public const string HtmlStartTitle =
        "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"https://www.w3.org/1999/xhtml\" ><head><title>";

    // Use this only when you don't need to add anything to the head section, otherwise use the separate constants.
    public const string HtmlEndTitleBody = "</title></head><body>";

    public const string HtmlEndTitle = "</title>";

    public const string HtmlEndHeadBody = "</head><body>";

    public const string HtmlEnd = "</body></html>";

    public static string Img(string src, string alt)
    {
        return $"<img src=\"{src}\" alt=\"{alt}\" />";
    }

    public static void Mail(HtmlGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        generator.WriteTagWithAttrs("a", "href", "mailto:radek.jancik@sunamo.cz");
        generator.WriteRaw("radek.jancik@sunamo.cz");
        generator.TerminateTag("a");
    }

    public static string HiddenField(string id, string value)
    {
        return $"<input type='hidden' id='{id}' value='{value}' />";
    }

    public static string GetH2(string title)
    {
        return "<h2 static class=\"velkaPismena tl\">" + title + "</h2>";
    }

    public static string NameValueBr(string name, string value)
    {
        return "<b>" + name + "</b>: " + value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Tr(string name, string value, bool isAddColon)
    {
        if (isAddColon)
            return "<tr><td>" + name + ": </td><td>" + value + "</td></tr>";
        return "<tr><td>" + name + " </td><td>" + value + "</td></tr>";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Anchor(string href, string displayText)
    {
        return "<a href=\"" + href + ">" + displayText + "</a>";
    }

    public static string TrColspan2(string name, string value, bool isAddColon)
    {
        if (isAddColon)
            return "<tr><td colspan='2'><b>" + name + ": </b></td></tr><tr><td colspan='2'>" + value + "</td></tr>";
        return "<tr><td colspan='2'><b>" + name + " </b></td></tr><tr><td colspan='2'>" + value + "</td></tr>";
    }
}
