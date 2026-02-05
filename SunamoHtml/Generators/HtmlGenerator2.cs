namespace SunamoHtml.Generators;

/// <summary>
/// Extended HTML generator with calendar, gallery, and form generation methods.
/// </summary>
public partial class HtmlGenerator2 : HtmlGenerator
{
    /// <summary>
    /// Generates a calendar for the specified year and month with HTML boxes for each day.
    /// </summary>
    /// <param name="htmlBoxesEveryDay">List of HTML content for each day.</param>
    /// <param name="year">The year.</param>
    /// <param name="month">The month (1-12).</param>
    /// <returns>HTML string representing the calendar.</returns>
    public static string Calendar(List<string> htmlBoxesEveryDay, int year, int month)
    {
        var colors = new List<string>(htmlBoxesEveryDay.Count);
        foreach (var item in htmlBoxesEveryDay)
            colors.Add(null!);
        return Calendar(htmlBoxesEveryDay, colors, year, month);
    }

    /// <summary>
    /// Generates HTML checkboxes from files in the specified directory.
    /// </summary>
    /// <param name="path">The directory path.</param>
    /// <param name="searchPattern">The search pattern for files.</param>
    /// <param name="searchOption">The search option.</param>
    /// <returns>HTML string with checkboxes.</returns>
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

    /// <summary>
    /// Generates a calendar with colored boxes for each day.
    /// </summary>
    /// <param name="htmlBoxesEveryDay">List of HTML content for each day.</param>
    /// <param name="colors">List of background colors for each day.</param>
    /// <param name="year">The year.</param>
    /// <param name="month">The month (1-12).</param>
    /// <returns>HTML string representing the calendar.</returns>
    public static string Calendar(List<string> htmlBoxesEveryDay, List<string> colors, int year, int month)
    {
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

    /// <summary>
    /// Generates a photo gallery with zoom functionality.
    /// </summary>
    /// <param name="memberNames">List of member names.</param>
    /// <param name="memberProfilePictures">List of profile picture URLs.</param>
    /// <param name="memberAnchors">List of anchor URLs.</param>
    /// <returns>HTML string representing the gallery.</returns>
    public static string GalleryZoomInProfilePhoto(List<string> memberNames, List<string> memberProfilePictures, List<string> memberAnchors)
    {
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

    /// <summary>
    /// Generates an HTML select element with options.
    /// </summary>
    /// <param name="id">The ID for the select element.</param>
    /// <param name="defaultValue">The default selected value.</param>
    /// <param name="list">The list of options.</param>
    /// <returns>HTML string representing the select element.</returns>
    public static string GetSelect(string id, object defaultValue, IList list)
    {
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

    /// <summary>
    /// Generates an HTML text input element.
    /// </summary>
    /// <param name="id">The ID for the input element.</param>
    /// <param name="value">The initial value.</param>
    /// <returns>HTML string representing the input element.</returns>
    public static string GetInputText(string id, string value)
    {
        var generator = new HtmlGenerator();
        generator.WriteTagWithAttrs("input", "type", "text", "name", "inputText" + id, "value", value);
        return generator.ToString();
    }

    /// <summary>
    /// Generates a top list with images (divs stacked vertically, not ol/ul>li).
    /// </summary>
    /// <param name="htmlGenerator">The HTML generator to use.</param>
    /// <param name="widthImage">Image width in pixels.</param>
    /// <param name="heightImage">Image height in pixels.</param>
    /// <param name="initialImageUri">Initial image URI.</param>
    /// <param name="photoLinks">List of photo link URLs.</param>
    /// <param name="textLinks">List of text link URLs.</param>
    /// <param name="innerHtmlText">List of inner HTML text.</param>
    /// <param name="srcPhoto">List of photo source paths.</param>
    /// <param name="arrayName">JavaScript array name.</param>
    /// <returns>HTML string representing the list with images.</returns>
    public static string TopListWithImages(HtmlGenerator htmlGenerator, int widthImage, int heightImage, string initialImageUri, List<string> photoLinks, List<string> textLinks, List<string> innerHtmlText, List<string> srcPhoto, string arrayName)
    {
        var count = photoLinks.Count;
        if (count == 0)
            return "";
        if (count != textLinks.Count)
            throw new Exception("Method HtmlGenerator2.TopListWithImages - photoLinks count does not match textLinks count");
        if (count != innerHtmlText.Count)
            throw new Exception("Method HtmlGenerator2.TopListWithImages - photoLinks count does not match innerHtmlText count");
        if (count != srcPhoto.Count)
            throw new Exception("Method HtmlGenerator2.TopListWithImages - photoLinks count does not match srcPhoto count");

        var parsedValue = 0;
        var isAnimated = int.TryParse(srcPhoto[0], out parsedValue);
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