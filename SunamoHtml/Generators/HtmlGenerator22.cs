namespace SunamoHtml.Generators;

/// <summary>
/// Extended HTML generator with checkbox lists, tables, and tree generation methods (part 3).
/// </summary>
public partial class HtmlGenerator2 : HtmlGenerator
{
    /// <summary>
    /// Generates checkbox list without duplicate checking.
    /// </summary>
    /// <param name="idClassCheckbox">ID class for checkboxes.</param>
    /// <param name="idClassSpan">ID class for spans.</param>
    /// <param name="idCheckBoxes">List of checkbox IDs.</param>
    /// <param name="list">List of checkbox labels.</param>
    /// <returns>HTML string with checkbox list.</returns>
    public static string GetForCheckBoxListWoCheckDuplicate(string idClassCheckbox, string idClassSpan, IList<string> idCheckBoxes, IList<string> list)
    {
        ArgumentNullException.ThrowIfNull(idClassCheckbox);
        ArgumentNullException.ThrowIfNull(idClassSpan);
        ArgumentNullException.ThrowIfNull(idCheckBoxes);
        ArgumentNullException.ThrowIfNull(list);

        var generator = new HtmlGenerator();
        if (idCheckBoxes.Count != list.Count)
            throw new InvalidOperationException("Unequal parameter count in method GetForCheckBoxListWoCheckDuplicate " + idCheckBoxes.Count + ":" + list.Count);
        for (var i = 0; i < idCheckBoxes.Count; i++)
        {
            var checkboxId = idCheckBoxes[i];
            generator.WriteNonPairTagWithAttrs("input", "type", "checkbox", "id", idClassCheckbox + checkboxId, "class", idClassCheckbox);
            generator.WriteTagWithAttrs("span", "id", idClassSpan + checkboxId, "class", idClassSpan);
            generator.WriteRaw(list[i]);
            generator.TerminateTag("span");
            generator.WriteBr();
        }

        return generator.ToString();
    }

    /// <summary>
    /// Executes HTML generator action and returns the generated string.
    /// </summary>
    /// <param name="action">The action to execute with the generator.</param>
    /// <returns>Generated HTML string.</returns>
    public static string HtmlGeneratorToString(Action<HtmlGenerator> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        var generator = new HtmlGenerator();
        action.Invoke(generator);
        var result = generator.ToString();
        return result;
    }

    /// <summary>
    /// Wraps text in italic tags.
    /// </summary>
    /// <param name="text">The text to wrap.</param>
    /// <returns>HTML string with italic tags.</returns>
    public static string Italic(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        return "<i>" + text + "</i>";
    }

    /// <summary>
    /// Generates a delete button with icon.
    /// </summary>
    /// <param name="htmlGenerator">The HTML generator.</param>
    /// <param name="text">Button text.</param>
    /// <param name="attributeName">Attribute name.</param>
    /// <param name="attributeValue">Attribute value.</param>
    public static void ButtonDelete(HtmlGenerator htmlGenerator, string text, string attributeName, string attributeValue)
    {
        ArgumentNullException.ThrowIfNull(htmlGenerator);
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(attributeName);
        ArgumentNullException.ThrowIfNull(attributeValue);

        htmlGenerator.WriteTagWithAttrs("button", attributeName, attributeValue);
        htmlGenerator.WriteTagWithAttrs("i", "class", "icon-remove");
        htmlGenerator.TerminateTag("i");
        htmlGenerator.WriteRaw(text);
        htmlGenerator.TerminateTag("button");
    }

    /// <summary>
    /// Wraps text in bold tags.
    /// </summary>
    /// <param name="text">The text to wrap.</param>
    /// <returns>HTML string with bold tags.</returns>
    public static string Bold(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        return "<b>" + text + "</b>";
    }

