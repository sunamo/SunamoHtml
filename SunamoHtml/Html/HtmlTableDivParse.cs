namespace SunamoHtml.Html;

/// <summary>
/// EN: Parser for HTML div elements arranged in table-like structure.
/// CZ: Parser pro HTML div elementy uspořádané v tabulkové struktuře.
/// </summary>
public class HtmlTableDivParse
{
    /// <summary>
    /// Gets or sets the parsed data divided into rows and columns.
    /// </summary>
    public List<List<HtmlNode>> Data { get; set; }

    /// <summary>
    /// Initializes a new instance with div elements divided into columns.
    /// </summary>
    /// <param name="divs">List of div nodes to parse.</param>
    /// <param name="columnCount">Number of columns per row.</param>
    public HtmlTableDivParse(List<HtmlNode> divs, int columnCount)
    {
        Data = CA.DivideBy(divs, columnCount);
    }
}
