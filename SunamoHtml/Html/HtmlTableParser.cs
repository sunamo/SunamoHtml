namespace SunamoHtml.Html;

/// <summary>
/// Represents the html table parser type.
/// </summary>
public sealed class HtmlTableParser
{
    // The parsed table data. If an element contains null, it was a colspan cell.
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819")]
    public string[][] Data { get; set; }

    /// <summary>
    /// Initializes a new instance of the html table parser class.
    /// </summary>
    /// <param name=""Performance"">The performance.</param>
    /// <param name=""CA1819"">The CA 1819.</param>
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

    /// <summary>
    /// Gets the row count.
    /// </summary>
    public int RowCount => Data.Length;

    /// <summary>
    /// Gets the column count.
    /// </summary>
    public int ColumnCount => Data.Length > 0 ? Data[0].Length : 0;

    /// <summary>
    /// Normalize values in column.
    /// </summary>
    /// <param name="chars">The chars.</param>
    /// <param name="isRemoveAlsoInnerHtmlOfSubNodes">The is remove also inner html of sub nodes.</param>
    /// <returns>The normalize values in column result.</returns>
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

    /// <summary>
    /// Column values.
    /// </summary>
    /// <param name="isRemoveAlsoInnerHtmlOfSubNodes">The is remove also inner html of sub nodes.</param>
    /// <returns>The column values result.</returns>
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

    /// <summary>
    /// Column values.
    /// </summary>
    /// <param name="isSkipFirstRow">The is skip first row.</param>
    /// <returns>The column values result.</returns>
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
