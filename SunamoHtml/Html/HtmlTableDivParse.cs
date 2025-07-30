namespace SunamoHtml.Html;

public class HtmlTableDivParse
{
    public List<List<HtmlNode>> Data { get; set; }
    public HtmlTableDivParse(List<HtmlNode> divs, int countOfColumn)
    {
        Data = CA.DivideBy(divs, countOfColumn);
    }
}