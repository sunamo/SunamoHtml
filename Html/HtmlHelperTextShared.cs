


namespace SunamoHtml;
public partial class HtmlHelperText
{
    public static string ReplacePairTag(string input, string tag, string forWhat)
    {
        input = input.Replace("<" + tag + ">", "<" + forWhat + ">");
        input = input.Replace("<" + tag + " ", "<" + forWhat + " ");
        input = input.Replace("</" + tag + ">", "</" + forWhat + ">");
        return input;
    }

    public static string InsertMissingEndingTags(string s, string tag)
    {
        StringBuilder text = new StringBuilder(s);

        var start = SH.ReturnOccurencesOfString(s, "<" + tag);
        var endingTag = "</" + tag + ">";
        var ends = SH.ReturnOccurencesOfString(s, endingTag);

        var startC = start.Count;
        var endsC = ends.Count;

        if (start.Count > ends.Count)
        {
            // In keys are start, in value end. If end isnt, then -1
            Dictionary<int, int> se = new Dictionary<int, int>();

            for (int i = start.Count - 1; i >= 0; i--)
            {
                var startActual = start[i];

                var endDx = -1;
                if (ends.Count != 0)
                {
                    endDx = ends.Count - 1;
                }
                var endActual = -1;
                if (endDx != -1)
                {
                    endActual = ends[endDx];
                }
                if (startActual > endActual)
                {
                    se.Add(startActual, -1);
                }
                else
                {
                    se.Add(startActual, endActual);
                    ends.RemoveAt(endDx);
                }
            }

            foreach (var item in se)
            {
                if (item.Value == -1)
                {
                    var dexEndOfStart = s.IndexOf(AllChars.gt, item.Key);

                    var space = s.IndexOf(AllChars.space, dexEndOfStart);

                    if (space != -1)
                    {

                        text.Insert(space, endingTag);
                    }
                }
            }
        }

        return text.ToString();
    }

    public static List<string> CreateH2FromNumberedList(List<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i] = lines[i].Trim();
        }
        //CA.Trim(lines);

        for (int i = 0; i < lines.Count; i++)
        {
            if (SH.IsNumbered(lines[i]))
            {
                lines[i] = WrapWith(lines[i], "h2");
            }
        }

        return lines;
    }

    public static List<string> GetAllEquvivalentsOfNonPairingTag(string v)
    {
        return new List<string>(["<" + v + ">", "<" + v + " />", "<" + v + "/>"]);
    }

    /// <summary>
    /// A2 only name like p, title etc.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="v"></param>
    private static string WrapWith(string s, string p)
    {
        return AllStrings.lt + p + AllStrings.gt + s + "</" + p + AllStrings.gt;
    }

    public static string RemoveAllNodes(string v)
    {
        var hd = HtmlAgilityHelper.CreateHtmlDocument();
        hd.LoadHtml(v);

        var nodes = hd.DocumentNode.Descendants().ToList();
        for (int i = 0; i < nodes.Count(); i++)
        {


            var node = nodes[i];

            if (node.NodeType != HtmlAgilityPack.HtmlNodeType.Text)
            {
                if (node.ParentNode.NodeType != HtmlAgilityPack.HtmlNodeType.Document)
                {
                    node.ParentNode.Remove();
                }
                else
                {
                    node.Remove();
                }
            }
        }

        return hd.DocumentNode.OuterHtml;
    }

    public static Tuple<string, string> GetTextBetweenTags(string c2, string scriptS, string scriptE, bool throwExceptionIfNotContains = true)
    {
        if (scriptS.EndsWith(">"))
        {
            return new Tuple<string, string>(SH.GetTextBetweenSimple(c2, scriptS, scriptE, throwExceptionIfNotContains), "");
        }
        var sc = c2.IndexOf(scriptS);
        if (sc == -1)
        {
            return new Tuple<string, string>("", "");
        }

        var ending = c2.IndexOf('>', sc);
        var e = c2.IndexOf(scriptE, ending);

        var r = SH.GetTextBetweenTwoCharsInts(c2, ending, e);

        var from = sc + scriptS.Length - 1;
        var to = ending;

        var attrs = string.Empty;

        if (from < to)
        {
            attrs = SH.GetTextBetweenTwoCharsInts(c2, from, to);

            if (attrs == " scop")
            {

            }
        }

        return new Tuple<string, string>(r, attrs);
    }
}
