namespace SunamoHtml._public.SunamoXml.Generators;

public class XmlGeneratorHtml
{
    private readonly Stack<string>? _stack;
    private readonly bool _isUseStack;

    public StringBuilder StringBuilder { get; set; } = new();

    public XmlGeneratorHtml() : this(false)
    {
    }

    public XmlGeneratorHtml(bool isUseStack)
    {
        _isUseStack = isUseStack;
        if (isUseStack) _stack = new Stack<string>();
    }

    public int Length()
    {
        return StringBuilder.Length;
    }

    public void WriteNonPairTagWith2Attrs(string tagName, string attribute1Name, string attribute1Value, string attribute2Name, string attribute2Value)
    {
        StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} {1}=\"{2}\" {3}=\"{4}\" />", tagName, attribute1Name, attribute1Value, attribute2Name, attribute2Value);
    }

    public void WriteNonPairTagWithAttr(string tagName, string attributeName, string attributeValue)
    {
        StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} {1}=\"{2}\" />", tagName, attributeName, attributeValue);
    }

    public void Insert(int index, string text)
    {
        StringBuilder.Insert(index, text);
    }

    public void AppendLine()
    {
        StringBuilder.AppendLine();
    }

    public void StartComment()
    {
        StringBuilder.Append("<!--");
    }

    public void EndComment()
    {
        StringBuilder.Append("-->");
    }

    public void WriteNonPairTagWithAttrs(string tagName, IList<string> attributes)
    {
        if (attributes == null) throw new ArgumentNullException(nameof(attributes));
        WriteNonPairTagWithAttrs(tagName, attributes.ToArray());
    }

    public void WriteNonPairTagWithAttrs(string tagName, params string[] attributes)
    {
        if (attributes == null) throw new ArgumentNullException(nameof(attributes));
        StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} ", tagName);
        for (var i = 0; i < attributes.Length; i++)
        {
            var attributeName = attributes[i];
            object attributeValue = attributes[++i];
            StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}=\"{1}\" ", attributeName, attributeValue);
        }

        StringBuilder.Append(" />");
    }

    public void WriteCData(string innerCData)
    {
        WriteRaw(string.Format(CultureInfo.InvariantCulture, "<![CDATA[{0}]]>", innerCData));
    }

    [SuppressMessage("Design", "CA1054:UriParametersShouldNotBeStrings")]
    public void WriteLink(string link, string innerText)
    {
        if (link == null) throw new ArgumentNullException(nameof(link));
        if (innerText == null) throw new ArgumentNullException(nameof(innerText));
        WriteTagWithAttrs("a", "href", link);
        WriteRaw(innerText);
        TerminateTag("a");
    }

    [Obsolete("only WriteTagWithAttrs should be used anymore")]
    public void WriteTagWithAttr(string tagName, string attributeName, string attributeValue, bool isSkipEmptyOrNull = false)
    {
        if (isSkipEmptyOrNull)
            if (string.IsNullOrWhiteSpace(attributeValue))
                return;
        var result = string.Format(CultureInfo.InvariantCulture, "<{0} {1}=\"{2}\">", tagName, attributeName, attributeValue);
        if (_isUseStack) _stack!.Push(result);
        StringBuilder.Append(result);
    }

    public void WriteRaw(string text)
    {
        StringBuilder.Append(text);
    }

    public void TerminateTag(string tagName)
    {
        StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "</{0}>", tagName);
    }

    public void WriteTag(string tagName)
    {
        var result = $"<{tagName}>";
        if (_isUseStack) _stack!.Push(result);
        StringBuilder.Append(result);
    }

    public override string ToString()
    {
        return StringBuilder.ToString();
    }

    public void WriteTagWithAttrs(string tagName, IList<string> attributes)
    {
        if (attributes == null) throw new ArgumentNullException(nameof(attributes));
        WriteTagWithAttrs(tagName, attributes.ToArray());
    }

    public void WriteTagWithAttrs(string tagName, params string[] attributes)
    {
        if (attributes == null) throw new ArgumentNullException(nameof(attributes));
        WriteTagWithAttrs(true, tagName, attributes);
    }

    private void WriteTagWithAttrs(string tagName, Dictionary<string, string> attributes)
    {
        WriteTagWithAttrs(true, tagName, DictionaryHelper.GetListStringFromDictionary(attributes).ToArray());
    }

    public void WriteTagWithAttrsCheckNull(string tagName, params string[] attributes)
    {
        if (attributes == null) throw new ArgumentNullException(nameof(attributes));
        WriteTagWithAttrs(false, tagName, attributes);
    }

    private static bool IsNulledOrEmpty(string text)
    {
        if (string.IsNullOrEmpty(text) || text == "(null)") return true;
        return false;
    }

    public void WriteTagNamespaceManager(string tagName, XmlNamespaceManager namespaceManager, params string[] attributes)
    {
        if (namespaceManager == null) throw new ArgumentNullException(nameof(namespaceManager));
        if (attributes == null) throw new ArgumentNullException(nameof(attributes));
        var allAttributes = XHelper.XmlNamespaces(namespaceManager, true);
        for (var i = 0; i < attributes.Length; i++) allAttributes.Add(attributes[i], attributes[++i]);
        WriteTagWithAttrs(tagName, allAttributes);
    }

    public void WriteNonPairTagWithAttrs(bool isAppendNull, string tagName, params string[] attributes)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} ", tagName);
        for (var i = 0; i < attributes.Length; i++)
        {
            var attributeName = attributes[i];
            var attributeValue = attributes[++i];
            if ((string.IsNullOrEmpty(attributeValue) && isAppendNull) || !string.IsNullOrEmpty(attributeValue))
                if ((!IsNulledOrEmpty(attributeName) && isAppendNull) || !IsNulledOrEmpty(attributeValue))
                    stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}=\"{1}\" ", attributeName, attributeValue);
        }

        stringBuilder.Append(" /");
        stringBuilder.Append(">");
        var result = stringBuilder.ToString();
        if (_isUseStack) _stack!.Push(result);
        StringBuilder.Append(result);
    }

    private void WriteTagWithAttrs(bool isAppendNull, string tagName, params string[] attributes)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} ", tagName);
        for (var i = 0; i < attributes.Length; i++)
        {
            var attributeName = attributes[i];
            var attributeValue = attributes[++i];
            if ((string.IsNullOrEmpty(attributeValue) && isAppendNull) || !string.IsNullOrEmpty(attributeValue))
                if ((!IsNulledOrEmpty(attributeName) && isAppendNull) || !IsNulledOrEmpty(attributeValue))
                    stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}=\"{1}\" ", attributeName, attributeValue);
        }

        stringBuilder.Append(">");
        var result = stringBuilder.ToString();
        if (_isUseStack) _stack!.Push(result);
        StringBuilder.Append(result);
    }

    public void WriteElement(string tagName, string innerContent)
    {
        StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0}>{1}</{0}>", tagName, innerContent);
    }

    public void WriteXmlDeclaration()
    {
        StringBuilder.Append(XmlTemplates.Xml);
    }

    [Obsolete("only WriteTagWithAttrs should be used anymore")]
    public void WriteTagWith2Attrs(string tagName, string attribute1Name, string attribute1Value, string attribute2Name, string attribute2Value)
    {
        var result = string.Format(CultureInfo.InvariantCulture, "<{0} {1}=\"{2}\" {3}=\"{4}\">", tagName, attribute1Name, attribute1Value, attribute2Name, attribute2Value);
        if (_isUseStack) _stack!.Push(result);
        StringBuilder.Append(result);
    }

    public void WriteNonPairTag(string tagName)
    {
        StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} />", tagName);
    }
}
