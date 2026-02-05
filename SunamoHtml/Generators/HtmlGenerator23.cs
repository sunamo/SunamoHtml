namespace SunamoHtml.Generators;

/// <summary>
/// Extended HTML generator with list generation methods (part 4).
/// </summary>
public partial class HtmlGenerator2 : HtmlGenerator
{
    /// <summary>
    /// Generates list items for UL without duplicate checking.
    /// </summary>
    /// <param name="baseAnchor">Base anchor URL.</param>
    /// <param name="ids">List of IDs for anchors.</param>
    /// <param name="texts">List of display texts.</param>
    /// <returns>HTML string with list items.</returns>
    public static string GetForUlWoCheckDuplicate(string baseAnchor, List<string> ids, List<string> texts)
    {
        return GetForUl(baseAnchor, ids, texts, false);
    }

    /// <summary>
    /// Generates list items for UL without duplicate checking.
    /// Counts in both parameters must be equal.
    /// </summary>
    /// <param name="anchors">List of anchor URLs.</param>
    /// <param name="items">List of display texts.</param>
    /// <returns>HTML string with list items.</returns>
    public static string GetForUlWoCheckDuplicate(List<string> anchors, List<string> items)
    {
        return GetForUl("", anchors, items, false);
    }

    /// <summary>
    /// Generates list items for UL without anchors.
    /// This method is used when you don't use any links in UL.
    /// </summary>
    /// <param name="items">List of items to display.</param>
    /// <returns>HTML string with list items.</returns>
    public static string GetForUlWoCheckDuplicate(List<string> items)
    {
        var generator = new HtmlGenerator();
        for (var i = 0; i < items.Count; i++)
        {
            generator.WriteTag("li");
            generator.WriteRaw(items[i]);
            generator.TerminateTag("li");
        }

        return generator.ToString();
    }

    /// <summary>
    /// Generates complete UL element with list items.
    /// </summary>
    /// <param name="baseAnchor">Base anchor URL.</param>
    /// <param name="items">List of items.</param>
    /// <returns>HTML string with UL element.</returns>
    public static string GetUlWoCheckDuplicate(string baseAnchor, List<string> items)
    {
        HtmlGenerator generator = new HtmlGenerator();
        for (int i = 0; i < items.Count; i++)
        {
            string text = items[i];
            generator.WriteTag("li");
            generator.WriteTagWithAttrs("a", "href", baseAnchor + (i + 1).ToString());
            generator.WriteRaw(text);
            generator.TerminateTag("a");
            generator.TerminateTag("li");
        }

        return generator.ToString() + "</ul>";
    }

    /// <summary>
    /// Generates complete UL element with list items and custom class.
    /// Takes only one parameter, so it's without links.
    /// </summary>
    /// <param name="list">List of items.</param>
    /// <param name="appendClass">Additional CSS class to append.</param>
    /// <returns>HTML string with UL element.</returns>
    public static string GetUlWoCheckDuplicate(List<string> list, string appendClass)
    {
        return "<ul class=\"textVlevo " + appendClass + "\">" + GetForUlWoCheckDuplicate(list) + "</ul>";
    }

    /// <summary>
    /// Generates complete UL element with anchors and texts.
    /// </summary>
    /// <param name="anchors">List of anchor URLs.</param>
    /// <param name="texts">List of display texts.</param>
    /// <returns>HTML string with UL element.</returns>
    public static string GetUlWoCheckDuplicate(List<string> anchors, List<string> texts)
    {
        return "<ul class=\"textVlevo\">" + GetForUlWoCheckDuplicate(anchors, texts) + "</ul>";
    }

    /// <summary>
    /// Generates an OL (ordered list) element.
    /// </summary>
    /// <param name="possibleAnswers">List of possible answers.</param>
    /// <param name="isCheckDuplicates">Whether to check for duplicates.</param>
    /// <returns>HTML string with OL element.</returns>
    public static string GetOl(List<string> possibleAnswers, bool isCheckDuplicates = false)
    {
        return HtmlGeneratorList.GetFor("", possibleAnswers, possibleAnswers, isCheckDuplicates, HtmlTags.Ol);
    }

    /// <summary>
    /// Generates OL element without duplicate checking.
    /// </summary>
    /// <param name="anchors">List of anchor URLs.</param>
    /// <param name="texts">List of items to display.</param>
    /// <returns>HTML string with OL element.</returns>
    public static string GetOlWoCheckDuplicate(List<string> anchors, List<string> texts)
    {
        if (anchors.Count != texts.Count)
            throw new Exception("Bullet points and links count differs");
        var generator = new HtmlGenerator();
        generator.WriteTag("ol");
        for (var i = 0; i < texts.Count; i++)
        {
            var text = texts[i];
            generator.WriteTag("li");
            generator.WriteRaw(text);
            generator.TerminateTag("li");
        }

        generator.TerminateTag("ol");
        return generator.ToString();
    }
}