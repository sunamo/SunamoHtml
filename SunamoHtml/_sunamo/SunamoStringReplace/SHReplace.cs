namespace SunamoHtml._sunamo.SunamoStringReplace;

internal class SHReplace
{
    internal static string ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(string text, string replaceWith)
    {
        return text.Replace("\r", replaceWith, StringComparison.Ordinal).Replace("\n", replaceWith, StringComparison.Ordinal).Replace("\t", replaceWith, StringComparison.Ordinal);
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

        foreach (var item in searchPatterns) text = text.Replace(item, replacement, StringComparison.Ordinal);
        return text;
    }

    internal static string ReplaceAllDoubleSpaceToSingle(string text, bool alsoHtml = false)
    {
        //text = SH.FromSpace160To32(text);

        if (alsoHtml)
        {
            text = text.Replace(" &nbsp;", " ", StringComparison.Ordinal);
            text = text.Replace("&nbsp; ", " ", StringComparison.Ordinal);
            text = text.Replace("&nbsp;", " ", StringComparison.Ordinal);
        }

        while (text.Contains("  ", StringComparison.Ordinal))
            text = text.Replace("  ",
                " ", StringComparison.Ordinal); //ReplaceAll2(text, "", "");

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

        foreach (var item in searchPatterns) text = text.Replace(item, replacement, StringComparison.Ordinal);
        return text;
    }
}
