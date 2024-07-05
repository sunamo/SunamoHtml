namespace SunamoHtml._sunamo.SunamoCollectionsChangeContent;
internal class CAChangeContent
{
    internal static List<string> ChangeContent0(dynamic /*ChangeContentArgs*/ a, List<string> files_in, Func<string, string> func)
    {
        for (int i = 0; i < files_in.Count; i++)
        {
            files_in[i] = func.Invoke(files_in[i]);
        }

        RemoveNullOrEmpty(a, files_in);

        return files_in;
    }

    private static void RemoveNullOrEmpty(dynamic /*ChangeContentArgs*/ a, List<string> files_in)
    {
        if (a != null)
        {
            if (a.removeNull)
            {
                files_in.Remove(null);
            }

            if (a.removeEmpty)
            {
                for (int i = files_in.Count - 1; i >= 0; i--)
                {
                    if (files_in[i].Trim() == string.Empty)
                    {
                        files_in.RemoveAt(i);
                    }
                }
            }

        }
    }
    internal static bool ChangeContentWithCondition(ChangeContentArgsHtml a, List<string> files_in, Predicate<string> predicate, Func<string, string> func)
    {
        bool changed = false;
        for (int i = 0; i < files_in.Count; i++)
        {
            if (predicate.Invoke(files_in[i]))
            {
                files_in[i] = func.Invoke(files_in[i]);
                changed = true;
            }
        }



        RemoveNullOrEmpty(a, files_in);

        return changed;
    }

    private static void RemoveNullOrEmpty(ChangeContentArgsHtml a, List<string> files_in)
    {
        if (a != null)
        {
            if (a.removeNull)
            {
                files_in.Remove(null);
            }

            if (a.removeEmpty)
            {
                for (int i = files_in.Count - 1; i >= 0; i--)
                {
                    if (files_in[i].Trim() == string.Empty)
                    {
                        files_in.RemoveAt(i);
                    }
                }
            }
        }
    }
}
