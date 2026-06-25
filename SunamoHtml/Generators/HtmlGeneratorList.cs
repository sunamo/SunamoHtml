namespace SunamoHtml.Generators;

public static class HtmlGeneratorList
{
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

    public static string Ul(string baseAnchor, IList<string> relativeAnchors, IList<string> titles, bool isCheckDuplicates)
    {
        return GetFor(baseAnchor, relativeAnchors, titles, isCheckDuplicates, HtmlTags.Ul);
    }

    public static string Ol(string baseAnchor, IList<string> relativeAnchors, IList<string> titles, bool isCheckDuplicates)
    {
        return GetFor(baseAnchor, relativeAnchors, titles, isCheckDuplicates, HtmlTags.Ol);
    }
}
