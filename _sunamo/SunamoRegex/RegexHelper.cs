namespace SunamoHtml._sunamo.SunamoRegex;

/// <summary>
///     Most NotTranslateAble class due to many regex and duplicated \
/// </summary>
internal static class RegexHelper
{
    internal static Regex rHtmlScript =
        new(@"<script[^>]*>[\s\S]*?</script>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    internal static Regex rHtmlComment = new(@"<!--[^>]*>[\s\S]*?-->", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    internal static Regex rYtVideoLink = new("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)",
        RegexOptions.Compiled);

    internal static Regex rBrTagCaseInsensitive = new(@"<br\s*/?>");

    internal static Regex rUri = new(@"(https?://[^\s]+)");

    //static Regex rUriOnlyOutsideTags = new Regex("https?:\/\/[^\s]*|<\/?\w+\b(?=\s|>)(?:='[^']*'|="[^ "]*" |=[^ '"][^\s>]*|[^>])*>|\&nbsp;John|(John)/gi");
    //static Regex rUriOnlyOutsideTags = new Regex("(text|simple)(?![^<]*>|[^<>]*</)");
    // cant compile
    //static Regex rHtmlTag = new Regex(@"(?<==)["']?((?:.(?!["']?\\s+(?:\S+)=|[>"']))+.)["']?");
    internal static Regex rHtmlTag = new("<\\s*([A-Za-z])*?[^>]*/?>");
    internal static Regex rgColor6 = new(@"^(?:[0-9a-fA-F]{3}){1,2}$");
    internal static Regex rgColor8 = new(@"^(?:[0-9a-fA-F]{3}){1,2}(?:[0-9a-fA-F]){2}$");
    internal static Regex rPreTagWithContent = new(@"<\s*pre[^>]*>(.*?)<\s*/\s*pre>", RegexOptions.Multiline);

    internal static Regex isGuid =
        new(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
            RegexOptions.Compiled);

    internal static Regex rImgTag = new(@"<img\s+([^>]*)(.*?)[^>]*>");
    internal static Regex rWpImgThumbnail = new(@"(https?:\/\/([^\s]+)-([0-9]*)x([0-9]*).jpg)");
    internal static Regex rNonPairXmlTagsUnvalid = new("<(?:\"[^\"]*\"['\"]*|'[^']*'['\"]*|[^'\">])+>");
    internal static readonly Regex rWhitespace = new(@"\s+");
    internal static string lastTelephone;

    static RegexHelper()
    {
        // this one I unfortunately cant use because I use .net core 2.0, available from 2.1
        // from https://haacked.com/archive/2004/10/25/usingregularexpressionstomatchhtml.aspx/ and https://gist.github.com/Haacked/7729259
        ////HtmlTagRegex.
        //RegexCompilationInfo[] compInfo =
        //{
        //        //HtmlTag Regex.
        //        new RegexCompilationInfo
        //        (
        //            @"<"
        //            +    @"(?<endTag>/)?"    //Captures the / if this is an end tag.
        //            +    @"(?<tagname>\w+)"    //Captures TagName
        //            +    @"("                //Groups tag contents
        //            +        @"(\s+"            //Groups attributes
        //            +            @"(?<attName>\w+)"  //Attribute name
        //            +            @"("                //groups =value portion.
        //            +                @"\s*=\s*"            // =
        //            +                @"(?:"        //Groups attribute "value" portion.
        //            +                    @"""(?<attVal>[^""]*)"""    // attVal='double quoted'
        //            +                    @"|'(?<attVal>[^']*)'"        // attVal='single quoted'
        //            +                    @"|(?<attVal>[^'"">\s]+)"    // attVal=urlnospaces
        //            +                @")"
        //            +            @")?"        //end optional att value portion.
        //            +        @")+\s*"        //One or more attribute pairs
        //            +        @"|\s*"            //Some white space.
        //            +    @")"
        //            + @"(?<completeTag>/)?>" //Captures the "/" if this is a complete tag.
        //            , RegexOptions.IgnoreCase
        //            , "HtmlTagRegex"
        //            , "Haack.RegularExpressions"
        //            , true
        //        )
        //        ,
        //        // Matches double words.
        //        new RegexCompilationInfo
        //        (
        //            @"\b(\w+)\s+\1\b"
        //            , RegexOptions.None
        //            , "DoubleWordRegex"
        //            , "Haack.RegularExpressions", true
        //        )
        //    };
        //AssemblyName assemblyName = new AssemblyName();
        //assemblyName.Name = "Haack.RegularExpressions";
        //assemblyName.Version = new Version("1.0.0.0");
        //Regex.CompileToAssembly(compInfo, assemblyName);
    }




    
    internal static bool IsUri(string text)
    {
        return rUri.IsMatch(text) && (text.StartsWith("http://") || text.StartsWith("https://"));
    }



    internal static string SanitizePhone(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return s;
        s = s.Replace(" ", "");
        if (!s.StartsWith("+")) s = "+420" + s;
        return s;
    }
}