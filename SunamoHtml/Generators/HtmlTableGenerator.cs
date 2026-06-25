namespace SunamoHtml.Generators;

public class HtmlTableGenerator : HtmlGeneratorExtended
{
    public void StartTable()
    {
        WriteTag(HtmlTags.Table);
    }

    public void EndTable()
    {
        TerminateTag(HtmlTags.Table);
    }

    public void EndTr()
    {
        TerminateTag(HtmlTags.Tr);
    }

    public void WriteRowTh(string cssClass, IList<string> possibleAnswersAll)
    {
        WriteRowWorker(WriteTh, cssClass, possibleAnswersAll);
    }

    public void WriteRow(string cssClass, IList<string> possibleAnswersAll)
    {
        WriteRowWorker(WriteTd, cssClass, possibleAnswersAll);
    }

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

    public void WriteRow(string cssClass, int count)
    {
        var list = new List<string>(count);
        for (var i = 0; i < count; i++)
            list.Add(string.Empty);
        WriteRow(cssClass, list);
    }

    public void EndTd()
    {
        TerminateTag(HtmlTags.Td);
    }

    public void StartTr(string cssClass)
    {
        WriteTagWithAttrs(HtmlTags.Tr, "class", cssClass);
    }

    public void StartTd()
    {
        WriteTag(HtmlTags.Td);
    }
}
