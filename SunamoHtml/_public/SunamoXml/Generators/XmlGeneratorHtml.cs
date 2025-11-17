// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoHtml._public.SunamoXml.Generators;

public class XmlGeneratorHtml
{
    private static Type type = typeof(XmlGeneratorHtml);
    private readonly Stack<string> _stack;
    private readonly bool _useStack;
    public StringBuilder stringBuilder = new();

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
        return stringBuilder.Length;
    }

    public void WriteNonPairTagWith2Attrs(string p1, string p2, string p3, string p4, string p5)
    {
        stringBuilder.AppendFormat("<{0} {1}=\"{2}\" {3}=\"{4}\" />", p1, p2, p3, p4, p5);
    }

    public void WriteNonPairTagWithAttr(string p1, string p2, string p3)
    {
        stringBuilder.AppendFormat("<{0} {1}=\"{2}\" />", p1, p2, p3);
    }

    public void Insert(int index, string text)
    {
        stringBuilder.Insert(index, text);
    }

    public void AppendLine()
    {
        stringBuilder.AppendLine();
    }

    public void StartComment()
    {
        stringBuilder.Append("<!--");
    }

    public void EndComment()
    {
        stringBuilder.Append("-->");
    }

    public void WriteNonPairTagWithAttrs(string tag, List<string> args)
    {
        WriteNonPairTagWithAttrs(tag, args.ToArray());
    }

    public void WriteNonPairTagWithAttrs(string tag, params string[] args)
    {
        stringBuilder.AppendFormat("<{0} ", tag);
        for (var i = 0; i < args.Length; i++)
        {
            var text = args[i];
            object hodnota = args[++i];
            stringBuilder.AppendFormat("{0}=\"{1}\" ", text, hodnota);
        }

        stringBuilder.Append(" />");
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
        var result = string.Format("<{0} {1}=\"{2}\">", tag, atribut, hodnota);
        if (_useStack) _stack.Push(result);
        stringBuilder.Append(result);
    }

    public void WriteRaw(string parameter)
    {
        stringBuilder.Append(parameter);
    }

    public void TerminateTag(string parameter)
    {
        stringBuilder.AppendFormat("</{0}>", parameter);
    }

    public void WriteTag(string parameter)
    {
        var result = $"<{parameter}>";
        if (_useStack) _stack.Push(result);
        stringBuilder.Append(result);
    }

    public override string ToString()
    {
        return stringBuilder.ToString();
    }

    public void WriteTagWithAttrs(string parameter, List<string> p_2)
    {
        WriteTagWithAttrs(parameter, p_2.ToArray());
    }


    public void WriteTagWithAttrs(string parameter, params string[] p_2)
    {
        WriteTagWithAttrs(true, parameter, p_2);
    }


    private void WriteTagWithAttrs(string nameTag, Dictionary<string, string> parameter)
    {
        WriteTagWithAttrs(true, nameTag, DictionaryHelper.GetListStringFromDictionary(parameter).ToArray());
    }


    public void WriteTagWithAttrsCheckNull(string parameter, params string[] p_2)
    {
        WriteTagWithAttrs(false, parameter, p_2);
    }


    private bool IsNulledOrEmpty(string text)
    {
        if (string.IsNullOrEmpty(text) || text == "(null)") return true;
        return false;
    }

    public void WriteTagNamespaceManager(string nameTag, XmlNamespaceManager nsmgr, params string[] args)
    {
        var parameter = XHelper.XmlNamespaces(nsmgr, true);
        for (var i = 0; i < args.Count(); i++) parameter.Add(args[i], args[++i]);
        WriteTagWithAttrs(nameTag, parameter);
    }

    public void WriteNonPairTagWithAttrs(bool appendNull, string parameter, params string[] p_2)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("<{0} ", parameter);
        for (var i = 0; i < p_2.Length; i++)
        {
            var attr = p_2[i];
            var val = p_2[++i];
            if ((string.IsNullOrEmpty(val) && appendNull) || !string.IsNullOrEmpty(val))
                if ((!IsNulledOrEmpty(attr) && appendNull) || !IsNulledOrEmpty(val))
                    stringBuilder.AppendFormat("{0}=\"{1}\" ", attr, val);
        }

        stringBuilder.Append(" /");
        stringBuilder.Append(">");
        var result = stringBuilder.ToString();
        if (_useStack) _stack.Push(result);
        this.stringBuilder.Append(result);
    }


    private void WriteTagWithAttrs(bool appendNull, string parameter, params string[] p_2)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("<{0} ", parameter);
        for (var i = 0; i < p_2.Length; i++)
        {
            var attr = p_2[i];
            var val = p_2[++i];
            if ((string.IsNullOrEmpty(val) && appendNull) || !string.IsNullOrEmpty(val))
                if ((!IsNulledOrEmpty(attr) && appendNull) || !IsNulledOrEmpty(val))
                    stringBuilder.AppendFormat("{0}=\"{1}\" ", attr, val);
        }

        stringBuilder.Append(">");
        var result = stringBuilder.ToString();
        if (_useStack) _stack.Push(result);
        this.stringBuilder.Append(result);
    }

    public void WriteElement(string nazev, string inner)
    {
        stringBuilder.AppendFormat("<{0}>{1}</{0}>", nazev, inner);
    }

    public void WriteXmlDeclaration()
    {
        stringBuilder.Append(XmlTemplates.xml);
    }

    [Obsolete("only WriteTagWithAttrs should be used anymore")]
    public void WriteTagWith2Attrs(string parameter, string p_2, string p_3, string p_4, string p_5)
    {
        var result = string.Format("<{0} {1}=\"{2}\" {3}=\"{4}\">", parameter, p_2, p_3, p_4, p_5);
        if (_useStack) _stack.Push(result);
        stringBuilder.Append(result);
    }

    public void WriteNonPairTag(string parameter)
    {
        stringBuilder.AppendFormat("<{0} />", parameter);
    }
}