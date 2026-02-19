namespace SunamoHtml._sunamo.SunamoCollectionsChangeContent;

internal class CAChangeContent
{
    internal static List<string> ChangeContent0(dynamic /*ChangeContentArgs*/ a, List<string> filesIn,
        Func<string, string> func)
    {
        for (var i = 0; i < filesIn.Count; i++) filesIn[i] = func.Invoke(filesIn[i]);

        RemoveNullOrEmpty(a, filesIn);

        return filesIn;
    }

    private static void RemoveNullOrEmpty(dynamic /*ChangeContentArgs*/ a, List<string> filesIn)
    {
        if (a != null)
        {
            if (a.RemoveNull) filesIn.Remove(null!);

            if (a.RemoveEmpty)
                for (var i = filesIn.Count - 1; i >= 0; i--)
                    if (string.IsNullOrEmpty(filesIn[i].Trim()))
                        filesIn.RemoveAt(i);
        }
    }

    internal static bool ChangeContentWithCondition(ChangeContentArgsHtml a, List<string> filesIn,
        Predicate<string> predicate, Func<string, string> func)
    {
        var changed = false;
        for (var i = 0; i < filesIn.Count; i++)
            if (predicate.Invoke(filesIn[i]))
            {
                filesIn[i] = func.Invoke(filesIn[i]);
                changed = true;
            }

        RemoveNullOrEmpty(a, filesIn);

        return changed;
    }

    private static void RemoveNullOrEmpty(ChangeContentArgsHtml a, List<string> filesIn)
    {
        if (a != null)
        {
            if (a.RemoveNull) filesIn.Remove(null!);

            if (a.RemoveEmpty)
                for (var i = filesIn.Count - 1; i >= 0; i--)
                    if (string.IsNullOrEmpty(filesIn[i].Trim()))
                        filesIn.RemoveAt(i);
        }
    }
}
