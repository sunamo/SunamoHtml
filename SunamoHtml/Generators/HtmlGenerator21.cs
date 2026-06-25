namespace SunamoHtml.Generators;

public partial class HtmlGenerator2 : HtmlGenerator
{
    // divs stacked vertically, not ol/ul>li
    [SuppressMessage("Design", "CA1054", Justification = "initialImageUri is used as a raw HTML attribute value, not as a System.Uri")]
    public static string TopListWithImages(HtmlGenerator htmlGenerator, int widthImage, int heightImage, string initialImageUri, IList<string> photoLinks, IList<string> textLinks, IList<string> innerHtmlText, IList<string> srcPhoto, IList<string> idBadges, string arrayName)
    {
        if (htmlGenerator == null) throw new ArgumentNullException(nameof(htmlGenerator));
        if (initialImageUri == null) throw new ArgumentNullException(nameof(initialImageUri));
        if (photoLinks == null) throw new ArgumentNullException(nameof(photoLinks));
        if (textLinks == null) throw new ArgumentNullException(nameof(textLinks));
        if (innerHtmlText == null) throw new ArgumentNullException(nameof(innerHtmlText));
        if (srcPhoto == null) throw new ArgumentNullException(nameof(srcPhoto));
        if (idBadges == null) throw new ArgumentNullException(nameof(idBadges));
        if (arrayName == null) throw new ArgumentNullException(nameof(arrayName));

        var count = photoLinks.Count;
        if (count == 0)
            return "";
        if (count != textLinks.Count)
            throw new InvalidOperationException("Method HtmlGenerator2.TopListWithImages - photoLinks count does not match textLinks count");
        if (count != innerHtmlText.Count)
            throw new InvalidOperationException("Method HtmlGenerator2.TopListWithImages - photoLinks count does not match innerHtmlText count");
        if (count != srcPhoto.Count)
            throw new InvalidOperationException("Method HtmlGenerator2.TopListWithImages - photoLinks count does not match srcPhoto count");
        if (count != idBadges.Count)
            throw new InvalidOperationException(Translate.FromKey(XlfKeys.MetodaHtmlGenerator2TopListWithImagesOdkazyPhoto) + " " + count + " does not match idBadges count " + idBadges.Count);

        var isAnimated = int.TryParse(srcPhoto[0], out _);
        for (var i = 0; i < count; i++)
        {
            htmlGenerator.WriteTagWithAttrs("div", "style", "padding: 5px;", "class", "hoverable");
            htmlGenerator.WriteTagWithAttrs("a", "href", photoLinks[i]);
            htmlGenerator.WriteTagWithAttrs("div", "style", "display: inline-block;", "id", "iosBadge" + idBadges[i], "class", "iosbRepair");
            if (isAnimated)
                htmlGenerator.WriteNonPairTagWithAttrs("img", "style", "margin-left: auto; margin-right: auto; vertical-align-middle; width: " + widthImage + "px;height:" + heightImage + "px", "id", arrayName + srcPhoto[i], "class", "alternatingImage", "src", initialImageUri, HtmlAttrs.Alt, textLinks[i]);
            else
                htmlGenerator.WriteNonPairTagWithAttrs("img", "src", srcPhoto[i], HtmlAttrs.Alt, textLinks[i]);
            htmlGenerator.TerminateTag("div");
            htmlGenerator.TerminateTag("a");
            htmlGenerator.WriteTagWithAttrs("a", "href", textLinks[i]);
            htmlGenerator.WriteRaw(innerHtmlText[i]);
            htmlGenerator.TerminateTag("a");
            htmlGenerator.TerminateTag("div");
        }

        return htmlGenerator.ToString();
    }

