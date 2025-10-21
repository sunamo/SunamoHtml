// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoHtml.Html;

public class HtmlTableDivParse
{
    public List<List<HtmlNode>> Data { get; set; }
    public HtmlTableDivParse(List<HtmlNode> divs, int countOfColumn)
    {
        Data = CA.DivideBy(divs, countOfColumn);
    }
}