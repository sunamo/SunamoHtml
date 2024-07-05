namespace SunamoHtml.Html;

public class HtmlHelperText
{
    public static string ReplacePairTag(string input, string tag, string forWhat)
    {
        input = input.Replace("<" + tag + ">", "<" + forWhat + ">");
        input = input.Replace("<" + tag + " ", "<" + forWhat + " ");
        input = input.Replace("</" + tag + ">", "</" + forWhat + ">");
        return input;
    }

    public static string InsertMissingEndingTags(string s, string tag)
    {
        StringBuilder text = new StringBuilder(s);

        var start = SH.ReturnOccurencesOfString(s, "<" + tag);
        var endingTag = "</" + tag + ">";
        var ends = SH.ReturnOccurencesOfString(s, endingTag);

        var startC = start.Count;
        var endsC = ends.Count;

        if (start.Count > ends.Count)
        {
            // In keys are start, in value end. If end isnt, then -1
            Dictionary<int, int> se = new Dictionary<int, int>();

            for (int i = start.Count - 1; i >= 0; i--)
            {
                var startActual = start[i];

                var endDx = -1;
                if (ends.Count != 0)
                {
                    endDx = ends.Count - 1;
                }
                var endActual = -1;
                if (endDx != -1)
                {
                    endActual = ends[endDx];
                }
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
            {
                if (item.Value == -1)
                {
                    var dexEndOfStart = s.IndexOf(AllChars.gt, item.Key);

                    var space = s.IndexOf(AllChars.space, dexEndOfStart);

                    if (space != -1)
                    {

                        text.Insert(space, endingTag);
                    }
                }
            }
        }

        return text.ToString();
    }

    public static List<string> CreateH2FromNumberedList(List<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i] = lines[i].Trim();
        }
        //CA.Trim(lines);

        for (int i = 0; i < lines.Count; i++)
        {
            if (SH.IsNumbered(lines[i]))
            {
                lines[i] = WrapWith(lines[i], "h2");
            }
        }

