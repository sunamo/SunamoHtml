namespace SunamoHtml.Generators;

/// <summary>
/// EN: Static class for generating HTML lists (UL/OL) with anchors and duplicate checking.
/// CZ: Statická třída pro generování HTML seznamů (UL/OL) s odkazy a kontrolou duplicit.
/// </summary>
public static class HtmlGeneratorList
{
    /// <summary>
    /// Generates an HTML list (UL or OL) with anchor links.
    /// If titles parameter is null, uses items for both href values and display text.
    /// </summary>
    /// <param name="baseAnchor">Base URL to prepend to each anchor href.</param>
    /// <param name="relativeAnchors">List of items to use as anchor href values.</param>
    /// <param name="titles">List of titles to display as link text. If null, uses items.</param>
    /// <param name="isCheckDuplicates">Whether to skip duplicate items.</param>
    /// <param name="tag">The HTML tag to use for the list (ul or ol).</param>
    /// <returns>HTML string with list and anchors.</returns>
    public static string GetFor(string baseAnchor, List<string> relativeAnchors, List<string>? titles, bool isCheckDuplicates,
        string tag)
    {
        var generator = new HtmlGenerator();
        List<string> alreadyWritten;

        if (titles == null)
            titles = relativeAnchors;

        alreadyWritten = new List<string>();
        generator.WriteTag(tag);
        for (var i = 0; i < relativeAnchors.Count; i++)
        {
            var text = relativeAnchors[i];
            if (!alreadyWritten.Contains(text))
            {
                if (isCheckDuplicates)
                    alreadyWritten.Add(text);

                generator.WriteTag("li");
                generator.WriteTagWithAttrs("a", "href", baseAnchor + relativeAnchors[i]);
                generator.WriteRaw(titles[i]);
                generator.TerminateTag("a");
                generator.TerminateTag("li");
            }
        }

        generator.TerminateTag(tag);
        return generator.ToString();
    }

    /// <summary>
    /// Generates an unordered list (UL) with anchor links.
    /// </summary>
    /// <param name="baseAnchor">Base URL to prepend to each anchor href.</param>
    /// <param name="relativeAnchors">List of items to use as anchor href values.</param>
    /// <param name="titles">List of titles to display as link text.</param>
    /// <param name="isCheckDuplicates">Whether to skip duplicate items.</param>
    /// <returns>HTML string with UL and anchors.</returns>
    public static string Ul(string baseAnchor, List<string> relativeAnchors, List<string> titles, bool isCheckDuplicates)
    {
        return GetFor(baseAnchor, relativeAnchors, titles, isCheckDuplicates, HtmlTags.Ul);
    }

    /// <summary>
    /// Generates an ordered list (OL) with anchor links.
    /// </summary>
    /// <param name="baseAnchor">Base URL to prepend to each anchor href.</param>
    /// <param name="relativeAnchors">List of items to use as anchor href values.</param>
    /// <param name="titles">List of titles to display as link text.</param>
    /// <param name="isCheckDuplicates">Whether to skip duplicate items.</param>
    /// <returns>HTML string with OL and anchors.</returns>
    public static string Ol(string baseAnchor, List<string> relativeAnchors, List<string> titles, bool isCheckDuplicates)
    {
        return GetFor(baseAnchor, relativeAnchors, titles, isCheckDuplicates, HtmlTags.Ol);
    }
}