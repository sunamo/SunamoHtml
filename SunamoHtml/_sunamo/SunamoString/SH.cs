namespace SunamoHtml._sunamo.SunamoString;

/// <summary>
/// EN: String helper methods (partial class - part 1).
/// CZ: Pomocné metody pro stringy (partial class - část 1).
/// </summary>
internal partial class SH
{
    /// <summary>
    /// EN: Joins a list of strings with newline separator.
    /// CZ: Spojí seznam stringů s oddělovačem nového řádku.
    /// </summary>
    /// <param name="list">The list of strings to join.</param>
    /// <returns>Joined string with newlines.</returns>
    internal static string JoinNL(List<string> list)
    {
        StringBuilder stringBuilder = new();
        foreach (var item in list)
            stringBuilder.AppendLine(item);
        var result = string.Empty;
        result = stringBuilder.ToString();
        return result;
    }

    /// <summary>
    /// EN: Checks if any of the specified letters appear in the last X characters of a string.
    /// CZ: Zkontroluje zda se některé ze zadaných písmen objevují v posledních X znacích stringu.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <param name="lastXChars">Number of last characters to check.</param>
    /// <param name="letters">Letters to search for.</param>
    /// <returns>True if any letter is found, false otherwise.</returns>
    internal static bool IsInLastXCharsTheseLetters(string text, int lastXChars, params char[] letters)
    {
        for (var i = text.Length - 1; i >= lastXChars; i--)
            foreach (var letter in letters)
                if (text[i] == letter)
                    return true;
        return false;
    }

    /// <summary>
    /// EN: Returns occurrences of search words in text as FromToWord objects.
    /// CZ: Vrátí výskyty hledaných slov v textu jako FromToWord objekty.
    /// </summary>
    /// <param name="entireContent">The entire content to search in.</param>
    /// <param name="searchWords">Words to search for.</param>
    /// <returns>List of FromToWord objects representing found occurrences.</returns>
    internal static List<FromToWord> ReturnOccurencesOfStringFromToWord(string entireContent, params string[] searchWords)
    {
        if (searchWords == null || searchWords.Length == 0)
            return new List<FromToWord>();
        entireContent = entireContent.ToLower();
        var result = new List<FromToWord>();
        var contentLength = entireContent.Length;
        for (var i = 0; i < contentLength; i++)
            foreach (var searchWord in searchWords)
            {
                var allMatch = true;
                var offset = 0;
                while (offset < searchWord.Length)
                {
                    var currentIndex = i + offset;
                    if (contentLength > currentIndex)
                    {
                        if (entireContent[currentIndex] != searchWord[offset])
                        {
                            allMatch = false;
                            break;
                        }
                    }
                    else
                    {
                        allMatch = false;
                        break;
                    }

                    offset++;
                }

                if (allMatch)
                {
                    var ftw = new FromToWord();
                    ftw.From = i;
                    ftw.To = i + offset - 1;
                    ftw.Word = searchWord;
                    result.Add(ftw);
                    i += offset;
                    break;
                }
            }

        return result;
    }

    /// <summary>
    /// EN: Gets the first part of a string before the specified delimiter character.
    /// CZ: Získá první část stringu před zadaným oddělovacím znakem.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiter">The delimiter character.</param>
    /// <returns>First part before the delimiter.</returns>
    internal static string GetFirstPartByLocation(string text, char delimiter)
    {
        var delimiterIndex = text.IndexOf(delimiter);
        return GetFirstPartByLocation(text, delimiterIndex);
    }

    /// <summary>
    /// EN: Splits text at specified position into before and after parts.
    /// CZ: Rozdělí text na zadané pozici na část před a po.
    /// </summary>
    /// <param name="before">Output: part before the position.</param>
    /// <param name="after">Output: part after the position.</param>
    /// <param name="text">The text to split.</param>
    /// <param name="position">The position to split at.</param>
    internal static void GetPartsByLocation(out string before, out string after, string text, int position)
    {
        if (position == -1)
        {
            before = text;
            after = "";
        }
        else
        {
            before = text.Substring(0, position);
            if (text.Length > position + 1)
                after = text.Substring(position + 1);
            else
                after = string.Empty;
        }
    }

    /// <summary>
    /// EN: Gets the first part of a string before the specified position.
    /// CZ: Získá první část stringu před zadanou pozicí.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="position">The position to split at.</param>
    /// <returns>First part before the position.</returns>
    internal static string GetFirstPartByLocation(string text, int position)
    {
        string before, after;
        before = text;
        if (position < text.Length)
            GetPartsByLocation(out before, out after, text, position);
        return before;
    }

    /// <summary>
    /// EN: Checks if any of the specified letters appear in the first X characters of a string.
    /// CZ: Zkontroluje zda se některé ze zadaných písmen objevují v prvních X znacích stringu.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <param name="firstXChars">Number of first characters to check.</param>
    /// <param name="letters">Letters to search for.</param>
    /// <returns>True if any letter is found, false otherwise.</returns>
    private static bool IsInFirstXCharsTheseLetters(string text, int firstXChars, params char[] letters)
    {
        for (var i = 0; i < firstXChars; i++)
            foreach (var letter in letters)
                if (text[i] == letter)
                    return true;
        return false;
    }

