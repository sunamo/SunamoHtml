namespace SunamoHtml._sunamo.SunamoString;

/// <summary>
/// EN: String helper methods (partial class - part 2).
/// CZ: Pomocné metody pro stringy (partial class - část 2).
/// </summary>
internal partial class SH
{
    /// <summary>
    /// EN: Returns X characters before and after a center position, ensuring whole words are included.
    /// CZ: Vrátí X znaků před a po středové pozici, zajistí že jsou zahrnutá celá slova.
    /// </summary>
    /// <param name="entireContent">The entire content to extract from.</param>
    /// <param name="centerPosition">The center position.</param>
    /// <param name="charsPerSide">Number of characters per side.</param>
    /// <returns>Text with whole words around the center position.</returns>
    internal static string XCharsBeforeAndAfterWholeWords(string entireContent, int centerPosition, int charsPerSide)
    {
        var rightSide = new StringBuilder();
        var currentWord = new StringBuilder();
        var leftSide = new StringBuilder();

        // Process left side from center backwards
        for (var i = centerPosition - 1; i >= 0; i--)
        {
            var ch = entireContent[i];
            if (ch == ' ')
            {
                var word = currentWord.ToString();
                currentWord.Clear();
                if (word != "")
                {
                    leftSide.Insert(0, word + " ");
                    if (leftSide.Length + " ".Length + word.Length > charsPerSide)
                        break;
                }
            }
            else
            {
                currentWord.Insert(0, ch);
            }
        }

        var leftResult = currentWord + " " + leftSide.ToString().TrimEnd(' ');
        leftResult = leftResult.TrimEnd(' ');
        charsPerSide += charsPerSide - leftResult.Length;
        currentWord.Clear();

        // Process right side from center forwards
        for (var i = centerPosition; i < entireContent.Length; i++)
        {
            var ch = entireContent[i];
            if (ch == ' ')
            {
                var word = currentWord.ToString();
                currentWord.Clear();
                if (word != "")
                {
                    rightSide.Append(" " + word);
                    if (rightSide.Length + " ".Length + word.Length > charsPerSide)
                        break;
                }
            }
            else
            {
                currentWord.Append(ch);
            }
        }

        var rightResult = rightSide.ToString().TrimStart(' ') + " " + currentWord;
        rightResult = rightResult.TrimStart(' ');
        var result = "";
        if (entireContent.Contains(leftResult + " ") && entireContent.Contains(" " + rightResult))
            result = leftResult + "" + rightResult;
        else
            result = leftResult + rightResult;
        return result;
    }

    /// <summary>
    /// EN: Gets text between two character positions (by integer indexes).
    /// CZ: Získá text mezi dvěma znakovými pozicemi (podle celočíselných indexů).
    /// </summary>
    /// <param name="text">The text to extract from.</param>
    /// <param name="beginIndex">The begin index.</param>
    /// <param name="endIndex">The end index.</param>
    /// <returns>Text between the two positions.</returns>
    internal static string GetTextBetweenTwoCharsInts(string text, int beginIndex, int endIndex)
    {
        if (endIndex > beginIndex)
            return text.Substring(beginIndex + 1, endIndex - beginIndex - 1);
        return text;
    }

    /// <summary>
    /// EN: Checks if a string represents a numbered list item (digits followed by dot).
    /// CZ: Zkontroluje zda string reprezentuje položku číslovaného seznamu (číslice následované tečkou).
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns>True if the text is a numbered item, false otherwise.</returns>
    internal static bool IsNumbered(string text)
    {
        var digitCount = 0;
        foreach (var character in text)
            if (char.IsNumber(character))
            {
                digitCount++;
            }
            else if (character == '.')
            {
                if (digitCount > 0)
                    return true;
            }
            else
            {
                return false;
            }

        return false;
    }

    /// <summary>
    /// EN: Converts the first character of a string to uppercase.
    /// CZ: Převede první znak stringu na velké písmeno.
    /// </summary>
    /// <param name="text">The text to convert.</param>
    /// <returns>Text with first character uppercase.</returns>
    internal static string FirstCharUpper(string text)
    {
        if (text.Length == 1)
            return text.ToUpper();
        var restOfString = text.Substring(1);
        return text[0].ToString().ToUpper() + restOfString;
    }

    /// <summary>
    /// EN: Converts first character of every dash-separated word to uppercase.
    /// CZ: Převede první znak každého pomlčkou odděleného slova na velké písmeno.
    /// </summary>
    /// <param name="text">The text to convert.</param>
    /// <returns>Text with first character of each dash-separated word uppercase, joined with spaces.</returns>
    internal static string FirstCharOfEveryWordUpperDash(string text)
    {
        return FirstCharOfEveryWordUpper(text, '-');
    }

