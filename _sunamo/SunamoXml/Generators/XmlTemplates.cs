namespace SunamoHtml;


/// <summary>
/// Summary description for XmlTemplates
/// </summary>
internal static class XmlTemplates
{
    /// <summary>
    /// "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
    /// VS apos instead of qm nevermind
    /// </summary>
    internal const string xml = "<?xml version='1.0' encoding='utf-8'?>";
    internal static string GetXml2(string n1, string n2)
    {
        return "<sunamo><n1><![CDATA[" + n1 + "]]></n1><n2><![CDATA[" + n2 + "]]></n2></sunamo>";
    }
    internal static string GetXml5(string n1, string n2, string n3, string n4, string n5)
    {
        return "<sunamo><n1><![CDATA[" + n1 + "]]></n1><n2><![CDATA[" + n2 + "]]></n2><n3><![CDATA[" + n3 + "]]></n3><n4><![CDATA[" + n4 + "]]></n4><n5><![CDATA[" + n5 + "]]></n5></sunamo>";
    }
    internal static string GetXml4(string n1, string n2, string n3, string n4)
    {
        return "<sunamo><n1><![CDATA[" + n1 + "]]></n1><n2><![CDATA[" + n2 + "]]></n2><n3><![CDATA[" + n3 + "]]></n3><n4><![CDATA[" + n4 + "]]></n4></sunamo>";
    }
    internal static string GetXml3(string n1, string n2, string n3)
    {
        return "<sunamo><n1><![CDATA[" + n1 + "]]></n1><n2><![CDATA[" + n2 + "]]></n2><n3><![CDATA[" + n3 + "]]></n3></sunamo>";
    }
    internal static string GetXml1(string n1)
    {
        return "<sunamo><n1><![CDATA[" + n1 + "]]></n1></sunamo>";
    }
}