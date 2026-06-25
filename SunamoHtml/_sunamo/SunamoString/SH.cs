namespace SunamoHtml._sunamo.SunamoString;

internal partial class SH
{
    internal static string JoinNL(List<string> list)
    {
        StringBuilder stringBuilder = new();
        foreach (var item in list)
            stringBuilder.AppendLine(item);
        return stringBuilder.ToString();
    }

    internal static bool IsInLastXCharsTheseLetters(string text, int lastXChars, params char[] letters)
    {
        for (var i = text.Length - 1; i >= lastXChars; i--)
            foreach (var letter in letters)
                if (text[i] == letter)
                    return true;
        return false;
    }

    internal static List<FromToWord> ReturnOccurencesOfStringFromToWord(string entireContent, params string[] searchWords)
    {
        if (searchWords == null || searchWords.Length == 0)
            return new List<FromToWord>();
        entireContent = entireContent.ToLowerInvariant();
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

    internal static string GetFirstPartByLocation(string text, char delimiter)
    {
        var delimiterIndex = text.IndexOf(delimiter);
        return GetFirstPartByLocation(text, delimiterIndex);
    }

    internal static void GetPartsByLocation(out string before, out string after, string text, int position)
    {
        if (position == -1)
        {
            before = text;
            after = string.Empty;
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

    internal static string GetFirstPartByLocation(string text, int position)
    {
        string before = text;
        if (position < text.Length)
            GetPartsByLocation(out before, out _, text, position);
        return before;
    }

    private static bool IsInFirstXCharsTheseLetters(string text, int firstXChars, params char[] letters)
    {
        for (var i = 0; i < firstXChars; i++)
            foreach (var letter in letters)
                if (text[i] == letter)
                    return true;
        return false;
    }

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
                if (!string.IsNullOrEmpty(data.Trim()))
                    isAddThreeDots = true;
                return data;
            }

            isAddThreeDots = true;
            return text.Substring(0, maxLength);
        }

        return text;
    }

    internal static string ShortForLettersCount(string text, int maxLength)
    {
        return ShortForLettersCount(text, maxLength, out _);
    }

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
                if (!string.IsNullOrEmpty(data.Trim()))
                    data = " ... " + data;
                return data;
            }

            return " ... " + text.Substring(text.Length - maxLength);
        }

        return text;
    }

    internal static string GetToFirst(string text, string searchFor)
    {
        var indexOfChar = text.IndexOf(searchFor, StringComparison.Ordinal);
        if (indexOfChar != -1)
            return text.Substring(0, indexOfChar + 1);
        return text;
    }

    internal static string GetTextBetweenSimple(string text, string after, string before, bool isThrowExceptionIfNotContains = true)
    {
        var foundIndex = int.MinValue;
        var result = GetTextBetween(text, after, before, out foundIndex, 0, isThrowExceptionIfNotContains);
        return result;
    }

    internal static string? GetTextBetween(string text, string after, string before, out int foundIndex, int startSearchingAt, bool isThrowExceptionIfNotContains = true)
    {
        string? result = null;
        foundIndex = text.IndexOf(after, startSearchingAt, StringComparison.Ordinal);
        var beforeIndex = text.IndexOf(before, foundIndex + after.Length, StringComparison.Ordinal);
        var afterFound = foundIndex != -1;
        var beforeFound = beforeIndex != -1;
        if (afterFound && beforeFound)
        {
            foundIndex += after.Length;
            beforeIndex -= 1;
            var length = beforeIndex - foundIndex + 1;
            if (length < 1)
            {
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

        return result?.Trim();
    }
}
