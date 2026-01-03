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
        WriteTag(HtmlTags.table);
    }

    /// <summary>
    /// Writes the closing table tag.
    /// </summary>
    public void EndTable()
    {
        TerminateTag(HtmlTags.table);
    }

    /// <summary>
    /// Writes the closing tr tag.
    /// </summary>
    public void EndTr()
    {
        TerminateTag(HtmlTags.tr);
    }

    /// <summary>
    /// Writes a table row with th (header) cells.
    /// </summary>
    /// <param name="additionalQuestionCssClass">CSS class to apply to the tr element.</param>
    /// <param name="possibleAnswersAll">List of cell contents.</param>
    public void WriteRowTh(string additionalQuestionCssClass, List<string> possibleAnswersAll)
    {
        WriteRowWorker(WriteTh, additionalQuestionCssClass, possibleAnswersAll);
    }

    /// <summary>
    /// Writes a table row with td (data) cells.
    /// </summary>
    /// <param name="additionalQuestionCssClass">CSS class to apply to the tr element.</param>
    /// <param name="possibleAnswersAll">List of cell contents.</param>
    public void WriteRow(string additionalQuestionCssClass, List<string> possibleAnswersAll)
    {
        WriteRowWorker(WriteTd, additionalQuestionCssClass, possibleAnswersAll);
    }

    /// <summary>
    /// Worker method that generates a table row with specified cell writer action.
    /// </summary>
    /// <param name="cellWriter">Action to write each cell (WriteTh or WriteTd).</param>
    /// <param name="additionalQuestionCssClass">CSS class to apply to the tr element.</param>
    /// <param name="possibleAnswersAll">List of cell contents.</param>
    public void WriteRowWorker(Action<string> cellWriter, string additionalQuestionCssClass,
        List<string> possibleAnswersAll)
    {
        WriteTagWithAttr(HtmlTags.tr, HtmlAttrs.C, additionalQuestionCssClass);
        foreach (var item in possibleAnswersAll)
            cellWriter(item);
        TerminateTag(HtmlTags.tr);
    }

    /// <summary>
    /// Writes a th (table header) element with the specified content.
    /// </summary>
    /// <param name="item">The content for the th element.</param>
    private void WriteTh(string item)
    {
        WriteElement(HtmlTags.th, item);
    }

    /// <summary>
    /// Writes a td (table data) element with the specified content.
    /// </summary>
    /// <param name="item">The content for the td element.</param>
    private void WriteTd(string item)
    {
        WriteElement(HtmlTags.td, item);
    }

    /// <summary>
    /// Writes a table row with specified number of empty td cells.
    /// </summary>
    /// <param name="additionalQuestionCssClass">CSS class to apply to the tr element.</param>
    /// <param name="count">Number of empty cells to generate.</param>
    public void WriteRow(string additionalQuestionCssClass, int count)
    {
        var list = new List<string>(count);
        for (var i = 0; i < count; i++)
            list.Add(string.Empty);
        WriteRow(additionalQuestionCssClass, list);
    }

    /// <summary>
    /// Writes the closing td tag.
    /// </summary>
    public void EndTd()
    {
        TerminateTag(HtmlTags.td);
    }

    /// <summary>
    /// Writes the opening tr tag with CSS class.
    /// </summary>
    /// <param name="mainQuestionsCssClass">CSS class to apply to the tr element.</param>
    public void StartTr(string mainQuestionsCssClass)
    {
        WriteTagWithAttr(HtmlTags.tr, "class", mainQuestionsCssClass);
    }

    /// <summary>
    /// Writes the opening td tag.
    /// </summary>
    public void StartTd()
    {
        WriteTag(HtmlTags.td);
    }
}
