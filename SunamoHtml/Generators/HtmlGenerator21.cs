namespace SunamoHtml.Generators;

/// <summary>
/// Extended HTML generator with list, anchor, and tag cloud generation methods (part 2).
/// </summary>
public partial class HtmlGenerator2 : HtmlGenerator
{
    /// <summary>
    /// Generates a top list with images and badges (divs stacked vertically, not ol/ul>li).
    /// </summary>
    /// <param name="htmlGenerator">The HTML generator to use.</param>
    /// <param name="widthImage">Image width in pixels.</param>
    /// <param name="heightImage">Image height in pixels.</param>
    /// <param name="initialImageUri">Initial image URI.</param>
    /// <param name="photoLinks">List of photo link URLs.</param>
    /// <param name="textLinks">List of text link URLs.</param>
    /// <param name="innerHtmlText">List of inner HTML text.</param>
    /// <param name="srcPhoto">List of photo source paths.</param>
    /// <param name="idBadges">List of badge IDs.</param>
    /// <param name="nameJsArray">JavaScript array name.</param>
    /// <returns>HTML string representing the list with images and badges.</returns>
    public static string TopListWithImages(HtmlGenerator htmlGenerator, int widthImage, int heightImage, string initialImageUri, List<string> photoLinks, List<string> textLinks, List<string> innerHtmlText, List<string> srcPhoto, List<string> idBadges, string nameJsArray)
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
        if (count != idBadges.Count)
            throw new Exception(Translate.FromKey(XlfKeys.MetodaHtmlGenerator2TopListWithImagesOdkazyPhoto) + " " + count + " does not match idBadges count " + idBadges.Count);

        var parsedValue = 0;
        var isAnimated = int.TryParse(srcPhoto[0], out parsedValue);
        for (var i = 0; i < count; i++)
        {
            htmlGenerator.WriteTagWithAttrs("div", "style", "padding: 5px;", "class", "hoverable");
            htmlGenerator.WriteTagWithAttrs("a", "href", photoLinks[i]);
            htmlGenerator.WriteTagWithAttrs("div", "style", "display: inline-block;", "id", "iosBadge" + idBadges[i], "class", "iosbRepair");
            if (isAnimated)
                htmlGenerator.WriteNonPairTagWithAttrs("img", "style", "margin-left: auto; margin-right: auto; vertical-align-middle; width: " + widthImage + "px;height:" + heightImage + "px", "id", nameJsArray + srcPhoto[i], "class", "alternatingImage", "src", initialImageUri, HtmlAttrs.alt, textLinks[i]);
            else
                htmlGenerator.WriteNonPairTagWithAttrs("img", "src", srcPhoto[i], HtmlAttrs.alt, textLinks[i]);
            htmlGenerator.TerminateTag("div");
            htmlGenerator.TerminateTag("a");
            htmlGenerator.WriteTagWithAttrs("a", "href", textLinks[i]);
            htmlGenerator.WriteRaw(innerHtmlText[i]);
            htmlGenerator.TerminateTag("a");
            htmlGenerator.TerminateTag("div");
        }

