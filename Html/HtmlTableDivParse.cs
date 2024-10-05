
namespace SunamoHtml.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HtmlTableDivParse
{
    public List<List<HtmlNode>> Data { get; set; }

    public HtmlTableDivParse(List<HtmlNode> divs, int countOfColumn)
    {
        Data = CA.DivideBy(divs, countOfColumn);
    }
}