namespace SunamoHtml._sunamo.SunamoXml;

internal class XH
{
    internal static string RemoveXmlDeclaration(string xml)
    {
        xml = Regex.Replace(xml, @"<\?xml.*?\?>", "");
        xml = Regex.Replace(xml, @"<\?xml.*?\>", "");
        xml = Regex.Replace(xml, @"<\?xml.*?\/>", "");
        return xml;
    }
}