        return htmlGenerator.ToString();
    }

    /// <summary>
    /// Generates list items for UL with duplicate checking.
    /// </summary>
    /// <param name="items">List of items to generate.</param>
    /// <returns>HTML string with list items.</returns>
    public static string GetForUlWCheckDuplicate(List<string> items)
    {
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

    /// <summary>
    /// Generates a success message div.
    /// </summary>
    /// <param name="message">The success message.</param>
    /// <returns>HTML string with success div.</returns>
    public static string Success(string message)
    {
        var generator = new HtmlGenerator();
        generator.WriteTagWithAttrs("div", "class", "success");
        generator.WriteRaw(message);
        generator.TerminateTag("div");
        return generator.ToString();
    }

    /// <summary>
    /// Creates an anchor link, automatically adding http:// if not present.
    /// Provide URL without https://, it will be added to the link automatically, but not to the text.
    /// </summary>
    /// <param name="url">The URL (without http://).</param>
    /// <returns>HTML anchor string.</returns>
    public static string Anchor(string url)
    {
        if (url.Contains("=\""))
            return url;
        var httpUrl = UH.AppendHttpIfNotExists(url);
        return "<a href=\"" + httpUrl + "\"" + ">" + url + "</a>";
    }

    /// <summary>
    /// Creates a mailto anchor link.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <returns>HTML mailto anchor string.</returns>
    public static string AnchorMailto(string email)
    {
        return "<a href=\"mailto:" + email + ">" + email + "</a>";
    }

    /// <summary>
    /// Creates an anchor with HTTP protocol.
    /// URL without http:// / https:// will have it added to the link.
    /// The text will always have http:// removed if present.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>HTML anchor string.</returns>
    public static string AnchorWithHttp(string url)
    {
        // First add if not present
        if (!url.StartsWith("http"))
            url = "http://" + url;
        var httpUrl = url;
        // Then remove to avoid duplication in display text
        var displayText = new Regex("https://").Replace(url, "", 1);
        return "<a href=\"" + httpUrl + "\"" + ">" + displayText + "</a>";
    }

    /// <summary>
    /// Creates an anchor with HTTP protocol and custom text.
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <param name="text">The link text.</param>
    /// <returns>HTML anchor string.</returns>
    public static string AnchorWithHttp(string url, string text)
    {
        var httpUrl = UH.AppendHttpIfNotExists(url);
        return "<a href=\"" + httpUrl + "\"" + ">" + text + "</a>";
    }

    /// <summary>
    /// Creates an anchor with HTTP protocol and optional target blank.
    /// </summary>
    /// <param name="isTargetBlank">Whether to open in new tab.</param>
    /// <param name="url">The URL.</param>
    /// <param name="text">The link text.</param>
    /// <returns>HTML anchor string.</returns>
    public static string AnchorWithHttp(bool isTargetBlank, string url, string text)
    {
        string httpUrl = UH.AppendHttpIfNotExists(url);
        return AnchorWithHttpCore(isTargetBlank, text, httpUrl);
    }

    /// <summary>
    /// Core method for creating anchors with HTTP protocol.
    /// </summary>
    /// <param name="isTargetBlank">Whether to open in new tab.</param>
    /// <param name="text">The link text.</param>
    /// <param name="httpUrl">The HTTP URL.</param>
    /// <returns>HTML anchor string.</returns>
    public static string AnchorWithHttpCore(bool isTargetBlank, string text, string httpUrl)
    {
        if (isTargetBlank)
            return "<a href=\"" + httpUrl + "\" target=\"_blank\">" + text + "</a>";
        return "<a href=\"" + httpUrl + ">" + text + "</a>";
    }

    /// <summary>
    /// Generates radio buttons without duplicate checking.
    /// </summary>
    /// <param name="nameOfRBs">The name attribute for radio buttons.</param>
    /// <param name="ids">List of radio button IDs/values.</param>
    /// <param name="labels">List of radio button labels.</param>
    /// <returns>HTML string with radio buttons.</returns>
    public static string GetRadioButtonsWoCheckDuplicate(string nameOfRBs, List<string> ids, List<string> labels)
    {
        var generator = new HtmlGenerator();
        for (var i = 0; i < ids.Count; i++)
        {
            generator.WriteNonPairTagWithAttrs("input", "type", "radio", "name", nameOfRBs, "value", ids[i]);
            generator.WriteRaw(labels[i]);
            generator.WriteBr();
        }

        return generator.ToString();
    }

    /// <summary>
    /// Generates radio buttons with click event handler.
    /// </summary>
    /// <param name="nameOfRBs">The name attribute for radio buttons.</param>
    /// <param name="ids">List of radio button IDs/values.</param>
    /// <param name="labels">List of radio button labels.</param>
    /// <param name="eventHandlerSelected">The onclick event handler.</param>
    /// <returns>HTML string with radio buttons.</returns>
    public static string GetRadioButtonsWoCheckDuplicate(string nameOfRBs, List<string> ids, List<string> labels, string eventHandlerSelected)
    {
        var generator = new HtmlGenerator();
        for (var i = 0; i < ids.Count; i++)
        {
            generator.WriteTagWithAttrs("input", "type", "radio", "name", nameOfRBs, "value", ids[i], "onclick", eventHandlerSelected);
            generator.WriteRaw(labels[i]);
            generator.WriteBr();
        }

        return generator.ToString();
    }

    /// <summary>
    /// Generates tag cloud HTML for jquery.tagcloud.js.
    /// </summary>
    /// <param name="wordCount">Dictionary of words and their counts.</param>
    /// <param name="prefixWithDot">Prefix with dot for JavaScript method.</param>
    /// <returns>HTML string for tag cloud.</returns>
    public static string GetWordsForTagCloud(Dictionary<string, short> wordCount, string prefixWithDot)
    {
        var nameJavascriptMethod = "AfterWordCloudClick";
        return GetWordsForTagCloud(wordCount, nameJavascriptMethod, prefixWithDot);
    }

    /// <summary>
    /// Generates tag cloud HTML for manage tags page.
    /// </summary>
    /// <param name="wordCount">Dictionary of words and their counts.</param>
    /// <param name="prefixWithDot">Prefix with dot for JavaScript method.</param>
    /// <returns>HTML string for tag cloud.</returns>
    public static string GetWordsForTagCloudManageTags(Dictionary<string, short> wordCount, string prefixWithDot)
    {
        var nameJavascriptMethod = "AfterWordCloudClick2";
        return GetWordsForTagCloud(wordCount, nameJavascriptMethod, prefixWithDot);
    }

    /// <summary>
    /// Core method for generating tag cloud HTML.
    /// </summary>
    /// <param name="wordCount">Dictionary of words and their counts.</param>
    /// <param name="nameJavascriptMethod">JavaScript method name.</param>
    /// <param name="prefixWithDot">Prefix with dot for JavaScript method.</param>
    /// <returns>HTML string for tag cloud.</returns>
    private static string GetWordsForTagCloud(Dictionary<string, short> wordCount, string nameJavascriptMethod, string prefixWithDot)
    {
        var generator = new HtmlGenerator();
        foreach (var item in wordCount)
        {
            var withoutSpaces = item.Key.Replace(" ", "");
            generator.WriteTagWithAttrs("a", "id", "tag" + withoutSpaces, "href", "javascript:" + prefixWithDot + nameJavascriptMethod + "($('#tag" + withoutSpaces + "'), '" + item.Key + "');", "rel", item.Value.ToString());
            generator.WriteRaw(SH.FirstCharOfEveryWordUpperDash(item.Key));
            generator.TerminateTag("a");
            generator.WriteRaw(" &nbsp; ");
        }

        return generator.ToString();
    }

    /// <summary>
    /// Writes a detail line with name and value.
    /// </summary>
    /// <param name="name">The detail name.</param>
    /// <param name="value">The detail value.</param>
    public void Detail(string name, object value)
    {
        WriteRaw("<b>" + name + ":</b> " + value);
        WriteBr();
    }

    /// <summary>
    /// Writes a detail line with name and value if value is not empty.
    /// </summary>
    /// <param name="name">The detail name.</param>
    /// <param name="value">The detail value.</param>
    public void DetailIfNotEmpty(string name, string value)
    {
        if (value != "")
        {
            WriteRaw("<b>" + name + ":</b> " + value);
            WriteBr();
        }
    }

    /// <summary>
    /// Returns a static detail line with name and value.
    /// </summary>
    /// <param name="name">The detail name.</param>
    /// <param name="value">The detail value.</param>
    /// <returns>HTML string with detail.</returns>
    public static string DetailStatic(string name, object value)
    {
        return "<b>" + name + ":</b> " + value + "<br />";
    }

    /// <summary>
    /// Returns a static detail line with custom color.
    /// </summary>
    /// <param name="color">The text color.</param>
    /// <param name="name">The detail name.</param>
    /// <param name="value">The detail value.</param>
    /// <returns>HTML string with colored detail.</returns>
    public static string DetailStatic(string color, string name, object value)
    {
        return "<div style='color:" + color + "'><b>" + name + ":</b> " + value + "</div>";
    }

    /// <summary>
    /// Shortens text to specified letter count with ellipsis tooltip.
    /// </summary>
    /// <param name="text">The text to shorten.</param>
    /// <param name="maxLength">Maximum length in characters.</param>
    /// <returns>HTML string with shortened text and tooltip.</returns>
    public static string ShortForLettersCount(string text, int maxLength)
    {
        text = text.Replace(" ", "");
        if (text.Length > maxLength)
        {
            var shortened = SH.ShortForLettersCount(text, maxLength);
            shortened += "<span static class='showonhover'><a href='#'> ... </a><span static class='hovertext'>" + new Regex(shortened).Replace(text, "", 1) + "</span></span>";
            return shortened;
        }

        return text;
    }

    /// <summary>
    /// Wraps text in li and i tags.
    /// </summary>
    /// <param name="text">The text to wrap.</param>
    /// <returns>HTML string with li>i tags.</returns>
    public static string LiI(string text)
    {
        return "<li><i>" + text + "</i></li>";
    }
}
