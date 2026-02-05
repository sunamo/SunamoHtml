namespace SunamoHtml._sunamo.SunamoStringReplace;

internal class SHReplace
{
    internal static string ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(string text, string replaceWith)
    {
        return text.Replace("\r", replaceWith).Replace("\n", replaceWith).Replace("\t", replaceWith);
    }

    internal static string ReplaceAllArray(string text, string replacement, params string[] searchPatterns)
    {
        //Stupid, replacement can be null

        //if (string.IsNullOrEmpty(replacement))
        //{
        //    return text;
        //}

        foreach (var item in searchPatterns)
            if (string.IsNullOrEmpty(item))
                return text;

        foreach (var item in searchPatterns) text = text.Replace(item, replacement);
        return text;
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

    internal static string ReplaceAll(string text, string replacement, params string[] searchPatterns)
    {
        foreach (var item in searchPatterns)
            if (string.IsNullOrEmpty(item))
                return text;

        foreach (var item in searchPatterns) text = text.Replace(item, replacement);
        return text;
    }
}