    /// <summary>
    /// Creates an anchor with custom label.
    /// </summary>
    /// <param name="uri">The URI.</param>
    /// <param name="text">The link text.</param>
    /// <returns>HTML anchor string.</returns>
    [SuppressMessage("Design", "CA1054", Justification = "uri is used as a raw HTML attribute value, not as a System.Uri")]
    public static string AnchorWithCustomLabel(string uri, string text)
    {
        ArgumentNullException.ThrowIfNull(uri);
        ArgumentNullException.ThrowIfNull(text);

        return "<a href=\"" + uri + ">" + text + "</a>";
    }

    /// <summary>
    /// Generates a table for all months of the year with colored boxes.
    /// </summary>
    /// <param name="allYearsHtmlBoxes">List of HTML boxes for each month (must have 12 items).</param>
    /// <param name="allMonthsBoxColors">List of colors for each month box (must have 12 items).</param>
    /// <returns>HTML string with months table.</returns>
    public static string AllMonthsTable(IList<string> allYearsHtmlBoxes, IList<string> allMonthsBoxColors)
    {
        ArgumentNullException.ThrowIfNull(allYearsHtmlBoxes);
        ArgumentNullException.ThrowIfNull(allMonthsBoxColors);

        if (allYearsHtmlBoxes.Count != 12)
            throw new InvalidOperationException("AllMonthsHtmlBoxes length is not 12.");
        if (allMonthsBoxColors.Count != 12)
            throw new InvalidOperationException("AllMonthsBoxColors length is not 12.");
        var generator = new HtmlGenerator();
        generator.WriteTagWithAttrs("table", "class", "tabulkaNaStredAutoSirka", "style", "width: 100%");
        generator.WriteTag("tr");

        // Write header row - month names
        var monthNames = DTConstants.MonthsInYearEN;
        generator.WriteTagWithAttrs("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareLeft bunkaTabulkyKalendareTop");
        generator.WriteElement("b", monthNames[0]);
        generator.TerminateTag("td");
        for (var i = 1; i < monthNames.Count - 1; i++)
        {
            generator.WriteTagWithAttrs("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareTop");
            generator.WriteElement("b", monthNames[i]);
            generator.TerminateTag("td");
        }

        generator.WriteTagWithAttrs("td", "class", "bunkaTabulkyKalendare bunkaTabulkyKalendareRight bunkaTabulkyKalendareTop");
        generator.WriteElement("b", monthNames[monthNames.Count - 1]);
        generator.TerminateTag("td");

        generator.TerminateTag("tr");
        generator.WriteTag("tr");
        for (var i = 0; i < allYearsHtmlBoxes.Count; i++)
        {
            var additionalClass = "";
            if (i == 0)
                additionalClass = "bunkaTabulkyKalendareLeft";
            else if (i == 11)
                additionalClass = "bunkaTabulkyKalendareRight";
            var color = allMonthsBoxColors[i];
            var appendStyle = "";
            if (string.Equals(color, "#030", StringComparison.Ordinal))
                appendStyle = "color:white;";
            generator.WriteTagWithAttrs("td", "class", "tableCenter bunkaTabulkyKalendare " + additionalClass, "style", appendStyle + "background-color:" + color);
            generator.WriteRaw("<b>" + allYearsHtmlBoxes[i] + "</b>");
            generator.TerminateTag("td");
        }

        generator.TerminateTag("tr");
        generator.TerminateTag("table");
        return generator.ToString();
    }

