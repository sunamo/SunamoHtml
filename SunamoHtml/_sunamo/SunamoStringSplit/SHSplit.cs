namespace SunamoHtml._sunamo.SunamoStringSplit;

/// <summary>
/// EN: String splitting utility methods.
/// CZ: Utility metody pro rozdělování stringů.
/// </summary>
internal class SHSplit
{
    /// <summary>
    /// EN: Splits text by space and punctuation characters, keeping the delimiters in the result.
    /// CZ: Rozdělí text podle mezer a interpunkčních znaků, zachová oddělovače ve výsledku.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <returns>List of words and delimiters.</returns>
    internal static List<string> SplitBySpaceAndPunctuationCharsLeave(string text)
    {
        var result = new List<string>();
        result.Add("");
        foreach (var character in text)
        {
            var isSpaceOrPunctuation = false;
            foreach (var delimiter in SHData.spaceAndPuntactionChars)
                if (character == delimiter)
                {
                    isSpaceOrPunctuation = true;
                    break;
                }

            if (isSpaceOrPunctuation)
            {
                if (result[result.Count - 1] == "")
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

    /// <summary>
    /// EN: Splits string and keeps delimiters in the result.
    /// CZ: Rozdělí string a zachová oddělovače ve výsledku.
    /// </summary>
    /// <param name="originalString">The string to split.</param>
    /// <param name="delimiters">List of delimiter characters.</param>
    /// <returns>List of split parts with delimiters.</returns>
    internal static List<string> SplitAndKeepDelimiters(string originalString, List<string> delimiters)
    {
        var result = Regex.Split(originalString, @"(?<=[" + string.Join("", delimiters) + "])");
        return result.ToList();
    }

    /// <summary>
    /// EN: Removes indexes from list where the character at that index has whitespace on both sides.
    /// CZ: Odstraní indexy ze seznamu kde znak na tom indexu má mezery na obou stranách.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <param name="indexes">List of indexes to filter.</param>
    internal static void RemoveWhichHaveWhitespaceAtBothSides(string text, List<int> indexes)
    {
        for (var i = indexes.Count - 1; i >= 0; i--)
            if (char.IsWhiteSpace(text[indexes[i] - 1]) && char.IsWhiteSpace(text[indexes[i] + 1]))
                indexes.RemoveAt(i);
    }

    /// <summary>
    /// EN: Splits string by delimiters, removing empty entries.
    /// CZ: Rozdělí string podle oddělovačů, odstraní prázdné záznamy.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">Delimiter strings.</param>
    /// <returns>List of non-empty split parts.</returns>
    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    /// <summary>
    /// EN: Splits string by delimiters, keeping empty entries.
    /// CZ: Rozdělí string podle oddělovačů, zachová prázdné záznamy.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">Delimiter strings.</param>
    /// <returns>List of all split parts including empty.</returns>
    internal static List<string> SplitNone(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.None).ToList();
    }

    /// <summary>
    /// EN: Splits string by character delimiters, keeping empty entries.
    /// CZ: Rozdělí string podle znakových oddělovačů, zachová prázdné záznamy.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">Delimiter characters.</param>
    /// <returns>List of all split parts including empty.</returns>
    internal static List<string> SplitNoneChar(string text, params char[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.None).ToList();
    }

    /// <summary>
    /// EN: Splits string by character delimiters, removing empty entries.
    /// CZ: Rozdělí string podle znakových oddělovačů, odstraní prázdné záznamy.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">Delimiter characters.</param>
    /// <returns>List of non-empty split parts.</returns>
    internal static List<string> SplitChar(string text, params char[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    /// <summary>
    /// EN: Splits text by whitespace characters, removing empty entries.
    /// CZ: Rozdělí text podle bílých znaků, odstraní prázdné záznamy.
    /// </summary>
    /// <param name="innerText">The text to split.</param>
    /// <returns>List of non-empty parts.</returns>
    internal static List<string> SplitByWhiteSpaces(string innerText)
    {
        WhitespaceCharService whitespaceChar = new WhitespaceCharService();

        return innerText.Split(whitespaceChar.whiteSpaceChars.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
