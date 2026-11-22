namespace SunamoHtml.Generators;

/// <summary>
/// Represents the html table generator type.
/// </summary>
public class HtmlTableGenerator : HtmlGeneratorExtended
{
    /// <summary>
    /// Start table.
    /// </summary>
    public void StartTable()
    {
        WriteTag(HtmlTags.Table);
    }

    /// <summary>
    /// End table.
    /// </summary>
    public void EndTable()
    {
        TerminateTag(HtmlTags.Table);
    }

    /// <summary>
    /// End tr.
    /// </summary>
    public void EndTr()
    {
        TerminateTag(HtmlTags.Tr);
    }

    /// <summary>
    /// Write row th.
    /// </summary>
    /// <param name="cssClass">The css class.</param>
    /// <param name="possibleAnswersAll">The possible answers all.</param>
    public void WriteRowTh(string cssClass, IList<string> possibleAnswersAll)
    {
        WriteRowWorker(WriteTh, cssClass, possibleAnswersAll);
    }

    /// <summary>
    /// Write row.
    /// </summary>
    /// <param name="cssClass">The css class.</param>
    /// <param name="possibleAnswersAll">The possible answers all.</param>
    public void WriteRow(string cssClass, IList<string> possibleAnswersAll)
    {
        WriteRowWorker(WriteTd, cssClass, possibleAnswersAll);
    }

    /// <summary>
    /// Write row worker.
    /// </summary>
    /// <param name="cellWriter">The cell writer.</param>
    /// <param name="cssClass">The css class.</param>
    /// <param name="possibleAnswersAll">The possible answers all.</param>
    public void WriteRowWorker(Action<string> cellWriter, string cssClass,
        IList<string> possibleAnswersAll)
    {
        if (cellWriter == null) throw new ArgumentNullException(nameof(cellWriter));
        if (possibleAnswersAll == null) throw new ArgumentNullException(nameof(possibleAnswersAll));
        WriteTagWithAttrs(HtmlTags.Tr, HtmlAttrs.C, cssClass);
        foreach (var item in possibleAnswersAll)
            cellWriter(item);
        TerminateTag(HtmlTags.Tr);
    }

    private void WriteTh(string content)
    {
        WriteElement(HtmlTags.Th, content);
    }

    private void WriteTd(string content)
    {
        WriteElement(HtmlTags.Td, content);
    }

    /// <summary>
    /// Write row.
    /// </summary>
    /// <param name="cssClass">The css class.</param>
    /// <param name="count">The count.</param>
    public void WriteRow(string cssClass, int count)
    {
        var list = new List<string>(count);
        for (var i = 0; i < count; i++)
            list.Add(string.Empty);
        WriteRow(cssClass, list);
    }

    /// <summary>
    /// End td.
    /// </summary>
    public void EndTd()
    {
        TerminateTag(HtmlTags.Td);
    }

    /// <summary>
    /// Start tr.
    /// </summary>
    /// <param name="cssClass">The css class.</param>
    public void StartTr(string cssClass)
    {
        WriteTagWithAttrs(HtmlTags.Tr, "class", cssClass);
    }

    /// <summary>
    /// Start td.
    /// </summary>
    public void StartTd()
    {
        WriteTag(HtmlTags.Td);
    }
}
