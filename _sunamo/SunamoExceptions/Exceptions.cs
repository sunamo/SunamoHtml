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
        var v = st.ToString();
        var l = v.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        l.RemoveAt(0);
        var i = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; i < l.Count; i++)
        {
            var item = l[i];
            if (fillAlsoFirstTwo)
                if (!item.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(item, out type, out methodName);
                    fillAlsoFirstTwo = false;
                }
            if (item.StartsWith("at System."))
            {
                l.Add(string.Empty);
                l.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, l));
    }
    internal static void TypeAndMethodName(string l, out string type, out string methodName)
    {
        var s2 = l.Split("at ")[1].Trim();
        var s = s2.Split("(")[0];
        var p = s.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = p[^1];
        p.RemoveAt(p.Count - 1);
        type = string.Join(".", p);
    }
    internal static string CallingMethod(int v = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(v)?.GetMethod();
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
            if (!originalText.Contains(item))
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