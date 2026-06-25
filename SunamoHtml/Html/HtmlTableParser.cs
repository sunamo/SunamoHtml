namespace SunamoHtml.Html;

public sealed class HtmlTableParser
{
    // EN: The parsed table data. If an element contains null, it was a colspan cell.
    // CZ: Naparsovaná tabulková data. Pokud prvek obsahuje null, jednalo se o colspan buňku.
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819")]
    public string[][] Data { get; set; }

    public HtmlTableParser(HtmlNode html, bool isIgnoreFirstRow)
    {
        Data = Array.Empty<string[]>();
        var startRow = 0;
        if (isIgnoreFirstRow)
            startRow++;
        if (html.Name != "table")
        {
            var htmlFirst = html.FirstChild;
            if (htmlFirst.Name != "table")
                return;
            html = htmlFirst;
        }

        var maxColumn = 0;
        var rows = HtmlHelper.ReturnAllTags(html, "tr");
        var maxRow = rows.Count;
        if (isIgnoreFirstRow)
            maxRow--;
        for (var result = startRow; result < rows.Count; result++)
        {
            var tds = HtmlHelper.ReturnAllTags(rows[result], "td", "th");
            var maxColumnActual = tds.Count;
            foreach (var cellRow in tds)
            {
                var tdWithColspan = HtmlAssistant.GetValueOfAttribute(HtmlAttrValue.Colspan, cellRow, true);
                if (!string.IsNullOrEmpty(tdWithColspan))
                {
                    var colspan = BTS.TryParseInt(tdWithColspan, 0);
                    if (colspan > 0)
                    {
                        maxColumnActual += colspan;
                        maxColumnActual--;
                    }
                }
            }

            if (maxColumnActual > maxColumn)
                maxColumn = maxColumnActual;
        }

        Data = new string[maxRow][];
        for (var rowIndex = 0; rowIndex < maxRow; rowIndex++)
            Data[rowIndex] = new string[maxColumn];
        for (var result = startRow; result < rows.Count; result++)
        {
            var ths = HtmlHelper.ReturnAllTags(rows[result], "th", "td");
            for (var count = 0; count < maxColumn; count++)
                if (ths.Count > count)
                {
                    var cellRow = ths[count];
                    var cell = cellRow.InnerText.Trim();
                    cell = WebUtility.HtmlDecode(cell);
                    cell = SHReplace.ReplaceAllDoubleSpaceToSingle(cell);
                    Data[result - startRow][count] = cell;
                    var tdWithColspan = HtmlAssistant.GetValueOfAttribute(HtmlAttrValue.Colspan, cellRow, true);
                    if (!string.IsNullOrEmpty(tdWithColspan))
                    {
                        var colspan = BTS.TryParseInt(tdWithColspan, 0);
                        if (colspan > 0)
                            for (var i = 0; i < colspan; i++)
                            {
                                count++;
                                Data[result - startRow][count] = null!;
                            }
                    }
                }
        }
    }

    public int RowCount => Data.Length;

    public int ColumnCount => Data.Length > 0 ? Data[0].Length : 0;

    public static void NormalizeValuesInColumn(IList<string> chars, bool isRemoveAlsoInnerHtmlOfSubNodes)
    {
        if (chars == null) throw new ArgumentNullException(nameof(chars));
        for (var i = 0; i < chars.Count; i++)
        {
            if (isRemoveAlsoInnerHtmlOfSubNodes)
                chars[i] = HtmlHelperText.RemoveAllNodes(chars[i]);
            else
                chars[i] = HtmlHelper.StripAllTags(chars[i]);
            chars[i] = WebUtility.HtmlDecode(chars[i]);
        }
    }

    public IList<string> ColumnValues(int columnIndex, bool isNormalizeValuesInColumn, bool isRemoveAlsoInnerHtmlOfSubNodes,
        bool isSkipFirstRow)
    {
        var d0 = Data.Length;
        var result = new List<string>();
        var i = 0;
        if (isSkipFirstRow)
            i = 1;
        for (; i < d0; i++)
            result.Add(Data[i][columnIndex]);
        FinalizeColumnValues(isNormalizeValuesInColumn, isRemoveAlsoInnerHtmlOfSubNodes, result);
        return result;
    }

    public IList<string> ColumnValues(string columnName, bool isNormalizeValuesInColumn, bool isRemoveAlsoInnerHtmlOfSubNodes)
    {
        var d0 = Data.Length;
        var d1 = Data.Length > 0 ? Data[0].Length : 0;
        var result = new List<string>();
        for (var i = 0; i < d1; i++)
        {
            var nameColumn = Data[0][i];
            var columnIndex = i;
            if (nameColumn == columnName)
                for (i = 1; i < d0; i++)
                    result.Add(Data[i][columnIndex]);
            if (result.Count != 0)
                break;
        }

        FinalizeColumnValues(isNormalizeValuesInColumn, isRemoveAlsoInnerHtmlOfSubNodes, result);
        return result;
    }

    private static void FinalizeColumnValues(bool isNormalizeValuesInColumn, bool isRemoveAlsoInnerHtmlOfSubNodes,
        List<string> result)
    {
        if (isNormalizeValuesInColumn || isRemoveAlsoInnerHtmlOfSubNodes)
            NormalizeValuesInColumn(result, isRemoveAlsoInnerHtmlOfSubNodes);
    }
}
