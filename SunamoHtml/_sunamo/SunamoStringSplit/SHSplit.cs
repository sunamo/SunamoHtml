namespace SunamoHtml._sunamo.SunamoStringSplit;

internal class SHSplit
{
    internal static List<string> SplitBySpaceAndPunctuationCharsLeave(string text)
    {
        var result = new List<string>();
        result.Add("");
        foreach (var character in text)
        {
            var isSpaceOrPunctuation = false;
            foreach (var delimiter in SHData.SpaceAndPuntactionChars)
                if (character == delimiter)
                {
                    isSpaceOrPunctuation = true;
                    break;
                }

            if (isSpaceOrPunctuation)
            {
                if (string.IsNullOrEmpty(result[result.Count - 1]))
                    result[result.Count - 1] += character.ToString();
                else
                    result.Add(character.ToString());

                result.Add("");
            }
            else
            {
                result[result.Count - 1] += character.ToString();
            }
        }

        return result;
    }

    internal static List<string> SplitAndKeepDelimiters(string text, List<string> delimiters)
    {
        var result = Regex.Split(text, @"(?<=[" + string.Join("", delimiters) + "])");
        return result.ToList();
    }

    internal static void RemoveWhichHaveWhitespaceAtBothSides(string text, List<int> indexes)
    {
        for (var i = indexes.Count - 1; i >= 0; i--)
            if (char.IsWhiteSpace(text[indexes[i] - 1]) && char.IsWhiteSpace(text[indexes[i] + 1]))
                indexes.RemoveAt(i);
    }

    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitNone(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.None).ToList();
    }

    internal static List<string> SplitNoneChar(string text, params char[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.None).ToList();
    }

    internal static List<string> SplitChar(string text, params char[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    internal static List<string> SplitByWhiteSpaces(string text)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();

        return text.Split(whitespaceChar.WhiteSpaceChars.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
