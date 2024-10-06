namespace SunamoHtml.Html;

// Row/column
public class HtmlTableParser
{
    /// <summary>
    ///     Pokud se bude v prvku vyskytovat null, jednalo se o colspan
    /// </summary>
    public string[,] data { get; set; }

    /// <summary>
    /// </summary>
    /// <param name="html"></param>
    public HtmlTableParser(HtmlNode html, bool ignoreFirstRow)
    {
        var startRow = 0;
        if (ignoreFirstRow) startRow++;
        if (html.Name != "table")
        {
            var htmlFirst = html.FirstChild;
            if (htmlFirst.Name != "table") return;
            html = htmlFirst;
        }

        var maxColumn = 0;
        var rows = HtmlHelper.ReturnAllTags(html, "tr");
        var maxRow = rows.Count;
        if (ignoreFirstRow) maxRow--;
        for (var r = startRow; r < rows.Count; r++)
        {
            var tds = HtmlHelper.ReturnAllTags(rows[r], "td", "th");
            var maxColumnActual = tds.Count;
            foreach (var cellRow in tds)
            {
                var tdWithColspan = HtmlHelper.GetValueOfAttribute(HtmlAttrValue.colspan, cellRow, true);
                if (tdWithColspan != "")
                {
                    var colspan = BTS.TryParseInt(tdWithColspan, 0);
                    if (colspan > 0)
                    {
                        maxColumnActual += colspan;
                        maxColumnActual--;
                    }
                }
            }

            if (maxColumnActual > maxColumn) maxColumn = maxColumnActual;
        }

        data = new string[maxRow, maxColumn];
        for (var r = startRow; r < rows.Count; r++)
        {
            //List<HtmlNode> tds = HtmlHelper.ReturnAllTags()
            var ths = HtmlHelper.ReturnAllTags(rows[r], "th", "td");
            for (var c = 0; c < maxColumn; c++)
                if (ths.Count > c)
                {
                    var cellRow = ths[c];
                    var cell = cellRow.InnerText.Trim();
                    //cell = HtmlHelperText.ConvertTextToHtml(cell);
                    cell = WebUtility.HtmlDecode(cell);
                    cell = SHReplace.ReplaceAllDoubleSpaceToSingle(cell);
                    data[r - startRow, c] = cell;
                    var tdWithColspan = HtmlHelper.GetValueOfAttribute(HtmlAttrValue.colspan, cellRow, true);
                    if (tdWithColspan != "")
                    {
                        var colspan = BTS.TryParseInt(tdWithColspan, 0);
                        if (colspan > 0)
                            for (var i = 0; i < colspan; i++)
                            {
                                c++;
                                data[r - startRow, c] = null;
                            }
                    }
                }
        }
    }

    public int RowCount => data.GetLength(0);
    public int ColumnCount => data.GetLength(1);

    public static void NormalizeValuesInColumn(List<string> chars, bool removeAlsoInnerHtmlOfSubNodes)
    {
        for (var i = 0; i < chars.Count; i++)
        {
            if (removeAlsoInnerHtmlOfSubNodes)
                chars[i] = HtmlHelperText.RemoveAllNodes(chars[i]);
            else
                chars[i] = HtmlHelper.StripAllTags(chars[i]);
            chars[i] = WebUtility.HtmlDecode(chars[i]);
        }
    }

    public List<string> ColumnValues(int dxColumn, bool normalizeValuesInColumn, bool removeAlsoInnerHtmlOfSubNodes,
        bool skipFirstRow)
    {
        var d0 = data.GetLength(0);
        var vr = new List<string>();
        var i = 0;
        if (skipFirstRow) i = 1;
        for (; i < d0; i++) vr.Add(data[i, dxColumn]);
        FinalizeColumnValues(normalizeValuesInColumn, removeAlsoInnerHtmlOfSubNodes, vr);
        return vr;
    }

    public List<string> ColumnValues(string v, bool normalizeValuesInColumn, bool removeAlsoInnerHtmlOfSubNodes)
    {
        var d0 = data.GetLength(0);
        var d1 = data.GetLength(1);
        var vr = new List<string>();
        for (var i = 0; i < d1; i++)
        {
            var nameColumn = data[0, i];
            var dxColumn = i;
            if (nameColumn == v)
                for (i = 1; i < d0; i++)
                    vr.Add(data[i, dxColumn]);
            if (vr.Count != 0) break;
        }

        FinalizeColumnValues(normalizeValuesInColumn, removeAlsoInnerHtmlOfSubNodes, vr);
        return vr;
    }

    private static void FinalizeColumnValues(bool normalizeValuesInColumn, bool removeAlsoInnerHtmlOfSubNodes,
        List<string> vr)
    {
        if (normalizeValuesInColumn || removeAlsoInnerHtmlOfSubNodes)
            NormalizeValuesInColumn(vr, removeAlsoInnerHtmlOfSubNodes);
    }
}