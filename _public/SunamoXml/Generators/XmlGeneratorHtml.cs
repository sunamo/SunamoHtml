namespace SunamoHtml._public.SunamoXml.Generators;

public class XmlGeneratorHtml
{
    private static Type type = typeof(XmlGeneratorHtml);
    private readonly Stack<string> _stack;
    private readonly bool _useStack;
    public StringBuilder sb = new();

    public XmlGeneratorHtml() : this(false)
    {
    }

    public XmlGeneratorHtml(bool useStack)
    {
        _useStack = useStack;
        if (useStack) _stack = new Stack<string>();
    }

    public int Length()
    {
        return sb.Length;
    }

    public void WriteNonPairTagWith2Attrs(string p1, string p2, string p3, string p4, string p5)
    {
        sb.AppendFormat("<{0} {1}=\"{2}\" {3}=\"{4}\" />", p1, p2, p3, p4, p5);
    }

    public void WriteNonPairTagWithAttr(string p1, string p2, string p3)
    {
        sb.AppendFormat("<{0} {1}=\"{2}\" />", p1, p2, p3);
    }

    public void Insert(int index, string text)
    {
        sb.Insert(index, text);
    }

    public void AppendLine()
    {
        sb.AppendLine();
    }

    public void StartComment()
    {
        sb.Append("<!--");
    }

    public void EndComment()
    {
        sb.Append("-->");
    }

    public void WriteNonPairTagWithAttrs(string tag, List<string> args)
    {
        WriteNonPairTagWithAttrs(tag, args.ToArray());
    }

    public void WriteNonPairTagWithAttrs(string tag, params string[] args)
    {
        sb.AppendFormat("<{0} ", tag);
        for (var i = 0; i < args.Length; i++)
        {
            var text = args[i];
            object hodnota = args[++i];
            sb.AppendFormat("{0}=\"{1}\" ", text, hodnota);
        }

        sb.Append(" />");
    }

    public void WriteCData(string innerCData)
    {
        WriteRaw(string.Format("<![CDATA[{0}]]>", innerCData));
    }

    public void WriteLink(string link, string innerText)
    {
        WriteTagWithAttrs("a", "href", link);
        WriteRaw(innerText);
        TerminateTag("a");
    }

    [Obsolete("only WriteTagWithAttrs should be used anymore")]
    public void WriteTagWithAttr(string tag, string atribut, string hodnota, bool skipEmptyOrNull = false)
    {
        if (skipEmptyOrNull)
            if (string.IsNullOrWhiteSpace(hodnota))
                return;
        var r = string.Format("<{0} {1}=\"{2}\">", tag, atribut, hodnota);
        if (_useStack) _stack.Push(r);
        sb.Append(r);
    }

    public void WriteRaw(string p)
    {
        sb.Append(p);
    }

    public void TerminateTag(string p)
    {
        sb.AppendFormat("</{0}>", p);
    }

    public void WriteTag(string p)
    {
        var r = $"<{p}>";
        if (_useStack) _stack.Push(r);
        sb.Append(r);
    }

    public override string ToString()
    {
        return sb.ToString();
    }

    public void WriteTagWithAttrs(string p, List<string> p_2)
    {
        WriteTagWithAttrs(p, p_2.ToArray());
    }


    public void WriteTagWithAttrs(string p, params string[] p_2)
    {
        WriteTagWithAttrs(true, p, p_2);
    }


    private void WriteTagWithAttrs(string nameTag, Dictionary<string, string> p)
    {
        WriteTagWithAttrs(true, nameTag, DictionaryHelper.GetListStringFromDictionary(p).ToArray());
    }


    public void WriteTagWithAttrsCheckNull(string p, params string[] p_2)
    {
        WriteTagWithAttrs(false, p, p_2);
    }

    public void WriteTagNamespaceManager(object rss, XmlNamespaceManager nsmgr, string v1, string v2)
    {
        ThrowEx.NotImplementedMethod();
    }

    private bool IsNulledOrEmpty(string s)
    {
        if (string.IsNullOrEmpty(s) || s == "(null)") return true;
        return false;
    }

    public void WriteTagNamespaceManager(string nameTag, XmlNamespaceManager nsmgr, params string[] args)
    {
        var p = XHelper.XmlNamespaces(nsmgr, true);
        for (var i = 0; i < args.Count(); i++) p.Add(args[i], args[++i]);
        WriteTagWithAttrs(nameTag, p);
    }

    public void WriteNonPairTagWithAttrs(bool appendNull, string p, params string[] p_2)
    {
        var sb = new StringBuilder();
        sb.AppendFormat("<{0} ", p);
        for (var i = 0; i < p_2.Length; i++)
        {
            var attr = p_2[i];
            var val = p_2[++i];
            if ((string.IsNullOrEmpty(val) && appendNull) || !string.IsNullOrEmpty(val))
                if ((!IsNulledOrEmpty(attr) && appendNull) || !IsNulledOrEmpty(val))
                    sb.AppendFormat("{0}=\"{1}\" ", attr, val);
        }

        sb.Append(" /");
        sb.Append(">");
        var r = sb.ToString();
        if (_useStack) _stack.Push(r);
        this.sb.Append(r);
    }


    private void WriteTagWithAttrs(bool appendNull, string p, params string[] p_2)
    {
        var sb = new StringBuilder();
        sb.AppendFormat("<{0} ", p);
        for (var i = 0; i < p_2.Length; i++)
        {
            var attr = p_2[i];
            var val = p_2[++i];
            if ((string.IsNullOrEmpty(val) && appendNull) || !string.IsNullOrEmpty(val))
                if ((!IsNulledOrEmpty(attr) && appendNull) || !IsNulledOrEmpty(val))
                    sb.AppendFormat("{0}=\"{1}\" ", attr, val);
        }

        sb.Append(">");
        var r = sb.ToString();
        if (_useStack) _stack.Push(r);
        this.sb.Append(r);
    }

    public void WriteElement(string nazev, string inner)
    {
        sb.AppendFormat("<{0}>{1}</{0}>", nazev, inner);
    }

    public void WriteXmlDeclaration()
    {
        sb.Append(XmlTemplates.xml);
    }

    [Obsolete("only WriteTagWithAttrs should be used anymore")]
    public void WriteTagWith2Attrs(string p, string p_2, string p_3, string p_4, string p_5)
    {
        var r = string.Format("<{0} {1}=\"{2}\" {3}=\"{4}\">", p, p_2, p_3, p_4, p_5);
        if (_useStack) _stack.Push(r);
        sb.Append(r);
    }

    public void WriteNonPairTag(string p)
    {
        sb.AppendFormat("<{0} />", p);
    }
}