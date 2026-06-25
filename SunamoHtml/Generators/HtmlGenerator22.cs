namespace SunamoHtml.Generators;

public partial class HtmlGenerator2 : HtmlGenerator
{
    public static string GetForCheckBoxListWoCheckDuplicate(string idClassCheckbox, string idClassSpan, IList<string> idCheckBoxes, IList<string> list)
    {
        if (idClassCheckbox == null) throw new ArgumentNullException(nameof(idClassCheckbox));
        if (idClassSpan == null) throw new ArgumentNullException(nameof(idClassSpan));
        if (idCheckBoxes == null) throw new ArgumentNullException(nameof(idCheckBoxes));
        if (list == null) throw new ArgumentNullException(nameof(list));

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

    public static string HtmlGeneratorToString(Action<HtmlGenerator> action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        var generator = new HtmlGenerator();
        action.Invoke(generator);
        return generator.ToString();
    }

    public static string Italic(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        return "<i>" + text + "</i>";
    }

    public static void ButtonDelete(HtmlGenerator htmlGenerator, string text, string attributeName, string attributeValue)
    {
        if (htmlGenerator == null) throw new ArgumentNullException(nameof(htmlGenerator));
        if (text == null) throw new ArgumentNullException(nameof(text));
        if (attributeName == null) throw new ArgumentNullException(nameof(attributeName));
        if (attributeValue == null) throw new ArgumentNullException(nameof(attributeValue));

        htmlGenerator.WriteTagWithAttrs("button", attributeName, attributeValue);
        htmlGenerator.WriteTagWithAttrs("i", "class", "icon-remove");
        htmlGenerator.TerminateTag("i");
        htmlGenerator.WriteRaw(text);
        htmlGenerator.TerminateTag("button");
    }

    public static string Bold(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        return "<b>" + text + "</b>";
    }

    [SuppressMessage("Design", "CA1054", Justification = "uri is used as a raw HTML attribute value, not as a System.Uri")]
    public static string AnchorWithCustomLabel(string uri, string text)
    {
        if (uri == null) throw new ArgumentNullException(nameof(uri));
        if (text == null) throw new ArgumentNullException(nameof(text));

        return "<a href=\"" + uri + ">" + text + "</a>";
    }

    public static string AllMonthsTable(IList<string> allYearsHtmlBoxes, IList<string> allMonthsBoxColors)
    {
        if (allYearsHtmlBoxes == null) throw new ArgumentNullException(nameof(allYearsHtmlBoxes));
        if (allMonthsBoxColors == null) throw new ArgumentNullException(nameof(allMonthsBoxColors));

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

    public static string AllYearsTable(IList<string> years, IList<string> allYearsHtmlBoxes, IList<string> allYearsBoxColors)
    {
        if (years == null) throw new ArgumentNullException(nameof(years));
        if (allYearsHtmlBoxes == null) throw new ArgumentNullException(nameof(allYearsHtmlBoxes));
        if (allYearsBoxColors == null) throw new ArgumentNullException(nameof(allYearsBoxColors));

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

    public static string GenerateTreeWithCheckBoxes(NTreeHtml<string> tree)
    {
        if (tree == null) throw new ArgumentNullException(nameof(tree));

        var generator = new HtmlGenerator();
        var depth = 0;
        AddTree(ref depth, generator, tree);
        return generator.ToString();
    }

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

    public static string CheckBox(string data)
    {
        if (!string.IsNullOrEmpty(data))
            return "<input type=\"checkbox\" />" + data + "<br />";
        return string.Empty;
    }

    // When URI args and titles are the same.
    public static string GetForUlWoCheckDuplicate(string baseAnchor, IList<string> items)
    {
        if (baseAnchor == null) throw new ArgumentNullException(nameof(baseAnchor));
        if (items == null) throw new ArgumentNullException(nameof(items));

        return GetForUl(baseAnchor, items, items, false);
    }

    public static string GetForUlWoCheckDuplicate(string baseAnchor, IList<string> ids, string findInText, string replaceInText, string suffix = "")
    {
        if (baseAnchor == null) throw new ArgumentNullException(nameof(baseAnchor));
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (findInText == null) throw new ArgumentNullException(nameof(findInText));
        if (replaceInText == null) throw new ArgumentNullException(nameof(replaceInText));
        if (suffix == null) throw new ArgumentNullException(nameof(suffix));

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

    public static string GetForUl(string baseAnchor, string[] ids, string[] texts, bool isSkipDuplicates)
    {
        if (baseAnchor == null) throw new ArgumentNullException(nameof(baseAnchor));
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (texts == null) throw new ArgumentNullException(nameof(texts));

        return GetForUl(baseAnchor, ids.ToList(), texts.ToList(), isSkipDuplicates);
    }

    public static string GetForUl(string baseAnchor, IList<string> ids, IList<string> texts, bool isSkipDuplicates)
    {
        if (baseAnchor == null) throw new ArgumentNullException(nameof(baseAnchor));
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (texts == null) throw new ArgumentNullException(nameof(texts));

        if (ids.Count != texts.Count)
            return "Error occurred, program sent fewer elements in one array than expected for rendering";
        var generator = new HtmlGenerator();
        var displayTexts = isSkipDuplicates ? texts.Distinct().ToList() : texts;
        for (var i = 0; i < displayTexts.Count; i++)
        {
            generator.WriteTag("li");
            generator.WriteTagWithAttrs("a", "href", baseAnchor + ids[i]);
            generator.WriteRaw(displayTexts[i]);
            generator.TerminateTag("a");
            generator.TerminateTag("li");
        }

        return generator.ToString();
    }
}