    /// <summary>
    /// EN: Shortens text to specified letter count, optionally adding three dots.
    /// CZ: Zkrátí text na zadaný počet písmen, volitelně přidá tři tečky.
    /// </summary>
    /// <param name="text">The text to shorten.</param>
    /// <param name="maxLength">Maximum length in characters.</param>
    /// <param name="isAddThreeDots">Output: whether three dots were added.</param>
    /// <returns>Shortened text.</returns>
    private static string ShortForLettersCount(string text, int maxLength, out bool isAddThreeDots)
    {
        isAddThreeDots = false;
        text = text.Trim();
        var textLength = text.Length;
        var isLonger = maxLength <= textLength;
        if (isLonger)
        {
            if (IsInFirstXCharsTheseLetters(text, maxLength, ' '))
            {
                var spaceIndex = 0;
                var data = text;
                var dataLength = data.Length;
                var counted = 0;
                for (var i = 0; i < dataLength; i++)
                {
                    counted++;
                    if (data[i] == ' ')
                    {
                        if (counted >= maxLength)
                            break;
                        spaceIndex = i;
                    }
                }

                data = data.Substring(0, spaceIndex + 1);
                if (data.Trim() != "")
                    isAddThreeDots = true;
                return data;
            }

            isAddThreeDots = true;
            return text.Substring(0, maxLength);
        }

        return text;
    }

    /// <summary>
    /// EN: Shortens text to specified letter count.
    /// CZ: Zkrátí text na zadaný počet písmen.
    /// </summary>
    /// <param name="text">The text to shorten.</param>
    /// <param name="maxLength">Maximum length in characters.</param>
    /// <returns>Shortened text.</returns>
    internal static string ShortForLettersCount(string text, int maxLength)
    {
        var isAddThreeDots = false;
        return ShortForLettersCount(text, maxLength, out isAddThreeDots);
    }

    /// <summary>
    /// EN: Shortens text from the end, adding "..." at the beginning if shortened.
    /// CZ: Zkrátí text od konce, přidá "..." na začátek pokud byl zkrácen.
    /// </summary>
    /// <param name="text">The text to shorten.</param>
    /// <param name="maxLength">Maximum length in characters.</param>
    /// <returns>Shortened text with "..." prefix if shortened.</returns>
    internal static string ShortForLettersCountThreeDotsReverse(string text, int maxLength)
    {
        text = text.Trim();
        var textLength = text.Length;
        var isLonger = maxLength <= textLength;
        if (isLonger)
        {
            if (IsInLastXCharsTheseLetters(text, maxLength, ' '))
            {
                var spaceIndex = 0;
                var data = text;
                var dataLength = data.Length;
                var counted = 0;
                for (var i = dataLength - 1; i >= 0; i--)
                {
                    counted++;
                    if (data[i] == ' ')
                    {
                        if (counted >= maxLength)
                            break;
                        spaceIndex = i;
                    }
                }

                data = data.Substring(spaceIndex + 1);
                if (data.Trim() != "")
                    data = " ... " + data;
                return data;
            }

            return " ... " + text.Substring(text.Length - maxLength);
        }

        return text;
    }

    /// <summary>
    /// EN: Gets text up to and including the first occurrence of a search string.
    /// CZ: Získá text až po první výskyt hledaného řetězce včetně.
    /// </summary>
    /// <param name="text">The input text.</param>
    /// <param name="searchFor">The string to search for.</param>
    /// <returns>Text up to and including the first occurrence.</returns>
    internal static string GetToFirst(string text, string searchFor)
    {
        var indexOfChar = text.IndexOf(searchFor);
        if (indexOfChar != -1)
            return text.Substring(0, indexOfChar + 1);
        return text;
    }

    /// <summary>
    /// EN: Gets text between two delimiters (simple version without output parameters).
    /// CZ: Získá text mezi dvěma oddělovači (jednoduchá verze bez výstupních parametrů).
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="after">Text after which to start.</param>
    /// <param name="before">Text before which to end.</param>
    /// <param name="isThrowExceptionIfNotContains">Whether to throw exception if delimiters not found.</param>
    /// <returns>Text between the delimiters.</returns>
    internal static string GetTextBetweenSimple(string text, string after, string before, bool isThrowExceptionIfNotContains = true)
    {
        var foundIndex = int.MinValue;
        var result = GetTextBetween(text, after, before, out foundIndex, 0, isThrowExceptionIfNotContains);
        return result;
    }

    /// <summary>
    /// EN: Gets text between two delimiters, returning the index where the 'after' string was found.
    /// CZ: Získá text mezi dvěma oddělovači, vrátí index kde byl nalezen 'after' řetězec.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="after">Text after which to start.</param>
    /// <param name="before">Text before which to end.</param>
    /// <param name="foundIndex">Output: index where 'after' was found.</param>
    /// <param name="startSearchingAt">Index to start searching at.</param>
    /// <param name="isThrowExceptionIfNotContains">Whether to throw exception if delimiters not found.</param>
    /// <returns>Text between the delimiters.</returns>
    internal static string GetTextBetween(string text, string after, string before, out int foundIndex, int startSearchingAt, bool isThrowExceptionIfNotContains = true)
    {
        string result = null;
        foundIndex = text.IndexOf(after, startSearchingAt);
        var beforeIndex = text.IndexOf(before, foundIndex + after.Length);
        var afterFound = foundIndex != -1;
        var beforeFound = beforeIndex != -1;
        if (afterFound && beforeFound)
        {
            foundIndex += after.Length;
            beforeIndex -= 1;
            var length = beforeIndex - foundIndex + 1;
            if (length < 1)
            {
                // Logically nonsense to return full text when length < 1
            }

            result = text.Substring(foundIndex, length).Trim();
        }
        else
        {
            if (isThrowExceptionIfNotContains)
                ThrowEx.NotContains(text, after, before);
            else
                return null;
        }

        return result.Trim();
    }
}