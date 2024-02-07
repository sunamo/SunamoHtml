﻿namespace SunamoHtml._sunamo;
internal class SHReplace
{

    internal static string ReplaceWhiteSpacesWithoutSpacesWithReplaceWith(string p, string replaceWith = "")
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
        {
            if (string.IsNullOrEmpty(item))
            {
                return vstup;
            }
        }

        foreach (var item in co)
        {
            vstup = vstup.Replace(item, zaCo);
        }
        return vstup;
    }
    internal static string ReplaceAllDoubleSpaceToSingle(string text, bool alsoHtml = false)
    {
        //text = SHSH.FromSpace160To32(text);

        if (alsoHtml)
        {
            text = text.Replace(" &nbsp;", " ");
            text = text.Replace("&nbsp; ", " ");
            text = text.Replace("&nbsp;", " ");
        }

        while (text.Contains(AllStrings.doubleSpace))
        {
            text = text.Replace(AllStrings.doubleSpace, AllStrings.space); //ReplaceAll2(text, AllStrings.space, AllStrings.doubleSpace);
        }

        // Here it was cycling, dont know why, therefore without while
        //while (text.Contains(AllStrings.doubleSpace16032))
        //{
        //text = ReplaceAll2(text, AllStrings.space, AllStrings.doubleSpace16032);
        //}

        //while (text.Contains(AllStrings.doubleSpace32160))
        //{
        //text = ReplaceAll2(text, AllStrings.space, AllStrings.doubleSpace32160);
        //}

        return text;
    }

    internal static string ReplaceAll(string value, string v1, string v2)
    {
        throw new NotImplementedException();
    }
}
