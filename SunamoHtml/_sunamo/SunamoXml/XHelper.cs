namespace SunamoHtml._sunamo.SunamoXml;

internal class XHelper
{
    internal static Dictionary<string, string> Namespaces = new();

    internal static Dictionary<string, string> XmlNamespaces(XmlNamespaceManager nsmgr, bool withPrexixedXmlnsColon)
    {
        var ns = new Dictionary<string, string>();
        foreach (string item2 in nsmgr)
        {
            var item = item2;

            if (withPrexixedXmlnsColon)
            {
                if (item == string.Empty || item == "xmlns")
                    item = "xmlns";
                else
                    item = "xmlns:" + item;
            }

            // Jaký je typ item, at nemusím používat slovník
            var value = nsmgr.LookupNamespace(item2) ?? string.Empty;

            if (!ns.ContainsKey(item)) ns.Add(item, value);
        }

        return ns;
    }

}