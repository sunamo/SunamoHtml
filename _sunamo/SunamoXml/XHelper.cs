using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoHtml;
internal class XHelper
{
    internal static Dictionary<string, string> ns = new Dictionary<string, string>();

    internal static Dictionary<string, string> XmlNamespaces(XmlNamespaceManager nsmgr, bool withPrexixedXmlnsColon)
    {
        Dictionary<string, string> ns = new Dictionary<string, string>();
        foreach (string item2 in nsmgr)
        {
            var item = item2;

            if (withPrexixedXmlnsColon)
            {
                if (item == string.Empty || item == Consts.xmlns)
                {
                    item = Consts.xmlns;
                }
                else
                {
                    item = "xmlns:" + item;
                }

            }

            // Jaký je typ item, at nemusím používat slovník
            var v = nsmgr.LookupNamespace(item2);

            if (!ns.ContainsKey(item))
            {
                ns.Add(item, v);
            }
        }

        return ns;
    }

    internal static void AddXmlNamespaces(XmlNamespaceManager nsmgr)
    {
        foreach (string item in nsmgr)
        {
            // Jaký je typ item, at nemusím používat slovník
            var v = nsmgr.LookupNamespace(item);
            if (!ns.ContainsKey(item))
            {
                ns.Add(item, v);
            }
        }
    }
}
