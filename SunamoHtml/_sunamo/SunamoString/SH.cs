namespace SunamoHtml._sunamo.SunamoString;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
internal partial class SH
{
    internal static string JoinNL(List<string> list)
    {
        StringBuilder stringBuilder = new();
        foreach (var item in list)
            stringBuilder.AppendLine(item);
        var result = string.Empty;
        result = stringBuilder.ToString();
        return result;
    }

    internal static bool IsInLastXCharsTheseLetters(string parameter, int pl, params char[] letters)
    {
        for (var i = parameter.Length - 1; i >= pl; i--)
            foreach (var item in letters)
                if (parameter[i] == item)
                    return true;
        return false;
    }

    internal static List<FromToWord> ReturnOccurencesOfStringFromToWord(string celyObsah, params string[] hledaneSlova)
    {
        if (hledaneSlova == null || hledaneSlova.Length == 0)
            return new List<FromToWord>();
        celyObsah = celyObsah.ToLower();
        var vr = new List<FromToWord>();
        var list = celyObsah.Length;
        for (var i = 0; i < list; i++)
            foreach (var item in hledaneSlova)
            {
                var vsechnoStejne = true;
                var pridat = 0;
                while (pridat < item.Length)
                {
                    var dex = i + pridat;
                    if (list > dex)
                    {
                        if (celyObsah[dex] != item[pridat])
                        {
                            vsechnoStejne = false;
                            break;
                        }
                    }
                    else
                    {
                        vsechnoStejne = false;
                        break;
                    }

                    pridat++;
                }

                if (vsechnoStejne)
                {
                    var ftw = new FromToWord();
                    ftw.from = i;
                    ftw.to = i + pridat - 1;
                    ftw.word = item;
                    vr.Add(ftw);
                    i += pridat;
                    break;
                }
            }

        return vr;
    }

    internal static string GetFirstPartByLocation(string p1, char deli)
    {
        var dx = p1.IndexOf(deli);
        return GetFirstPartByLocation(p1, dx);
    }

    internal static void GetPartsByLocation(out string pred, out string za, string text, int pozice)
    {
        if (pozice == -1)
        {
            pred = text;
            za = "";
        }
        else
        {
            pred = text.Substring(0, pozice);
            if (text.Length > pozice + 1)
                za = text.Substring(pozice + 1);
            else
                za = string.Empty;
        }
    }

    internal static string GetFirstPartByLocation(string p1, int dx)
    {
        string parameter, z;
        parameter = p1;
        if (dx < p1.Length)
            GetPartsByLocation(out parameter, out z, p1, dx);
        return parameter;
    }

    private static bool IsInFirstXCharsTheseLetters(string parameter, int pl, params char[] letters)
    {
        for (var i = 0; i < pl; i++)
            foreach (var item in letters)
                if (parameter[i] == item)
                    return true;
        return false;
    }

    private static string ShortForLettersCount(string parameter, int p_2, out bool pridatTriTecky)
    {
        pridatTriTecky = false;
        // Vše tu funguje výborně
        parameter = parameter.Trim();
        var pl = parameter.Length;
        var jeDelsiA1 = p_2 <= pl;
        if (jeDelsiA1)
        {
            if (IsInFirstXCharsTheseLetters(parameter, p_2, ' '))
            {
                var dexMezery = 0;
                var data = parameter; //parameter.Substring(parameter.Length - zkratitO);
                var to = data.Length;
                var napocitano = 0;
                for (var i = 0; i < to; i++)
                {
                    napocitano++;
                    if (data[i] == ' ')
                    {
                        if (napocitano >= p_2)
                            break;
                        dexMezery = i;
                    }
                }

                data = data.Substring(0, dexMezery + 1);
                if (data.Trim() != "")
                    pridatTriTecky = true;
                //d = data ;
                return data;
            //}
            }

            pridatTriTecky = true;
            return parameter.Substring(0, p_2);
        }

        return parameter;
    }

    internal static string ShortForLettersCount(string parameter, int p_2)
    {
        var pridatTriTecky = false;
        return ShortForLettersCount(parameter, p_2, out pridatTriTecky);
    }

    internal static string ShortForLettersCountThreeDotsReverse(string parameter, int p_2)
    {
        parameter = parameter.Trim();
        var pl = parameter.Length;
        var jeDelsiA1 = p_2 <= pl;
        if (jeDelsiA1)
        {
            if (IsInLastXCharsTheseLetters(parameter, p_2, ' '))
            {
                var dexMezery = 0;
                var data = parameter; //parameter.Substring(parameter.Length - zkratitO);
                var to = data.Length;
                var napocitano = 0;
                for (var i = to - 1; i >= 0; i--)
                {
                    napocitano++;
                    if (data[i] == ' ')
                    {
                        if (napocitano >= p_2)
                            break;
                        dexMezery = i;
                    }
                }

                data = data.Substring(dexMezery + 1);
                if (data.Trim() != "")
                    data = " ... " + data;
                return data;
            //}
            }

            return " ... " + parameter.Substring(parameter.Length - p_2);
        }

        return parameter;
    }

    internal static string GetToFirst(string input, string searchFor)
    {
        var indexOfChar = input.IndexOf(searchFor);
        if (indexOfChar != -1)
            return input.Substring(0, indexOfChar + 1);
        return input;
    }

    internal static string GetTextBetweenSimple(string parameter, string after, string before, bool throwExceptionIfNotContains = true)
    {
        var dxOfFounded = int.MinValue;
        var temp = GetTextBetween(parameter, after, before, out dxOfFounded, 0, throwExceptionIfNotContains);
        return temp;
    }

    internal static string GetTextBetween(string parameter, string after, string before, out int dxOfFounded, int startSearchingAt, bool throwExceptionIfNotContains = true)
    {
        string vr = null;
        dxOfFounded = parameter.IndexOf(after, startSearchingAt);
        var p3 = parameter.IndexOf(before, dxOfFounded + after.Length);
        var b2 = dxOfFounded != -1;
        var b3 = p3 != -1;
        if (b2 && b3)
        {
            dxOfFounded += after.Length;
            p3 -= 1;
            // When I return between ( ), there must be +1
            var length = p3 - dxOfFounded + 1;
            if (length < 1)
            {
            // Takhle to tu bylo předtím ale logicky je to nesmysl.
            //return parameter;
            }

            vr = parameter.Substring(dxOfFounded, length).Trim();
        }
        else
        {
            if (throwExceptionIfNotContains)
                ThrowEx.NotContains(parameter, after, before);
            else
                // 24-1-21 return null instead of parameter
                return null;
        //vr = parameter;
        }

        return vr.Trim();
    }
}