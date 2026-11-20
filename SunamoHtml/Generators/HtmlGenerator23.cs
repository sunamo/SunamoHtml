namespace SunamoHtml.Generators;

public partial class HtmlGenerator2 : HtmlGenerator
{
    /// <summary>
    /// Get for ul wo check duplicate.
    /// </summary>
    /// <param name="baseAnchor">The base anchor.</param>
    /// <param name="ids">The ids.</param>
    /// <param name="texts">The texts.</param>
    /// <returns>The get for ul wo check duplicate result.</returns>
    public static string GetForUlWoCheckDuplicate(string baseAnchor, IList<string> ids, IList<string> texts)
    {
        if (baseAnchor == null) throw new ArgumentNullException(nameof(baseAnchor));
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (texts == null) throw new ArgumentNullException(nameof(texts));

        return GetForUl(baseAnchor, ids, texts, false);
    }

    // Counts in both parameters must be equal.
    /// <summary>
    /// Get for ul wo check duplicate.
    /// </summary>
    /// <param name="anchors">The anchors.</param>
    /// <param name="items">The items.</param>
    /// <returns>The get for ul wo check duplicate result.</returns>
    public static string GetForUlWoCheckDuplicate(IList<string> anchors, IList<string> items)
    {
        if (anchors == null) throw new ArgumentNullException(nameof(anchors));
        if (items == null) throw new ArgumentNullException(nameof(items));

        return GetForUl("", anchors, items, false);
    }

    // This method is used when you don't use any links in UL.
    /// <summary>
    /// Get for ul wo check duplicate.
    /// </summary>
    /// <param name="items">The items.</param>
    /// <returns>The get for ul wo check duplicate result.</returns>
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

    /// <summary>
    /// Get ul wo check duplicate.
    /// </summary>
    /// <param name="baseAnchor">The base anchor.</param>
    /// <param name="items">The items.</param>
    /// <returns>The get ul wo check duplicate result.</returns>
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
    /// <summary>
    /// Get ul wo check duplicate.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="appendClass">The append class.</param>
    /// <returns>The get ul wo check duplicate result.</returns>
    public static string GetUlWoCheckDuplicate(IList<string> list, string appendClass)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (appendClass == null) throw new ArgumentNullException(nameof(appendClass));

        return "<ul class=\"textVlevo " + appendClass + "\">" + GetForUlWoCheckDuplicate(list) + "</ul>";
    }

    /// <summary>
    /// Get ul wo check duplicate.
    /// </summary>
    /// <param name="anchors">The anchors.</param>
    /// <param name="texts">The texts.</param>
    /// <returns>The get ul wo check duplicate result.</returns>
    public static string GetUlWoCheckDuplicate(IList<string> anchors, IList<string> texts)
    {
        if (anchors == null) throw new ArgumentNullException(nameof(anchors));
        if (texts == null) throw new ArgumentNullException(nameof(texts));

        return "<ul class=\"textVlevo\">" + GetForUlWoCheckDuplicate(anchors, texts) + "</ul>";
    }

    /// <summary>
    /// Get ol.
    /// </summary>
    /// <param name="possibleAnswers">The possible answers.</param>
    /// <param name="isCheckDuplicates">The is check duplicates.</param>
    /// <returns>The get ol result.</returns>
    public static string GetOl(IList<string> possibleAnswers, bool isCheckDuplicates = false)
    {
        if (possibleAnswers == null) throw new ArgumentNullException(nameof(possibleAnswers));

        return HtmlGeneratorList.GetFor("", possibleAnswers, possibleAnswers, isCheckDuplicates, HtmlTags.Ol);
    }

    /// <summary>
    /// Get ol wo check duplicate.
    /// </summary>
    /// <param name="anchors">The anchors.</param>
    /// <param name="texts">The texts.</param>
    /// <returns>The get ol wo check duplicate result.</returns>
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
