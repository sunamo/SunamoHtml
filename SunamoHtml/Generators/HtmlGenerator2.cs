namespace SunamoHtml.Generators;

public partial class HtmlGenerator2 : HtmlGenerator
{
    public static string Calendar(IList<string> htmlBoxesEveryDay, int year, int month)
    {
        if (htmlBoxesEveryDay == null) throw new ArgumentNullException(nameof(htmlBoxesEveryDay));
        var colors = new List<string>(htmlBoxesEveryDay.Count);
        foreach (var item in htmlBoxesEveryDay)
            colors.Add(null!);
        return Calendar(htmlBoxesEveryDay, colors, year, month);
    }

    public static string GenerateHtmlCheckBoxesFromFiles(string path, string searchPattern, SearchOption searchOption)
    {
        var generator = new HtmlGenerator();
        var files = Directory.GetFiles(path, searchPattern, searchOption);
        foreach (var item in files)
        {
            generator.WriteTagWithAttrs("input", "type", "checkbox");
            generator.WriteRaw(Path.GetFileName(item));
            generator.WriteBr();
        }

        return generator.ToString();
    }

    public static string Calendar(IList<string> htmlBoxesEveryDay, IList<string> colors, int year, int month)
    {
        if (htmlBoxesEveryDay == null) throw new ArgumentNullException(nameof(htmlBoxesEveryDay));
        if (colors == null) throw new ArgumentNullException(nameof(colors));
        var generator = new HtmlGenerator();
        generator.WriteTagWithAttrs("table", "class", "tabulkaNaStredAutoSirka", "style", "width: 600px");
        generator.WriteTag("tr");

        // Write header row - day names
        var daysOfWeek = DTConstants.DaysInWeekEN;
        generator.WriteTagWithAttrs("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareLeft bunkaTabulkyKalendareTop");
        generator.WriteElement("b", daysOfWeek[0]);
        generator.TerminateTag("td");
        for (var i = 1; i < daysOfWeek.Count - 1; i++)
        {
            generator.WriteTagWithAttrs("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareTop");
            generator.WriteElement("b", daysOfWeek[i]);
            generator.TerminateTag("td");
        }

        generator.WriteTagWithAttrs("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareRight bunkaTabulkyKalendareTop");
        generator.WriteElement("b", daysOfWeek[daysOfWeek.Count - 1]);
        generator.TerminateTag("td");

        generator.TerminateTag("tr");
        generator.WriteTag("tr");

        var dateTime = new DateTime(year, month, 1);
        var emptyAtStart = 0;
        var dayOfWeek = dateTime.DayOfWeek;
        switch (dayOfWeek)
        {
            case DayOfWeek.Friday:
                emptyAtStart = 4;
                break;
            case DayOfWeek.Monday:
                break;
            case DayOfWeek.Saturday:
                emptyAtStart = 5;
                break;
            case DayOfWeek.Sunday:
                emptyAtStart = 6;
                break;
            case DayOfWeek.Thursday:
                emptyAtStart = 3;
                break;
            case DayOfWeek.Tuesday:
                emptyAtStart = 1;
                break;
            case DayOfWeek.Wednesday:
                emptyAtStart = 2;
                break;
        }

        for (var emptyIndex = 0; emptyIndex < emptyAtStart; emptyIndex++)
        {
            var cellClass = "";
            if (emptyIndex == 0)
                cellClass = "bunkaTabulkyKalendareLeft";
            generator.WriteTagWithAttrs("td", "class", "bunkaTabulkyKalendare " + cellClass);
            generator.WriteRaw("&nbsp;");
            generator.TerminateTag("td");
        }

        var rowCount2 = emptyAtStart + htmlBoxesEveryDay.Count / 7;
        if (emptyAtStart != 0)
            rowCount2++;
        var initialEmptyCount = emptyAtStart;
        var currentRow = 1;
        for (var i = 1; i < htmlBoxesEveryDay.Count + 1; i++, emptyAtStart++)
        {
            var additionalClass = "";
            if (emptyAtStart % 7 == 0)
            {
                additionalClass = "bunkaTabulkyKalendareLeft";
                currentRow++;
                generator.TerminateTag("tr");
                generator.WriteTag("tr");
            }
            else if (emptyAtStart % 7 == 6)
            {
                additionalClass = "bunkaTabulkyKalendareRight";
            }

            var color = colors[i - 1];
            var appendStyle = "";
            if (color == "#030")
                appendStyle = "color:white;";
            var dateText = i + "." + month + ".";
            generator.WriteTagWithAttrs("td", "class", "tableCenter bunkaTabulkyKalendare " + additionalClass, "style", appendStyle + "background-color:" + colors[i - 1]);
            generator.WriteRaw("<b>" + dateText + "</b>");
            generator.WriteBr();
            generator.WriteRaw(htmlBoxesEveryDay[i - 1]);
            generator.TerminateTag("td");
        }

        if (initialEmptyCount == 0)
            currentRow--;
        var remainingCells = currentRow * 7 - initialEmptyCount - htmlBoxesEveryDay.Count;
        for (var emptyIndex = 0; emptyIndex < remainingCells; emptyIndex++)
        {
            var cellClass = "";
            if (remainingCells - 1 == emptyIndex)
                cellClass = "bunkaTabulkyKalendareRight";
            generator.WriteTagWithAttrs("td", "class", "bunkaTabulkyKalendare " + cellClass);
            generator.WriteRaw("&nbsp;");
            generator.TerminateTag("td");
        }

        generator.TerminateTag("tr");
        generator.TerminateTag("table");
        return generator.ToString();
    }

