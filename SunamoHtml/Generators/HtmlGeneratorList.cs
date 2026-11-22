namespace SunamoHtml.Generators;

/// <summary>
/// Represents the html generator list type.
/// </summary>
public static class HtmlGeneratorList
{
    /// <summary>
    /// Get for.
    /// </summary>
    /// <param name="baseAnchor">The base anchor.</param>
    /// <param name="relativeAnchors">The relative anchors.</param>
    /// <param name="titles">The titles.</param>
    /// <param name="isCheckDuplicates">The is check duplicates.</param>
    /// <param name="tag">The tag.</param>
    /// <returns>The get for result.</returns>
    public static string GetFor(string baseAnchor, IList<string> relativeAnchors, IList<string>? titles, bool isCheckDuplicates,
        string tag)
    {
        if (relativeAnchors == null) throw new ArgumentNullException(nameof(relativeAnchors));
        var generator = new HtmlGenerator();

        if (titles == null)
            titles = relativeAnchors;

        var alreadyWritten = new List<string>();
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
    /// Ul.
    /// </summary>
    /// <param name="baseAnchor">The base anchor.</param>
    /// <param name="relativeAnchors">The relative anchors.</param>
    /// <param name="titles">The titles.</param>
    /// <param name="isCheckDuplicates">The is check duplicates.</param>
    /// <returns>The ul result.</returns>
    public static string Ul(string baseAnchor, IList<string> relativeAnchors, IList<string> titles, bool isCheckDuplicates)
    {
        return GetFor(baseAnchor, relativeAnchors, titles, isCheckDuplicates, HtmlTags.Ul);
    }

    /// <summary>
    /// Ol.
    /// </summary>
    /// <param name="baseAnchor">The base anchor.</param>
    /// <param name="relativeAnchors">The relative anchors.</param>
    /// <param name="titles">The titles.</param>
    /// <param name="isCheckDuplicates">The is check duplicates.</param>
    /// <returns>The ol result.</returns>
    public static string Ol(string baseAnchor, IList<string> relativeAnchors, IList<string> titles, bool isCheckDuplicates)
    {
        return GetFor(baseAnchor, relativeAnchors, titles, isCheckDuplicates, HtmlTags.Ol);
    }
}
