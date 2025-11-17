// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoHtml.Html;

public class HtmlHelperText
{
    private const string regexHtmlTag = "<[^<>]+>";

    private static Type type = typeof(HtmlHelperText);

    public static string ReplacePairTag(string input, string tag, string forWhat)
    {
        input = input.Replace("<" + tag + ">", "<" + forWhat + ">");
        input = input.Replace("<" + tag + " ", "<" + forWhat + " ");
        input = input.Replace("</" + tag + ">", "</" + forWhat + ">");
        return input;
    }

    public static string InsertMissingEndingTags(string text, string tag)
    {
        var textBuilder = new StringBuilder(text);

        var start = SH.ReturnOccurencesOfString(text, "<" + tag);
        var endingTag = "</" + tag + ">";
        var ends = SH.ReturnOccurencesOfString(text, endingTag);

        var startC = start.Count;
        var endsC = ends.Count;

        if (start.Count > ends.Count)
        {
            // In keys are start, in value end. If end isnt, then -1
            var se = new Dictionary<int, int>();

            for (var i = start.Count - 1; i >= 0; i--)
            {
                var startActual = start[i];

                var endDx = -1;
                if (ends.Count != 0) endDx = ends.Count - 1;
                var endActual = -1;
                if (endDx != -1) endActual = ends[endDx];
                if (startActual > endActual)
                {
                    se.Add(startActual, -1);
                }
                else
                {
                    se.Add(startActual, endActual);
                    ends.RemoveAt(endDx);
                }
            }

            foreach (var item in se)
                if (item.Value == -1)
                {
                    var dexEndOfStart = textBuilder.ToString().IndexOf('>', item.Key);

                    var space = textBuilder.ToString().IndexOf(' ', dexEndOfStart);

                    if (space != -1) textBuilder.Insert(space, endingTag);
                }
        }

        return textBuilder.ToString();
    }

    public static List<string> CreateH2FromNumberedList(List<string> lines)
    {
        for (var i = 0; i < lines.Count; i++) lines[i] = lines[i].Trim();
        //CA.Trim(lines);

        for (var i = 0; i < lines.Count; i++)
            if (SH.IsNumbered(lines[i]))
                lines[i] = WrapWith(lines[i], "h2");

        return lines;
    }

    public static List<string> GetAllEquvivalentsOfNonPairingTag(string v)
    {
        return new List<string>(["<" + v + ">", "<" + v + " />", "<" + v + "/>"]);
    }

    /// <summary>
    ///     A2 only name like p, title etc.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="v"></param>
    private static string WrapWith(string text, string p)
    {
        return "<" + p + ">" + text + "</" + p + ">";
    }

    public static string RemoveAllNodes(string v)
    {
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(v);

        var nodes = hd.DocumentNode.Descendants().ToList();
        for (var i = 0; i < nodes.Count(); i++)
        {
            var node = nodes[i];

            if (node.NodeType != HtmlNodeType.Text)
            {
                if (node.ParentNode.NodeType != HtmlNodeType.Document)
                    node.ParentNode.Remove();
                else
                    node.Remove();
            }
        }

        return hd.DocumentNode.OuterHtml;
    }

    public static Tuple<string, string> GetTextBetweenTags(string c2, string scriptS, string scriptE,
        bool throwExceptionIfNotContains = true)
    {
        if (scriptS.EndsWith(">"))
            return new Tuple<string, string>(SH.GetTextBetweenSimple(c2, scriptS, scriptE, throwExceptionIfNotContains),
                "");
        var sc = c2.IndexOf(scriptS);
        if (sc == -1) return new Tuple<string, string>(" ", "");

        var ending = c2.IndexOf('>', sc);
        var element = c2.IndexOf(scriptE, ending);

        var result = SH.GetTextBetweenTwoCharsInts(c2, ending, element);

        var from = sc + scriptS.Length - 1;
        var to = ending;

        var attrs = string.Empty;

        if (from < to)
        {
            attrs = SH.GetTextBetweenTwoCharsInts(c2, from, to);

            if (attrs == " scop")
            {
            }
        }

        return new Tuple<string, string>(result, attrs);
    }

    public static List<string> GetAllTags(string i)
    {
        var tags = Regex.Matches(i, regexHtmlTag);
        var sourceList = new List<string>();
        foreach (Match item in tags) sourceList.Add(item.Value);
        return sourceList;
    }