    public static string GalleryZoomInProfilePhoto(IList<string> memberNames, IList<string> memberProfilePictures, IList<string> memberAnchors)
    {
        if (memberNames == null) throw new ArgumentNullException(nameof(memberNames));
        if (memberProfilePictures == null) throw new ArgumentNullException(nameof(memberProfilePictures));
        if (memberAnchors == null) throw new ArgumentNullException(nameof(memberAnchors));
        var generator = new HtmlGenerator();
        generator.WriteTag("ul");
        for (var i = 0; i < memberNames.Count; i++)
        {
            generator.WriteTag("li");
            generator.WriteTagWithAttrs("a", "href", memberAnchors[i]);
            generator.WriteTag("p");
            generator.WriteRaw(memberNames[i]);
            generator.TerminateTag("p");
            generator.WriteTagWithAttrs("div", "style", "background-image: url(" + memberProfilePictures[i] + ");");
            generator.TerminateTag("div");
            generator.TerminateTag("a");
            generator.TerminateTag("li");
        }

        generator.TerminateTag("ul");
        return generator.ToString();
    }

    public static string GetSelect(string id, object defaultValue, IList list)
    {
        if (defaultValue == null) throw new ArgumentNullException(nameof(defaultValue));
        if (list == null) throw new ArgumentNullException(nameof(list));
        var generator = new HtmlGenerator();
        generator.WriteTagWithAttrs("select", "name", "select" + id);
        foreach (var item2 in list)
        {
            var item = item2?.ToString() ?? string.Empty;
            if (item != defaultValue.ToString())
            {
                generator.WriteElement("option", item);
            }
            else
            {
                generator.WriteTagWithAttrs("option", "selected", "selected");
                generator.WriteRaw(item);
                generator.TerminateTag("option");
            }
        }

        generator.TerminateTag("select");
        return generator.ToString();
    }

    public static string GetInputText(string id, string value)
    {
        var generator = new HtmlGenerator();
        generator.WriteTagWithAttrs("input", "type", "text", "name", "inputText" + id, "value", value);
        return generator.ToString();
    }

    // divs stacked vertically, not ol/ul>li
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1054")]
    public static string TopListWithImages(HtmlGenerator htmlGenerator, int widthImage, int heightImage, string initialImageUri, IList<string> photoLinks, IList<string> textLinks, IList<string> innerHtmlText, IList<string> srcPhoto, string arrayName)
    {
        if (htmlGenerator == null) throw new ArgumentNullException(nameof(htmlGenerator));
        if (photoLinks == null) throw new ArgumentNullException(nameof(photoLinks));
        if (textLinks == null) throw new ArgumentNullException(nameof(textLinks));
        if (innerHtmlText == null) throw new ArgumentNullException(nameof(innerHtmlText));
        if (srcPhoto == null) throw new ArgumentNullException(nameof(srcPhoto));
        var count = photoLinks.Count;
        if (count == 0)
            return "";
        if (count != textLinks.Count)
            throw new ArgumentException("Method HtmlGenerator2.TopListWithImages - photoLinks count does not match textLinks count");
        if (count != innerHtmlText.Count)
            throw new ArgumentException("Method HtmlGenerator2.TopListWithImages - photoLinks count does not match innerHtmlText count");
        if (count != srcPhoto.Count)
            throw new ArgumentException("Method HtmlGenerator2.TopListWithImages - photoLinks count does not match srcPhoto count");

        var isAnimated = int.TryParse(srcPhoto[0], out _);
        for (var i = 0; i < count; i++)
        {
            htmlGenerator.WriteTagWithAttrs("div", "style", "padding: 5px;");
            htmlGenerator.WriteTagWithAttrs("a", "href", photoLinks[i]);
            htmlGenerator.WriteTagWithAttrs("div", "style", "display: inline-block;");
            if (isAnimated)
                htmlGenerator.WriteNonPairTagWithAttrs("img", "style", "margin-left: auto; margin-right: auto; vertical-align-middle; width: " + widthImage + "px;height:" + heightImage + "px", "id", arrayName + srcPhoto[i], "class", "alternatingImage", "src", initialImageUri, "alt", textLinks[i]);
            else
                htmlGenerator.WriteNonPairTagWithAttrs("img", "src", srcPhoto[i], "alt", textLinks[i]);
            htmlGenerator.TerminateTag("div");
            htmlGenerator.TerminateTag("a");
            htmlGenerator.WriteTagWithAttrs("a", "href", textLinks[i]);
            htmlGenerator.WriteRaw(innerHtmlText[i]);
            htmlGenerator.TerminateTag("a");
            htmlGenerator.TerminateTag("div");
        }

        return htmlGenerator.ToString();
    }
}
