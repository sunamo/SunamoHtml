namespace SunamoHtml._sunamo.SunamoString;

internal partial class SH
{
    internal static string XCharsBeforeAndAfterWholeWords(string text, int centerPosition, int charsPerSide)
    {
        var rightSide = new StringBuilder();
        var currentWord = new StringBuilder();
        var leftSide = new StringBuilder();

        // Process left side from center backwards
        for (var i = centerPosition - 1; i >= 0; i--)
        {
            var ch = text[i];
            if (ch == ' ')
            {
                var word = currentWord.ToString();
                currentWord.Clear();
                if (!string.IsNullOrEmpty(word))
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
        for (var i = centerPosition; i < text.Length; i++)
        {
            var ch = text[i];
            if (ch == ' ')
            {
                var word = currentWord.ToString();
                currentWord.Clear();
                if (!string.IsNullOrEmpty(word))
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
        var result = leftResult + rightResult;
        return result;
    }

    internal static string GetTextBetweenTwoCharsInts(string text, int beginIndex, int endIndex)
    {
        if (endIndex > beginIndex)
            return text.Substring(beginIndex + 1, endIndex - beginIndex - 1);
        return text;
    }

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

    internal static string FirstCharUpper(string text)
    {
        if (text.Length == 1)
            return text.ToUpperInvariant();
        var restOfString = text.Substring(1);
        return text[0].ToString().ToUpperInvariant() + restOfString;
    }

    internal static string FirstCharOfEveryWordUpperDash(string text)
    {
        return FirstCharOfEveryWordUpper(text, '-');
    }

    private static string FirstCharOfEveryWordUpper(string text, char delimiter)
    {
        var words = SHSplit.SplitChar(text, delimiter);
        for (var i = 0; i < words.Count; i++)
            words[i] = FirstCharUpper(words[i]);
        return string.Join(" ", words);
    }

    internal static bool MatchWildcard(string text, string mask)
    {
        return IsMatchRegex(text, mask, '?', '*');
    }

    private static bool IsMatchRegex(string text, string pattern, char singleWildcard, char multipleWildcard)
    {
        if (text == pattern)
            return true;
        var escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        var escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pattern = Regex.Escape(pattern);
        pattern = pattern.Replace(escapedSingle, ".", StringComparison.Ordinal);
        pattern = "^" + pattern.Replace(escapedMultiple, ".*", StringComparison.Ordinal) + "$";
        var regex = new Regex(pattern);
        return regex.IsMatch(text);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWithChar(string text, char wrapChar, bool isTrimWrapping = false, bool isAlsoIfWhitespaceOrEmpty = true)
    {
        if (string.IsNullOrWhiteSpace(text) && !isAlsoIfWhitespaceOrEmpty)
            return string.Empty;
        return WrapWith(isTrimWrapping ? text.Trim() : text, wrapChar.ToString());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string WrapWith(string text, string wrapper)
    {
        return wrapper + text + wrapper;
    }

    internal static List<int> IndexesOfChars(string text, char character)
    {
        return IndexesOfCharsList(text, new List<char>(character));
    }

    internal static List<int> IndexesOfCharsList(string text, List<char> characters)
    {
        var indexes = new List<int>();
        foreach (var character in characters)
            indexes.AddRange(ReturnOccurencesOfString(text, character.ToString()));
        indexes.Sort();
        return indexes;
    }

    internal static List<int> ReturnOccurencesOfString(string text, string searchFor)
    {
        var results = new List<int>();
        for (var index = 0; index < text.Length - searchFor.Length + 1; index++)
        {
            var substring = text.Substring(index, searchFor.Length);
            if (string.Equals(substring, searchFor, StringComparison.Ordinal))
                results.Add(index);
        }

        return results;
    }
}
