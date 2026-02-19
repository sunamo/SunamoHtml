namespace SunamoHtml.Html;

/// <summary>
/// EN: Parser for HTML div elements arranged in table-like structure.
/// CZ: Parser pro HTML div elementy uspořádané v tabulkové struktuře.
/// </summary>
public sealed class HtmlTableDivParse
{
    /// <summary>
    /// Gets the parsed data divided into rows and columns.
    /// </summary>
    public IList<List<HtmlNode>> Data { get; }

    /// <summary>
    /// Initializes a new instance with div elements divided into columns.
    /// </summary>
    /// <param name="divs">List of div nodes to parse.</param>
    /// <param name="columnCount">Number of columns per row.</param>
    public HtmlTableDivParse(IList<HtmlNode> divs, int columnCount)
    {
        ArgumentNullException.ThrowIfNull(divs);
        Data = CA.DivideBy(divs.ToList(), columnCount);
    }
}