    /// <summary>
    /// Generates a table for all years with colored boxes.
    /// </summary>
    /// <param name="years">List of year labels.</param>
    /// <param name="allYearsHtmlBoxes">List of HTML boxes for each year.</param>
    /// <param name="allYearsBoxColors">List of colors for each year box.</param>
    /// <returns>HTML string with years table.</returns>
    public static string AllYearsTable(IList<string> years, IList<string> allYearsHtmlBoxes, IList<string> allYearsBoxColors)
    {
        ArgumentNullException.ThrowIfNull(years);
        ArgumentNullException.ThrowIfNull(allYearsHtmlBoxes);
        ArgumentNullException.ThrowIfNull(allYearsBoxColors);

        var yearsCount = years.Count;
        if (allYearsHtmlBoxes.Count != yearsCount)
            throw new InvalidOperationException("Element count in AllYearsHtmlBoxes is not the same as in years collection");
        if (allYearsBoxColors.Count != yearsCount)
            throw new InvalidOperationException("Element count in AllYearsBoxColors is not the same as in years collection");
        var generator = new HtmlGenerator();
        generator.WriteTagWithAttrs("table", "class", "tabulkaNaStredAutoSirka", "style", "width: 200px");

        for (var i = 0; i < yearsCount; i++)
        {
            var additionalClass = "";
            generator.WriteTag("tr");
            var topClass = "";
            if (i == 0)
                topClass = "bunkaTabulkyKalendareTop ";
            additionalClass = "bunkaTabulkyKalendareLeft";
            generator.WriteTagWithAttrs("td", "class", "tableCenter bunkaTabulkyKalendare " + topClass + additionalClass);
            generator.WriteRaw("<b>" + years[i] + "</b>");
            generator.TerminateTag("td");
            additionalClass = "bunkaTabulkyKalendareRight";
            var color = allYearsBoxColors[i];
            var appendStyle = "";
            if (string.Equals(color, "#030", StringComparison.Ordinal))
                appendStyle = "color:white;";
            generator.WriteTagWithAttrs("td", "class", "tableCenter bunkaTabulkyKalendare " + topClass + additionalClass, "style", appendStyle + "background-color:" + color);
            generator.WriteRaw(allYearsHtmlBoxes[i]);
            generator.TerminateTag("td");
        }

        generator.TerminateTag("tr");
        generator.TerminateTag("table");
        return generator.ToString();
    }

    /// <summary>
    /// Generates a tree structure with checkboxes.
    /// </summary>
    /// <param name="tree">The tree structure to generate.</param>
    /// <returns>HTML string with tree and checkboxes.</returns>
    public static string GenerateTreeWithCheckBoxes(NTreeHtml<string> tree)
    {
        ArgumentNullException.ThrowIfNull(tree);

        var generator = new HtmlGenerator();
        var depth = 0;
        AddTree(ref depth, generator, tree);
        return generator.ToString();
    }

    /// <summary>
    /// Recursively adds tree nodes with checkboxes.
    /// </summary>
    /// <param name="depth">Current tree depth.</param>
    /// <param name="htmlGenerator">The HTML generator.</param>
    /// <param name="tree">The tree node.</param>
    private static void AddTree(ref int depth, HtmlGenerator htmlGenerator, NTreeHtml<string> tree)
    {
        depth++;
        htmlGenerator.WriteTag(HtmlTags.Ol);
        htmlGenerator.WriteRaw(CheckBox(tree.Data));
        foreach (var item in tree.Children)
        {
            htmlGenerator.WriteTag(HtmlTags.Li);
            htmlGenerator.WriteRaw(CheckBox(item.Data));
            foreach (var childNode in item.Children)
                AddTree(ref depth, htmlGenerator, childNode);
            htmlGenerator.TerminateTag(HtmlTags.Li);
        }

        htmlGenerator.TerminateTag(HtmlTags.Ol);
    }

    /// <summary>
    /// Generates a checkbox with label.
    /// </summary>
    /// <param name="data">The checkbox label.</param>
    /// <returns>HTML string with checkbox.</returns>
    public static string CheckBox(string data)
    {
        if (!string.IsNullOrEmpty(data))
            return "<input type=\"checkbox\" />" + data + "<br />";
        return string.Empty;
    }

    /// <summary>
    /// Generates list items for UL without duplicate checking.
    /// When URI args and titles are the same.
    /// </summary>
    /// <param name="baseAnchor">Base anchor URL (e.g. "EditMister.aspx?mid=").</param>
    /// <param name="items">List of items (used as both IDs and display text).</param>
    /// <returns>HTML string with list items.</returns>
    public static string GetForUlWoCheckDuplicate(string baseAnchor, IList<string> items)
    {
        ArgumentNullException.ThrowIfNull(baseAnchor);
        ArgumentNullException.ThrowIfNull(items);

        return GetForUl(baseAnchor, items, items, false);
    }

