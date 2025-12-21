namespace SunamoHtml._sunamo.SunamoStringReplace;

internal class SHReplace
{
    internal static string ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(string p, string replaceWith)
    {
        return p.Replace("\r", replaceWith).Replace("\n", replaceWith).Replace("\t", replaceWith);
    }

    internal static string ReplaceAllArray(string vstup, string zaCo, params string[] co)
    {
        //Stupid, zaCo can be null

        //if (string.IsNullOrEmpty(zaCo))
        //{
        //    return vstup;
        //}

        foreach (var item in co)
            if (string.IsNullOrEmpty(item))
                return vstup;

        foreach (var item in co) vstup = vstup.Replace(item, zaCo);
        return vstup;
    }

    internal static string ReplaceAllDoubleSpaceToSingle(string text, bool alsoHtml = false)
    {
        //text = SH.FromSpace160To32(text);

        if (alsoHtml)
        {
            text = text.Replace(" &nbsp;", " ");
            text = text.Replace("&nbsp; ", " ");
            text = text.Replace("&nbsp;", " ");
        }

        while (text.Contains("  "))
            text = text.Replace("  ",
                " "); //ReplaceAll2(text, "", "");

        // Here it was cycling, dont know why, therefore without while
        //while (text.Contains("space160 + space"))
        //{
        //text = ReplaceAll2(text, "", "space160 + space");
        //}

        //while (text.Contains("space + space160"))
        //{
        //text = ReplaceAll2(text, "", "space + space160");
        //}

        return text;
    }

    internal static string ReplaceAll(string vstup, string zaCo, params string[] co)
    {
        foreach (var item in co)
            if (string.IsNullOrEmpty(item))
                return vstup;

        foreach (var item in co) vstup = vstup.Replace(item, zaCo);
        return vstup;
    }
}