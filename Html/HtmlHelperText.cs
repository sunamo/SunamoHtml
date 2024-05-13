
namespace SunamoHtml;


/// <summary>
/// HtmlHelperText - for methods which NOT operate on HtmlAgiityHelper!
/// HtmlAgilityHelper - getting new nodes
/// HtmlAssistant - Only for methods which operate on HtmlAgiityHelper!
/// </summary>
public partial class HtmlHelperText
{
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

    private static string WrapWith(string s, string v)
    {
        throw new NotImplementedException();
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