    public static string GetForUlWCheckDuplicate(IList<string> items)
    {
        if (items == null) throw new ArgumentNullException(nameof(items));

        var generator = new HtmlGenerator();
        var alreadyWritten = new List<string>();
        for (var i = 0; i < items.Count; i++)
        {
            var text = items[i];
            if (!alreadyWritten.Contains(text))
            {
                alreadyWritten.Add(text);
                generator.WriteTag("li");
                generator.WriteRaw(text);
                generator.TerminateTag("li");
            }
        }

        return generator.ToString();
    }

    public static string Success(string message)
    {
        if (message == null) throw new ArgumentNullException(nameof(message));

        var generator = new HtmlGenerator();
        generator.WriteTagWithAttrs("div", "class", "success");
        generator.WriteRaw(message);
        generator.TerminateTag("div");
        return generator.ToString();
    }

    // Provide URL without https://, it will be added to the link automatically, but not to the text.
    [SuppressMessage("Design", "CA1054", Justification = "url is used as a raw HTML attribute value, not as a System.Uri")]
    public static string Anchor(string url)
    {
        if (url == null) throw new ArgumentNullException(nameof(url));

        if (url.Contains("=\"", StringComparison.Ordinal))
            return url;
        var httpUrl = UH.AppendHttpIfNotExists(url);
        return "<a href=\"" + httpUrl + "\"" + ">" + url + "</a>";
    }

