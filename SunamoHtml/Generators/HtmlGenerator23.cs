namespace SunamoHtml.Generators;

public partial class HtmlGenerator2 : HtmlGenerator
{
    public static string GetForUlWoCheckDuplicate(string baseAnchor, IList<string> ids, IList<string> texts)
    {
        if (baseAnchor == null) throw new ArgumentNullException(nameof(baseAnchor));
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (texts == null) throw new ArgumentNullException(nameof(texts));

        return GetForUl(baseAnchor, ids, texts, false);
    }

    // Counts in both parameters must be equal.
    public static string GetForUlWoCheckDuplicate(IList<string> anchors, IList<string> items)
    {
        if (anchors == null) throw new ArgumentNullException(nameof(anchors));
        if (items == null) throw new ArgumentNullException(nameof(items));

        return GetForUl("", anchors, items, false);
    }

    // This method is used when you don't use any links in UL.
    public static string GetForUlWoCheckDuplicate(IList<string> items)
    {
        if (items == null) throw new ArgumentNullException(nameof(items));

        var generator = new HtmlGenerator();
        for (var i = 0; i < items.Count; i++)
        {
            generator.WriteTag("li");
            generator.WriteRaw(items[i]);
            generator.TerminateTag("li");
        }

        return generator.ToString();
    }

    public static string GetUlWoCheckDuplicate(string baseAnchor, IList<string> items)
    {
        if (baseAnchor == null) throw new ArgumentNullException(nameof(baseAnchor));
        if (items == null) throw new ArgumentNullException(nameof(items));

        var generator = new HtmlGenerator();
        for (var i = 0; i < items.Count; i++)
        {
            var text = items[i];
            generator.WriteTag("li");
            generator.WriteTagWithAttrs("a", "href", baseAnchor + (i + 1).ToString(CultureInfo.InvariantCulture));
            generator.WriteRaw(text);
            generator.TerminateTag("a");
            generator.TerminateTag("li");
        }

        return generator.ToString() + "</ul>";
    }

    // Takes only one parameter, so it's without links.
    public static string GetUlWoCheckDuplicate(IList<string> list, string appendClass)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (appendClass == null) throw new ArgumentNullException(nameof(appendClass));

        return "<ul class=\"textVlevo " + appendClass + "\">" + GetForUlWoCheckDuplicate(list) + "</ul>";
    }

    public static string GetUlWoCheckDuplicate(IList<string> anchors, IList<string> texts)
    {
        if (anchors == null) throw new ArgumentNullException(nameof(anchors));
        if (texts == null) throw new ArgumentNullException(nameof(texts));

        return "<ul class=\"textVlevo\">" + GetForUlWoCheckDuplicate(anchors, texts) + "</ul>";
    }

    public static string GetOl(IList<string> possibleAnswers, bool isCheckDuplicates = false)
    {
        if (possibleAnswers == null) throw new ArgumentNullException(nameof(possibleAnswers));

        return HtmlGeneratorList.GetFor("", possibleAnswers, possibleAnswers, isCheckDuplicates, HtmlTags.Ol);
    }

    public static string GetOlWoCheckDuplicate(IList<string> anchors, IList<string> texts)
    {
        if (anchors == null) throw new ArgumentNullException(nameof(anchors));
        if (texts == null) throw new ArgumentNullException(nameof(texts));

        if (anchors.Count != texts.Count)
            throw new InvalidOperationException("Bullet points and links count differs");
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
