namespace SunamoHtml._sunamo.SunamoXml;
internal class XH
{
    internal static string RemoveXmlDeclaration(string vstup)
    {
        vstup = Regex.Replace(vstup, @"<\?xml.*?\?>", "");
        vstup = Regex.Replace(vstup, @"<\?xml.*?\>", "");
        vstup = Regex.Replace(vstup, @"<\?xml.*?\/>", "");
        return vstup;
    }
}