    public static string AnchorMailto(string email)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));

        return "<a href=\"mailto:" + email + ">" + email + "</a>";
    }

    // URL without http:// / https:// will have it added to the link.
    // The text will always have http:// removed if present.
    [SuppressMessage("Design", "CA1054", Justification = "url is used as a raw HTML attribute value, not as a System.Uri")]
    public static string AnchorWithHttp(string url)
    {
        if (url == null) throw new ArgumentNullException(nameof(url));

        // First add if not present
        if (!url.StartsWith("http", StringComparison.Ordinal))
            url = "http://" + url;
        var httpUrl = url;
        // Then remove to avoid duplication in display text
        var displayText = new Regex("https://").Replace(url, "", 1);
        return "<a href=\"" + httpUrl + "\"" + ">" + displayText + "</a>";
    }

    [SuppressMessage("Design", "CA1054", Justification = "url is used as a raw HTML attribute value, not as a System.Uri")]
    public static string AnchorWithHttp(string url, string text)
    {
        if (url == null) throw new ArgumentNullException(nameof(url));
        if (text == null) throw new ArgumentNullException(nameof(text));

        var httpUrl = UH.AppendHttpIfNotExists(url);
        return "<a href=\"" + httpUrl + "\"" + ">" + text + "</a>";
    }

    [SuppressMessage("Design", "CA1054", Justification = "url is used as a raw HTML attribute value, not as a System.Uri")]
    public static string AnchorWithHttp(bool isTargetBlank, string url, string text)
    {
        if (url == null) throw new ArgumentNullException(nameof(url));
        if (text == null) throw new ArgumentNullException(nameof(text));

        string httpUrl = UH.AppendHttpIfNotExists(url);
        return AnchorWithHttpCore(isTargetBlank, text, httpUrl);
    }

    [SuppressMessage("Design", "CA1054", Justification = "httpUrl is used as a raw HTML attribute value, not as a System.Uri")]
    public static string AnchorWithHttpCore(bool isTargetBlank, string text, string httpUrl)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));
        if (httpUrl == null) throw new ArgumentNullException(nameof(httpUrl));

        if (isTargetBlank)
            return "<a href=\"" + httpUrl + "\" target=\"_blank\">" + text + "</a>";
        return "<a href=\"" + httpUrl + ">" + text + "</a>";
    }

    public static string GetRadioButtonsWoCheckDuplicate(string name, IList<string> ids, IList<string> labels)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (labels == null) throw new ArgumentNullException(nameof(labels));

        var generator = new HtmlGenerator();
        for (var i = 0; i < ids.Count; i++)
        {
            generator.WriteNonPairTagWithAttrs("input", "type", "radio", "name", name, "value", ids[i]);
            generator.WriteRaw(labels[i]);
            generator.WriteBr();
        }

        return generator.ToString();
    }

    public static string GetRadioButtonsWoCheckDuplicate(string name, IList<string> ids, IList<string> labels, string eventHandlerSelected)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (ids == null) throw new ArgumentNullException(nameof(ids));
        if (labels == null) throw new ArgumentNullException(nameof(labels));
        if (eventHandlerSelected == null) throw new ArgumentNullException(nameof(eventHandlerSelected));

        var generator = new HtmlGenerator();
        for (var i = 0; i < ids.Count; i++)
        {
            generator.WriteTagWithAttrs("input", "type", "radio", "name", name, "value", ids[i], "onclick", eventHandlerSelected);
            generator.WriteRaw(labels[i]);
            generator.WriteBr();
        }

        return generator.ToString();
    }

    // For jquery.tagcloud.js
    public static string GetWordsForTagCloud(Dictionary<string, short> wordCount, string cssSelector)
    {
        if (wordCount == null) throw new ArgumentNullException(nameof(wordCount));
        if (cssSelector == null) throw new ArgumentNullException(nameof(cssSelector));

        var onClickHandler = "AfterWordCloudClick";
        return GetWordsForTagCloud(wordCount, onClickHandler, cssSelector);
    }

    public static string GetWordsForTagCloudManageTags(Dictionary<string, short> wordCount, string cssSelector)
    {
        if (wordCount == null) throw new ArgumentNullException(nameof(wordCount));
        if (cssSelector == null) throw new ArgumentNullException(nameof(cssSelector));

        var onClickHandler = "AfterWordCloudClick2";
        return GetWordsForTagCloud(wordCount, onClickHandler, cssSelector);
    }

    private static string GetWordsForTagCloud(Dictionary<string, short> wordCount, string onClickHandler, string cssSelector)
    {
        var generator = new HtmlGenerator();
        foreach (var item in wordCount)
        {
            var withoutSpaces = item.Key.Replace(" ", "", StringComparison.Ordinal);
            generator.WriteTagWithAttrs("a", "id", "tag" + withoutSpaces, "href", "javascript:" + cssSelector + onClickHandler + "($('#tag" + withoutSpaces + "'), '" + item.Key + "');", "rel", item.Value.ToString(CultureInfo.InvariantCulture));
            generator.WriteRaw(SH.FirstCharOfEveryWordUpperDash(item.Key));
            generator.TerminateTag("a");
            generator.WriteRaw(" &nbsp; ");
        }

        return generator.ToString();
    }

    public void Detail(string name, object value)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        WriteRaw("<b>" + name + ":</b> " + value);
        WriteBr();
    }

    public void DetailIfNotEmpty(string name, string value)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        if (!string.IsNullOrEmpty(value))
        {
            WriteRaw("<b>" + name + ":</b> " + value);
            WriteBr();
        }
    }

    public static string DetailStatic(string name, object value)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        return "<b>" + name + ":</b> " + value + "<br />";
    }

    public static string DetailStatic(string color, string name, object value)
    {
        if (color == null) throw new ArgumentNullException(nameof(color));
        if (name == null) throw new ArgumentNullException(nameof(name));

        return "<div style='color:" + color + "'><b>" + name + ":</b> " + value + "</div>";
    }

    public static string ShortForLettersCount(string text, int maxLength)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        text = text.Replace(" ", "", StringComparison.Ordinal);
        if (text.Length > maxLength)
        {
            var shortened = SH.ShortForLettersCount(text, maxLength);
            shortened += "<span static class='showonhover'><a href='#'> ... </a><span static class='hovertext'>" + new Regex(shortened).Replace(text, "", 1) + "</span></span>";
            return shortened;
        }

        return text;
    }

    public static string LiI(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        return "<li><i>" + text + "</i></li>";
    }
}
