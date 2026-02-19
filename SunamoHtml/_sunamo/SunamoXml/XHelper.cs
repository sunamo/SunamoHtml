namespace SunamoHtml._sunamo.SunamoXml;

internal sealed class XHelper
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
                if (string.IsNullOrEmpty(item) || item == "xmlns")
                    item = "xmlns";
                else
                    item = "xmlns:" + item;
            }

            // Jaký je typ item, at nemusím používat slovník
            var value = nsmgr.LookupNamespace(item2) ?? string.Empty;

            ns.TryAdd(item, value);
        }

        return ns;
    }

}