namespace SunamoHtml._sunamo.SunamoExceptions;

// © www.sunamo.cz. All Rights Reserved.
internal sealed partial class Exceptions
{
    #region Other
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    internal static Tuple<string, string, string> PlaceOfException(
bool fillAlsoFirstTwo = true)
    {
        StackTrace st = new();
        var value = st.ToString();
        var lines = value.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var i = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; i < lines.Count; i++)
        {
            var item = lines[i];
            if (fillAlsoFirstTwo)
                if (!item.StartsWith("   at ThrowEx", StringComparison.Ordinal))
                {
                    TypeAndMethodName(item, out type, out methodName);
                    fillAlsoFirstTwo = false;
                }
            if (item.StartsWith("at System.", StringComparison.Ordinal))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }
    internal static void TypeAndMethodName(string lines, out string type, out string methodName)
    {
        var s2 = lines.Split("at ")[1].Trim();
        var text = s2.Split('(')[0];
        var parameter = text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parameter[^1];
        parameter.RemoveAt(parameter.Count - 1);
        type = string.Join(".", parameter);
    }
    internal static string CallingMethod(int value = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(value)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region IsNullOrWhitespace
    readonly static StringBuilder sbAdditionalInfoInner = new();
    readonly static StringBuilder sbAdditionalInfo = new();
    #endregion

    #region OnlyReturnString 
    internal static string? NotImplementedMethod(string before)
    {
        return CheckBefore(before) + "Not implemented method.";
    }
    #endregion

    internal static string? HasOddNumberOfElements(string before, string listName, ICollection list)
    {
        return list.Count % 2 == 1 ? CheckBefore(before) + listName + " has odd number of elements " + list.Count : null;
    }

    internal static string? NotInt(string before, string what, int? value)
    {
        return !value.HasValue ? CheckBefore(before) + what + " is not with value " + value + " valid integer number" : null;
    }
    internal static string? InvalidParameter(string before, string valueVar, string nameVar)
    {
        return valueVar != WebUtility.UrlDecode(valueVar)
        ? CheckBefore(before) + valueVar + " is url encoded " + nameVar
        : null;
    }
    internal static string? NotContains(string before, string originalText, params string[] shouldContains)
    {
        List<string> notContained = [];
        foreach (var item in shouldContains)
            if (!originalText.Contains(item, StringComparison.Ordinal))
                notContained.Add(item);
        return notContained.Count == 0
        ? null
        : CheckBefore(before) + "Original text dont contains: " + string.Join(",", notContained) + ". Original text: " + originalText;
    }
    internal static string? IsEmpty(string before, IEnumerable folders, string colName,
    string additionalMessage = "")
    {
        return !folders.OfType<object>().Any()
        ? CheckBefore(before) + colName + " has no elements. " + additionalMessage
        : null;
    }

    internal static string? DifferentCountInLists(string before, string namefc, int countfc, string namesc, int countsc)
    {
        if (countfc != countsc)
            return CheckBefore(before) + " different count elements in collection" + " " +
            string.Concat(namefc + "-" + countfc) + " vs. " +
            string.Concat(namesc + "-" + countsc);
        return null;
    }
}