namespace SunamoHtml._public.SunamoXml.Generators;

/// <summary>
/// Represents the xml generator html type.
/// </summary>
public class XmlGeneratorHtml
{
    private readonly Stack<string>? _stack;
    private readonly bool _isUseStack;

    /// <summary>
    /// Gets or sets the string builder.
    /// </summary>
    public StringBuilder StringBuilder { get; set; } = new();

    /// <summary>
    /// Xml generator html.
    /// </summary>
    /// <returns>The xml generator html result.</returns>
    public XmlGeneratorHtml() : this(false)
    {
    }

    /// <summary>
    /// Xml generator html.
    /// </summary>
    /// <param name="isUseStack">The is use stack.</param>
    /// <returns>The xml generator html result.</returns>
    public XmlGeneratorHtml(bool isUseStack)
    {
        _isUseStack = isUseStack;
        if (isUseStack) _stack = new Stack<string>();
    }

    /// <summary>
    /// Initializes a new instance of the length class.
    /// </summary>
    /// <param name="isUseStack">The is use stack.</param>
    public int Length()
    {
        return StringBuilder.Length;
    }

    /// <summary>
    /// Write non pair tag with 2 attrs.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attribute1Name">The attribute 1 name.</param>
    /// <param name="attribute1Value">The attribute 1 value.</param>
    /// <param name="attribute2Name">The attribute 2 name.</param>
    /// <param name="attribute2Value">The attribute 2 value.</param>
    /// <returns>The write non pair tag with 2 attrs result.</returns>
    public void WriteNonPairTagWith2Attrs(string tagName, string attribute1Name, string attribute1Value, string attribute2Name, string attribute2Value)
    {
        StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} {1}=\"{2}\" {3}=\"{4}\" />", tagName, attribute1Name, attribute1Value, attribute2Name, attribute2Value);
    }

    /// <summary>
    /// Initializes a new instance of the write non pair tag with attr class.
    /// </summary>
    /// <param name="CultureInfo.InvariantCulture">The culture info invariant culture.</param>
    /// <param name="{1}">The 1.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attribute1Name">The attribute 1 name.</param>
    /// <param name="attribute1Value">The attribute 1 value.</param>
    /// <param name="attribute2Name">The attribute 2 name.</param>
    /// <param name="attribute2Value">The attribute 2 value.</param>
    public void WriteNonPairTagWithAttr(string tagName, string attributeName, string attributeValue)
    {
        StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} {1}=\"{2}\" />", tagName, attributeName, attributeValue);
    }

    /// <summary>
    /// Initializes a new instance of the insert class.
    /// </summary>
    /// <param name="CultureInfo.InvariantCulture">The culture info invariant culture.</param>
    /// <param name="{1}">The 1.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributeName">The attribute name.</param>
    /// <param name="attributeValue">The attribute value.</param>
    public void Insert(int index, string text)
    {
        StringBuilder.Insert(index, text);
    }

    /// <summary>
    /// Initializes a new instance of the append line class.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="text">The text.</param>
    public void AppendLine()
    {
        StringBuilder.AppendLine();
    }

    /// <summary>
    /// Initializes a new instance of the start comment class.
    /// </summary>
    public void StartComment()
    {
        StringBuilder.Append("<!--");
    }

    /// <summary>
    /// Initializes a new instance of the end comment class.
    /// </summary>
    /// <param name=""<!--"">The "&lt;!--".</param>
    public void EndComment()
    {
        StringBuilder.Append("-->");
    }

    /// <summary>
    /// Initializes a new instance of the write non pair tag with attrs class.
    /// </summary>
    /// <param name=""-->"">The "--&gt;".</param>
    public void WriteNonPairTagWithAttrs(string tagName, IList<string> attributes)
    {
        if (attributes == null) throw new ArgumentNullException(nameof(attributes));
        WriteNonPairTagWithAttrs(tagName, attributes.ToArray());
    }

    /// <summary>
    /// Initializes a new instance of the write non pair tag with attrs class.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes.ToArray()">The attributes to array.</param>
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

    /// <summary>
    /// Initializes a new instance of the write c data class.
    /// </summary>
    /// <param name="/>"">The /&gt;".</param>
    public void WriteCData(string innerCData)
    {
        WriteRaw(string.Format(CultureInfo.InvariantCulture, "<![CDATA[{0}]]>", innerCData));
    }

    /// <summary>
    /// Write link.
    /// </summary>
    /// <param name=""Design"">The design.</param>
    /// <param name=""CA1054:UriParametersShouldNotBeStrings"">The CA 1054 uri parameters should not be strings.</param>
    /// <returns>The write link result.</returns>
    [SuppressMessage("Design", "CA1054:UriParametersShouldNotBeStrings")]
    public void WriteLink(string link, string innerText)
    {
        if (link == null) throw new ArgumentNullException(nameof(link));
        if (innerText == null) throw new ArgumentNullException(nameof(innerText));
        WriteTagWithAttrs("a", "href", link);
        WriteRaw(innerText);
        TerminateTag("a");
    }

    /// <summary>
    /// Write tag with attr.
    /// </summary>
    /// <param name="anymore"">The anymore.</param>
    /// <returns>The write tag with attr result.</returns>
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

    /// <summary>
    /// Initializes a new instance of the write raw class.
    /// </summary>
    /// <param name="result">The result.</param>
    public void WriteRaw(string text)
    {
        StringBuilder.Append(text);
    }

    /// <summary>
    /// Initializes a new instance of the terminate tag class.
    /// </summary>
    /// <param name="text">The text.</param>
    public void TerminateTag(string tagName)
    {
        StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "</{0}>", tagName);
    }

    /// <summary>
    /// Initializes a new instance of the write tag class.
    /// </summary>
    /// <param name="CultureInfo.InvariantCulture">The culture info invariant culture.</param>
    /// <param name=""</{0}>"">The 0.</param>
    /// <param name="tagName">The tag name.</param>
    public void WriteTag(string tagName)
    {
        var result = $"<{tagName}>";
        if (_isUseStack) _stack!.Push(result);
        StringBuilder.Append(result);
    }

    /// <summary>
    /// Initializes a new instance of the to string class.
    /// </summary>
    /// <param name="result">The result.</param>
    public override string ToString()
    {
        return StringBuilder.ToString();
    }

    /// <summary>
    /// Write tag with attrs.
    /// </summary>
    /// <returns>The write tag with attrs result.</returns>
    public void WriteTagWithAttrs(string tagName, IList<string> attributes)
    {
        if (attributes == null) throw new ArgumentNullException(nameof(attributes));
        WriteTagWithAttrs(tagName, attributes.ToArray());
    }

    /// <summary>
    /// Initializes a new instance of the write tag with attrs class.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="attributes.ToArray()">The attributes to array.</param>
    public void WriteTagWithAttrs(string tagName, params string[] attributes)
    {
        if (attributes == null) throw new ArgumentNullException(nameof(attributes));
        WriteTagWithAttrs(true, tagName, attributes);
    }

    private void WriteTagWithAttrs(string tagName, Dictionary<string, string> attributes)
    {
        WriteTagWithAttrs(true, tagName, DictionaryHelper.GetListStringFromDictionary(attributes).ToArray());
    }

    /// <summary>
    /// Initializes a new instance of the write tag with attrs check null class.
    /// </summary>
    /// <param name="true">The true.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="DictionaryHelper.GetListStringFromDictionary(attributes).ToArray()">The dictionary helper get list string from dictionary attributes to array.</param>
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

    /// <summary>
    /// Write tag namespace manager.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="namespaceManager">The namespace manager.</param>
    /// <param name="attributes">The attributes.</param>
    /// <returns>The write tag namespace manager result.</returns>
    public void WriteTagNamespaceManager(string tagName, XmlNamespaceManager namespaceManager, params string[] attributes)
    {
        if (namespaceManager == null) throw new ArgumentNullException(nameof(namespaceManager));
        if (attributes == null) throw new ArgumentNullException(nameof(attributes));
        var allAttributes = XHelper.XmlNamespaces(namespaceManager, true);
        for (var i = 0; i < attributes.Length; i++) allAttributes.Add(attributes[i], attributes[++i]);
        WriteTagWithAttrs(tagName, allAttributes);
    }

    /// <summary>
    /// Initializes a new instance of the write non pair tag with attrs class.
    /// </summary>
    /// <param name="tagName">The tag name.</param>
    /// <param name="allAttributes">The all attributes.</param>
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

    /// <summary>
    /// Initializes a new instance of the write element class.
    /// </summary>
    /// <param name="result">The result.</param>
    public void WriteElement(string tagName, string innerContent)
    {
        StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0}>{1}</{0}>", tagName, innerContent);
    }

    /// <summary>
    /// Initializes a new instance of the write xml declaration class.
    /// </summary>
    /// <param name="CultureInfo.InvariantCulture">The culture info invariant culture.</param>
    /// <param name=""<{0}>{1}</{0}>"">The 0 1 0.</param>
    /// <param name="tagName">The tag name.</param>
    /// <param name="innerContent">The inner content.</param>
    public void WriteXmlDeclaration()
    {
        StringBuilder.Append(XmlTemplates.Xml);
    }

    /// <summary>
    /// Write tag with 2 attrs.
    /// </summary>
    /// <param name="anymore"">The anymore.</param>
    /// <returns>The write tag with 2 attrs result.</returns>
    [Obsolete("only WriteTagWithAttrs should be used anymore")]
    public void WriteTagWith2Attrs(string tagName, string attribute1Name, string attribute1Value, string attribute2Name, string attribute2Value)
    {
        var result = string.Format(CultureInfo.InvariantCulture, "<{0} {1}=\"{2}\" {3}=\"{4}\">", tagName, attribute1Name, attribute1Value, attribute2Name, attribute2Value);
        if (_isUseStack) _stack!.Push(result);
        StringBuilder.Append(result);
    }

    /// <summary>
    /// Initializes a new instance of the write non pair tag class.
    /// </summary>
    /// <param name="result">The result.</param>
    public void WriteNonPairTag(string tagName)
    {
        StringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} />", tagName);
    }
}
