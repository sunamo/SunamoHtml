namespace SunamoHtml.Html;

public sealed class HtmlTableDivParse
{
    public IList<List<HtmlNode>> Data { get; }

    public HtmlTableDivParse(IList<HtmlNode> divs, int columnCount)
    {
        if (divs == null) throw new ArgumentNullException(nameof(divs));
        Data = CA.DivideBy(divs.ToList(), columnCount);
    }
}