    public static string RemoveHtmlTags(string ClipboardS2)
    {
        return SHReplace.ReplaceAll(HtmlHelper.RemoveAllTags(ClipboardS2), " ", "");
    }

    public static string RemoveAspxComments(string c)
    {
        c = Regex.Replace(c, ConstsAspx.startAspxComment + ".*?" + ConstsAspx.endAspxComment, string.Empty,
            RegexOptions.Singleline);
        return c;
    }

    public static bool ContainsTag(string text, List<string> allHtmlTagsWithLeftArrow)
    {
        foreach (var item in allHtmlTagsWithLeftArrow)
            if (text.Contains(item))
                return true;
        return false;
    }

    /// <summary>
    ///     Get type of tag (paired ended, paired not ended, non paired)
    /// </summary>
    /// <param name="tag"></param>
    public static HtmlTagSyntax GetSyntax(ref string tag)
    {
        ThrowEx.InvalidParameter((string)tag, "tag");

        tag = SH.GetToFirst((string)tag, " ");
        tag = tag.Trim().TrimStart('<').TrimEnd('>').ToLower();

        if (AllLists.HtmlNonPairTags.Contains((string)tag)) return HtmlTagSyntax.NonPairingNotEnded;
        tag = tag.TrimEnd('/');
        if (AllLists.HtmlNonPairTags.Contains((string)tag)) return HtmlTagSyntax.NonPairingEnded;
        if (tag[tag.Length - 1] == '/') return HtmlTagSyntax.End;
        return HtmlTagSyntax.Start;
    }


    public static string TrimInnerOfEncodedHtml(string value)
    {
        value = SHReplace.ReplaceAll(value, "&gt;", "&gt; ");
        value = SHReplace.ReplaceAll(value, "&lt;", " &lt;");
        return value;
    }

    public static List<string> SplitBySpaceAndLtGt(string shortDescription)
    {
        var f = SHSplit.Split(shortDescription, "<", ">", " ");
        return f;
    }

    public static bool IsHtmlEntity(string i)
    {
        i = i.TrimStart('&').TrimEnd(';');
        return AllLists.htmlEntities.Contains(i);
    }

    public static List<string> GetContentOfTags(string text, string pre)
    {
        var result = new List<string>();
        var start = $"<{pre}";
        var end = $"</{pre}>";
        var dex = text.IndexOf(start);
        while (dex != -1)
        {
            var dexEndLetter = text.IndexOf('>', dex);

            var dex2 = text.IndexOf(start, dex + start.Length);
            var dexEnd = text.IndexOf(end, dex);

            if (dex2 != -1)
                if (dexEnd > dex2)
                    throw new Exception($"Another starting tag is before ending <{pre}>");

            result.Add(SH.GetTextBetweenTwoCharsInts(text, dexEndLetter, dexEnd).Trim());

            dex = text.IndexOf(start, dexEnd);
        }

        return result;
    }

    public static bool IsCssDeclarationName(string decl)
    {
        if (AllLists.allCssKeys.Contains(decl)) return true;
        return false;
    }

    public static string ConvertTextToHtml(List<string> lines)
    {
        //CA.RemoveStringsEmpty2(lines);
        lines = lines.Where(d => !string.IsNullOrWhiteSpace(d)).ToList();

        var endP = "</p>";

        CAChangeContent.ChangeContent0(null, lines, AddIntoParagraph);

        var result = SH.JoinNL(lines);
        result = result.Replace(endP,
            endP + "\r" +
            "\n"); // SHReplace.ReplaceAll(result, endP + "\r" + "\n", endP);


        return result;
    }

    private static string AddIntoParagraph(string text)
    {
        const string spaceDash = " -";

        if (text.Contains(spaceDash))
        {
            text = "<b>" + text;

            text = text.Replace(spaceDash, "</b>" + spaceDash);
        }

        //string s2 = string.Empty;
        if (text[0] == '<')
        {
            var tag = GetFirstTag(text).ToLower();

            if (AllLists.PairingTagsDontWrapToParagraph.Contains(tag)) return text;
            if (tag.StartsWith("/"))
                if (AllLists.PairingTagsDontWrapToParagraph.Contains(tag.Substring(1)))
                    return text;

            //s2 = text.Substring(1);
        }

        return WrapWith(text, "p");
    }


    private static string GetFirstTag(string text)
    {
        var between = SH.GetTextBetweenSimple(text, "<", ">");

        if (between.Contains(" ")) return SH.GetToFirst(between, " ");
        return between;
    }
}