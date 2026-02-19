namespace SunamoHtml._sunamo.SunamoRegex;

/// <summary>
///     Most NotTranslateAble class due to many regex and duplicated \
/// </summary>
internal static class RegexHelper
{
    internal static Regex RHtmlScript =
        new(@"<script[^>]*>[\s\S]*?</script>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    internal static Regex RHtmlComment = new(@"<!--[^>]*>[\s\S]*?-->", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    internal static Regex RYtVideoLink = new("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)",
        RegexOptions.Compiled);

    internal static Regex RBrTagCaseInsensitive = new(@"<br\s*/?>");

    internal static Regex RUri = new(@"(https?://[^\s]+)");

    //static Regex rUriOnlyOutsideTags = new Regex("https?:\/\/[^\s]*|<\/?\w+\b(?=\s|>)(?:='[^']*'|="[^ "]*" |=[^ '"][^\s>]*|[^>])*>|\&nbsp;John|(John)/gi");
    //static Regex rUriOnlyOutsideTags = new Regex("(text|simple)(?![^<]*>|[^<>]*</)");
    // cant compile
    //static Regex rHtmlTag = new Regex(@"(?<==)["']?((?:.(?!["']?\\s+(?:\S+)=|[>"']))+.)["']?");
    internal static Regex RHtmlTag = new("<\\s*([A-Za-z])*?[^>]*/?>");
    internal static Regex RgColor6 = new(@"^(?:[0-9a-fA-F]{3}){1,2}$");
    internal static Regex RgColor8 = new(@"^(?:[0-9a-fA-F]{3}){1,2}(?:[0-9a-fA-F]){2}$");
    internal static Regex RPreTagWithContent = new(@"<\s*pre[^>]*>(.*?)<\s*/\s*pre>", RegexOptions.Multiline);

    internal static Regex IsGuid =
        new(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
            RegexOptions.Compiled);

    internal static Regex RImgTag = new(@"<img\s+([^>]*)(.*?)[^>]*>");
    internal static Regex RWpImgThumbnail = new(@"(https?:\/\/([^\s]+)-([0-9]*)x([0-9]*).jpg)");
    internal static Regex RNonPairXmlTagsUnvalid = new("<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");
    internal static readonly Regex RWhitespace = new(@"\s+");
    internal static string LastTelephone = string.Empty;

    internal static bool IsUri(string text)
    {
        return RUri.IsMatch(text) && (text.StartsWith("http://", StringComparison.Ordinal) || text.StartsWith("https://", StringComparison.Ordinal));
    }

}