    /// <summary>
    /// EN: Converts first character of every word (separated by delimiter) to uppercase.
    /// CZ: Převede první znak každého slova (odděleného oddělovačem) na velké písmeno.
    /// Returns words joined with space.
    /// </summary>
    /// <param name="text">The text to convert.</param>
    /// <param name="delimiter">The delimiter character.</param>
    /// <returns>Text with first character of each word uppercase, joined with spaces.</returns>
    private static string FirstCharOfEveryWordUpper(string text, char delimiter)
    {
        var words = SHSplit.SplitChar(text, delimiter);
        for (var i = 0; i < words.Count; i++)
            words[i] = FirstCharUpper(words[i]);
        return string.Join(" ", words);
    }

    /// <summary>
    /// EN: Matches a string against a wildcard pattern.
    /// CZ: Porovná string s wildcard vzorem.
    /// </summary>
    /// <param name="name">The string to match.</param>
    /// <param name="mask">The wildcard mask pattern.</param>
    /// <returns>True if the string matches the pattern, false otherwise.</returns>
    internal static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, '?', '*');
    }

    /// <summary>
    /// EN: Checks if a string matches a pattern with wildcard characters.
    /// CZ: Zkontroluje zda string odpovídá vzoru s wildcard znaky.
    /// </summary>
    /// <param name="text">The text to match.</param>
    /// <param name="pattern">The pattern to match against.</param>
    /// <param name="singleWildcard">Single character wildcard.</param>
    /// <param name="multipleWildcard">Multiple character wildcard.</param>
    /// <returns>True if the text matches the pattern, false otherwise.</returns>
    private static bool IsMatchRegex(string text, string pattern, char singleWildcard, char multipleWildcard)
    {
        if (text == pattern)
            return true;
        var escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        var escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pattern = Regex.Escape(pattern);
        pattern = pattern.Replace(escapedSingle, ".");
        pattern = "^" + pattern.Replace(escapedMultiple, ".*") + "$";
        var regex = new Regex(pattern);
        return regex.IsMatch(text);
    }

    /// <summary>
    /// EN: Wraps a value with a character on both sides.
    /// CZ: Obalí hodnotu znakem z obou stran.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <param name="wrapChar">The character to wrap with.</param>
    /// <param name="isTrimWrapping">Whether to trim the value before wrapping.</param>
    /// <param name="isAlsoIfWhitespaceOrEmpty">Whether to wrap even if value is whitespace or empty.</param>
    /// <returns>Wrapped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWithChar(string value, char wrapChar, bool isTrimWrapping = false, bool isAlsoIfWhitespaceOrEmpty = true)
    {
        if (string.IsNullOrWhiteSpace(value) && !isAlsoIfWhitespaceOrEmpty)
            return string.Empty;
        return WrapWith(isTrimWrapping ? value.Trim() : value, wrapChar.ToString());
    }

    /// <summary>
    /// EN: Wraps a value with a string on both sides.
    /// CZ: Obalí hodnotu stringem z obou stran.
    /// </summary>
    /// <param name="value">The value to wrap.</param>
    /// <param name="wrapper">The string to wrap with.</param>
    /// <returns>Wrapped value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWith(string value, string wrapper)
    {
        return wrapper + value + wrapper;
    }

    /// <summary>
    /// EN: Returns indexes of all occurrences of a character in a string.
    /// CZ: Vrátí indexy všech výskytů znaku ve stringu.
    /// </summary>
    /// <param name="text">The input string.</param>
    /// <param name="character">The character to find.</param>
    /// <returns>List of indexes where the character appears.</returns>
    internal static List<int> IndexesOfChars(string text, char character)
    {
        return IndexesOfCharsList(text, new List<char>(character));
    }

    /// <summary>
    /// EN: Returns indexes of all occurrences of any character from a list in a string.
    /// CZ: Vrátí indexy všech výskytů jakéhokoliv znaku ze seznamu ve stringu.
    /// IndexesOfChars - for single char, ReturnOccurencesOfString - for string.
    /// </summary>
    /// <param name="text">The input string.</param>
    /// <param name="characters">List of characters to find.</param>
    /// <returns>Sorted list of indexes where any of the characters appear.</returns>
    internal static List<int> IndexesOfCharsList(string text, List<char> characters)
    {
        var indexes = new List<int>();
        foreach (var character in characters)
            indexes.AddRange(ReturnOccurencesOfString(text, character.ToString()));
        indexes.Sort();
        return indexes;
    }

    /// <summary>
    /// EN: Returns indexes of all occurrences of a substring in a string.
    /// CZ: Vrátí indexy všech výskytů podřetězce ve stringu.
    /// </summary>
    /// <param name="entireText">The text to search in.</param>
    /// <param name="searchFor">The substring to search for.</param>
    /// <returns>List of indexes where the substring appears.</returns>
    internal static List<int> ReturnOccurencesOfString(string entireText, string searchFor)
    {
        var results = new List<int>();
        for (var index = 0; index < entireText.Length - searchFor.Length + 1; index++)
        {
            var substring = entireText.Substring(index, searchFor.Length);
            var firstChar = substring[0];
            var searchFirstChar = searchFor[0];
            if (substring == searchFor)
                results.Add(index);
        }

        return results;
    }
}