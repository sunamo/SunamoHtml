namespace SunamoHtml.Html;

/// <summary>
/// Represents the html table div parse type.
/// </summary>
public sealed class HtmlTableDivParse
{
    /// <summary>
    /// Gets the data.
    /// </summary>
    public IList<List<HtmlNode>> Data { get; }

    /// <summary>
    /// Html table div parse.
    /// </summary>
    /// <param name="divs">The divs.</param>
    /// <param name="columnCount">The column count.</param>
    /// <returns>The html table div parse result.</returns>
    public HtmlTableDivParse(IList<HtmlNode> divs, int columnCount)
    {
        if (divs == null) throw new ArgumentNullException(nameof(divs));
        Data = CA.DivideBy(divs.ToList(), columnCount);
    }
}
