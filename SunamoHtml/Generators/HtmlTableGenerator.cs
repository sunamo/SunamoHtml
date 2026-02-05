namespace SunamoHtml.Generators;

/// <summary>
/// EN: HTML table generator with methods for creating table structure (table, tr, td, th).
/// CZ: HTML generátor tabulek s metodami pro vytváření struktury tabulky (table, tr, td, th).
/// </summary>
public class HtmlTableGenerator : HtmlGeneratorExtended
{
    /// <summary>
    /// Writes the opening table tag.
    /// </summary>
    public void StartTable()
    {
        WriteTag(HtmlTags.Table);
    }

    /// <summary>
    /// Writes the closing table tag.
    /// </summary>
    public void EndTable()
    {
        TerminateTag(HtmlTags.Table);
    }

    /// <summary>
    /// Writes the closing tr tag.
    /// </summary>
    public void EndTr()
    {
        TerminateTag(HtmlTags.Tr);
    }

    /// <summary>
    /// Writes a table row with th (header) cells.
    /// </summary>
    /// <param name="cssClass">CSS class to apply to the tr element.</param>
    /// <param name="possibleAnswersAll">List of cell contents.</param>
    public void WriteRowTh(string cssClass, List<string> possibleAnswersAll)
    {
        WriteRowWorker(WriteTh, cssClass, possibleAnswersAll);
    }

    /// <summary>
    /// Writes a table row with td (data) cells.
    /// </summary>
    /// <param name="cssClass">CSS class to apply to the tr element.</param>
    /// <param name="possibleAnswersAll">List of cell contents.</param>
    public void WriteRow(string cssClass, List<string> possibleAnswersAll)
    {
        WriteRowWorker(WriteTd, cssClass, possibleAnswersAll);
    }

    /// <summary>
    /// Worker method that generates a table row with specified cell writer action.
    /// </summary>
    /// <param name="cellWriter">Action to write each cell (WriteTh or WriteTd).</param>
    /// <param name="cssClass">CSS class to apply to the tr element.</param>
    /// <param name="possibleAnswersAll">List of cell contents.</param>
    public void WriteRowWorker(Action<string> cellWriter, string cssClass,
        List<string> possibleAnswersAll)
    {
        WriteTagWithAttrs(HtmlTags.Tr, HtmlAttrs.C, cssClass);
        foreach (var item in possibleAnswersAll)
            cellWriter(item);
        TerminateTag(HtmlTags.Tr);
    }

    /// <summary>
    /// Writes a th (table header) element with the specified content.
    /// </summary>
    /// <param name="content">The content for the th element.</param>
    private void WriteTh(string content)
    {
        WriteElement(HtmlTags.Th, content);
    }

    /// <summary>
    /// Writes a td (table data) element with the specified content.
    /// </summary>
    /// <param name="content">The content for the td element.</param>
    private void WriteTd(string content)
    {
        WriteElement(HtmlTags.Td, content);
    }

    /// <summary>
    /// Writes a table row with specified number of empty td cells.
    /// </summary>
    /// <param name="cssClass">CSS class to apply to the tr element.</param>
    /// <param name="count">Number of empty cells to generate.</param>
    public void WriteRow(string cssClass, int count)
    {
        var list = new List<string>(count);
        for (var i = 0; i < count; i++)
            list.Add(string.Empty);
        WriteRow(cssClass, list);
    }

    /// <summary>
    /// Writes the closing td tag.
    /// </summary>
    public void EndTd()
    {
        TerminateTag(HtmlTags.Td);
    }

    /// <summary>
    /// Writes the opening tr tag with CSS class.
    /// </summary>
    /// <param name="cssClass">CSS class to apply to the tr element.</param>
    public void StartTr(string cssClass)
    {
        WriteTagWithAttrs(HtmlTags.Tr, "class", cssClass);
    }

    /// <summary>
    /// Writes the opening td tag.
    /// </summary>
    public void StartTd()
    {
        WriteTag(HtmlTags.Td);
    }
}