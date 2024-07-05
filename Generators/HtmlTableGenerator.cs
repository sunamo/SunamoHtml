namespace SunamoHtml.Generators;

public class HtmlTableGenerator : HtmlGeneratorExtended
{
    static Type type = typeof(HtmlTableGenerator);
    //public HtmlGeneratorExtended g = new HtmlGeneratorExtended();

    public void StartTable()
    {
        WriteTag(HtmlTags.table);
    }

    public void EndTable()
    {
        TerminateTag(HtmlTags.table);
    }


    public void EndTr()
    {
        TerminateTag(HtmlTags.tr);
    }

    public void WriteRowTh(string additionalQuestionCssClass, List<string> possibleAnswersAll)
    {
        WriteRowWorker(WriteTh, additionalQuestionCssClass, possibleAnswersAll);
    }

    /// <summary>
    /// td
    /// </summary>
    /// <param name="additionalQuestionCssClass"></param>
    /// <param name="possibleAnswersAll"></param>
    public void WriteRow(string additionalQuestionCssClass, List<string> possibleAnswersAll)
    {
        WriteRowWorker(WriteTd, additionalQuestionCssClass, possibleAnswersAll);
    }

    public void WriteRowWorker(Action<string> WriteTd, string additionalQuestionCssClass, List<string> possibleAnswersAll)
    {
        WriteTagWithAttr(HtmlTags.tr, HtmlAttrs.c, additionalQuestionCssClass, false);
        foreach (var item in possibleAnswersAll)
        {
            WriteTd(item);
        }
        TerminateTag(HtmlTags.tr);
    }

    private void WriteTh(string item)
    {
        WriteElement(HtmlTags.th, item);
    }

    private void WriteTd(string item)
    {
        WriteElement(HtmlTags.td, item);
    }

    public void WriteRow(string additionalQuestionCssClass, int count)
    {
        List<string> list = new List<string>(count);
        for (int i = 0; i < count; i++)
        {
            list.Add(string.Empty);
        }
        WriteRow(additionalQuestionCssClass, list);
    }

    public void EndTd()
    {
        TerminateTag(HtmlTags.td);
    }

    public void StartTr(string mainQuestionsCssClass)
    {
        WriteTag(HtmlTags.tr);
    }

    public void StartTd(object p)
    {
        WriteTag(HtmlTags.td);
    }
}