    /// <summary>
    /// Generates list items for UL with text replacement.
    /// </summary>
    /// <param name="baseAnchor">Base anchor URL.</param>
    /// <param name="ids">List of IDs for anchors.</param>
    /// <param name="findInText">Text to find in display text.</param>
    /// <param name="replaceInText">Text to replace with.</param>
    /// <param name="suffix">Optional suffix to append to anchors.</param>
    /// <returns>HTML string with list items.</returns>
    public static string GetForUlWoCheckDuplicate(string baseAnchor, IList<string> ids, string findInText, string replaceInText, string suffix = "")
    {
        ArgumentNullException.ThrowIfNull(baseAnchor);
        ArgumentNullException.ThrowIfNull(ids);
        ArgumentNullException.ThrowIfNull(findInText);
        ArgumentNullException.ThrowIfNull(replaceInText);
        ArgumentNullException.ThrowIfNull(suffix);

        var generator = new HtmlGenerator();
        for (var i = 0; i < ids.Count; i++)
        {
            var text = ids[i];
            generator.WriteTag("li");
            generator.WriteTagWithAttrs("a", "href", baseAnchor + text + suffix);
            if (!string.IsNullOrEmpty(findInText) && !string.IsNullOrEmpty(replaceInText))
                generator.WriteRaw(text.Replace(findInText, replaceInText, StringComparison.Ordinal));
            else
                generator.WriteRaw(text);
            generator.TerminateTag("a");
            generator.TerminateTag("li");
        }

        return generator.ToString();
    }

    /// <summary>
    /// Generates list items for UL (array version).
    /// </summary>
    /// <param name="baseAnchor">Base anchor URL.</param>
    /// <param name="ids">Array of IDs.</param>
    /// <param name="texts">Array of display texts.</param>
    /// <param name="isSkipDuplicates">Whether to skip duplicate texts.</param>
    /// <returns>HTML string with list items.</returns>
    public static string GetForUl(string baseAnchor, string[] ids, string[] texts, bool isSkipDuplicates)
    {
        ArgumentNullException.ThrowIfNull(baseAnchor);
        ArgumentNullException.ThrowIfNull(ids);
        ArgumentNullException.ThrowIfNull(texts);

        return GetForUl(baseAnchor, ids.ToList(), texts.ToList(), isSkipDuplicates);
    }

    /// <summary>
    /// Generates list items for UL (list version).
    /// </summary>
    /// <param name="baseAnchor">Base anchor URL.</param>
    /// <param name="ids">List of IDs.</param>
    /// <param name="texts">List of display texts.</param>
    /// <param name="isSkipDuplicates">Whether to skip duplicate texts.</param>
    /// <returns>HTML string with list items.</returns>
    public static string GetForUl(string baseAnchor, IList<string> ids, IList<string> texts, bool isSkipDuplicates)
    {
        ArgumentNullException.ThrowIfNull(baseAnchor);
        ArgumentNullException.ThrowIfNull(ids);
        ArgumentNullException.ThrowIfNull(texts);

        if (ids.Count != texts.Count)
            return "Error occurred, program sent fewer elements in one array than expected for rendering";
        var generator = new HtmlGenerator();
        var displayTexts = isSkipDuplicates ? texts.Distinct().ToList() : texts;
        for (var i = 0; i < displayTexts.Count; i++)
        {
            var text = displayTexts[i];
            generator.WriteTag("li");
            generator.WriteTagWithAttrs("a", "href", baseAnchor + ids[i]);
            generator.WriteRaw(displayTexts[i]);
            generator.TerminateTag("a");
            generator.TerminateTag("li");
        }

        return generator.ToString();
    }
}
