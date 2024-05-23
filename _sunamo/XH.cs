using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoHtml;
public class XH
{
    public static string RemoveXmlDeclaration(string vstup)
    {
        vstup = Regex.Replace(vstup, @"<\?xml.*?\?>", "");
        vstup = Regex.Replace(vstup, @"<\?xml.*?\>", "");
        vstup = Regex.Replace(vstup, @"<\?xml.*?\/>", "");
        return vstup;
    }
}