        return lines;
    }

    public static List<string> GetAllEquvivalentsOfNonPairingTag(string v)
    {
        return new List<string>(["<" + v + ">", "<" + v + " />", "<" + v + "/>"]);
    }

    /// <summary>
    /// A2 only name like p, title etc.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="v"></param>
    private static string WrapWith(string s, string p)
    {
        return AllStrings.lt + p + AllStrings.gt + s + "</" + p + AllStrings.gt;
    }

    public static string RemoveAllNodes(string v)
    {
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(v);

        var nodes = hd.DocumentNode.Descendants().ToList();
        for (int i = 0; i < nodes.Count(); i++)
        {


            var node = nodes[i];

            if (node.NodeType != HtmlAgilityPack.HtmlNodeType.Text)
            {
                if (node.ParentNode.NodeType != HtmlAgilityPack.HtmlNodeType.Document)
                {
                    node.ParentNode.Remove();
                }
                else
                {
                    node.Remove();
                }
            }
        }

        return hd.DocumentNode.OuterHtml;
    }

    public static Tuple<string, string> GetTextBetweenTags(string c2, string scriptS, string scriptE, bool throwExceptionIfNotContains = true)
    {
        if (scriptS.EndsWith(">"))
        {
            return new Tuple<string, string>(SH.GetTextBetweenSimple(c2, scriptS, scriptE, throwExceptionIfNotContains), "");
        }
        var sc = c2.IndexOf(scriptS);
        if (sc == -1)
        {
            return new Tuple<string, string>("", "");
        }

        var ending = c2.IndexOf('>', sc);
        var e = c2.IndexOf(scriptE, ending);

        var r = SH.GetTextBetweenTwoCharsInts(c2, ending, e);

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

        return new Tuple<string, string>(r, attrs);
    }

    private static Type type = typeof(HtmlHelperText);

    const string regexHtmlTag = "<[^<>]+>";

    public static List<string> GetAllTags(string i)
    {
        var tags = Regex.Matches(i, regexHtmlTag);
        List<string> ls = new List<string>();
        foreach (Match item in tags)
        {
            ls.Add(item.Value);
        }
        return ls;
    }

    public static string RemoveHtmlTags(string ClipboardS2)
    {
        return SHReplace.ReplaceAll(HtmlHelper.RemoveAllTags(ClipboardS2), AllStrings.space, AllStrings.doubleSpace);
    }

    public static string RemoveAspxComments(string c)
    {
        c = Regex.Replace(c, AspxConsts.startAspxComment + ".*?" + AspxConsts.endAspxComment, String.Empty, RegexOptions.Singleline);
        return c;
    }

    public static bool ContainsTag(string s, List<string> allHtmlTagsWithLeftArrow)
    {
        foreach (var item in allHtmlTagsWithLeftArrow)
        {
            if (s.Contains(item))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Get type of tag (paired ended, paired not ended, non paired)
    /// </summary>
    /// <param name="tag"></param>
    public static HtmlTagSyntax GetSyntax(ref string tag)
    {
        ThrowEx.InvalidParameter((string)tag, "tag");

        tag = SH.GetToFirst((string)tag, AllStrings.space);
        tag = tag.Trim().TrimStart(AllChars.lt).TrimEnd(AllChars.gt).ToLower();

        if (AllLists.HtmlNonPairTags.Contains((string)tag))
        {
            return HtmlTagSyntax.NonPairingNotEnded;
        }
        tag = tag.TrimEnd(AllChars.slash);
        if (AllLists.HtmlNonPairTags.Contains((string)tag))
        {
            return HtmlTagSyntax.NonPairingEnded;
        }
        if (tag[tag.Length - 1] == AllChars.slash)
        {
            return HtmlTagSyntax.End;
        }
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
        var f = SHSplit.SplitMore(shortDescription, new String[] { AllStrings.lt, AllStrings.gt, AllStrings.space });
        return f;
    }

    public static bool IsHtmlEntity(string i)
    {
        i = i.TrimStart('&').TrimEnd(';');
        return AllLists.htmlEntities.Contains(i);
    }

    public static List<string> GetContentOfTags(string text, string pre)
    {
        List<string> result = new List<string>();
        string start = $"<{pre}";
        string end = $"</{pre}>";
        int dex = text.IndexOf(start);
        while (dex != -1)
        {
            int dexEndLetter = text.IndexOf(AllChars.gt, dex);

            int dex2 = text.IndexOf(start, dex + start.Length);
            int dexEnd = text.IndexOf(end, dex);

            if (dex2 != -1)
            {
                if (dexEnd > dex2)
                {
                    throw new Exception($"Another starting tag is before ending <{pre}>");
                }
            }

            result.Add(SH.GetTextBetweenTwoCharsInts(text, dexEndLetter, dexEnd).Trim());

            dex = text.IndexOf(start, dexEnd);
        }

        return result;
    }

    public static bool IsCssDeclarationName(string decl)
    {
        if (AllLists.allCssKeys.Contains(decl))
        {
            return true;
        }
        return false;
    }

    public static string ConvertTextToHtml(string text)
    {
        var lines = SHGetLines.GetLines(text);

        //CA.RemoveStringsEmpty2(lines);
        lines = lines.Where(d => !string.IsNullOrWhiteSpace(d)).ToList();

        var endP = "</p>";

        CAChangeContent.ChangeContent0(null, lines, AddIntoParagraph);

        var result = SHSunamoExceptions.JoinNL(lines);
        result = result.Replace(endP, endP + AllStrings.cr + AllStrings.nl); // SHReplace.ReplaceAll(result, endP + AllStrings.cr + AllStrings.nl, endP);



        return result;
    }

    private static string AddIntoParagraph(string s)
    {
        const string spaceDash = " -";

        if (s.Contains(spaceDash))
        {
            s = "<b>" + s;

            s = s.Replace(spaceDash, "</b>" + spaceDash);
        }

        //string s2 = string.Empty;
        if (s[0] == AllChars.lt)
        {
            var tag = HtmlHelperText.GetFirstTag(s).ToLower();

            if (AllLists.PairingTagsDontWrapToParagraph.Contains(tag))
            {
                return s;
            }
            if (tag.StartsWith(AllStrings.slash))
            {
                if (AllLists.PairingTagsDontWrapToParagraph.Contains(tag.Substring(1)))
                {
                    return s;
                }
            }

            //s2 = s.Substring(1);
        }
        return WrapWith(s, "p");
    }



    private static string GetFirstTag(string s)
    {
        var between = SH.GetTextBetweenSimple(s, AllStrings.lt, AllStrings.gt, true);

        if (between.Contains(AllStrings.space))
        {
            return SH.GetToFirst(between, AllStrings.space);
        }
        return between;
    }
}
